using System.Collections.Generic;
using AutoMapper;
using Newtonsoft.Json;
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
            CreateMap<CreateCriteriaRequestDto, Criteria>();
            CreateMap<UpdateCriteriaRequestDto, Criteria>();
            CreateMap<WaterObject, WaterObjectDto>();
            CreateMap<Review, ReviewDto>().ForMember(
                dest => dest.Criterias,
                opt => opt.MapFrom(src => JsonConvert.DeserializeObject<List<CriteriaDto>>(src.Criterias)));
            CreateMap<CreateReviewRequestDto, Review>().ForMember(
                dest => dest.Criterias,
                opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.Criterias)));
            CreateMap<UpdateReviewRequestDto, Review>().ForMember(
                dest => dest.Criterias,
                opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.Criterias)));
        }
    }
}
