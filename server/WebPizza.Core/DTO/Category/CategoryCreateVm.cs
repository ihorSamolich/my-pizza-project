using Microsoft.AspNetCore.Http;

namespace WebPizza.Core.DTO.Category;

public class CategoryCreateVm
{
    public string Name { get; set; } = null!;

    public IFormFile Image { get; set; } = null!;
}
