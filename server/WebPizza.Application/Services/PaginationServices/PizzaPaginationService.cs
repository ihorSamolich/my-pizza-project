using AutoMapper;
using WebPizza.Core.DTO.Pizza;
using WebPizza.Core.Entities;
using WebPizza.Infrastructure.Data;

namespace WebPizza.Application.Services.PaginationServices;

public class PizzaPaginationService(
    PizzaDbContext context,
    IMapper mapper
) : PaginationService<PizzaEntity, PizzaVm, PizzaFilterVm>(mapper)
{
    protected override IQueryable<PizzaEntity> GetQuery() => context.Pizzas.OrderBy(c => c.Name);

    protected override IQueryable<PizzaEntity> FilterQuery(IQueryable<PizzaEntity> query, PizzaFilterVm paginationVm)
    {
        if (paginationVm.CategoryId > 0)
            query = query.Where(c => c.CategoryId == paginationVm.CategoryId);

        if (paginationVm.Name is not null)
            query = query.Where(c => c.Name.ToLower().Contains(paginationVm.Name.ToLower()));


        return query;
    }
}
