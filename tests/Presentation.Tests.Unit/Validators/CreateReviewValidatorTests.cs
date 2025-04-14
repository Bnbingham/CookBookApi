namespace CookBookApi.Presentation.Tests.Unit.Validators;

using FluentValidation.TestHelper;
using Presentation.Requests;
using Presentation.Validators;
using Xunit;

public class CreateReviewValidatorTests
{
    private static readonly CreateIngredientValidator Validator = new();

    [Theory]
    [InlineData("Test", "Test")]
    public void Validator_ShouldNotHaveValidationErrorFor_ValidRequest(string name, string description)
    {
        // Arrange
        var command = new CreateIngredientRequest
        {
            Name = name,
            Description = description
        };

        // Act
        var result = Validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(request => request.Name);
        result.ShouldNotHaveValidationErrorFor(request => request.Description);
    }

    [Fact]
    public void Validator_ShouldHaveValidationErrorFor_IngredientName()
    {
        // Arrange
        var command = new CreateIngredientRequest
        {
            Name = "",
            Description = "Test"
        };

        // Act
        var result = Validator.TestValidate(command);

        // Assert
        _ = result.ShouldHaveValidationErrorFor(command => command.Name)
            .WithErrorMessage("A name was not supplied to create the ingredient.");

        result.ShouldNotHaveValidationErrorFor(request => request.Description);
    }

    [Fact]
    public void Validator_ShouldHaveValidationErrorFor_IngredientDescription()
    {
        // Arrange
        var command = new CreateIngredientRequest
        {
            Name = "Test",
            Description = ""
        };

        // Act
        var result = Validator.TestValidate(command);

        // Assert
        _ = result.ShouldHaveValidationErrorFor(command => command.Description)
            .WithErrorMessage("A description was not supplied to create the ingredient.");

        result.ShouldNotHaveValidationErrorFor(request => request.Name);
    }


}
