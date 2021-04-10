using AutoMapper;
using RiversECO.Dtos.Common;
using RiversECO.Dtos.Requests;
using RiversECO.Dtos.Responses;
using RiversECO.Models;

namespace RiversECO.API.Infrastructure
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Criteria, CriteriaDto>();
            CreateMap<Criteria, ReviewCriteriaDto>();
            CreateMap<CreateCriteriaRequestDto, Criteria>();
            CreateMap<UpdateCriteriaRequestDto, Criteria>();
            CreateMap<WaterObject, WaterObjectDto>();
            CreateMap<Review, ReviewDto>();
            CreateMap<CreateReviewRequestDto, Review>();
            CreateMap<UpdateReviewRequestDto, Review>();
            CreateMap<User, UserDto>();
        }
    }
}
