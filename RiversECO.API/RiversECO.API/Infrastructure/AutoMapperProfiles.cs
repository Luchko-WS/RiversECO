using AutoMapper;
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
