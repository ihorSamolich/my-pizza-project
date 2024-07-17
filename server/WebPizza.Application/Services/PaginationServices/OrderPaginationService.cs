using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebPizza.Core.DTO.Order;
using WebPizza.Core.Entities;
using WebPizza.Core.Interfaces;
using WebPizza.Infrastructure.Data;

namespace WebPizza.Application.Services.PaginationServices;

public class OrderPaginationService(
    PizzaDbContext context,
    IMapper mapper,
    IScopedIdentityService scopedIdentityService
) : PaginationService<OrderEntity, OrderVm, OrderFilterVm>(mapper)
{

    protected override IQueryable<OrderEntity> GetQuery() => context.Orders
        .Include(o => o.OrderItems);

    protected override IQueryable<OrderEntity> FilterQuery(IQueryable<OrderEntity> query, OrderFilterVm vm)
    {
        query = query.Where(b => b.UserId == scopedIdentityService.GetRequiredUser().Id);

        return query;
    }
}