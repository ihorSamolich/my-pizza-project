using AutoMapper;
using WebPizza.Data;
using WebPizza.Data.Entities;
using WebPizza.Services.PaginationServices.Base;
using WebPizza.ViewModels.Category;
using WebPizza.ViewModels.Pizza;

namespace WebPizza.Services.PaginationServices;


public class PizzaPaginationService(
    PizzaDbContext context,
    IMapper mapper
) : PaginationService<PizzaEntity, PizzaVm, PizzaFilterVm>(mapper)
{
    protected override IQueryable<PizzaEntity> GetQuery() => context.Pizzas.OrderBy(c => c.Name);

    protected override IQueryable<PizzaEntity> FilterQuery(IQueryable<PizzaEntity> query, PizzaFilterVm paginationVm)
    {
        return query;
    }
}
