using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using WebPizza.Data;
using WebPizza.ViewModels.Category;

using Microsoft.EntityFrameworkCore;
using WebPizza.ViewModels.Pizza;
using WebPizza.ViewModels.Ingredient;
using WebPizza.Services.ControllerServices.Interfaces;

namespace WebPizza.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class PizzaController(IMapper mapper,
    IPizzaControllerService service,
    PizzaDbContext pizzaContext
    ) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var list = await pizzaContext.Pizzas
               .ProjectTo<PizzaVm>(mapper.ConfigurationProvider)
               .ToArrayAsync();

            return Ok(list);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var pizza = await pizzaContext.Pizzas
            .ProjectTo<PizzaVm>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (pizza is null)
            return NotFound();

        return Ok(pizza);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] PizzaCreateVm vm)
    {
        try
        {
            await service.CreateAsync(vm);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await service.DeleteIfExistsAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

}
