using AutoMapper;
using CookBook.Contracts;
using CookBook.Models;

namespace CookBook.Configuration.Mappings;

public class RecipesMappingProfile : Profile
{
    public RecipesMappingProfile()
    {
        CreateMap<CreateRecipeDto, Recipe>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Name, 
                opt => opt.MapFrom(src => src.Name.Trim()))
            .ForMember(dest => dest.Description, 
                opt => opt.MapFrom(src => src.Description.Trim()));
        
        CreateMap<(int ReciepId, UpdateRecipeDto RecipeDto), Recipe>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RecipeDto))
            .ForMember(dest => dest.Name, 
                opt => opt.MapFrom(src => src.RecipeDto.Name.Trim()))
            .ForMember(dest => dest.Description, 
                opt => opt.MapFrom(src => src.RecipeDto.Description.Trim()));
    }
}