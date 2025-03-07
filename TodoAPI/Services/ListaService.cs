using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using TodoAPI.DTOs.ListaDTO;
using TodoAPI.Model;
using TodoAPI.Repository.ListaRepo;
using TodoAPI.Repository.UnityOfWork;
using TodoAPI.Utils.ErrorResponses;
using TodoAPI.Utils.Pagination;

namespace TodoAPI.Services;

public class ListaService : IListaService
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IGenericErrorHandler _errorHandler;

    public ListaService(IUnitOfWork uof, IMapper mapper, IHttpContextAccessor httpContextAccessor, IGenericErrorHandler errorHandler)
    {
        _uof = uof;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _errorHandler = errorHandler;
    }

    public HttpContext? HttpContext { get => _httpContextAccessor.HttpContext; set => throw new NotImplementedException(); }

    public async Task<GetListaResponseDTO> Create(CreateListaDTO listaDTO)
    {
        Lista lista = _mapper.Map<Lista>(listaDTO);

         int.TryParse(HttpContext?.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value, out int userId);

        lista.UserId = userId;

        GetListaResponseDTO createdLista = _mapper.Map<GetListaResponseDTO>(_uof.ListaRepository.Create(lista));

        await _uof.CommitAsync();

        return createdLista;
    }

    public async Task<CUDListaResponseDTO> Delete(int id)
    {
        int.TryParse(HttpContext?.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value, out int userId);

        Lista? lista = await _uof.ListaRepository.Get(l => l.listaId == id, userId);

        if (lista is null) _errorHandler.ResourceNotFound($"Lista com ID = {id} não existe.");

        _uof.ListaRepository.Delete(lista);

        await _uof.CommitAsync();

        CUDListaResponseDTO listaResponse = _mapper.Map<CUDListaResponseDTO>(lista);

        return listaResponse;
    }

    public async Task<GetListaResponseDTO> Get(int id)
    {
        int.TryParse(HttpContext?.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value, out int userId);

        Lista? lista = await _uof.ListaRepository.Get(l => l.listaId == id, userId);

        if (lista is null) _errorHandler.ResourceNotFound($"Lista com ID = {id} não foi encontrada.");

        GetListaResponseDTO listaDTO = _mapper.Map<GetListaResponseDTO>(lista);

        return listaDTO;
    }

    public async Task<(IEnumerable<GetListaResponseDTO> Lista, object Metadata)> GetAll(QueryParameters QueryParam)
    {
        int.TryParse(HttpContext?.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value, out int userId);

        IEnumerable<Lista> listas = await _uof.ListaRepository.GetAll(userId);

        IQueryable<Lista> listaQueryable =  listas.OrderBy(l => l.listaId).AsQueryable();

        PagedList<Lista> listaPaginada = PagedList<Lista>.toPagedList(listaQueryable, QueryParam.PageNumber, QueryParam.Pagesize);

        IEnumerable<GetListaResponseDTO> listaMapeada = _mapper.Map<IEnumerable<GetListaResponseDTO>>(listaPaginada);

        var metadata = new
        {
            listaPaginada.TotalCount,
            listaPaginada.PageSize,
            listaPaginada.CurrentPage,
            listaPaginada.TotalPages,
            listaPaginada.HasNext,
            listaPaginada.HasPrevious
        };

        return (listaMapeada, metadata);
    }

    public async Task<CUDListaResponseDTO> Update(int id, CreateListaDTO listaDTO)
    {
        int.TryParse(HttpContext?.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value, out int userId);

        Lista? lista = await _uof.ListaRepository.Get(l => l.listaId == id);

        if (lista is null) _errorHandler.ResourceNotFound($"Lista com ID = {id} não foi encontrada.");

        lista.titulo = listaDTO.titulo;

        _uof.ListaRepository.Update(lista);

        await _uof.CommitAsync();

        CUDListaResponseDTO listaResponse = _mapper.Map<CUDListaResponseDTO>(lista);

        return listaResponse;
    }
}
