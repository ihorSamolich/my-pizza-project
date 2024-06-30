using Bogus;
using Bogus.DataSets;
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

                var fakeCategories = configuration
                    .GetSection("DefaultSeedData:PizzaCategories")
                    .Get<string[]>();

                if (fakeCategories is null)
                    throw new Exception("Configuration DefaultSeedData:PizzaCategories is invalid");

                var categoryEntities = fakeCategories.Select(name => new CategoryEntity
                {
                    Name = name,
                    DateCreated = DateTime.UtcNow.AddDays(faker.Random.Int(-10, -1))
                }).ToList();

                foreach (var category in categoryEntities)
                {
                    var imageUrl = faker.Image.LoremFlickrUrl(keywords: "pizza", width: 1000, height: 800);
                    var imageBase64 = await GetImageAsBase64Async(httpClient, imageUrl);

                    category.Image = await imageService.SaveImageAsync(imageBase64);
                }

                context.Categories.AddRange(categoryEntities);
                await context.SaveChangesAsync();
            }

            // Ingredient seed
            if (await context.Ingredients.CountAsync() < 1)
            {
                Faker faker = new Faker();

                var fakeIngredients = configuration
                    .GetSection("DefaultSeedData:PizzaIngredients")
                    .Get<string[]>();

                if (fakeIngredients is null)
                    throw new Exception("Configuration DefaultSeedData:PizzaIngredients is invalid");

                var ingredientEntities = fakeIngredients.Select(name => new IngredientEntity
                {
                    Name = name,
                    DateCreated = DateTime.UtcNow.AddDays(faker.Random.Int(-10, -1))
                }).ToList();

                foreach (var ingredient in ingredientEntities)
                {
                    var imageUrl = faker.Image.LoremFlickrUrl(keywords: "food", width: 1000, height: 800);
                    var imageBase64 = await GetImageAsBase64Async(httpClient, imageUrl);

                    ingredient.Image = await imageService.SaveImageAsync(imageBase64);
                }

                context.Ingredients.AddRange(ingredientEntities);
                await context.SaveChangesAsync();
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

                var pizzaNames = configuration
                    .GetSection("DefaultSeedData:PizzaNames")
                    .Get<string[]>();

                if (pizzaNames is null)
                    throw new Exception("Configuration DefaultSeedData:PizzaNames is invalid");

                var fakePizzas = pizzaNames.Select(name => new PizzaEntity
                {
                    Name = name,
                    Description = faker.Lorem.Sentence(),
                    Rating = faker.Random.Double(0, 5),
                    IsAvailable = faker.Random.Bool(),
                    CategoryId = faker.PickRandom(context.Categories.Select(c => c.Id).ToList())
                }).ToList();

                foreach (var pizza in fakePizzas)
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
                        .Take(faker.Random.Int(1, 5))
                        .Select(i => new PizzaIngredientEntity { IngredientId = i.Id })
                        .ToList();

                    pizza.Sizes = context.Sizes
                        .OrderBy(i => Guid.NewGuid())
                        .Take(faker.Random.Int(1, 5))
                        .Select(s => new PizzaSizePriceEntity
                        {
                            SizeId = s.Id,
                            Price = faker.Random.Decimal(100, 400)
                        })
                        .ToList();
                }

                context.Pizzas.AddRange(fakePizzas);
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
