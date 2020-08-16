using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using RiversECO.Contracts.Repositories;
using RiversECO.Dtos.Requests;
using RiversECO.Dtos.Responses;
using RiversECO.Models;

namespace RiversECO.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CriteriaController : ControllerBase
    {
        private readonly ICriteriasRepository _repository;
        private readonly IMapper _mapper;

        public CriteriaController(ICriteriasRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var criteria = await _repository.GetByIdAsync(id);
            var criteriaToReturn = _mapper.Map<CriteriaDto>(criteria);
            return Ok(criteriaToReturn);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var criterias = await _repository.GetAllAsync();
            var criteriasToReturn = _mapper.Map<PagedListDto<CriteriaDto>>(criterias);
            return Ok(criterias);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateCriteriaRequestDto dto)
        {
            var criteriaToCreate = _mapper.Map<Criteria>(dto);
            _repository.Create(criteriaToCreate);

            if (await _repository.SaveAllChangesAsync())
            {
                var criteriaToReturn = _mapper.Map<CriteriaDto>(criteriaToCreate);
                return CreatedAtRoute(nameof(Get), new { id = criteriaToCreate.Id }, criteriaToReturn);
            }

            return BadRequest("Could not create a criteria.");
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]UpdateCriteriaRequestDto dto)
        {
            var criteriaFromRepo = await _repository.GetByIdAsync(dto.Id);
            _mapper.Map(dto, criteriaFromRepo);
            await _repository.SaveAllChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            _repository.Delete(id);
            await _repository.SaveAllChangesAsync();
            return Ok();
        }
    }
}
