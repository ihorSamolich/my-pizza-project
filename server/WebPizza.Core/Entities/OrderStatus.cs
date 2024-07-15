using System.ComponentModel.DataAnnotations.Schema;

namespace WebPizza.Core.Entities;

[Table("tbl_order_status")]
public class OrderStatusEntity : BaseEntity
{
    public string Name { get; set; } = null!;
    public ICollection<OrderEntity> Orders { get; set; } = new List<OrderEntity>();
}
