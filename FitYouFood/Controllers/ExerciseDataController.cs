using AutoMapper;
using FitYouFood.API.Dtos.ExerciseData;
using FitYouFood.API.Services.Interfaces;
using FitYouFood.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("exerciseId")]
        public async Task<IActionResult> GetExerciseData(int exerciseDataId, string userId)
        {
            if (!await _exerciseDataService.ExerciseDataExistsAsync(exerciseDataId, userId))
                return NotFound();

            var exercise = _mapper.Map<ExerciseDataDto>(await _exerciseDataService.GetExerciseData(exerciseDataId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(exercise);
        }

        [HttpPost("CreateExercisedata")]
        public async Task<IActionResult> PostExerciseData([FromBody] ExerciseDataDto exerciseDataDto)
        {
            if(exerciseDataDto == null)
                return BadRequest(ModelState);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var exerciseMap = _mapper.Map<ExerciseData>(exerciseDataDto);

            if(!await _exerciseDataService.CreateExerciseData(exerciseMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created");
        }
    }
}
