using WebPizza.ViewModels.PizzaSizes;

namespace WebPizza.ViewModels.Pizza;

public class PizzaCreateVm
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int CategoryId { get; set; }

    // Список фото
    public IEnumerable<IFormFile>? Photos { get; set; } = null!;

    // Список інгредієнтів
    public IEnumerable<int>? IngredientIds { get; set; } = null!;

    // Розміри та ціни
    public IEnumerable<PizzaSizePriceCreateVm> Sizes { get; set; } = null!;
}
