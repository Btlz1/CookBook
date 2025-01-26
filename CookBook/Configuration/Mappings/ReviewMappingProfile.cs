using AutoMapper;
using CookBook.Contracts;
using CookBook.Models;

namespace CookBook.Configuration.Mappings;

public class ReviewMappingProfile : Profile
{
    public ReviewMappingProfile()
    {
        CreateMap<CreateReviewDto, Review>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Trim()))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description.Trim()));
        
        CreateMap<ReviewVm, Review>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Trim()))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description.Trim()))
            .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.Score.ToString()));

        CreateMap<(int id, UpdateReviewDto dto), Review>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.dto.Name.Trim()))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.dto.Description.Trim()));

    }
}


