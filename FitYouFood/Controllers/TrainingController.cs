using AutoMapper;
using FitYouFood.API.Dtos.Training;
using FitYouFood.API.Services.Interfaces;
using FitYouFood.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitYouFood.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TrainingController(ITrainingService _trainingService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetUserTrainings(string userId)
        {
            var trainings = _mapper.Map<List<TrainingDto>>(await _trainingService.GetTrainings(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(trainings);
        }

        [HttpGet("{trainingId")]
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
            //TODO: change all controllers validation to single 'if' if possible
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
    }
}
