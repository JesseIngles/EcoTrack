using EcoTrack.DAL.IRepository;
using EcoTrack.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EcoTrack.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
  private readonly IAuth _auth;
  public AuthController(IAuth auth)
  {
    _auth = auth;
  }

  [HttpPost("cadastrar")]
  public DTO_Resposta Cadastrar(DTO_User novoUser)
  {
    return _auth.Cadastrar(novoUser);
  }

  [HttpPost("login")]
  public DTO_Resposta Login(DTO_Login credenciais)
  {
    return _auth.Logar(credenciais);
  }
}