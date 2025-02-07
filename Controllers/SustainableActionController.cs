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
  [HttpPost("/cadastrar")]
  public DTO_Resposta Cadastrar(DTO_SustainableAction sustainableAction)
  {
    sustainableAction.UserId = Guid.Parse(User.FindFirst(JwtRegisteredClaimNames.Sub).Value);
    return _sustainable.Cadastrar(sustainableAction);
  }
  [Authorize]
  [HttpPost("/atualizar")]
  public DTO_Resposta Atualizar(Guid id, DTO_SustainableAction sustainableAction)
  {
    sustainableAction.UserId = Guid.Parse(User.FindFirst(JwtRegisteredClaimNames.Sub).Value);
    return _sustainable.Atualizar(id, sustainableAction);
  }
  [Authorize]
  [HttpPost("/eliminar/id")]
  public DTO_Resposta Eliminar(Guid id)
  {
    return _sustainable.Eliminar(id);
  }
  [AllowAnonymous]
  [HttpPost("/listar")]
  public DTO_Resposta Listar()
  {
    return _sustainable.Listar();
  }
}