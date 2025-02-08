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
  public DTO_Resposta Cadastrar(DTO_SustainableAction sustainableAction, Guid userId)
  {
    var userIdClaim = User.FindFirst("id").Value; 
    if (!Guid.TryParse(userIdClaim, out var UserId))
    {
      return new DTO_Resposta { mensagem = "Erro: usuário não autenticado ou token inválido" };
    }

    return _sustainable.Cadastrar(sustainableAction, userId);
  }

  [Authorize]
  [HttpPut("atualizar")]
  public DTO_Resposta Atualizar(Guid id, DTO_SustainableAction sustainableAction)
  {
    var userIdClaim = User.FindFirst("id").Value; 
    
    if (!Guid.TryParse(userIdClaim, out var userId))
    {
      return new DTO_Resposta { mensagem = "Erro: usuário não autenticado ou token inválido" };
    }

    return _sustainable.Atualizar(id, sustainableAction);
  }

  [Authorize]
  [HttpDelete("eliminar/id")]
  public DTO_Resposta Eliminar(Guid id)
  {
    var userIdClaim = User.FindFirst("id").Value; 
    
    if (!Guid.TryParse(userIdClaim, out var userId))
    {
      return new DTO_Resposta { mensagem = "Erro: usuário não autenticado ou token inválido" };
    } 
    return _sustainable.Eliminar(id);
  }
  [AllowAnonymous]
  [HttpGet("listar")]
  public DTO_Resposta Listar()
  {
    return _sustainable.Listar();
  }
}