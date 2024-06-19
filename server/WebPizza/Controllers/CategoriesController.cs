using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using WebPizza.Data;
using WebPizza.Data.Entities;
using WebPizza.Services.Interfaces;
using WebPizza.ViewModels.Category;

namespace WebPizza.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController(IMapper mapper,
    PizzaDbContext pizzaContext,
    IValidator<CategoryCreateVm> createValidator,
    //IValidator<CategoryEditVm> updateValidator,
    IImageService imageService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var list = await pizzaContext.Categories.ToListAsync();
            return Ok(list);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var category = await pizzaContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            imageService.DeleteImageIfExists(category.Image);
            pizzaContext.Categories.Remove(category);
            await pizzaContext.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromForm] CategoryCreateVm vm)
    {
        var validationResult = await createValidator.ValidateAsync(vm);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var category = mapper.Map<CategoryEntity>(vm);

        try
        {
            category.Image = await imageService.SaveImageAsync(vm.Image);
            await pizzaContext.Categories.AddAsync(category);
            await pizzaContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = category.Id }, category);
        }
        catch (Exception)
        {
            imageService.DeleteImageIfExists(category.Image);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromForm] CategoryEditVm vm)
    {
        //var validationResult = await updateValidator.ValidateAsync(vm);

        //if (!validationResult.IsValid)
        //    return BadRequest(validationResult.Errors);

        var category = await pizzaContext.Categories.FirstOrDefaultAsync(c => c.Id == vm.Id);

        if (category == null)
        {
            return NotFound();
        }

        var oldImage = category.Image;

        category.Name = vm.Name;

        if (vm.Image != null)
        {
            category.Image = await imageService.SaveImageAsync(vm.Image);
            imageService.DeleteImageIfExists(oldImage);
        }

        try
        {
            pizzaContext.Categories.Update(category);
            await pizzaContext.SaveChangesAsync();
            return Ok(category);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

}