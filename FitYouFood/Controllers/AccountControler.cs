using AutoMapper;
using FitYouFood.API.Dtos.Account;
using FitYouFood.API.Dtos.Meal;
using FitYouFood.API.Services;
using FitYouFood.API.Services.Interfaces;
using FitYouFood.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FitYouFood.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountControler(UserManager<User> _userManager,
        IMapper _mapper,
        ITokenService _tokenService,
        IUserService _userService,
        IMealService _mealService,
        SignInManager<User> singInManager) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName.ToLower());

            if (user == null) return Unauthorized("Invalid username!");

            var result = await singInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Username not found or Password was incrorect");

            return Ok(
                new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                }
            );
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = new User()
                {
                    UserName = registerDto.UserName,
                    Email = registerDto.Email,
                };

                var createdUser = await _userManager.CreateAsync(user, registerDto.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto
                            {
                                UserName = user.UserName,
                                Email = user.Email,
                                Token = _tokenService.CreateToken(user)
                            });
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized("User ID claim not found in the token.");

            var user = await _userService.GetUserByIdAsync(userId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userMap = _mapper.Map<UserUpdateDto>(user);

            return Ok(userMap);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateUserData([FromBody] UserUpdateDto userDto)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized("User ID claim not found in the token.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Retrieve the existing user from the database
            var existingUser = await _userService.GetUserByIdAsync(userId);

            if (existingUser == null)
                return NotFound("User not found");

            // Map only the updated properties from userDto to existingUser
            _mapper.Map(userDto, existingUser);

            // Update the user in the database
            var (updateSucceeded, errors) = await _userService.UpdateUser(existingUser);
            if (!updateSucceeded)
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully updated");
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteAccount()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized("User ID claim not found in the token.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingUser = await _userService.GetUserByIdAsync(userId);

            if (existingUser == null)
                return NotFound("User not found");

            existingUser.Email = $"{existingUser.Id}@email.com";
            existingUser.NormalizedEmail = $"{existingUser.Id}@email.com".ToUpper();
            existingUser.UserName = $"{existingUser.Id}";
            existingUser.NormalizedUserName = $"{existingUser.Id}".ToUpper();
            existingUser.PasswordHash = $"{existingUser.Id}";

            var (updateSucceeded, errors) = await _userService.UpdateUser(existingUser);
            if (!updateSucceeded)
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully deleted");
        }
    }
}
