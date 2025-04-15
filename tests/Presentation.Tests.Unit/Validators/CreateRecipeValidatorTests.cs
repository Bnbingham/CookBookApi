namespace CookBookApi.Presentation.Tests.Unit.Validators;

using FluentValidation.TestHelper;
using Presentation.Requests;
using Presentation.Validators;
using Xunit;
using System.Collections.Generic;
using System;

public class CreateRecipeValidatorTests
{
    private static readonly CreateRecipeValidator Validator = new();

    [Fact]
    public void Validator_ShouldNotHaveValidationErrorFor_ValidRequest()
    {
        // Arrange
        var request = new CreateRecipeRequest
        {
            Title = "Test Recipe",
            Description = "Test Description",
            Instructions = "Test Instructions",
            RecipeLineItems = new List<CreateRecipeLineItemRequest>
            {
                new()
                {
                    IngredientId = Guid.NewGuid(),
                    Quantity = 1,
                    UnitOfMeasurement = "cup"
                }
            }
        };

        // Act
        var result = Validator.TestValidate(request);

        // Assert
        result.ShouldNotHaveValidationErrorFor(r => r.Title);
        result.ShouldNotHaveValidationErrorFor(r => r.Description);
        result.ShouldNotHaveValidationErrorFor(r => r.Instructions);
        result.ShouldNotHaveValidationErrorFor(r => r.RecipeLineItems);
    }

    [Fact]
    public void Validator_ShouldHaveValidationErrorFor_EmptyTitle()
    {
        // Arrange
        var request = new CreateRecipeRequest
        {
            Title = "",
            Description = "Test Description",
            Instructions = "Test Instructions",
            RecipeLineItems = new List<CreateRecipeLineItemRequest>
            {
                new()
                {
                    IngredientId = Guid.NewGuid(),
                    Quantity = 1,
                    UnitOfMeasurement = "cup"
                }
            }
        };

        // Act
        var result = Validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(r => r.Title)
            .WithErrorMessage("A title was not provided.");
    }

    [Fact]
    public void Validator_ShouldHaveValidationErrorFor_EmptyDescription()
    {
        // Arrange
        var request = new CreateRecipeRequest
        {
            Title = "Test Recipe",
            Description = "",
            Instructions = "Test Instructions",
            RecipeLineItems = new List<CreateRecipeLineItemRequest>
            {
                new()
                {
                    IngredientId = Guid.NewGuid(),
                    Quantity = 1,
                    UnitOfMeasurement = "cup"
                }
            }
        };

        // Act
        var result = Validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(r => r.Description)
            .WithErrorMessage("A description was not provided.");
    }

    [Fact]
    public void Validator_ShouldHaveValidationErrorFor_EmptyInstructions()
    {
        // Arrange
        var request = new CreateRecipeRequest
        {
            Title = "Test Recipe",
            Description = "Test Description",
            Instructions = "",
            RecipeLineItems = new List<CreateRecipeLineItemRequest>
            {
                new()
                {
                    IngredientId = Guid.NewGuid(),
                    Quantity = 1,
                    UnitOfMeasurement = "cup"
                }
            }
        };

        // Act
        var result = Validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(r => r.Instructions)
            .WithErrorMessage("Instructions were not provided.");
    }

    [Fact]
    public void Validator_ShouldHaveValidationErrorFor_EmptyRecipeLineItems()
    {
        // Arrange
        var request = new CreateRecipeRequest
        {
            Title = "Test Recipe",
            Description = "Test Description",
            Instructions = "Test Instructions",
            RecipeLineItems = new List<CreateRecipeLineItemRequest>()
        };

        // Act
        var result = Validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(r => r.RecipeLineItems)
            .WithErrorMessage("Recipe line items were not provided.");
    }

    [Fact]
    public void Validator_ShouldHaveValidationErrorFor_EmptyIngredientId()
    {
        // Arrange
        var request = new CreateRecipeRequest
        {
            Title = "Test Recipe",
            Description = "Test Description",
            Instructions = "Test Instructions",
            RecipeLineItems = new List<CreateRecipeLineItemRequest>
            {
                new()
                {
                    IngredientId = Guid.Empty,
                    Quantity = 1,
                    UnitOfMeasurement = "cup"
                }
            }
        };

        // Act
        var result = Validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(r => r.RecipeLineItems)
            .WithErrorMessage("Ingredient ID was not provided.");
    }

    [Fact]
    public void Validator_ShouldHaveValidationErrorFor_InvalidQuantity()
    {
        // Arrange
        var request = new CreateRecipeRequest
        {
            Title = "Test Recipe",
            Description = "Test Description",
            Instructions = "Test Instructions",
            RecipeLineItems = new List<CreateRecipeLineItemRequest>
            {
                new()
                {
                    IngredientId = Guid.NewGuid(),
                    Quantity = 0,
                    UnitOfMeasurement = "cup"
                }
            }
        };

        // Act
        var result = Validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(r => r.RecipeLineItems)
            .WithErrorMessage("Quantity was not provided.");
    }

    [Fact]
    public void Validator_ShouldHaveValidationErrorFor_EmptyUnitOfMeasurement()
    {
        // Arrange
        var request = new CreateRecipeRequest
        {
            Title = "Test Recipe",
            Description = "Test Description",
            Instructions = "Test Instructions",
            RecipeLineItems = new List<CreateRecipeLineItemRequest>
            {
                new()
                {
                    IngredientId = Guid.NewGuid(),
                    Quantity = 1,
                    UnitOfMeasurement = null
                }
            }
        };

        // Act
        var result = Validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(r => r.RecipeLineItems)
            .WithErrorMessage("Unit of measure was not provided.");
    }
}
