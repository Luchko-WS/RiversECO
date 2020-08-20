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
                opt => opt.MapFrom(src => JsonConvert.DeserializeObject<CriteriaDto[]>(src.Criterias)));
            CreateMap<CreateReviewRequestDto, Review>().ForMember(
                dest => dest.Criterias,
                opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.Criterias)));
            CreateMap<UpdateReviewRequestDto, Review>().ForMember(
                dest => dest.Criterias,
                opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.Criterias)));
            CreateMap(typeof(PagedList<>), typeof(PagedListDto<>))
                    .ConvertUsing(typeof(PagedListConverter<,>));
        }
    }

    public class PagedListConverter<TEntity, TDto> : ITypeConverter<PagedList<TEntity>, PagedListDto<TDto>>
    {
        public PagedListDto<TDto> Convert(PagedList<TEntity> source, PagedListDto<TDto> destination, ResolutionContext context)
        {
            var pagedResultDto = new PagedListDto<TDto>();
            pagedResultDto.Page = new PageDto
            {
                Number = source.PageNumber,
                Size = source.PageSize,
                Total = source.Total
            };

            foreach (var item in source)
            {
                var objDto = context.Mapper.Map<TEntity, TDto>(item);
                pagedResultDto.Items.Add(objDto);
            }

            return pagedResultDto;
        }
    }
}
