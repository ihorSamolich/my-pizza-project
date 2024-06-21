namespace WebPizza.ViewModels.Category
{
    public class CategoryVm
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Image { get; set; } = null!;
        public DateTime DateCreated { get; set; }
    }
}
