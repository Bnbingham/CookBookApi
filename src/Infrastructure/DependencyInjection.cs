namespace CookBookApi.Infrastructure;

using System;
using Application.Authors;
using CookBookApi.Application.CookBooks;
using CookBookApi.Application.Ingredients;
using CookBookApi.Application.Recipes;
using CookBookApi.Infrastructure.Databases.CookBooks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        _ = services.AddDbContext<CookBooksDbContext>(options =>
            options.UseInMemoryDatabase($"CookBooks-{Guid.NewGuid()}"), ServiceLifetime.Singleton);

        _ = services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        _ = services.AddSingleton<EntityFrameworkCookBookRepository>();

        _ = services.AddSingleton<IAuthorsRepository>(p =>
            p.GetRequiredService<EntityFrameworkCookBookRepository>());
        _ = services.AddSingleton<ICookBookRepository>(x =>
            x.GetRequiredService<EntityFrameworkCookBookRepository>());
        _ = services.AddSingleton<IIngredientsRepository>(x =>
            x.GetRequiredService<EntityFrameworkCookBookRepository>());
        _ = services.AddSingleton<IRecipesRepository>(x =>
            x.GetRequiredService<EntityFrameworkCookBookRepository>());


        _ = services.AddSingleton(TimeProvider.System);

        return services;
    }
}
