using AutoMapper;
using FitYouFood.API.Dtos.IngredientAmountDtos;
using FitYouFood.API.Dtos.Meal;
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
    public class MealController(IMealService _mealService,
        IIngredientService _ingredientService,
        IIngredientAmountService _ingredientAmountService,
        IMapper _mapper, IUserService _userService
        ) : ControllerBase
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
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized("User ID claim not found in the token.");

            if (!await _userService.IsAdmin(userId))
                return Unauthorized("Only admins can add meals");

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
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized("User ID claim not found in the token.");

            if (!await _userService.IsAdmin(userId))
                return Unauthorized("Only admins can edit meals");

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
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized("User ID claim not found in the token.");

            if (!await _userService.IsAdmin(userId))
                return Unauthorized("Only admins can add ingredients to a meal");

            if (!await _mealService.MealExistsAsync(mealId))
                return NotFound("Meal not found");

            if (!await _ingredientService.IngredientExistsAsync(ingredientAmountDto.IngredientId))
                return NotFound("Ingredient not found");

            var ingredientAmount = _mapper.Map<IngredientAmount>(ingredientAmountDto);
            await _mealService.AddIngredientAmount(ingredientAmount);

            return Ok("Ingredient was added successfully");
        }

        [HttpPut("{mealId}/Ingredients/{ingredientId}")]
        public async Task<IActionResult> UpdateIngredient(int mealId, int ingredientId, [FromBody]IngredientAmountDto ingredientAmountDto)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized("User ID claim not found in the token.");

            if (!await _userService.IsAdmin(userId))
                return Unauthorized("Only admins can edit ingredients in a meal");

            if (!await _ingredientAmountService.IngredientAmountExistsAsync(mealId, ingredientId))
                return NotFound();

            var ingredientAmount = await _ingredientAmountService.GetIngredientAmount(mealId, ingredientId);

            _mapper.Map(ingredientAmountDto, ingredientAmount);

            await _mealService.UpdateIngredientAmount(ingredientAmount);

            return Ok("Ingredient was updated successfully");
        }

        [HttpDelete("{mealId}/Ingredients/{ingredientId}")]
        public async Task<IActionResult> DeleteIngredient(int mealId, int ingredientId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized("User ID claim not found in the token.");

            if (!await _userService.IsAdmin(userId))
                return Unauthorized("Only admins can delete ingredients from a meal");

            if (!await _ingredientAmountService.IngredientAmountExistsAsync(mealId, ingredientId))
                return NotFound();

            var ingredientAmount = await _ingredientAmountService.GetIngredientAmount(mealId, ingredientId);

            await _mealService.DeleteIngredientAmount(ingredientAmount);

            return Ok("Deleted succesfully");
        }


        [HttpPost("/AddUserMeal/{mealId}")]
        public async Task<IActionResult> AddUserMeal(int mealId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized("User ID claim not found in the token.");

            var result = await _userService.AddMeal(userId, mealId);
            if (!result)
                return BadRequest("Could not add meal to user.");

            return Ok("Meal added successfully.");
        }

        [HttpDelete("/RemoveUserMeal/{mealId}")]
        public async Task<IActionResult> RemoveUserMeal(int mealId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized("User ID claim not found in the token.");

            var result = await _userService.RemoveMeal(userId, mealId);
            if (!result)
                return BadRequest("Could not remove meal from user.");

            return Ok("Meal removed successfully.");
        }

        [HttpGet("/UserMeals")]
        public async Task<IActionResult> GetUserMeals()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if(userId == null)
                return Unauthorized("User ID claim not found in the token.");

            var meals = _mapper.Map<ICollection<MealDto>>(await _userService.GetMeals(userId));
            if (meals == null || meals.Count == 0)
                return NotFound("No meals found for the user.");

            return Ok(meals);
        }
    }
}
