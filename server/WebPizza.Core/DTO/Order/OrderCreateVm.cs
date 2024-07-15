namespace WebPizza.Core.DTO.Order;

public class OrderCreateVm
{
    public IEnumerable<OrderItemVm> OrderItems { get; set; } = null!;
    public string DeliveryAddress { get; set; } = null!;
    public bool IsDelivery { get; set; }
}

public class OrderItemVm
{
    public int PizzaId { get; set; }
    public int PizzaSizeId { get; set; }
    public int Quantity { get; set; }
}