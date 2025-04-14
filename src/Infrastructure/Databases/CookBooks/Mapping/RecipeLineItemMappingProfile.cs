namespace CookBookApi.Infrastructure.Databases.CookBooks.Mapping;

using AutoMapper;
using Application = Application.RecipeLineItems.Entities;
using Infrastructure = Models;

internal class RecipeLineItemMappingProfile : Profile
{
    public RecipeLineItemMappingProfile()
    {
        _ = this.CreateMap<Infrastructure.RecipeLineItem, Application.RecipeLineItem>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Quantity, o => o.MapFrom(s => s.Quantity))
            .ForMember(d => d.UnitOfMeasurement, o => o.MapFrom(s => s.UnitOfMeasurement))
            .ForMember(d => d.Ingredient, o => o.MapFrom(s => s.Ingredient))
            .ReverseMap();
    }
}
