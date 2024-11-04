using AutoMapper;
using FitYouFood.API.Dtos.IngredientAmountDtos;
using FitYouFood.API.Dtos.Meal;
using FitYouFood.API.Services;
using FitYouFood.API.Services.Interfaces;
using FitYouFood.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitYouFood.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MealController(IMealService _mealService, IIngredientService _ingredientService, IIngredientAmountService _ingredientAmountService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetMeals()
        {
            var meals = _mapper.Map<List<MealDto>>(await _mealService.GetMeals());

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(meals);
        }

        [HttpGet("{mealId}")]
        public async Task<IActionResult> GetMeal(int mealId)
        {
            if(!await _mealService.MealExistsAsync(mealId))
                return NotFound();

            var meal = _mapper.Map<MealDto>(await _mealService.GetMeal(mealId));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(meal);
        }

        [HttpPost]
        public async Task<IActionResult> PostMeal([FromBody] MealCreateDto mealDto)
        {
            if (!ModelState.IsValid || mealDto == null)
                return BadRequest(ModelState);

            var mealMap = _mapper.Map<Meal>(mealDto);
            mealMap.Description = "";

            if(!await _mealService.CreateMeal(mealMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut]
        public async Task<IActionResult> PutMeal(int mealId, [FromBody] MealUpdateDto mealDto)
        {
            if (!ModelState.IsValid || mealDto == null)
                return BadRequest(ModelState);

            if (!await _mealService.MealExistsAsync(mealId))
                return NotFound();

            var existingMeal = await _mealService.GetMeal(mealId);
            if (existingMeal == null)
                return NotFound();

            _mapper.Map(mealDto, existingMeal);

            if (!await _mealService.UpdateMeal(existingMeal))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully updated");
        }


        [HttpPost("{mealId}/Ingredients")]
        public async Task<IActionResult> AddIngredient(int mealId, [FromBody] IngredientAmountDto ingredientAmountDto)
        {
            if (await _mealService.MealExistsAsync(mealId))
                return NotFound("Meal not found");

            if (await _ingredientService.IngredientExistsAsync(ingredientAmountDto.IngredientId))
                return NotFound("Ingredient not found");

            var ingredientAmount = _mapper.Map<IngredientAmount>(ingredientAmountDto);
            await _mealService.AddIngredientAmount(ingredientAmount);

            return Ok("Ingredient was added successfully");
        }

        [HttpPut("{mealId}/Ingredients/{ingredientId}")]
        public async Task<IActionResult> UpdateIngredient(int mealId, int ingredientId, [FromBody]IngredientAmountDto ingredientAmountDto)
        {
            if (!await _ingredientAmountService.IngredientAmountExistsAsync(mealId, ingredientId))
                return NotFound();

            var ingredientAmount = await _ingredientAmountService.GetIngredientAmount(mealId, ingredientId);

            _mapper.Map(ingredientAmountDto, ingredientAmount);

            await _mealService.UpdateIngredientAmount(ingredientAmount);

            return Ok("Ingredient was updated successfully");
        }
    }
}
