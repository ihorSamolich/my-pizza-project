using Microsoft.EntityFrameworkCore;
using WebPizza.Core.Interfaces;
using WebPizza.Infrastructure.Data;

namespace WebPizza.Application.Services;

public class ExistingEntityCheckerService(
 PizzaDbContext context
) : IExistingEntityCheckerService
{
    public async Task<bool> IsCorrectCategoryId(int id, CancellationToken cancellationToken) =>
        await context.Categories.AnyAsync(c => c.Id == id, cancellationToken);

    public async Task<bool> IsCorrectIngredientId(int id, CancellationToken cancellationToken) =>
        await context.Ingredients.AnyAsync(c => c.Id == id, cancellationToken);
}

