using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using RiversECO.Contracts.Repositories;
using RiversECO.Dtos.Responses;

namespace RiversECO.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WaterObjectController : ControllerBase
    {
        private readonly IWaterObjectsRepository _repository;
        private readonly IMapper _mapper;

        public WaterObjectController(IWaterObjectsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var waterObject = _repository.GetById(id);
            var waterObjectToReturn = _mapper.Map<WaterObjectDto>(waterObject);
            return Ok(waterObjectToReturn);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var waterObjects = _repository.GetAll();
            var waterObjectsToReturn = _mapper.Map<List<WaterObjectDto>>(waterObjects);
            return Ok(waterObjectsToReturn);
        }
    }
}
