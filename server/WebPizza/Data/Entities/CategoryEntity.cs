using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPizza.Data.Entities
{
    [Table("tbl_categories")]
    public class CategoryEntity
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255), Required]
        public string Name { get; set; }
        [StringLength(255), Required]
        public string Image { get; set; }
    }
}
