using AutoMapper;
using WebPizza.Data.Entities;
using WebPizza.ViewModels.Category;

namespace WebPizza.Mapper;
public class AppMapProfile : Profile
{
    public AppMapProfile()
    {
        CreateMap<CategoryCreateVm, CategoryEntity>()
            .ForMember(c => c.Image, opt => opt.Ignore());
    }

}