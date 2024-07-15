using System.ComponentModel.DataAnnotations.Schema;

namespace WebPizza.Core.Entities;

[Table("tbl_order_items")]

public class OrderItemEntity : BaseEntity
{
    public int Quantity { get; set; }

    public int OrderId { get; set; }
    public int PizzaId { get; set; }
    public int SizePriceId { get; set; }


    public OrderEntity Order { get; set; } = null!;
    public PizzaEntity Pizza { get; set; } = null!;
    public PizzaSizePriceEntity SizePrice { get; set; } = null!;
}
