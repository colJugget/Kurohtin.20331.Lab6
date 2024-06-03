using Kurohtin.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kurohtin.API.Data
{
    public static class DbInitializer
    {
        public static async Task SeedData(WebApplication app)
    {
        // Uri проекта
        var uri = "https://localhost:7002/";
        // Получение контекста БД
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        // Выполнение миграций
        await context.Database.MigrateAsync();
        // Заполнение данными
            if (!context.Categories.Any() && !context.Crepezhy.Any())
        {
            var categories = new Category[]
            {
            new Category { Name="Болты", NormalizedName="bolty"},
            new Category { Name="Гайки", NormalizedName="gaiki"},
            new Category { Name="Шайбы", NormalizedName="shaiby"},
            new Category { Name="Гроверы", NormalizedName="grovery"},
            new Category { Name="Шплинты", NormalizedName="shplinty"},
            new Category { Name="Дюбели", NormalizedName="dubeli"}
            };
            await context.Categories.AddRangeAsync(categories);
            await context.SaveChangesAsync();

            var dishes = new List<Crepezh>
            {
                new Crepezh {Name="Болт",
                    Description="8 мм",
                    Gramms =20,
                    Category=categories.FirstOrDefault(c=>c.NormalizedName.Equals("bolty")),
                    Image=uri+"Images/bolt.jpg" },
                new Crepezh {  Name="Болт",
                    Description="10 мм",
                    Gramms =30,
                    Category=categories.FirstOrDefault(c=>c.NormalizedName.Equals("bolty")),
                    Image=uri+"Images/bolt.jpg" },
                new Crepezh { Name="Гайка",
                    Description="12 мм",
                    Gramms =35,
                    Category=categories.FirstOrDefault(c=>c.NormalizedName.Equals("gaiki")),
                    Image=uri+"Images/gaika.jpg" },
                new Crepezh { Name = "Гайка",
                    Description="10 мм",
                    Gramms =24,
                    Category=categories.FirstOrDefault(c=>c.NormalizedName.Equals("gaiki")),
                    Image=uri + "Images/gaika.jpg" },
                new Crepezh { Name = "Шайба",
                    Description="6 мм",
                    Gramms =10,
                    Category=categories.FirstOrDefault(c=>c.NormalizedName.Equals("shaiby")),
                    Image=uri + "Images/shaiba.jpg" }
                new Crepezh { Name = "Шуруп",
                    Description="6 мм",
                    Gramms =10,
                    Category=categories.FirstOrDefault(c=>c.NormalizedName.Equals("shurupy")),
                    Image=uri + "Images/shurup.jpg" }
            };
            await context.AddRangeAsync(Crepezhy);
            await context.SaveChangesAsync();

        }
    }
    }
}
