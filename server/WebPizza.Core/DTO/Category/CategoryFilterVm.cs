using WebPizza.Core.DTO.Pagination;

namespace WebPizza.Core.DTO.Category;
public class CategoryFilterVm : PaginationVm
{
    public string? Name { get; set; }
}
