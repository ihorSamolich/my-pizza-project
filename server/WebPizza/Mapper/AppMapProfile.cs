using AutoMapper;
using WebPizza.Data.Entities;
using WebPizza.ViewModels.Category;
using WebPizza.ViewModels.Ingredient;

namespace WebPizza.Mapper;
public class AppMapProfile : Profile
{
    public AppMapProfile()
    {
        // Category

        CreateMap<CategoryCreateVm, CategoryEntity>()
            .ForMember(c => c.Image, opt => opt.Ignore());

        CreateMap<CategoryEntity, CategoryVm>();


        // Ingredient

        CreateMap<IngredientCreateVm, IngredientEntity>()
            .ForMember(c => c.Image, opt => opt.Ignore());

        CreateMap<IngredientEntity, IngredientVm>();
    }

}