namespace CookBookApi.Presentation.Endpoints;

using CookBookApi.Application.Common.Exceptions;
using CookBookApi.Presentation.Filters;
using CookBookApi.Presentation.Requests;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities = Application.Recipes.Entities;
using Queries = Application.Recipes.Queries;
using Commands = Application.Recipes.Commands;
using CookBookApi.Application.RecipeLineItems.Entities;
using CookBookApi.Application.Ingredients.Entities;
using CookBookApi.Application.Common.Enums;

public static class RecipesEndpoints
{
    public static WebApplication MapRecipesEndpoints(this WebApplication app)
    {
        var root = app.MapGroup("/api/recipe")
            .AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory)
            .WithTags("recipe")
            .WithDescription("Lookup and Find Recipes")
            .WithOpenApi();

        _ = root.MapGet("/", GetRecipes)
            .Produces<List<Entities.Recipe>>()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Lookup all Recipes")
            .WithDescription("\n    GET /recipe");

        _ = root.MapGet("/{id}", GetRecipeById)
            .Produces<Entities.Recipe>()
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Lookup an Recipe by their Id")
            .WithDescription("\n    GET /recipe/00000000-0000-0000-0000-000000000000");

        _ = root.MapPost("/", CreateRecipe)
            .Produces<Entities.Recipe>()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Create a new Recipe")
            .WithDescription("\n    POST /recipe");

        _ = root.MapPut("/{id}", UpdateRecipe)
            .Produces<Entities.Recipe>()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Update an Recipe")
            .WithDescription("\n    PUT /recipe/00000000-0000-0000-0000-000000000000");


        _ = root.MapDelete("/{id}", DeleteRecipe)
            .Produces<Entities.Recipe>()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Delete an Recipe")
            .WithDescription("\n    DELETE /recipe/00000000-0000-0000-0000-000000000000");

        return app;
    }

    public static async Task<IResult> GetRecipes([FromServices] IMediator mediator)
    {
        try
        {
            return Results.Ok(await mediator.Send(new Queries.GetRecipes.GetRecipesQuery()));
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
        }
    }

    public static async Task<IResult> GetRecipeById([Validate][FromRoute] Guid id, [FromServices] IMediator mediator)
    {
        try
        {
            return Results.Ok(await mediator.Send(new Queries.GetRecipeById.GetRecipeByIdQuery
            {
                Id = id
            }));
        }
        catch (NotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
        }
    }

    public static async Task<IResult> CreateRecipe([Validate][FromBody] CreateRecipeRequest request, [FromServices] IMediator mediator)
    {
        try
        {
            var response = await mediator.Send(new Commands.CreateRecipe.CreateRecipeCommand
            {
                Title = request.Title,
                Description = request.Description,
                Instructions = request.Instructions,
                AuthorId = request.AuthorId,
                RecipeLineItems = [.. request.RecipeLineItems.Select(rli => new RecipeLineItem(Guid.NewGuid(), new Ingredient(rli.IngredientId, rli.IngredientName, null), rli.Quantity, Enum.Parse<UnitOfMeasurementType>(rli.UnitOfMeasurement)))],
                CookBookIds = request.CookBookIds
            });

            return Results.Created($"/api/recipe/{response.Id}", response);
        }
        catch (NotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
        }
    }

    public static async Task<IResult> UpdateRecipe([Validate][FromRoute] Guid id, [Validate][FromBody] UpdateRecipeRequest request, [FromServices] IMediator mediator)
    {
        try
        {
            _ = await mediator.Send(new Commands.UpdateRecipe.UpdateRecipeCommand
            {
                Id = id,
                Title = request.Title,
                Description = request.Description,
                Instructions = request.Instructions,
                RecipeLineItems = [.. request.RecipeLineItems.Select(rli => new RecipeLineItem(rli.IngredientId, new Ingredient(rli.IngredientId, rli.IngredientName, null), rli.Quantity, Enum.Parse<UnitOfMeasurementType>(rli.UnitOfMeasurement)))],
                CookBookIds = request.CookBookIds
            });

            return Results.NoContent();
        }
        catch (NotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
        }
    }

    public static async Task<IResult> DeleteRecipe([Validate][FromRoute] Guid id, [FromServices] IMediator mediator)
    {
        try
        {
            _ = await mediator.Send(new Commands.DeleteRecipe.DeleteRecipeCommand
            {
                Id = id,
            });

            return Results.NoContent();
        }
        catch (NotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
        }
    }
}
