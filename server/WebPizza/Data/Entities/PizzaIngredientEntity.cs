using System.ComponentModel.DataAnnotations.Schema;

namespace WebPizza.Data.Entities;

[Table("tbl_pizza_ingredients")]
public class PizzaIngredientEntity
{
    public int PizzaId { get; set; }
    public PizzaEntity Pizza { get; set; } = null!;

    public int IngredientId { get; set; }
    public IngredientEntity Ingredient { get; set; } = null!;
}
