using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using TodoAPI.DTOs.ItemDTO;
using TodoAPI.Model;

namespace TodoAPI.DTOs.ListaDTO;

public class GetListaResponseDTO
{
    public GetListaResponseDTO()
    {
        itens = new Collection<GetItemResponseDTO>();
    }

    public int listaId { get; set; }
    public String titulo { get; set; }
    public ICollection<GetItemResponseDTO>? itens { get; set; }
}
