using AutoMapper;
using LibraryApp.Data.Dtos;
using LibraryApp.Data.Entities;

namespace LibraryApp.Data.Mappings
{
    public class BookMapper : Profile
    {
        public BookMapper()
        {
            CreateMap<BookDto, Book>();
        }
    }
}
