namespace CookBookApi.Infrastructure.Databases.CookBooks.Models;

using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using CookBookApi.Application.Common.Enums;

[ExcludeFromCodeCoverage]
internal record RecipeLineItem : Entity
{
    [ForeignKey("Ingredient")]
    public Guid IngredientId { get; set; }

    public Ingredient Ingredient { get; set; }

    public decimal Quantity { get; set; }

    public UnitOfMeasurementType UnitOfMeasurement { get; set; }
}
