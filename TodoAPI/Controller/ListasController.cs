using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TodoAPI.DTOs.ListaDTO;
using TodoAPI.Model;
using TodoAPI.Services;
using TodoAPI.Utils.ErrorResponses;
using TodoAPI.Utils.Pagination;

namespace TodoAPI.Controller;

[ApiController]
[Route("[Controller]")]
public class ListasController : ControllerBase
{

    private readonly IListaService _listaService;


    public ListasController(IListaService listaService, IGenericErrorHandler errorHandler)
    {
        _listaService = listaService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult> GetAll([FromQuery] QueryParameters parameters)
    {
        var response = await _listaService.GetAll(parameters);

        Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(response.Metadata));

        return Ok(new { count = response.Lista.Count(), data = response.Lista });
    }

    [HttpGet("{id:int}", Name = "ObterLista")]
    [Authorize]
    public async Task<ActionResult> GetCategoria(int id)
    {
        GetListaResponseDTO lista = await _listaService.Get(id);

        return Ok(new { data = lista });
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> CreateLista(CreateListaDTO listaDTO)
    {
        GetListaResponseDTO lista = await _listaService.Create(listaDTO);

        return new CreatedAtRouteResult("ObterLista", new { id = lista.listaId } , lista);
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<ActionResult> DeleteLista(int id)
    {
        CUDListaResponseDTO lista = await _listaService.Delete(id);

        return Ok(new { message = $"Item {id} deletado com sucesso", data = lista});
    }

    [HttpPatch("{id:int}")]
    [Authorize]
    public async Task<ActionResult> UpdateLista(int id, CreateListaDTO listaDTO)
    {
        CUDListaResponseDTO lista = await _listaService.Update(id, listaDTO);

        return Ok(new { message = $"Item {id} alterado com sucesso", data = lista });
    }
}
