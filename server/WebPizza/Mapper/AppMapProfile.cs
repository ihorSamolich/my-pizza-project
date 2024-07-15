using AutoMapper;
using WebPizza.Core.DTO.Account;
using WebPizza.Core.DTO.Category;
using WebPizza.Core.DTO.Ingredient;
using WebPizza.Core.DTO.Order;
using WebPizza.Core.DTO.Pizza;
using WebPizza.Core.DTO.PizzaSizes;
using WebPizza.Core.DTO.Sizes;
using WebPizza.Core.Entities;
using WebPizza.Core.Entities.Identity;

namespace WebPizza.Mapper;
public class AppMapProfile : Profile
{
    public AppMapProfile()
    {
        // User
        CreateMap<RegisterVm, UserEntity>();

        // Category
        CreateMap<CategoryCreateVm, CategoryEntity>()
            .ForMember(c => c.Image, opt => opt.Ignore())
            .ForMember(c => c.Pizzas, opt => opt.Ignore());


        CreateMap<CategoryEntity, CategoryVm>()
            .ForMember(dest => dest.NumberOfPizzas, opt => opt.MapFrom(src => src.Pizzas.Count));


        // Ingredient
        CreateMap<IngredientCreateVm, IngredientEntity>()
            .ForMember(c => c.Image, opt => opt.Ignore());

        CreateMap<IngredientEntity, IngredientVm>();
        CreateMap<IngredientVm, IngredientEntity>();

        CreateMap<PizzaIngredientEntity, IngredientVm>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Ingredient.Id))
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Ingredient.Name))
              .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Ingredient.Image));


        // Pizza
        CreateMap<PizzaEntity, PizzaVm>();

        CreateMap<PizzaCreateVm, PizzaEntity>()
             .ForMember(c => c.IsAvailable, opt => opt.Ignore())
             .ForMember(c => c.Rating, opt => opt.Ignore())
             .ForMember(c => c.Photos, opt => opt.Ignore())
             .ForMember(c => c.Sizes, opt => opt.Ignore());

        CreateMap<PizzaSizePriceEntity, PizzaSizePriceVm>()
               .ForMember(dest => dest.SizeName, opt => opt.MapFrom(src => src.Size.Name));

        CreateMap<PizzaPhotoEntity, PizzaPhotoVm>();

        // Sizes
        CreateMap<PizzaSizeEntity, SizeVm>();

        // Order Item Entity
        CreateMap<OrderItemEntity, OrderItemFullVm>()
             .ForMember(dest => dest.PizzaSizeId, opt => opt.MapFrom(src => src.SizePriceId));

        // Order
        CreateMap<OrderEntity, OrderVm>()
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

    }

}