namespace CookBookApi.Infrastructure.Databases.CookBooks.Mapping;

using AutoMapper;
using Application = Application.Ingredients.Entities;
using Infrastructure = Models;

internal class IngredientMappingProfile : Profile
{
    public IngredientMappingProfile()
    {
        _ = this.CreateMap<Infrastructure.Ingredient, Application.Ingredient>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
            .ReverseMap();
    }
}
