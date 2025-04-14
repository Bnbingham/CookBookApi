namespace CookBookApi.Infrastructure.Databases.CookBooks.Models;

using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
internal record Recipe : Entity
{
    public string Title { get; set; }

    public string Description { get; set; }

    public string Instructions { get; set; }

    [ForeignKey("Author")]
    public Guid AuthorId { get; init; }

    public Author Author { get; init; }

    public ICollection<CookBook> CookBooks { get; set; } = [];

    public ICollection<RecipeLineItem> RecipeLineItems { get; set; } = [];
}
