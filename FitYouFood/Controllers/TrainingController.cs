using AutoMapper;
using FitYouFood.API.Dtos.ExerciseDataDtos;
using FitYouFood.API.Dtos.Training;
using FitYouFood.API.Services;
using FitYouFood.API.Services.Interfaces;
using FitYouFood.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitYouFood.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TrainingController(ITrainingService _trainingService, IExerciseDataService _exerciseDataService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetUserTrainings(string userId)
        {
            var trainings = _mapper.Map<List<TrainingDto>>(await _trainingService.GetTrainings(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(trainings);
        }

        [HttpGet("{trainingId}")]
        public async Task<IActionResult> GetTraining(int trainingId)
        {
            if (!await _trainingService.TrainingExistsAsync(trainingId))
                return NotFound();

            var training = _mapper.Map<TrainingDto>(await _trainingService.GetTraining(trainingId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(training);
        }

        [HttpPost]
        public async Task<IActionResult> PostTraining([FromBody]TrainingCreateDto trainingDto)
        {
            if(trainingDto == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var trainingMap = _mapper.Map<Training>(trainingDto);

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
            if(trainingDto == null || !ModelState.IsValid)
                return BadRequest(ModelState);
            if (!await _trainingService.TrainingExistsAsync(trainingId))
                return NotFound();

            var trainingMap = _mapper.Map<Training>(trainingDto);

            if(!await _trainingService.CreateTraining(trainingMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully updated");
        }

        [HttpDelete("{trainingId}")]
        public async Task<IActionResult> DeleteTraining(int trainingId)
        {
            if (!await _trainingService.TrainingExistsAsync(trainingId))
                return NotFound();
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var training = await _trainingService.GetTraining(trainingId);
            training.IsDeleted = true;

            if(!await _trainingService.UpdateTraining(training))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpPost("{trainingId}/Exercise")]
        public async Task<IActionResult> PostExercise(int trainingId, int exerciseDataId)
        {
            if (!await _trainingService.TrainingExistsAsync(trainingId) && !await _exerciseDataService.ExerciseDataExistsAsync(exerciseDataId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var exerciseData = await _exerciseDataService.GetExerciseData(exerciseDataId);


            exerciseData.TrainingId = trainingId;

            if (!await _exerciseDataService.UpdateExerciseData(exerciseData))
            {
                ModelState.AddModelError("", "Something went wrong while adding Exercise");
                return StatusCode(500, ModelState);
            }
            return Ok($"Successfully added exercise to training");
        }

        [HttpDelete("{trainingId}/Exercise/{exerciseDataId}")]
        public async Task<IActionResult> DeleteExercise(int trainingId, int exerciseDataId)
        {
            if (!await _trainingService.TrainingExistsAsync(trainingId) && !await _exerciseDataService.ExerciseDataExistsAsync(exerciseDataId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var exerciseData = await _exerciseDataService.GetExerciseData(exerciseDataId);

            exerciseData.TrainingId = null;

            if (!await _exerciseDataService.UpdateExerciseData(exerciseData))
            {
                ModelState.AddModelError("", "Something went wrong while deleting Exercise");
                return StatusCode(500, ModelState);
            }
            return Ok($"Successfully deleted exercise from training");
        }
    }
}
