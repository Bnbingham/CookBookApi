namespace CookBookApi.Infrastructure.Databases.CookBooks.Extensions;

using System;
using Bogus;
using CookBookApi.Application.Common.Enums;
using Models;

internal static class CookBooksDbContextExtensions
{
    public static CookBooksDbContext AddData(this CookBooksDbContext context)
    {
        var authors = new Faker<Author>()
            .RuleFor(a => a.Id, _ => Guid.NewGuid())
            .RuleFor(a => a.FirstName, f => f.Person.FirstName)
            .RuleFor(a => a.LastName, f => f.Person.LastName)
            .RuleFor(a => a.DateCreated, f => f.Date.Past())
            .RuleFor(a => a.DateModified, f => f.Date.Past())
            .Generate(15);

        context.AddRange(authors);

        var ingredients = new Faker<Ingredient>()
            .RuleFor(i => i.Id, _ => Guid.NewGuid())
            .RuleFor(i => i.Name, f => f.Commerce.ProductName())
            .RuleFor(i => i.Description, f => f.Commerce.ProductDescription())
            .RuleFor(i => i.DateCreated, f => f.Date.Past())
            .RuleFor(i => i.DateModified, f => f.Date.Past())
            .Generate(25);

        context.AddRange(ingredients);

        var recipes = new Faker<Recipe>()
            .RuleFor(r => r.Id, _ => Guid.NewGuid())
            .RuleFor(r => r.Title, f => f.Commerce.ProductName())
            .RuleFor(r => r.Description, f => f.Commerce.ProductDescription())
            .RuleFor(r => r.Instructions, f => f.Commerce.ProductDescription())
            .RuleFor(r => r.AuthorId, f => f.PickRandom(authors).Id)
            .RuleFor(r => r.RecipeLineItems, (f, r) => f.Make(f.Random.Number(1, 10), () => new RecipeLineItem
            {
                Id = Guid.NewGuid(),
                Ingredient = f.PickRandom(ingredients),
                Quantity = Math.Round(f.Random.Decimal(1, 100), 2),
                UnitOfMeasurement = f.PickRandom<UnitOfMeasurementType>()
            }
            ))
            .RuleFor(r => r.DateCreated, f => f.Date.Past())
            .RuleFor(r => r.DateModified, f => f.Date.Past())
            .Generate(25);

        context.AddRange(recipes);

        var cookbooks = new Faker<CookBook>()
            .RuleFor(c => c.Id, _ => Guid.NewGuid())
            .RuleFor(c => c.Title, f => f.Commerce.ProductName())
            .RuleFor(c => c.Description, f => f.Commerce.ProductDescription())
            .RuleFor(c => c.DateCreated, f => f.Date.Past())
            .RuleFor(c => c.DateModified, f => f.Date.Past())
            .RuleFor(c => c.Recipes, f => [.. f.PickRandom(recipes, f.Random.Number(1, 10))])
            .Generate(25);

        context.AddRange(cookbooks);


        _ = context.SaveChanges();

        return context;
    }
}
