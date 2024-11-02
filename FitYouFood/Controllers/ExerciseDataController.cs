using AutoMapper;
using FitYouFood.API.Dtos.ExerciseDataDtos;
using FitYouFood.API.Services.Interfaces;
using FitYouFood.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;

namespace FitYouFood.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExerciseDataController(IExerciseDataService _exerciseDataService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetUserExerciseDatas(string userId)
        {
            var exercises = _mapper.Map<List<ExerciseDataDto>>(await _exerciseDataService.GetUserExerciseDatas(userId));
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(exercises);
        }

        [HttpGet("{exerciseDataId}")]
        public async Task<IActionResult> GetExerciseData(int exerciseDataId)
        {
            if (!await _exerciseDataService.ExerciseDataExistsAsync(exerciseDataId))
                return NotFound();

            var exercise = _mapper.Map<ExerciseDataDto>(await _exerciseDataService.GetExerciseData(exerciseDataId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(exercise);
        }

        [HttpPost("CreateExercisedata")]
        public async Task<IActionResult> PostExerciseData([FromBody] ExerciseDataCreateDto exerciseDataDto)
        {
            if(exerciseDataDto == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var exerciseMap = _mapper.Map<ExerciseData>(exerciseDataDto);

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
            if (exerciseDataDto == null || exerciseDataId != exerciseDataDto.Id || !ModelState.IsValid)
                return BadRequest(ModelState);
            if (!await _exerciseDataService.ExerciseDataExistsAsync(exerciseDataId))
                return NotFound();

            var exercise = await _exerciseDataService.GetExerciseData(exerciseDataId);
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
            if(!await _exerciseDataService.ExerciseDataExistsAsync(exerciseDataId) || !ModelState.IsValid)
                return NotFound(ModelState);

            var exercise = await _exerciseDataService.GetExerciseData(exerciseDataId);

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
