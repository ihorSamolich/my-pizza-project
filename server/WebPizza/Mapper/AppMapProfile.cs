using AutoMapper;
using WebPizza.Data.Entities;
using WebPizza.ViewModels.Category;
using WebPizza.ViewModels.Ingredient;
using WebPizza.ViewModels.Pizza;
using WebPizza.ViewModels.PizzaSizes;

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
        CreateMap<IngredientVm, IngredientEntity>();

        CreateMap<PizzaIngredientEntity, IngredientVm>()
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Ingredient.Name))
              .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Ingredient.Image));


        // Pizza
        CreateMap<PizzaEntity, PizzaVm>();

        CreateMap<PizzaSizePriceEntity, PizzaSizePriceVm>()
               .ForMember(dest => dest.SizeName, opt => opt.MapFrom(src => src.Size.Name));


    }

}