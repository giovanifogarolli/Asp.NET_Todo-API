using AutoMapper;
using TodoAPI.DTOs.ItemDTO;
using TodoAPI.DTOs.ListaDTO;
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

        CreateMap<Lista, CreateListaDTO>().ReverseMap();
        CreateMap<Lista, GetListaResponseDTO>().ReverseMap();
        CreateMap<Lista, CUDListaResponseDTO>().ReverseMap();

    }
}
