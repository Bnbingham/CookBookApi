namespace CookBookApi.Application.Recipes;

using System.Threading.Tasks;
using CookBookApi.Application.RecipeLineItems.Entities;
using Entities;

public interface IRecipesRepository
{
    public Task<List<Recipe>> GetRecipes(CancellationToken cancellationToken);
    public Task<Recipe> GetRecipeById(Guid id, CancellationToken cancellationToken);
    public Task<bool> RecipeExists(Guid id, CancellationToken cancellationToken);
    public Task<Recipe> CreateRecipe(string title, string description, string instructions, Guid authorId, List<RecipeLineItem> recipeLineItems, List<Guid> cookBookIds, CancellationToken cancellationToken);
    public Task<bool> UpdateRecipe(Guid id, string title, string description, string instructions, List<RecipeLineItem> recipeLineItems, CancellationToken cancellationToken);
    public Task<bool> DeleteRecipe(Guid id, CancellationToken cancellationToken);
}
