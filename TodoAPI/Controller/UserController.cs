using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using TodoAPI.DTOs.UserDTO;
using TodoAPI.Model;
using TodoAPI.Repository.UnityOfWork;
using TodoAPI.Services;
using TodoAPI.Utils;

namespace TodoAPI.Controller;

[Route("[Controller]")]
[ApiController]
public class UserController : ControllerBase
{

    public readonly IUnitOfWork _uof;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;
    public readonly IMapper _mapper;

    public UserController(IUnitOfWork uof, IMapper mapper, IConfiguration configuration, ITokenService tokenService)
    {
        _mapper = mapper;
        _uof = uof;
        _configuration = configuration;
        _tokenService = tokenService;
    }

    [HttpGet(Name = "ObterUser")]
    public async Task<ActionResult> getUser(int id)
    {
        User? user = await _uof.UserRepository.Get(u => u.UserId == id);

        if(user is null)
        {
            return NotFound(new { errorMessage = "Usuário não encontrado" });
        }

        GetUserDTO userDTO = _mapper.Map<GetUserDTO>(user);

        return Ok(userDTO);
    }

    [HttpPost("/login")]
    public async Task<ActionResult> login(LoginUserDTO loginUserDTO)
    {

        var jwtSettings = _configuration.GetSection("JWT");

        if(loginUserDTO is null)
        {
            return BadRequest(new { errorMessage = "Corpo inválido" });
        }

        User user = await _uof.UserRepository.findByName(loginUserDTO.Username);

        if(user is null)
        {
            return Unauthorized(new { errorMessage = "Usuário ou Senha incorreto(a)" });
        }

        bool senhaValida = PasswordHasher.Verify(loginUserDTO.Password, user.Password);

        if (!senhaValida)
        {
            return Unauthorized(new { errorMessage = "Usuário ou Senha incorreto(a" });
        }

        List<Claim> authClaims = new()
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.Username),
            new Claim(JwtRegisteredClaimNames.Iss, jwtSettings["ValidIssuer"]),
            new Claim(JwtRegisteredClaimNames.Aud, jwtSettings["ValidAudience"]),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            new Claim(JwtRegisteredClaimNames.Exp, DateTimeOffset.UtcNow.AddMinutes(jwtSettings.GetValue<double>("TokenValidityInMinutes")).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        JwtSecurityToken token = _tokenService.GenerateAcessToken(authClaims, _configuration);

        return Ok(new
        {
            Message = "Autenticado com sucesso",
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = token.ValidTo
        });

    }

    [HttpPost("/cadastro")]
    public async Task<ActionResult> CadastrarUser(CreateUserRequestDTO cadastrarUserDTO)
    {
        User user = await _uof.UserRepository.findByName(cadastrarUserDTO.Username);

        if(user is not null)
        {
            return BadRequest(new { errorMessage = "Usuário já cadastrado" });
        }

        user = _mapper.Map<User>(cadastrarUserDTO);

        user.Password = PasswordHasher.Hash(user.Password);

        _uof.UserRepository.Create(user);

        await _uof.CommitAsync();

        return new CreatedAtRouteResult("ObterUser", new 
        {   message = "Usuario cadastrado com sucesso", 
            id = user.UserId
        },
            user);
    }

}
