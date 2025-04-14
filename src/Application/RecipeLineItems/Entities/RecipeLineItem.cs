namespace CookBookApi.Application.RecipeLineItems.Entities;

using CookBookApi.Application.Common.Enums;
using CookBookApi.Application.Ingredients.Entities;

public record RecipeLineItem(Guid Id, Ingredient Ingredient, decimal Quantity, UnitOfMeasurementType UnitOfMeasurement);
