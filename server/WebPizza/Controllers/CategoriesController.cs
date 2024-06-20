using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebPizza.Data;
using WebPizza.Services.ControllerServices.Interfaces;
using WebPizza.ViewModels.Category;

namespace WebPizza.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CategoriesController(IMapper mapper,
    PizzaDbContext pizzaContext,
    IValidator<CategoryCreateVm> createValidator,
    ICategoryControllerService service,
    IValidator<CategoryEditVm> editValidator
    ) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var list = mapper.Map<List<CategoryVm>>(await pizzaContext.Categories.ToListAsync());

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
        var category = mapper.Map<CategoryVm>(await pizzaContext.Categories.FirstOrDefaultAsync(c => c.Id == id));

        if (category is null)
            return NotFound();

        return Ok(category);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CategoryCreateVm vm)
    {
        var validationResult = await createValidator.ValidateAsync(vm);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

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

    [HttpPatch]
    public async Task<IActionResult> Update([FromForm] CategoryEditVm vm)
    {
        var validationResult = await editValidator.ValidateAsync(vm);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        try
        {
            await service.UpdateAsync(vm);
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