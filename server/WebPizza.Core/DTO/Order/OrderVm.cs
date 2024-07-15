using WebPizza.Core.DTO.Pizza;
using WebPizza.Core.DTO.PizzaSizes;
using WebPizza.Core.DTO.Sizes;
using WebPizza.Core.Entities;

namespace WebPizza.Core.DTO.Order;

public class OrderVm
{
    public int Id { get; set; }
    public decimal TotalAmount { get; set; }
    public string DeliveryAddress { get; set; } = null!;
    public bool IsDelivery { get; set; }
    public DateTime DateCreated { get; set; }
    public IEnumerable<OrderItemFullVm> OrderItems { get; set; } = new List<OrderItemFullVm>();
}

public class OrderItemFullVm
{
    public int PizzaId { get; set; }
    public int PizzaSizeId { get; set; }
    public int Quantity { get; set; }
    public PizzaVm Pizza { get; set; } = null!;
    public PizzaSizePriceVm SizePrice { get; set; } = null!;
}

