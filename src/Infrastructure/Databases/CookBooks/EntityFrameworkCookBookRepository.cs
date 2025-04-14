namespace CookBookApi.Infrastructure.Databases.CookBooks;

using System;
using Application.Authors;
// using Application.Common.Enums;
// using Application.Common.Exceptions;
using Application.Ingredients;
using Application.Recipes;
using AutoMapper;
using Extensions;
using Microsoft.EntityFrameworkCore;
using Models;
using ApplicationAuthor = Application.Authors.Entities.Author;
using ApplicationRecipe = Application.Recipes.Entities.Recipe;
using ApplicationIngredient = Application.Ingredients.Entities.Ingredient;
using CookBookApi.Application.CookBooks;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using CookBookApi.Application.Common.Exceptions;
using CookBookApi.Application.Common.Enums;

internal class EntityFrameworkCookBookRepository : IAuthorsRepository, IRecipesRepository, ICookBookRepository, IIngredientsRepository
{
    private readonly CookBooksDbContext context;
    private readonly TimeProvider timeProvider;
    private readonly IMapper mapper;

    public EntityFrameworkCookBookRepository(
        CookBooksDbContext context,
        TimeProvider timeProvider,
        IMapper mapper)
    {
        this.context = context;
        this.timeProvider = timeProvider;
        this.mapper = mapper;

        if (this.context != null)
        {
            _ = this.context.Database.EnsureDeleted();
            _ = this.context.Database.EnsureCreated();
            _ = this.context.AddData();
        }
    }

    #region Authors
    public virtual async Task<List<ApplicationAuthor>> GetAuthors(CancellationToken cancellationToken)
    {
        var authors = await this.context.Authors
            .Include(a => a.Recipes)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return this.mapper.Map<List<ApplicationAuthor>>(authors);
    }

    public async Task<ApplicationAuthor> GetAuthorById(Guid id, CancellationToken cancellationToken)
    {
        var author = await this.context.Authors
            .Include(a => a.Recipes)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        return this.mapper.Map<ApplicationAuthor>(author);
    }

    public async Task<bool> AuthorExists(Guid id, CancellationToken cancellationToken)
    {
        return await this.context.Authors.AsNoTracking().AnyAsync(a => a.Id == id, cancellationToken);
    }
    #endregion Authors

    #region CookBooks

    public async Task<List<Application.CookBooks.Entities.CookBook>> GetCookBooks(CancellationToken cancellationToken)
    {
        var cookBooks = await this.context.CookBooks
            .Include(c => c.Recipes)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return this.mapper.Map<List<Application.CookBooks.Entities.CookBook>>(cookBooks);
    }

    public async Task<Application.CookBooks.Entities.CookBook> GetCookBookById(Guid id, CancellationToken cancellationToken)
    {
        var cookBook = await this.context.CookBooks
            .Include(c => c.Recipes)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        return this.mapper.Map<Application.CookBooks.Entities.CookBook>(cookBook);
    }

    public async Task<bool> AddRecipeToCookBook(Guid cookBookId, Guid recipeId, CancellationToken cancellationToken)
    {
        try
        {
            var cookBook = this.context.CookBooks.FirstOrDefault(c => c.Id == cookBookId);
            var recipe = this.context.Recipes.FirstOrDefault(r => r.Id == recipeId);

            NotFoundException.ThrowIfNull(cookBook, EntityType.CookBook);
            NotFoundException.ThrowIfNull(recipe, EntityType.Recipe);

            cookBook.Recipes.Add(recipe);
            cookBook.DateModified = this.timeProvider.GetUtcNow().UtcDateTime;

            _ = this.context.Update(cookBook);
            _ = await this.context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> CookBookExists(Guid id, CancellationToken cancellationToken)
    {
        return await this.context.CookBooks.AsNoTracking().AnyAsync(c => c.Id == id, cancellationToken);
    }
    #endregion CookBooks

    #region Ingredients

    public async Task<bool> IngredientExists(Guid id, CancellationToken cancellationToken)
    {
        return await this.context.Ingredients.AsNoTracking().AnyAsync(i => i.Id == id, cancellationToken);
    }

    public async Task<List<ApplicationIngredient>> GetIngredients(CancellationToken cancellationToken)
    {
        var ingredients = await this.context.Ingredients
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return this.mapper.Map<List<ApplicationIngredient>>(ingredients);
    }

    public async Task<ApplicationIngredient> GetIngredientById(Guid id, CancellationToken cancellationToken)
    {
        var ingredient = await this.context.Ingredients
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);

        return this.mapper.Map<ApplicationIngredient>(ingredient);
    }

    public async Task<ApplicationIngredient> CreateIngredient(string name, string description, CancellationToken cancellationToken)
    {
        var ingredientEntity = new Ingredient
        {
            Name = name,
            Description = description,
            DateCreated = this.timeProvider.GetUtcNow().UtcDateTime,
            DateModified = this.timeProvider.GetUtcNow().UtcDateTime
        };

        var id = this.context.Add(ingredientEntity).Entity.Id;

        _ = await this.context.SaveChangesAsync(cancellationToken);

        return await this.GetIngredientById(id, cancellationToken);
    }

    public async Task<bool> UpdateIngredient(Guid id, string name, string description, CancellationToken cancellationToken)
    {
        try
        {
            var ingredient = this.context.Ingredients.FirstOrDefault(i => i.Id == id);

            NotFoundException.ThrowIfNull(ingredient, EntityType.Ingredient);

            ingredient.Name = name;
            ingredient.Description = description;
            ingredient.DateModified = this.timeProvider.GetUtcNow().UtcDateTime;

            _ = this.context.Update(ingredient);
            _ = await this.context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> DeleteIngredient(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            _ = this.context.Remove(this.context.Ingredients.Single(i => i.Id == id));
            _ = await this.context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    #endregion Ingredients

    #region Recipes

    public async Task<List<ApplicationRecipe>> GetRecipes(CancellationToken cancellationToken)
    {
        var recipes = await this.context.Recipes
            .Include(r => r.RecipeLineItems)
            .ThenInclude(rli => rli.Ingredient)
            .Include(r => r.Author)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return this.mapper.Map<List<ApplicationRecipe>>(recipes);
    }

    public async Task<ApplicationRecipe> GetRecipeById(Guid id, CancellationToken cancellationToken)
    {
        var recipe = await this.context.Recipes
            .Include(r => r.RecipeLineItems)
            .ThenInclude(rli => rli.Ingredient)
            .Include(r => r.Author)
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

        return this.mapper.Map<ApplicationRecipe>(recipe);
    }

    public async Task<ApplicationRecipe> CreateRecipe(string title, string description, string instructions, Guid authorId, List<Application.RecipeLineItems.Entities.RecipeLineItem> recipeLineItems, List<Guid> cookBookIds, CancellationToken cancellationToken)
    {
        var recipe = new Recipe
        {
            Title = title,
            Description = description,
            Instructions = instructions,
            AuthorId = authorId,
            RecipeLineItems = [.. recipeLineItems.Select(rli => new RecipeLineItem
            {
                IngredientId = rli.Ingredient.Id,
                Quantity = rli.Quantity,
                UnitOfMeasurement = rli.UnitOfMeasurement
            })],
            DateCreated = this.timeProvider.GetUtcNow().UtcDateTime,
            DateModified = this.timeProvider.GetUtcNow().UtcDateTime
        };

        var id = this.context.Add(recipe).Entity.Id;

        _ = await this.context.SaveChangesAsync(cancellationToken);

        return await this.GetRecipeById(id, cancellationToken);
    }

    public async Task<bool> UpdateRecipe(Guid id, string title, string description, string instructions, List<Application.RecipeLineItems.Entities.RecipeLineItem> recipeLineItems, CancellationToken cancellationToken)
    {
        try
        {
            var recipe = this.context.Recipes.FirstOrDefault(r => r.Id == id);

            NotFoundException.ThrowIfNull(recipe, EntityType.Recipe);

            var lineItems = this.context.RecipeLineItems.Where(rli => rli.Id == id).ToList();
            var missingLineItems = recipeLineItems.Where(rli => !lineItems.Any(rli2 => rli2.Id == rli.Id)).ToList();

            foreach (var lineItem in missingLineItems)
            {
                var ingredient = this.context.Ingredients.FirstOrDefault(i => i.Id == lineItem.Ingredient.Id);

                NotFoundException.ThrowIfNull(ingredient, EntityType.Ingredient);

                var newLineItem = new RecipeLineItem
                {
                    IngredientId = ingredient.Id,
                    Quantity = lineItem.Quantity,
                    UnitOfMeasurement = lineItem.UnitOfMeasurement
                };

                this.context.RecipeLineItems.Add(newLineItem);
            }

            foreach (var lineItem in lineItems)
            {
                lineItem.Quantity = recipeLineItems.First(rli => rli.Id == lineItem.Id).Quantity;
                lineItem.UnitOfMeasurement = recipeLineItems.First(rli => rli.Id == lineItem.Id).UnitOfMeasurement;
            }

            recipe.RecipeLineItems = lineItems;

            recipe.Title = title;
            recipe.Description = description;
            recipe.Instructions = instructions;
            recipe.DateModified = this.timeProvider.GetUtcNow().UtcDateTime;

            _ = this.context.Update(recipe);
            _ = await this.context.SaveChangesAsync(cancellationToken);

        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }


    public async Task<bool> RecipeExists(Guid id, CancellationToken cancellationToken)
    {
        return await this.context.Recipes.AsNoTracking().AnyAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<bool> DeleteRecipe(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            _ = this.context.Remove(this.context.Recipes.Single(r => r.Id == id));
            _ = await this.context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }

    #endregion Recipes
}
