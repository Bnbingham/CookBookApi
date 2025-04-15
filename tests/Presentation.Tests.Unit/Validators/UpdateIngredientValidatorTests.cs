namespace CookBookApi.Presentation.Tests.Unit.Validators;

using FluentValidation.TestHelper;
using Presentation.Requests;
using Presentation.Validators;
using Xunit;

public class UpdateReviewValidatorTests
{
    private static readonly UpdateIngredientValidator Validator = new();

    [Fact]
    public void Validator_ShouldNotHaveValidationErrorFor_IngredientName()
    {
        // Arrange
        var request = new UpdateIngredientRequest
        {
            Name = "Test",
            Description = "Test"
        };

        // Act
        var result = Validator.TestValidate(request);

        // Assert
        result.ShouldNotHaveValidationErrorFor(request => request.Name);
        result.ShouldNotHaveValidationErrorFor(request => request.Description);
    }

    [Fact]
    public void Validator_ShouldHaveValidationErrorFor_ReviewAuthorId()
    {
        // Arrange
        var request = new UpdateIngredientRequest
        {
            Name = "",
            Description = "Test"
        };

        // Act
        var result = Validator.TestValidate(request);

        // Assert
        _ = result.ShouldHaveValidationErrorFor(request => request.Name)
            .WithErrorMessage("A name was not supplied to Update the ingredient.");

        result.ShouldNotHaveValidationErrorFor(request => request.Description);
    }

    [Fact]
    public void Validator_ShouldHaveValidationErrorFor_ReviewedMovieId()
    {
        // Arrange
        var request = new UpdateIngredientRequest
        {
            Name = "Test",
            Description = ""
        };

        // Act
        var result = Validator.TestValidate(request);

        // Assert
        _ = result.ShouldHaveValidationErrorFor(request => request.Description)
            .WithErrorMessage("A description was not supplied to Update the ingredient.");

        result.ShouldNotHaveValidationErrorFor(request => request.Name);
    }


}
