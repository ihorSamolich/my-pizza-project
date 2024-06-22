using WebPizza.ViewModels.Category;
using WebPizza.ViewModels.Ingredient;
using WebPizza.ViewModels.PizzaSizes;

namespace WebPizza.ViewModels.Pizza
{
    public class PizzaVm
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Rating { get; set; }
        public bool IsAvailable { get; set; }
        public CategoryVm Category { get; set; } = null!;

        public IEnumerable<PizzaPhotoVm> Photos { get; set; } = null!;
        public IEnumerable<IngredientVm> Ingredients { get; set; } = null!;
        public IEnumerable<PizzaSizePriceVm> Sizes { get; set; } = null!;


        public DateTime DateCreated { get; set; }
    }
}
