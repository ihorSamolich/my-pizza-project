
using WebPizza.Core.DTO.Pagination;

namespace WebPizza.Core.DTO.Pizza;

public class PizzaFilterVm : PaginationVm
{
    public int CategoryId { get; set; }
}
