using AutoMapper;
using CookBook.Contracts;
using CookBook.Models;

namespace CookBook.Configuration.Mappings;

public class IngredientMappingProfile : Profile
{
    public IngredientMappingProfile()
    {
        CreateMap<CreateIngredientDto, Ingredient>()
            .ForMember(dest => dest.IngredientId, opt => opt.Ignore());
 
        CreateMap<IngredientRC, Ingredient>()
            .ForMember(dest => dest.IngredientId, opt => opt.Ignore());
    }
}