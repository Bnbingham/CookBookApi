namespace CookBookApi.Infrastructure.Databases.CookBooks.Mapping;

using AutoMapper;
using Application = Application.CookBooks.Entities;
using Infrastructure = Models;
using ApplicationRecipes = Application.Recipes.Entities;

internal class CookBookMappingProfile : Profile
{
    public CookBookMappingProfile()
    {
        _ = this.CreateMap<Application.CookBook, Infrastructure.CookBook>()
            .ForMember(d => d.DateCreated, o => o.Ignore())
            .ForMember(d => d.DateModified, o => o.Ignore())
            .ForMember(d => d.Recipes, o => o.Ignore())
            .ReverseMap()
            .ConstructUsing((src, ctx) => new Application.CookBook(
                src.Id,
                src.Title,
                src.Description,
                ctx.Mapper.Map<ICollection<ApplicationRecipes.CookBookRecipe>>(src.Recipes) ?? []));
    }
}