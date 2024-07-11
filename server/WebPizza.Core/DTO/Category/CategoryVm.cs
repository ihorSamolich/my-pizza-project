
namespace WebPizza.Core.DTO.Category;
public class CategoryVm
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Image { get; set; } = null!;
    public DateTime DateCreated { get; set; }

    public int NumberOfPizzas { get; set; }


    //public IEnumerable<PizzaVm> Pizzas { get; set; } = null!;

}
