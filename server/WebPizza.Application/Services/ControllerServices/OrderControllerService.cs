using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPizza.Core.DTO.Ingredient;
using WebPizza.Core.DTO.Order;
using WebPizza.Core.Entities;
using WebPizza.Core.Entities.Identity;
using WebPizza.Core.Interfaces.ControllerInterfaces;
using WebPizza.Infrastructure.Data;

namespace WebPizza.Application.Services.ControllerServices;

public class OrderControllerService(
    PizzaDbContext pizzaContext,
    IMapper mapper
     ) : IOrderControllerService
{
    public async Task CreateAsync(OrderCreateVm vm, UserEntity user)
    {
        try
        {
            var order = new OrderEntity
            {
                IsDelivery = vm.IsDelivery,
                DeliveryAddress = vm.DeliveryAddress,
                UserId = user.Id,
                DateCreated = DateTime.UtcNow,
                OrderStatusId = 1,
                TotalAmount = 0,
                OrderItems = new List<OrderItemEntity>()
            };

            foreach (var item in vm.OrderItems)
            {
                order.OrderItems.Add(new OrderItemEntity
                {
                    PizzaId = item.PizzaId,
                    SizePriceId = item.PizzaSizeId,
                    Quantity = item.Quantity
                });

                var sizePrice = await pizzaContext.PizzaSizes
                     .FirstOrDefaultAsync(sp => sp.Id == item.PizzaSizeId);

                if (sizePrice != null)
                {
                    order.TotalAmount += sizePrice.Price * item.Quantity;
                }
            }

            pizzaContext.Orders.Add(order);
            await pizzaContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw new Exception("Error ingredient created");
        }
    }

    public async Task DeleteIfExistsAsync(int id)
    {
        throw new NotImplementedException();
    }
}

