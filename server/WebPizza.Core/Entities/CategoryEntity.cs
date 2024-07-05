using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebPizza.Core.Entities;

[Table("tbl_categories")]
public class CategoryEntity : BaseEntity
{
    [StringLength(255), Required]
    public string Name { get; set; } = null!;

    [StringLength(255), Required]
    public string Image { get; set; } = null!;

    public ICollection<PizzaEntity> Pizzas { get; set; } = null!;
}
