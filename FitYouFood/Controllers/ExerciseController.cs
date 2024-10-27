using AutoMapper;
using FitYouFood.API.Dtos.Exercise;
using FitYouFood.API.Services.Interfaces;
using FitYouFood.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FitYouFood.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExerciseController(IExerciseService _exerciseService, IMapper _mapper) : ControllerBase
    {
        [HttpGet("{exerciseId}")]
        public async Task<IActionResult> GetExercise(int exerciseId)
        {
            if (!await _exerciseService.ExerciseExistsAsync(exerciseId))
                return NotFound();

            var exercise = _mapper.Map<ExerciseDto>(await _exerciseService.GetExercise(exerciseId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(exercise);
        }

        [HttpGet]
        public async Task<IActionResult> GetExercises()
        {
            var exercises = _mapper.Map<List<ExerciseDto>>(await _exerciseService.GetExercises());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(exercises);
        }

        [HttpPost("CreateExercise")]
        public async Task<IActionResult> PostExercise([FromBody] ExerciseDto exerciseDto)
        {
            if(exerciseDto == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var exerciseMap = _mapper.Map<Exercise>(exerciseDto);

            if (!await _exerciseService.CreateExercise(exerciseMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created");
        }

        [HttpPut("{exerciseId}")]
        public async Task<IActionResult> PutExercise (int exerciseId, [FromBody] ExerciseDto exerciseDto)
        {
            if(exerciseDto == null || exerciseId != exerciseDto.Id)
                return BadRequest(ModelState);
            
            if(!await _exerciseService.ExerciseExistsAsync(exerciseId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var exerciseMap = _mapper.Map<Exercise>(exerciseDto);

            if(!await _exerciseService.UpdateExercise(exerciseMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating exercise");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{exerciseId}")]
        public async Task<IActionResult> DeleteExercise (int exerciseId)
        {
            if (!await _exerciseService.ExerciseExistsAsync(exerciseId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var exercise = await _exerciseService.GetExercise(exerciseId);

            exercise.IsDeleted = true;

            if (!await _exerciseService.UpdateExercise(exercise))
            {
                ModelState.AddModelError("", "Something went wrong while updating exercise");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
