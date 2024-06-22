namespace WebPizza.ViewModels.PizzaSizes
{
    public class PizzaSizePriceVm
    {
        public int Id { get; set; }
        public string SizeName { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
