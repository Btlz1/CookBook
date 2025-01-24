using AutoMapper;
using CookBook.Contracts;
using CookBook.Models;

namespace CookBook.Configuration.Mappings;

public class RecipesMappingProfile : Profile
{
    public RecipesMappingProfile()
    {
        CreateMap<CreateRecipeDto, RecipeModel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Name, 
                opt => opt.MapFrom(src => src.Name.Trim()))
            .ForMember(dest => dest.Description, 
                opt => opt.MapFrom(src => src.Description.Trim()))
            .ForMember(dest => dest.RecipeIngredients, 
                opt => opt.MapFrom(src => src.Ingredients));

       CreateMap<(int RecipeId, UpdateRecipeDto RecipeDto), RecipeModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RecipeId))
            .ForMember(dest => dest.Name, 
                opt => opt.MapFrom(src => src.RecipeDto.Name.Trim()))
            .ForMember(dest => dest.Description, 
                opt => opt.MapFrom(src => src.RecipeDto.Description.Trim()))
            .ForMember(dest => dest.RecipeIngredients, 
                opt => opt.MapFrom(src => src.RecipeDto.Ingredients));

       CreateMap<IngredientVm, RecipeIngredient>()
           .ForMember(dest => dest.IngredientId, opt => opt.MapFrom(src => src.IngredientId))
           .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
           .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit));

    }
}