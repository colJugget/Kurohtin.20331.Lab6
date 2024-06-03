using Kurohtin.Domain.Entities;
using Kurohtin.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kurohtin.UI.Services.CategoryService
{
    public class MemoryCategoryService : ICategoryService
    {
        public Task<ResponseData<List<Category>>> GetCategoryListAsync()
        {
            var categories = new List<Category>
        {
            new Category {Id=1, Name="Болты", NormalizedName="bolty"},
            new Category {Id=2, Name="Шайбы", NormalizedName="shaiby"},
            new Category {Id=3, Name="Гайки", NormalizedName="gaiki"},
            new Category {Id=4, Name="Дюбели", NormalizedName="dubeli"},
            new Category {Id=5, Name="Шплинты", NormalizedName="shplinty"},
            new Category {Id=6, Name="Шурупы", NormalizedName="shurupy"}
        };
            var result = new ResponseData<List<Category>>();
            result.Data = categories;
            return Task.FromResult(result);
        }
    }

}
