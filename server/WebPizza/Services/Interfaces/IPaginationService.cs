using WebPizza.ViewModels.Pagination;

namespace WebPizza.Services.Interfaces;

public interface IPaginationService<EntityVmType, PaginationVmType> where PaginationVmType : PaginationVm
{
    Task<PageVm<EntityVmType>> GetPageAsync(PaginationVmType vm);
}
