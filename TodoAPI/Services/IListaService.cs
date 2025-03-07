using Microsoft.AspNetCore.Mvc;
using TodoAPI.DTOs.ListaDTO;
using TodoAPI.Model;
using TodoAPI.Utils.Pagination;

namespace TodoAPI.Services;

public interface IListaService
{
    public Task<GetListaResponseDTO> Create(CreateListaDTO listaDTO);

    public Task<CUDListaResponseDTO> Delete(int id);

    public Task<GetListaResponseDTO> Get(int id);

    public Task<(IEnumerable<GetListaResponseDTO> Lista, object Metadata)> GetAll(QueryParameters QueryParam);

    public Task<CUDListaResponseDTO> Update(int id, CreateListaDTO listaDTO);
}
