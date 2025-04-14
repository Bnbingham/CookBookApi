namespace CookBookApi.Application.Ingredients;

using System.Threading.Tasks;
using Entities;

public interface IIngredientsRepository
{
    public Task<List<Ingredient>> GetIngredients(CancellationToken cancellationToken);
    public Task<Ingredient> CreateIngredient(string name, string description, CancellationToken cancellationToken);
    public Task<bool> UpdateIngredient(Guid id, string name, string description, CancellationToken cancellationToken);
    public Task<bool> DeleteIngredient(Guid id, CancellationToken cancellationToken);
    public Task<bool> IngredientExists(Guid id, CancellationToken cancellationToken);
}
