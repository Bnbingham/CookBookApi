namespace CookBookApi.Presentation.Validators;

using CookBookApi.Presentation.Requests;
using FluentValidation;

public class UpdateIngredientValidator : AbstractValidator<UpdateIngredientRequest>
{
    public UpdateIngredientValidator()
    {
        _ = this.RuleFor(r => r.Name).NotEmpty().WithMessage("A name was not supplied to Update the ingredient.");
        _ = this.RuleFor(r => r.Description).NotEmpty().WithMessage("A description was not supplied to Update the ingredient.");
    }
}
