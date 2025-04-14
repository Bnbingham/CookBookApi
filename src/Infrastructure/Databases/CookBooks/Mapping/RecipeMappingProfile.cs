namespace CookBookApi.Infrastructure.Databases.CookBooks.Mapping;

using AutoMapper;
using Application = Application.Recipes.Entities;
using ApplicationRecipeLineItems = Application.RecipeLineItems.Entities;
using ApplicationIngredients = Application.Ingredients.Entities;

using Infrastructure = Models;
using ApplicationRecipes = Application.Recipes.Entities;

internal class RecipeMappingProfile : Profile
{
    public RecipeMappingProfile()
    {
        _ = this.CreateMap<Application.Recipe, Infrastructure.Recipe>()
            .ForMember(d => d.DateCreated, o => o.Ignore())
            .ForMember(d => d.DateModified, o => o.Ignore())
            .ForMember(d => d.Author, o => o.Ignore())
            .ForMember(d => d.CookBooks, o => o.Ignore())
            .ForMember(d => d.RecipeLineItems, o => o.MapFrom(s => s.RecipeLineItems.Select(rli => new Infrastructure.RecipeLineItem
            {
                Id = rli.Id,
                IngredientId = rli.Ingredient.Id,
                Quantity = rli.Quantity,
                UnitOfMeasurement = rli.UnitOfMeasurement,
                Ingredient = new Infrastructure.Ingredient
                {
                    Id = rli.Ingredient.Id,
                    Name = rli.Ingredient.Name,
                    Description = rli.Ingredient.Description
                }
            })))
            .ReverseMap()
            .ConstructUsing((src, ctx) => new Application.Recipe(
                src.Id,
                src.Title,
                src.Description,
                src.Instructions,
                src.AuthorId,
                [.. src.RecipeLineItems.Select(rli => new ApplicationRecipeLineItems.RecipeLineItem(rli.Id, new ApplicationIngredients.Ingredient(rli.IngredientId, rli.Ingredient.Name, rli.Ingredient.Description), rli.Quantity, rli.UnitOfMeasurement))]));

        _ = this.CreateMap<Infrastructure.Recipe, ApplicationRecipes.AuthorRecipe>()
            .ConstructUsing((src, ctx) => new ApplicationRecipes.AuthorRecipe(src.Id, src.Title));

        _ = this.CreateMap<Infrastructure.Recipe, ApplicationRecipes.CookBookRecipe>()
            .ConstructUsing((src, ctx) => new ApplicationRecipes.CookBookRecipe(src.Id, src.Title));
    }
}
