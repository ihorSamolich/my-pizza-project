using WebPizza.Core.DTO.Pagination;

namespace WebPizza.Core.Interfaces;

public interface IPaginationService<EntityVmType, PaginationVmType> where PaginationVmType : PaginationVm
{
    Task<PageVm<EntityVmType>> GetPageAsync(PaginationVmType vm);
}
