using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPizza.Data.Entities
{
    [Table("tbl_categories")]
    public class CategoryEntity : BaseEntity
    {
        [StringLength(255), Required]
        public string Name { get; set; } = null!;

        [StringLength(255), Required]
        public string Image { get; set; } = null!;

        public ICollection<PizzaEntity> Pizzas { get; set; } = null!;
    }
}
