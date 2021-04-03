using System;
using System.Collections.Generic;
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
    public class ReviewController : ControllerBase
    {
        private readonly IReviewsRepository _reviewsRepository;
        private readonly ICriteriasRepository _criteriasRepository;
        private readonly IMapper _mapper;

        public ReviewController(
            IReviewsRepository reviewsRepository,
            ICriteriasRepository criteriasRepository,
            IMapper mapper)
        {
            _reviewsRepository = reviewsRepository;
            _criteriasRepository = criteriasRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = nameof(GetReview))]
        public async Task<IActionResult> GetReview(Guid id)
        {
            var review = await _reviewsRepository.GetByIdAsync(id);
            var reviewToReturn = _mapper.Map<ReviewDto>(review);
            return Ok(reviewToReturn);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reviews = await _reviewsRepository.GetAllAsync();
            var reviewsToReturn = _mapper.Map<List<ReviewDto>>(reviews);
            return Ok(reviewsToReturn);
        }

        [HttpGet("for/{waterObjectId}")]
        public async Task<IActionResult> GetAllForWaterObject(Guid waterObjectId)
        {
            var reviews = await _reviewsRepository.GetAllForWaterObjectAsync(waterObjectId);
            var reviewsToReturn = _mapper.Map<List<ReviewDto>>(reviews);
            return Ok(reviewsToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateReviewRequestDto dto)
        {
            var reviewToCreate = _mapper.Map<Review>(dto);

            var criteria = await _criteriasRepository.GetCriteriaByName(dto.CriteriaName);
            if (criteria == null)
            {
                criteria = new Criteria { Name = dto.CriteriaName };
                _criteriasRepository.Create(criteria);
            }
            reviewToCreate.Criteria = criteria;

            _reviewsRepository.Create(reviewToCreate);

            if (await _reviewsRepository.SaveAllChangesAsync())
            {
                var reviewFromDb = await _reviewsRepository.GetByIdAsync(reviewToCreate.Id);
                var droToReturn = _mapper.Map<ReviewDto>(reviewFromDb);
                return CreatedAtRoute(nameof(GetReview), new { id = reviewToCreate.Id }, droToReturn);
            }

            return BadRequest("Could not create a review.");
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]UpdateReviewRequestDto dto)
        {
            var reviewFromRepo = await _reviewsRepository.GetByIdAsync(dto.Id);
            _mapper.Map(dto, reviewFromRepo);

            var criteria = await _criteriasRepository.GetCriteriaByName(dto.CriteriaName);
            if (criteria?.Id != reviewFromRepo.CriteriaId)
            {
                if (criteria == null)
                {
                    criteria = new Criteria { Name = dto.CriteriaName };
                    _criteriasRepository.Create(criteria);
                }
                reviewFromRepo.Criteria = criteria;
            }

            if (await _reviewsRepository.SaveAllChangesAsync())
            {
                return Ok();
            }

            return BadRequest("Could not update a review.");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]Guid[] ids)
        {
            foreach (var id in ids)
            {
                _reviewsRepository.Delete(id);
            }

            await _reviewsRepository.SaveAllChangesAsync();
            return Ok();
        }
    }
}
