using Microsoft.AspNetCore.Http;
using WebPizza.Core.DTO.PizzaSizes;

namespace WebPizza.Core.DTO.Pizza;

public class PizzaEditVm
{
    public int Id { get; set; }

    public string? Name { get; set; } = null!;

    public string? Description { get; set; } = null!;

    public int? CategoryId { get; set; }

    public IEnumerable<IFormFile>? Photos { get; set; } = null!;

    public IEnumerable<int>? IngredientIds { get; set; } = null!;

    public IEnumerable<PizzaSizePriceCreateVm>? Sizes { get; set; } = null!;
}
