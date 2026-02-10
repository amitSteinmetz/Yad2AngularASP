using AutoMapper;
using backend.DTOs;
using backend.Models.AssetModels;

namespace backend.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AssetDto, Asset>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PublishDate, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
