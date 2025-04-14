namespace CookBookApi.Presentation.Endpoints;

using CookBookApi.Application.Common.Exceptions;
using CookBookApi.Presentation.Filters;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities = Application.CookBooks.Entities;
using Queries = Application.CookBooks.Queries;

public static class CookBooksEndpoints
{
    public static WebApplication MapCookBookEndpoints(this WebApplication app)
    {
        var root = app.MapGroup("/api/cookbook")
            .AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory)
            .WithTags("cookbook")
            .WithDescription("Lookup and Find CookBooks")
            .WithOpenApi();

        _ = root.MapGet("/", GetCookBooks)
            .Produces<List<Entities.CookBook>>()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Lookup all CookBooks")
            .WithDescription("\n    GET /cookbook");

        _ = root.MapGet("/{id}", GetCookBookById)
            .Produces<Entities.CookBook>()
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Lookup an CookBook by their Id")
            .WithDescription("\n    GET /cookbook/00000000-0000-0000-0000-000000000000");

        return app;
    }

    public static async Task<IResult> GetCookBooks([FromServices] IMediator mediator)
    {
        try
        {
            return Results.Ok(await mediator.Send(new Queries.GetCookBooks.GetCookBooksQuery()));
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
        }
    }

    public static async Task<IResult> GetCookBookById([Validate][FromRoute] Guid id, [FromServices] IMediator mediator)
    {
        try
        {
            return Results.Ok(await mediator.Send(new Queries.GetCookBookById.GetCookBookByIdQuery
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
}
