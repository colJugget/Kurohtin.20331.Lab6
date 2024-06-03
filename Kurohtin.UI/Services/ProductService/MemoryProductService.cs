using Kurohtin.Domain.Entities;
using Kurohtin.Domain.Models;
using Kurohtin.UI.Services.CategoryService;
using Microsoft.AspNetCore.Mvc;

namespace Kurohtin.UI.Services.ProductService
{
    public class MemoryProductService : IProductService
    {
        private readonly ICategoryService categoryService;
        private readonly IConfiguration _config;
        List<Crepezh> _crepezhy;
        List<Category> _categories;
        public MemoryProductService([FromServices] IConfiguration config, ICategoryService categoryService)
        {
            _categories = categoryService.GetCategoryListAsync().Result.Data;
            _config = config;
            SetupData();
        }

        private void SetupData()
        {
            _crepezhy = new List<Crepezh>
        {
            new Crepezh {Id = 1, Name="<Болт>",
                Description="Диаметр 8",
                Gramms =200, Image="Images/bolt.jpg",
                CategoryId= _categories.Find(c=>c.NormalizedName.Equals("bolty")).Id
                },
            new Crepezh { Id = 2, Name="Болт",
                Description="Диаметр 10",
                Gramms =330, Image="Images/bolt.jpg",
                CategoryId= _categories.Find(c=>c.NormalizedName.Equals("bolty")).Id},
            new Crepezh { Id = 3, Name="Шайба-плоская",
                Description="Диаметр 10",
                Gramms =35, Image="Images/shaiba.jpg",
                CategoryId= _categories.Find(c=>c.NormalizedName.Equals("shaiby")).Id},
            new Crepezh { Id = 4, Name = "Шайба-гровер",
                Description="Диаметр 6",
                Gramms =24, Image="Images/grover.jpg",
                CategoryId= _categories.Find(c=>c.NormalizedName.Equals("shaiby")).Id
                },
            new Crepezh { Id = 5, Name = "Гайка",
                Description="Диаметр 6",
                Gramms =80, Image="Images/gaika.jpg",
                CategoryId= _categories.Find(c=>c.NormalizedName.Equals("gaiki")).Id}
        };
        }

        public Task<ResponseData<ProductListModel<Crepezh>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            // Создать объект результата
            var result = new ResponseData<ProductListModel<Crepezh>>();
            // Id категории для фильрации
            int? categoryId = null;

            // если требуется фильтрация, то найти Id категории
            // с заданным categoryNormalizedName
            if (categoryNormalizedName != null)
                categoryId = _categories
                .Find(c => c.NormalizedName.Equals(categoryNormalizedName))
                 ?.Id;

            // Выбрать объекты, отфильтрованные по Id категории,
            // если этот Id имеется
            var data = _crepezhy
                .Where(d => categoryId == null || d.CategoryId.Equals(categoryId))?
                .ToList();

            // получить размер страницы из конфигурации
            int pageSize = _config.GetSection("ItemsPerPage").Get<int>();
            // получить общее количество страниц
            int totalPages = (int)Math.Ceiling(data.Count / (double)pageSize);


            var listData = new ProductListModel<Crepezh>()
            {
                Items = data.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList(),
                CurrentPage = pageNo,
                TotalPages = totalPages
            };

            // поместить данные в объект результата
            result.Data = listData;

            // Если список пустой
            if (data.Count == 0)
            {
                result.Success = false;
                result.ErrorMessage = "Нет объектов в выбраннной категории";
            }
            // Вернуть результат
            return Task.FromResult(result);
        }

        public Task<ResponseData<Crepezh>> CreateProductAsync(Crepezh product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<Crepezh>> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }



        public Task UpdateProductAsync(int id, Crepezh product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }
    }

}
