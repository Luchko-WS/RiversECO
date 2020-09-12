using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Newtonsoft.Json;
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
            await ValidateCriterias(reviewToReturn);
            return Ok(reviewToReturn);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reviews = await _reviewsRepository.GetAllAsync();
            var reviewsToReturn = _mapper.Map<List<ReviewDto>>(reviews);
            await ValidateCriterias(reviewsToReturn.ToArray());
            return Ok(reviewsToReturn);
        }

        [HttpGet("for/{waterObjectId}")]
        public async Task<IActionResult> GetAllForWaterObject(Guid waterObjectId)
        {
            var reviews = await _reviewsRepository.GetAllForWaterObjectAsync(waterObjectId);
            var reviewsToReturn = _mapper.Map<List<ReviewDto>>(reviews);
            await ValidateCriterias(reviewsToReturn.ToArray());
            return Ok(reviewsToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateReviewRequestDto dto)
        {
            var reviewToCreate = _mapper.Map<Review>(dto);
            _reviewsRepository.Create(reviewToCreate);

            if (await _reviewsRepository.SaveAllChangesAsync())
            {
                var reviewToReturn = _mapper.Map<ReviewDto>(reviewToCreate);
                return CreatedAtRoute(nameof(GetReview), new { id = reviewToCreate.Id }, reviewToReturn);
            }

            return BadRequest("Could not create a review.");
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]UpdateReviewRequestDto dto)
        {
            var reviewFromRepo = await _reviewsRepository.GetByIdAsync(dto.Id);
            _mapper.Map(dto, reviewFromRepo);

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

        private async Task ValidateCriterias(params ReviewDto[] reviews)
        {
            var criteriasFromRepo = (await _criteriasRepository.GetAllAsync())
                .ToDictionary(x => x.Id);

            var reviewsModified = false;
            foreach (var review in reviews)
            {
                var needUpdateReviewInDb = false;
                for (var i = 0; i < review.Criterias.Count; i++)
                {
                    var criteriaDto = review.Criterias[i];
                    if (criteriasFromRepo.TryGetValue(criteriaDto.Id, out var criteriaFromRepo))
                    {
                        if (criteriaFromRepo.Name != criteriaDto.Name)
                        {
                            criteriaDto.Name = criteriaFromRepo.Name;
                            needUpdateReviewInDb = true;
                        }

                        if (criteriaFromRepo.Description != criteriaDto.Description)
                        {
                            criteriaDto.Description = criteriaFromRepo.Description;
                            needUpdateReviewInDb = true;
                        }
                    }
                    else
                    {
                        review.Criterias.RemoveAt(i--);
                        needUpdateReviewInDb = true;
                    }
                }

                if (needUpdateReviewInDb)
                {
                    var reviewToUpdate = await _reviewsRepository.GetByIdAsync(review.Id);
                    reviewToUpdate.Criterias = JsonConvert.SerializeObject(review.Criterias);
                    reviewsModified = true;
                }
            }

            if (reviewsModified)
            {
                await _reviewsRepository.SaveAllChangesAsync();
            }
        }
    }
}
