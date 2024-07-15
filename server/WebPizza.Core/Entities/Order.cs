using System.ComponentModel.DataAnnotations.Schema;
using WebPizza.Core.Entities.Identity;

namespace WebPizza.Core.Entities;

[Table("tbl_orders")]
public class OrderEntity : BaseEntity
{
    public string DeliveryAddress { get; set; } = null!;
    public bool IsDelivery { get; set; }
    public decimal TotalAmount { get; set; }

    public int UserId { get; set; }
    public int OrderStatusId { get; set; }

    public UserEntity User { get; set; } = null!;
    public OrderStatusEntity OrderStatus { get; set; } = null!;
    public ICollection<OrderItemEntity> OrderItems { get; set; } = new List<OrderItemEntity>();
}
