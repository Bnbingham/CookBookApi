
namespace CookBookApi.Presentation.Requests;

public class UpdateRecipeRequest
{
    public string Title { get; init; }

    public string Description { get; init; }

    public string Instructions { get; init; }

    public Guid AuthorId { get; init; }

    public List<UpdateRecipeLineItemRequest> RecipeLineItems { get; init; }

    public List<Guid> CookBookIds { get; init; }
}

public class UpdateRecipeLineItemRequest
{
    public Guid IngredientId { get; init; }

    public string IngredientName { get; init; }

    public decimal Quantity { get; init; }

    public string UnitOfMeasurement { get; init; }
}
