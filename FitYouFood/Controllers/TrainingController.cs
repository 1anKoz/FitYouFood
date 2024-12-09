using AutoMapper;
using FitYouFood.API.Dtos.ExerciseDataDtos;
using FitYouFood.API.Dtos.Training;
using FitYouFood.API.Services;
using FitYouFood.API.Services.Interfaces;
using FitYouFood.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitYouFood.API.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class TrainingController(ITrainingService _trainingService, IExerciseDataService _exerciseDataService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetUserTrainings()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized("User ID claim not found in the token.");

            var trainings = _mapper.Map<List<TrainingDto>>(await _trainingService.GetTrainings(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(trainings);
        }

        [HttpGet("{trainingId}")]
        public async Task<IActionResult> GetTraining(int trainingId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized("User ID claim not found in the token.");

            if (!await _trainingService.TrainingExistsAsync(trainingId))
                return NotFound();

            var training = _mapper.Map<TrainingDto>(await _trainingService.GetTraining(trainingId, userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(training);
        }

        [HttpPost]
        public async Task<IActionResult> PostTraining([FromBody]TrainingCreateDto trainingDto)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized("User ID claim not found in the token.");

            if (trainingDto == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var trainingMap = _mapper.Map<Training>(trainingDto);

            trainingMap.UserId = userId;

            if(!await _trainingService.CreateTraining(trainingMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{trainingId}")]
        public async Task<IActionResult> PutTraining(int trainingId, [FromBody]TrainingUpdateDto trainingDto)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized("User ID claim not found in the token.");

            var training = await _trainingService.GetTraining(trainingId, userId);

            if (training == null)
                return NotFound();

            if(training.UserId != userId)
                return Unauthorized();

            if (trainingDto == null || !ModelState.IsValid)
                return BadRequest(ModelState);
            if (!await _trainingService.TrainingExistsAsync(trainingId))
                return NotFound();

            var trainingMap = _mapper.Map<Training>(trainingDto);

            trainingMap.UserId = userId;

            if (!await _trainingService.UpdateTraining(trainingMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully updated");
        }

        [HttpDelete("{trainingId}")]
        public async Task<IActionResult> DeleteTraining(int trainingId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized("User ID claim not found in the token.");

            if (!await _trainingService.TrainingExistsAsync(trainingId))
                return NotFound();
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var training = await _trainingService.GetTraining(trainingId, userId);

            if (training == null)
                return NotFound();

            if (training.UserId != userId)
                return Unauthorized();

            training.IsDeleted = true;

            if(!await _trainingService.UpdateTraining(training))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
