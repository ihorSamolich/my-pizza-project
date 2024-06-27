using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebPizza.Data;
using WebPizza.Services.ControllerServices.Interfaces;
using WebPizza.ViewModels.Pizza;
using WebPizza.ViewModels.Sizes;

namespace WebPizza.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class SizesController(IMapper mapper,
    PizzaDbContext pizzaContext
    ) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var list = await pizzaContext.Sizes
              .ProjectTo<SizeVm>(mapper.ConfigurationProvider)
              .ToArrayAsync();

            return Ok(list);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }
}
