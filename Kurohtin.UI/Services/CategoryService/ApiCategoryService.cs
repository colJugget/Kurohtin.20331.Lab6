using Kurohtin.UI.Services.CategoryService;
using System.Net.Http.Json;
using System.Text.Json;
using Kurohtin.Domain.Entities;
using Kurohtin.Domain.Models;

namespace Kurohtin.UI.Services.CategoryService;

public class ApiCategoryService(HttpClient httpClient) : ICategoryService
{
    public async Task<ResponseData<List<Category>>> GetCategoryListAsync()
    {
        var result = await httpClient.GetAsync(httpClient.BaseAddress);
        if (result.IsSuccessStatusCode)
        {
            return await result.Content.ReadFromJsonAsync<ResponseData<List<Category>>>();
        };

        var response = new ResponseData<List<Category>>
        { Success = false, ErrorMessage = "Ошибка чтения API" };
        return response;
    }
}