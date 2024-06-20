namespace WebPizza.Services.Interfaces
{
    public interface IExistingEntityCheckerService
    {
        Task<bool> IsCorrectCategoryId(int id, CancellationToken cancellationToken);

    }
}
