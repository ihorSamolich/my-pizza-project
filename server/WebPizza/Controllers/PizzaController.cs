using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using WebPizza.Data;
using WebPizza.ViewModels.Category;

using Microsoft.EntityFrameworkCore;
using WebPizza.ViewModels.Pizza;

namespace WebPizza.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class PizzaController(IMapper mapper,
    PizzaDbContext pizzaContext
    ) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var list = await pizzaContext.Pizzas
               .Include(x => x.Photos)
               .ProjectTo<PizzaVm>(mapper.ConfigurationProvider)
               .ToArrayAsync();

            return Ok(list);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

}
