using Microsoft.AspNetCore.Http;

namespace WebPizza.Core.DTO.Ingredient;

public class IngredientCreateVm
{
    public string Name { get; set; } = null!;

    public IFormFile Image { get; set; } = null!;
}
