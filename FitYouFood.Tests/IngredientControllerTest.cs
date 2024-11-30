using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FitYouFood.API.Controllers;
using FitYouFood.API.Dtos.IngredientDtos;
using FitYouFood.API.Services.Interfaces;
using FitYouFood.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace FitYouFood.Tests;

public class IngredientControllerTest
{
    private readonly Mock<IIngredientService> _mockIngredientService;
    private readonly Mock<IMapper> _mockMapper;
    private readonly IngredientController _controller;

    public IngredientControllerTest()
    {
        _mockIngredientService = new Mock<IIngredientService>();
        _mockMapper = new Mock<IMapper>();
        _controller = new IngredientController(_mockIngredientService.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task GetIngredients_ReturnsOkResult_WithListOfIngredients()
    {
        // Arrange
        var ingredientList = new List<Ingredient>
        {
            new Ingredient { Id = 1, Name = "Ingredient1", Protein = 10, Fat = 5, Carbs = 20, IsOfficial = true, IsDeleted = false },
            new Ingredient { Id = 2, Name = "Ingredient2", Protein = 15, Fat = 10, Carbs = 25, IsOfficial = true, IsDeleted = false }
        };
        var ingredientDtoList = new List<IngredientDto>
        {
            new IngredientDto { Id = 1, Name = "Ingredient1" },
            new IngredientDto { Id = 2, Name = "Ingredient2" }
        };

        _mockIngredientService.Setup(service => service.GetIngredients()).ReturnsAsync(ingredientList);
        _mockMapper.Setup(mapper => mapper.Map<List<IngredientDto>>(ingredientList)).Returns(ingredientDtoList);

        // Act
        var result = await _controller.GetIngredients();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<IngredientDto>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }
}