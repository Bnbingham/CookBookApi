namespace CookBookApi.Infrastructure.Databases.CookBooks;

using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Models;

internal class CookBooksDbContext(DbContextOptions<CookBooksDbContext> options) : DbContext(options)
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<CookBook> CookBooks { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<RecipeLineItem> RecipeLineItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        _ = modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}