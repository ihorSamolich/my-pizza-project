using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebPizza.Core.DTO.Sizes;
using WebPizza.Infrastructure.Data;

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
