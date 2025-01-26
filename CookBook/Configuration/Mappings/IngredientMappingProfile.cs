using AutoMapper;
using CookBook.Contracts;
using CookBook.Models;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Configuration.Mappings;

public class IngredientMappingProfile : Profile
{
    public IngredientMappingProfile()
    {
        CreateMap<CreateIngredientDto, Ingredient>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<IngredientRC, Ingredient>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Trim()));
    }
}