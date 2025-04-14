namespace CookBookApi.Infrastructure.Databases.CookBooks.Mapping;

using AutoMapper;
using Application = Application.Authors.Entities;
using Infrastructure = Models;
using ApplicationRecipes = Application.Recipes.Entities;

internal class AuthorMappingProfile : Profile
{
    public AuthorMappingProfile()
    {
        _ = this.CreateMap<Application.Author, Infrastructure.Author>()
            .ForMember(d => d.DateCreated, o => o.Ignore())
            .ForMember(d => d.DateModified, o => o.Ignore())
            .ForMember(d => d.Recipes, o => o.Ignore())
            .ReverseMap()
            .ConstructUsing((src, ctx) => new Application.Author(
                src.Id,
                src.FirstName,
                src.LastName,
                ctx.Mapper.Map<ICollection<ApplicationRecipes.AuthorRecipe>>(src.Recipes) ?? []));
    }
}
