using AutoMapper;
using FitYouFood.API.Dtos.ExerciseDataDtos;
using FitYouFood.API.Services;
using FitYouFood.API.Services.Interfaces;
using FitYouFood.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims;

namespace FitYouFood.API.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ExerciseDataController(IExerciseDataService _exerciseDataService, IMapper _mapper, ITrainingService _trainingService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetUserExerciseDatas()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized("User ID claim not found in the token.");

            var exercises = _mapper.Map<List<ExerciseDataDto>>(await _exerciseDataService.GetUserExerciseDatas(userId));
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(exercises);
        }

        [HttpGet("{exerciseDataId}")]
        public async Task<IActionResult> GetExerciseData(int exerciseDataId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized("User ID claim not found in the token.");

            if (!await _exerciseDataService.ExerciseDataExistsAsync(exerciseDataId))
                return NotFound();

            var exercise = _mapper.Map<ExerciseDataDto>(await _exerciseDataService.GetExerciseData(exerciseDataId, userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(exercise);
        }

        [HttpPost("CreateExercisedata")]
        public async Task<IActionResult> PostExerciseData([FromBody] ExerciseDataCreateDto exerciseDataDto)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized("User ID claim not found in the token.");

            if (exerciseDataDto == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var training = await _trainingService.GetTraining(exerciseDataDto.TrainingId, userId);

            if (training == null || training.UserId != userId)
                return Unauthorized();

            var exerciseMap = _mapper.Map<ExerciseData>(exerciseDataDto);

            exerciseMap.UserId = userId;

            if(!await _exerciseDataService.CreateExerciseData(exerciseMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created");
        }

        [HttpPut("{exerciseDataId}")]
        public async Task<IActionResult> PutExerciseData(int exerciseDataId, [FromBody] ExerciseDataUpdateDto exerciseDataDto)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized("User ID claim not found in the token.");

            if (exerciseDataDto == null || exerciseDataId != exerciseDataDto.Id || !ModelState.IsValid)
                return BadRequest(ModelState);
            if (!await _exerciseDataService.ExerciseDataExistsAsync(exerciseDataId))
                return NotFound();

            var exercise = await _exerciseDataService.GetExerciseData(exerciseDataId, userId);
            exercise.Difficulty = exerciseDataDto.Difficulty;
            exercise.HowMuchMoreRepsAbleToDo = exerciseDataDto.HowMuchMoreRepsAbleToDo;
            exercise.WhenExercised = DateTime.Now;


            if(!await _exerciseDataService.UpdateExerciseData(exercise))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{exerciseDataId}")]
        public async Task<IActionResult> DeleteExercisedata(int exerciseDataId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized("User ID claim not found in the token.");

            if (!await _exerciseDataService.ExerciseDataExistsAsync(exerciseDataId) || !ModelState.IsValid)
                return NotFound(ModelState);

            var exercise = await _exerciseDataService.GetExerciseData(exerciseDataId, userId);

            exercise.IsDeleted = true;

            if (!await _exerciseDataService.UpdateExerciseData(exercise))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
