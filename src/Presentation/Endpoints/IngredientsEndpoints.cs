namespace CookBookApi.Presentation.Endpoints;

using CookBookApi.Application.Common.Exceptions;
using CookBookApi.Presentation.Filters;
using CookBookApi.Presentation.Requests;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities = Application.Ingredients.Entities;
using Queries = Application.Ingredients.Queries;
using Commands = Application.Ingredients.Commands;


public static class IngredientsEndpoints
{
    public static WebApplication MapIngredientsEndpoints(this WebApplication app)
    {
        var root = app.MapGroup("/api/ingredient")
            .AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory)
            .WithTags("ingredient")
            .WithDescription("Lookup and Find Ingredients")
            .WithOpenApi();

        _ = root.MapGet("/", GetIngredients)
            .Produces<List<Entities.Ingredient>>()
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Lookup all Ingredients")
            .WithDescription("\n    GET /ingredient");

        _ = root.MapPost("/", CreateIngredient)
            .Produces<Entities.Ingredient>()
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Create an Ingredient")
            .WithDescription("\n    POST /ingredient");

        _ = root.MapPut("/{id}", UpdateIngredient)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Update an Ingredient")
            .WithDescription("\n    PUT /ingredient/00000000-0000-0000-0000-000000000000");

        _ = root.MapDelete("/{id}", DeleteIngredient)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Delete an Ingredient")
            .WithDescription("\n    DELETE /ingredient/00000000-0000-0000-0000-000000000000");

        return app;
    }

    public static async Task<IResult> GetIngredients([FromServices] IMediator mediator)
    {
        try
        {
            return Results.Ok(await mediator.Send(new Queries.GetIngredients.GetIngredientsQuery()));
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
        }
    }

    public static async Task<IResult> CreateIngredient([Validate][FromBody] CreateIngredientRequest request, [FromServices] IMediator mediator)
    {
        try
        {
            var response = await mediator.Send(new Commands.CreateIngredient.CreateIngredientCommand
            {
                Name = request.Name,
                Description = request.Description
            });

            return Results.Created($"/api/ingredient/{response.Id}", response);
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

    public static async Task<IResult> UpdateIngredient([Validate][FromRoute] Guid id, [Validate][FromBody] UpdateIngredientRequest request, [FromServices] IMediator mediator)
    {
        try
        {
            _ = await mediator.Send(new Commands.UpdateIngredient.UpdateIngredientCommand
            {
                Id = id,
                Name = request.Name,
                Description = request.Description
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

    public static async Task<IResult> DeleteIngredient([Validate][FromRoute] Guid id, [FromServices] IMediator mediator)
    {
        try
        {
            _ = await mediator.Send(new Commands.DeleteIngredient.DeleteIngredientCommand
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
