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
using TodoAPI.Utils.Pagination;

namespace TodoAPI.Controller;

[Route("[Controller]")]
[ApiController]
public class ItensController : ControllerBase
{

    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public ItensController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _uof = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Item>>> GetAll()
    {
        IEnumerable<Item> itens = await _uof.ItemRepository.GetAll();

        IEnumerable<GetItemResponseDTO> itensDTO = _mapper.Map<IEnumerable<GetItemResponseDTO>>(itens);

        return Ok(new { success = true, quantity = itensDTO.Count(), data = itensDTO });
    }

    [HttpGet("pagination")]
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

        return Ok(new { success = true, data = itensDTO });
    }

    [HttpGet("categoria/{categoria:int}")]
    public async Task<ActionResult<IQueryable<Item>>> GetItensPorCategoria(int categoria, [FromQuery] QueryParameters itensParameters)
    {
        int.TryParse(User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value, out int userId);

        PagedList<Item> itens = await _uof.ItemRepository.GetItemPorCategoria(categoria, userId, itensParameters);

        if(itens is null || !itens.Any())
        {
            return NotFound("Categoria vazia ou não existente");
        }

        IEnumerable<GetItemResponseDTO> itensDTO = _mapper.Map<IEnumerable<GetItemResponseDTO>>(itens);

        return Ok(itensDTO);
    }

    [HttpGet("{id:int}", Name = "ObterItem")]
    public async Task<ActionResult> GetItem(int id)
    {
        Item item = await _uof.ItemRepository.Get(i=> i.itemId == id);

        if(item is null)
        {
            return NotFound(new { error = "Não existe item cadastrado" });
        }

        GetItemResponseDTO itemDTO = _mapper.Map<GetItemResponseDTO>(item);

        return Ok(new { success = true, data = itemDTO });
    }

    [HttpPost]
    public async Task<ActionResult> PostItem(CreateItemRequestDTO itemDTO)
    {
        if(itemDTO is null)
        {
            return BadRequest("Dados inválidos");
        }

        Item item = _mapper.Map<Item>(itemDTO);

        item.dataInicio = DateTime.Now;
        item.status = false;

        Item CreatedItem = _uof.ItemRepository.Create(item);
        await _uof.CommitAsync();

        return new CreatedAtRouteResult("ObterItem", new { id = CreatedItem.itemId }, CreatedItem);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteItem(int id)
    {

        Item item = await _uof.ItemRepository.Get(i => i.itemId == id);

        if (item == null) {
            return NotFound("Item não existe");
        }

        _uof.ItemRepository.Delete(item);

        await _uof.CommitAsync();

        return Ok(new { message = "Item excluido com sucesso", data = _mapper.Map<GetItemResponseDTO>(item)} );
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateItem(int id, Item item)
    {
        Item UpdatedItem = await _uof.ItemRepository.Get(i => i.itemId == id);

        if (UpdatedItem == null)
            return NotFound("Item não existe");

        _uof.ItemRepository.Update(item);

        await _uof.CommitAsync();

        return Ok(new { message = "Item Alterado com sucesso", data = _mapper.Map<GetItemResponseDTO>(UpdatedItem) });
    }

    [HttpPatch("concluir/{id:int}")]
    public async Task<ActionResult> ConcludeItem(int id)
    {
        Item item = await _uof.ItemRepository.Get(i => i.itemId == id);

        if (item == null)
            return NotFound("Item não existe");

        item.status = true;
        item.dataFim = DateTime.Now;

        await _uof.CommitAsync();

        return Ok(new { message = "Item concluído com sucesso", data = _mapper.Map<GetItemResponseDTO>(item)});
    }

}
