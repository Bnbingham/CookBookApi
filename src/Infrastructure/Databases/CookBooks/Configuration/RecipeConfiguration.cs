namespace CookBookApi.Infrastructure.Databases.CookBooks.Configuration;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

internal class RecipeConfiguration : EntityConfiguration<Recipe>
{
    public override void Configure(EntityTypeBuilder<Recipe> builder)
    {
        base.Configure(builder);

        _ = builder.HasMany(m => m.RecipeLineItems).WithOne();
        _ = builder.HasOne(m => m.Author).WithMany(r => r.Recipes).HasForeignKey(r => r.AuthorId);
        _ = builder.HasMany(m => m.CookBooks).WithMany(r => r.Recipes);
    }
}
