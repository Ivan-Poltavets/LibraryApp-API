using AutoMapper;
using LibraryApp.Data.Dtos;
using LibraryApp.Data.Entities;

namespace LibraryApp.Data.Mappings;

public class CartMapper : Profile
{
    public CartMapper()
    {
        CreateMap<Cart, OrderDto>();
    }
}
