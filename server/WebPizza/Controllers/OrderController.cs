using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebPizza.Core.DTO.Ingredient;
using WebPizza.Core.DTO.Order;
using WebPizza.Core.DTO.Sizes;
using WebPizza.Core.Entities;
using WebPizza.Core.Entities.Identity;
using WebPizza.Core.Interfaces;
using WebPizza.Core.Interfaces.ControllerInterfaces;
using WebPizza.Infrastructure.Data;

namespace WebPizza.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class OrderController(
    IMapper mapper,
    IOrderControllerService service,
    IIdentityService identityService,
    PizzaDbContext pizzaContext
    ) : ControllerBase
{
    [HttpGet]
    //[Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var list = await pizzaContext.Orders
              .ProjectTo<OrderVm>(mapper.ConfigurationProvider)
              .ToArrayAsync();

            return Ok(list);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> Create([FromBody] OrderCreateVm vm)
    {
        var user = await identityService.GetCurrentUserAsync(this);

        if (user == null)
        {
            return BadRequest("User not found!");
        }

        if (vm != null)
        {
            try
            {
                await service.CreateAsync(vm, user);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        return StatusCode(500, "Empty request");
    }
}

