using AutoMapper;
using TodoAPI.DTOs.ItemDTO;
using TodoAPI.DTOs.UserDTO;
using TodoAPI.Model;

namespace TodoAPI.DTOs;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Item, GetItemResponseDTO>().ForMember(dest => dest.listaTitulo, opt => opt.MapFrom(src => src.lista.titulo)).ReverseMap();
        CreateMap<Item, CreateItemRequestDTO>().ReverseMap();
        CreateMap<User, GetUserDTO>().ReverseMap();
        CreateMap<User, CreateUserRequestDTO>().ReverseMap();
    }
}
