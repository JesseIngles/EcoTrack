using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EcoTrack.DAL.IRepository;
using EcoTrack.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoTrack.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SustainableActionController : Controller
{
  private readonly ISustainableAction _sustainable;
  public SustainableActionController(ISustainableAction sustainable)
  {
    _sustainable = sustainable;
  }
  [Authorize]
  [HttpPost("cadastrar")]
  public DTO_Resposta Cadastrar(DTO_SustainableAction sustainableAction)
  {
    var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value; // Buscar pelo Subject do Token

    if (!Guid.TryParse(userIdClaim, out var userId))
    {
      return new DTO_Resposta { mensagem = "Erro: usuário não autenticado ou token inválido" };
    }

    sustainableAction.UserId = userId;
    return _sustainable.Cadastrar(sustainableAction);
  }

  [Authorize]
  [HttpPut("atualizar")]
  public DTO_Resposta Atualizar(Guid id, DTO_SustainableAction sustainableAction)
  {
    var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

    if (!Guid.TryParse(userIdClaim, out var userId))
    {
      return new DTO_Resposta { mensagem = "Erro: usuário não autenticado ou token inválido" };
    }

    sustainableAction.UserId = userId;
    return _sustainable.Atualizar(id, sustainableAction);
  }

  [Authorize]
  [HttpDelete("eliminar/id")]
  public DTO_Resposta Eliminar(Guid id)
  {
    return _sustainable.Eliminar(id);
  }
  [AllowAnonymous]
  [HttpGet("listar")]
  public DTO_Resposta Listar()
  {
    return _sustainable.Listar();
  }
}