using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebPizza.Data;
using WebPizza.Services.ControllerServices.Interfaces;
using WebPizza.Services.Interfaces;
using WebPizza.ViewModels.Category;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebPizza.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CategoriesController(IMapper mapper,
    PizzaDbContext pizzaContext,
    IValidator<CategoryCreateVm> createValidator,
    ICategoryControllerService service,
    IPaginationService<CategoryVm, CategoryFilterVm> pagination,
    IValidator<CategoryEditVm> editValidator
    ) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var list = await pizzaContext.Categories
                .ProjectTo<CategoryVm>(mapper.ConfigurationProvider)
                .ToArrayAsync();

            return Ok(list);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetPage([FromQuery] CategoryFilterVm vm)
    {
        try
        {
            return Ok(await pagination.GetPageAsync(vm));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await pizzaContext.Categories
            .ProjectTo<CategoryVm>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(c => c.Id == id);

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