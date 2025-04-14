namespace CookBookApi.Infrastructure.Databases.CookBooks.Configuration;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

internal class RecipeLineItemConfiguration : EntityConfiguration<RecipeLineItem>
{
    public override void Configure(EntityTypeBuilder<RecipeLineItem> builder)
    {
        base.Configure(builder);

        _ = builder.HasOne(i => i.Ingredient).WithMany(i => i.RecipeLineItems).HasForeignKey(i => i.IngredientId);
    }
}
