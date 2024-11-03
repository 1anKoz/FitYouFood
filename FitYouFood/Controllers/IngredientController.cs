using AutoMapper;
using FitYouFood.API.Dtos.Ingredient;
using FitYouFood.API.Services.Interfaces;
using FitYouFood.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitYouFood.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IngredientController(IIngredientService _ingredientService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetIngredients()
        {
            var ingredients = _mapper.Map<List<IngredientDto>>(await _ingredientService.GetIngredients());

            if (!ModelState.IsValid)
                return BadRequest(ingredients);

            return Ok(ingredients);
        }

        [HttpGet("{ingredientId}")]
        public async Task<IActionResult> GetIngredient(int ingredientId)
        {
            if(!await _ingredientService.IngredientExistsAsync(ingredientId))
                return NotFound();

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var ingredient = _mapper.Map<IngredientDto>(await _ingredientService.GetIngredient(ingredientId));

            return Ok(ingredient);
        }

        [HttpPost]
        public async Task<IActionResult> PostIngredient([FromBody] IngredientDto ingredientDto)
        {
            if(ingredientDto == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var ingredientMap = _mapper.Map<Ingredient>(ingredientDto);

            if(!await _ingredientService.CreateIngredient(ingredientMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created");
        }

        [HttpPut("{ingredientId}")]
        public async Task<IActionResult> PutIngredient(int ingredientId, [FromBody] IngredientDto ingredientDto)
        {
            if(ingredientDto == null || ingredientId != ingredientDto.Id || !ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _ingredientService.IngredientExistsAsync(ingredientId))
                return NotFound();

            var ingredientMap = _mapper.Map<Ingredient>(ingredientDto);

            if(!await _ingredientService.UpdateIngredient(ingredientMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating exercise");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{ingredientId}")]
        public async Task<IActionResult> DeleteIngredient(int ingredientId)
        {
            if (!await _ingredientService.IngredientExistsAsync(ingredientId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ingredient = await _ingredientService.GetIngredient(ingredientId);

            ingredient.IsDeleted = true;

            if(!await _ingredientService.UpdateIngredient(ingredient))
            {
                ModelState.AddModelError("", "Something went wrong while updating exercise");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
