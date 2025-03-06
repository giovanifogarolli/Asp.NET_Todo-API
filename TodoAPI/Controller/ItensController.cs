using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using TodoAPI.Context;
using TodoAPI.DTOs.ItemDTO;
using TodoAPI.Model;
using TodoAPI.Repository.item;
using TodoAPI.Repository.Itens;
using TodoAPI.Repository.UnityOfWork;
using TodoAPI.Utils.ErrorResponses;
using TodoAPI.Utils.Pagination;

namespace TodoAPI.Controller;

[Route("[Controller]")]
[ApiController]
public class ItensController : ControllerBase
{

    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;
    private readonly IGenericErrorHandler _errorHandler;

    public ItensController(IUnitOfWork unitOfWork, IMapper mapper, IGenericErrorHandler errorHandler)
    {
        _uof = unitOfWork;
        _mapper = mapper;
        _errorHandler = errorHandler;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Item>>> GetItens([FromQuery] QueryParameters itensParameters)
    {
        int.TryParse(User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value, out int userId);

        PagedList<Item> itens = await _uof.ItemRepository.GetItens(itensParameters, userId);

        var metadata = new
        {
            itens.TotalCount,
            itens.PageSize,
            itens.CurrentPage,
            itens.TotalPages,
            itens.HasNext,
            itens.HasPrevious
        };

        Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

        IEnumerable<GetItemResponseDTO> itensDTO = _mapper.Map<IEnumerable<GetItemResponseDTO>>(itens);

        return Ok(new {quantity = itens.Count() , data = itensDTO });
    }

    [HttpGet("categoria/{categoria:int}")]
    [Authorize]
    public async Task<ActionResult<IQueryable<Item>>> GetItensPorCategoria(int categoria, [FromQuery] QueryParameters itensParameters)
    {
        int.TryParse(User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value, out int userId);

        PagedList<Item> itens = await _uof.ItemRepository.GetItemPorCategoria(categoria, userId, itensParameters);

        if (!itens.Any()) return _errorHandler.ResourceNotFound($"Categoria ID = {categoria} não existe.");

        IEnumerable<GetItemResponseDTO> itensDTO = _mapper.Map<IEnumerable<GetItemResponseDTO>>(itens);

        return Ok(new { quantity = itensDTO.Count(), data = itensDTO });
    }

    [HttpGet("{id:int}", Name = "ObterItem")]
    [Authorize]
    public async Task<ActionResult> GetItem(int id)
    {
        Item? item = await _uof.ItemRepository.Get(i=> i.itemId == id);

        if (item == null) _errorHandler.ResourceNotFound(detail: $"Não existe item com o ID = {id}, informe um ID valido.");

        GetItemResponseDTO itemDTO = _mapper.Map<GetItemResponseDTO>(item);

        return Ok(new {data = itemDTO });
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> PostItem(CreateItemRequestDTO itemDTO)
    {
        Item item = _mapper.Map<Item>(itemDTO);

        item.dataInicio = DateTime.Now;
        item.status = false;

        Item CreatedItem = _uof.ItemRepository.Create(item);
        await _uof.CommitAsync();

        return new CreatedAtRouteResult("ObterItem", new { id = CreatedItem.itemId }, CreatedItem);
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<ActionResult> DeleteItem(int id)
    {

        Item? item = await _uof.ItemRepository.Get(i => i.itemId == id);

        if (item == null) return _errorHandler.ResourceNotFound(detail: $"Não existe item com o ID = {id}, informe um ID valido.");

        _uof.ItemRepository.Delete(item);

        await _uof.CommitAsync();

        return Ok(new { message = "Item excluido com sucesso", data = _mapper.Map<GetItemResponseDTO>(item)} );
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<ActionResult> UpdateItem(int id, Item item)
    {
        Item? UpdatedItem = await _uof.ItemRepository.Get(i => i.itemId == id);

        if (item == null) return _errorHandler.ResourceNotFound(detail: $"Não existe item com o ID = {id}, informe um ID valido.");

        _uof.ItemRepository.Update(item);

        await _uof.CommitAsync();

        return Ok(new { message = "Item Alterado com sucesso", data = _mapper.Map<GetItemResponseDTO>(UpdatedItem) });
    }

    [HttpPatch("concluir/{id:int}")]
    [Authorize]
    public async Task<ActionResult> ConcludeItem(int id)
    {
        Item? item = await _uof.ItemRepository.Get(i => i.itemId == id);

        if (item == null) return _errorHandler.ResourceNotFound(detail: $"Não existe item com o ID = {id}, informe um ID valido.");

        item.status = true;
        item.dataFim = DateTime.Now;

        await _uof.CommitAsync();

        return Ok(new { message = "Item concluído com sucesso", data = _mapper.Map<GetItemResponseDTO>(item)});
    }

}
