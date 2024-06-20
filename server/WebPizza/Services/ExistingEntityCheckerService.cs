using Microsoft.EntityFrameworkCore;
using WebPizza.Data;
using WebPizza.Services.Interfaces;

namespace WebPizza.Services;

public class ExistingEntityCheckerService(
    PizzaDbContext context
 ) : IExistingEntityCheckerService
{
    public async Task<bool> IsCorrectCategoryId(int id, CancellationToken cancellationToken) =>
        await context.Categories.AnyAsync(c => c.Id == id, cancellationToken);
}
