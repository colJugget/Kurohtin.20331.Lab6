using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using Kurohtin.API.Data;
using Kurohtin.Domain.Entities;
using Kurohtin.Domain.Models;


namespace Kurohtin.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CrepezhController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public CrepezhController(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    // GET: api/Crepezhy
    [HttpGet]
    public async Task<ActionResult<ResponseData<ProductListModel<Crepezh>>>> GetCrepezhy(
                    string? category,
                    int pageNo = 1,
                    int pageSize = 3)
    {

        // Создать объект результата
        var result = new ResponseData<ProductListModel<Crepezh>>();
        // Фильтрация по категории загрузка данных категории
        var data = _context.Crepezhy
            .Include(d => d.Category)
            .Where(d => String.IsNullOrEmpty(category)
                    || d.Category.NormalizedName.Equals(category));

        // Подсчет общего количества страниц
        int totalPages = (int)Math.Ceiling(data.Count() / (double)pageSize);
        if (pageNo > totalPages)
            pageNo = totalPages;

        // Создание объекта ProductListModel с нужной страницей данных
        var listData = new ProductListModel<Crepezh>()
        {
            Items = await data
                            .Skip((pageNo - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync(),
            CurrentPage = pageNo,
            TotalPages = totalPages
        };
        // поместить данные в объект результата
        result.Data = listData;

        // Если список пустой
        if (data.Count() == 0)
        {
            result.Success = false;
            result.ErrorMessage = "Нет объектов в выбраннной категории";
        }

        return result;
    }

    
    // POST: api/Crepezhy
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Consumes("multipart/form-data", "application/json")]
    public async Task<ActionResult<Crepezh>> PostCrepezh(Crepezh crepezh)
    {
        _context.Crepezhy.Add(crepezh);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCrepezh", new { id = crepezh.Id }, crepezh);
    }

   
        // Путь к папке wwwroot/Images
        var imagesPath = Path.Combine(_env.WebRootPath, "Images");
        // получить случайное имя файла
        var randomName = Path.GetRandomFileName();
        // получить расширение в исходном файле
        var extension = Path.GetExtension(image.FileName);
        // задать в новом имени расширение как в исходном файле
        var fileName = Path.ChangeExtension(randomName, extension);
        // полный путь к файлу
        var filePath = Path.Combine(imagesPath, fileName);
        // создать файл и открыть поток для чтения
        using var stream = System.IO.File.OpenWrite(filePath);
        // скопировать файл в поток
        await image.CopyToAsync(stream);
        // получить Url хоста
        var host = "https://" + Request.Host;
        // Url файла изображения
        var url = $"{host}/Images/{fileName}";
        // Сохранить url файла в объекте
        dish.Image = url;
        await _context.SaveChangesAsync();
        return Ok();

    }

    private void DeleteImage(string fileName)
    {

        // Путь к файлу
        var imagesPath = Path.Combine(_env.WebRootPath, "Images", fileName);
        var fInfo = new FileInfo(imagesPath);
        if (fInfo.Exists)
        {
            fInfo.Delete();
        }
    }
}