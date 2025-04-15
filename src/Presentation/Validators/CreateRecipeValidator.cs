namespace CookBookApi.Presentation.Validators;

using CookBookApi.Presentation.Requests;
using FluentValidation;

public class CreateRecipeValidator : AbstractValidator<CreateRecipeRequest>
{
    public CreateRecipeValidator()
    {
        _ = this.RuleFor(r => r.Title).NotEmpty().WithMessage("A title was not provided.");
        _ = this.RuleFor(r => r.Instructions).NotEmpty().WithMessage("Instructions were not provided.");
        _ = this.RuleFor(r => r.Description).NotEmpty().WithMessage("A description was not provided.");
        _ = this.RuleFor(r => r.RecipeLineItems).NotEmpty().WithMessage("Recipe line items were not provided.");
        _ = this.RuleFor(r => r.RecipeLineItems).Must(r => r.Count > 0).WithMessage("Recipe line items were not provided.");
        _ = this.RuleFor(r => r.RecipeLineItems).Must(r => r.All(i => i.IngredientId != Guid.Empty)).WithMessage("Ingredient ID was not provided.");
        _ = this.RuleFor(r => r.RecipeLineItems).Must(r => r.All(i => i.Quantity > 0)).WithMessage("Quantity was not provided.");
        _ = this.RuleFor(r => r.RecipeLineItems).Must(r => r.All(i => i.UnitOfMeasurement != null)).WithMessage("Unit of measure was not provided.");

    }
}
