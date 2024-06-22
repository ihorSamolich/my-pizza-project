using Bogus;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
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
            var configuration = scope.ServiceProvider.GetService<IConfiguration>();

            using var httpClient = new HttpClient();

            context.Database.Migrate();



            // Category seed
            if (await context.Categories.CountAsync() < 1)
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

            // Ingredient seed
            if (await context.Ingredients.CountAsync() < 1)
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

            // Sizes seed
            if (await context.Sizes.CountAsync() < 1)
            {
                var pizzaSizes = configuration
                    .GetSection("DefaultSeedData:PizzaSizes")
                    .Get<string[]>();

                if (pizzaSizes is null)
                    throw new Exception("Configuration DefaultSeedData:PizzaSizes is invalid");

                List<PizzaSizeEntity> sizes = new List<PizzaSizeEntity>();

                foreach (var size in pizzaSizes)
                {
                    sizes.Add(new PizzaSizeEntity { Name = size });
                }

                context.Sizes.AddRange(sizes);
                await context.SaveChangesAsync();
            }

            // Pizza seed
            if (await context.Pizzas.CountAsync() < 1)
            {
                var faker = new Faker("en");

                var fakePizzas = new Faker<PizzaEntity>("uk")
                    .RuleFor(o => o.Name, f => f.Commerce.ProductName())
                    .RuleFor(o => o.Description, f => f.Lorem.Sentence())
                    .RuleFor(o => o.Rating, f => f.Random.Double(0, 5))
                    .RuleFor(o => o.IsAvailable, f => f.Random.Bool())
                    .RuleFor(o => o.CategoryId, f => f.PickRandom(context.Categories.Select(c => c.Id).ToList()));

                var pizzas = fakePizzas.Generate(10);

                foreach (var pizza in pizzas)
                {
                    int numberOfPhotos = faker.Random.Int(1, 5);
                    for (int i = 0; i < numberOfPhotos; i++)
                    {
                        var imageUrl = faker.Image.LoremFlickrUrl(keywords: "pizza", width: 1000, height: 800);
                        var imageBase64 = await GetImageAsBase64Async(httpClient, imageUrl);

                        pizza.Photos.Add(new PizzaPhotoEntity
                        {
                            Name = await imageService.SaveImageAsync(imageBase64),
                            Priority = i + 1
                        });
                    }

                    pizza.Ingredients = context.Ingredients
                        .OrderBy(i => Guid.NewGuid())
                        .Take(new Faker().Random.Int(1, 5))
                        .Select(i => new PizzaIngredientEntity { IngredientId = i.Id })
                        .ToList();

                    pizza.Sizes = context.Sizes
                        .Select(s => new PizzaSizePriceEntity
                        {
                            SizeId = s.Id,
                            Price = faker.Random.Decimal(100, 400)
                        })
                        .ToList();
                }

                context.Pizzas.AddRange(pizzas);
                await context.SaveChangesAsync();
            }
        }
    }

    private static async Task<string> GetImageAsBase64Async(HttpClient httpClient, string imageUrl)
    {
        var imageBytes = await httpClient.GetByteArrayAsync(imageUrl);
        return Convert.ToBase64String(imageBytes);
    }
}
