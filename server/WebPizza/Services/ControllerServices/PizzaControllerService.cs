using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebPizza.Data;
using WebPizza.Data.Entities;
using WebPizza.Services.ControllerServices.Interfaces;
using WebPizza.Services.Interfaces;
using WebPizza.ViewModels.Ingredient;
using WebPizza.ViewModels.Pizza;

namespace WebPizza.Services.ControllerServices
{
    public class PizzaControllerService(
        PizzaDbContext pizzaContext,
        IMapper mapper,
        IImageService imageService
    ) : IPizzaControllerService
    {
        public async Task CreateAsync(PizzaCreateVm vm)
        {
            var pizza = mapper.Map<PizzaEntity>(vm);

            try
            {
                pizza.DateCreated = DateTime.UtcNow;
                pizza.Rating = 0;
                pizza.IsAvailable = true;

                int priorityIndex = 1;

                if (vm.Photos != null && vm.Photos.Any())
                {
                    foreach (var photo in vm.Photos)
                    {
                        pizza.Photos.Add(new PizzaPhotoEntity
                        {
                            Name = await imageService.SaveImageAsync(photo),
                            Priority = priorityIndex
                        });
                        priorityIndex++;
                    }
                }

                if (vm.IngredientIds != null && vm.IngredientIds.Any())
                {
                    foreach (var ingredientId in vm.IngredientIds)
                    {
                        pizza.Ingredients.Add(new PizzaIngredientEntity
                        {
                            IngredientId = ingredientId
                        });
                    }
                }

                if (vm.Sizes != null && vm.Sizes.Any())
                {
                    foreach (var size in vm.Sizes)
                    {
                        pizza.Sizes.Add(new PizzaSizePriceEntity
                        {
                            SizeId = size.SizeId,
                            Price = size.Price
                        });
                    }
                }

                await pizzaContext.Pizzas.AddAsync(pizza);
                await pizzaContext.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Error pizza created");
            }
        }

        public async Task DeleteIfExistsAsync(int id)
        {
            var pizza = await pizzaContext.Pizzas
                .Include(x => x.Photos)
                .FirstOrDefaultAsync(c => c.Id == id);

            try
            {
                if (pizza == null)
                {
                    throw new Exception("Pizza not found");
                }


                if (pizza.Photos != null && pizza.Photos.Any())
                {
                    foreach (var photo in pizza.Photos)
                    {
                        imageService.DeleteImageIfExists(photo.Name);
                    }
                }
                pizzaContext.Pizzas.Remove(pizza);
                await pizzaContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
