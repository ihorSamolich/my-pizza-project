using Bogus;
using Microsoft.EntityFrameworkCore;
using WebPizza.Data.Entities;
using WebPizza.Services;
using WebPizza.Services.Interfaces;

namespace WebPizza.Data;

public static class SeederDB
{
    public static async void SeedData(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();
            var imageService = scope.ServiceProvider.GetService<IImageService>();

            using var httpClient = new HttpClient();

            context.Database.Migrate();

            if (await context.Categories.CountAsync() < 10)
            {
                Faker faker = new Faker();

                var fakeCategory = new Faker<CategoryEntity>("uk")
                    .RuleFor(o => o.DateCreated, f => DateTime.UtcNow.AddDays(f.Random.Int(-10, -1)))
                    .RuleFor(c => c.Name, f => f.Commerce.Product());

                var fakeCategories = fakeCategory.Generate(10);

                foreach (var category in fakeCategories)
                {
                    var imageUrl = faker.Image.LoremFlickrUrl(keywords: "dish", width: 1000, height: 800);
                    var imageBase64 = await GetImageAsBase64Async(httpClient, imageUrl);

                    category.Image = await imageService.SaveImageAsync(imageBase64);
                }

                context.Categories.AddRange(fakeCategories);
                context.SaveChanges();
            }

            if (await context.Ingredients.CountAsync() < 10)
            {
                Faker faker = new Faker();

                var fakeIngredient = new Faker<IngredientEntity>("uk")
                    .RuleFor(o => o.DateCreated, f => DateTime.UtcNow.AddDays(f.Random.Int(-10, -1)))
                    .RuleFor(c => c.Name, f => f.Commerce.ProductMaterial());

                var fakeIngredients = fakeIngredient.Generate(10);

                foreach (var ingredient in fakeIngredients)
                {
                    var imageUrl = faker.Image.LoremFlickrUrl(keywords: "fruit", width: 1000, height: 800);
                    var imageBase64 = await GetImageAsBase64Async(httpClient, imageUrl);

                    ingredient.Image = await imageService.SaveImageAsync(imageBase64);
                }

                context.Ingredients.AddRange(fakeIngredients);
                context.SaveChanges();
            }
        }
    }

    private static async Task<string> GetImageAsBase64Async(HttpClient httpClient, string imageUrl)
    {
        var imageBytes = await httpClient.GetByteArrayAsync(imageUrl);
        return Convert.ToBase64String(imageBytes);
    }
}
