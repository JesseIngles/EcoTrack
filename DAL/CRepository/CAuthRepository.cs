using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EcoTrack.DAL.Database.AppDbContexts;
using EcoTrack.DAL.Database.Entities;
using EcoTrack.DAL.IRepository;
using EcoTrack.DTO;
using Microsoft.IdentityModel.Tokens;

namespace EcoTrack.DAL.CRepository;

public class CAuthRepository : IAuth
{
  private readonly AppDbContext _db;
  public CAuthRepository(AppDbContext db)
  {
    _db = db;
  }
  private string GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Tchilla".PadRight(128)));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>{
          new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
          new Claim(JwtRegisteredClaimNames.Email, user.Email),
        };
        var token = new JwtSecurityToken(
            issuer: "CodePoint",
            audience: "CodePoint",
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

  public DTO_Resposta Cadastrar(DTO_User novoUser)
  {
    DTO_Resposta resposta = new DTO_Resposta();
    try
    {
      if (novoUser == null)
      {
        resposta.mensagem = "Dados inválidos";
        return resposta;
      }

      User NovoUser = new User
      {
        Email = novoUser.Email,
        Password = novoUser.Password
      };

      _db.Users.Add(NovoUser);
      _db.SaveChanges();

      resposta.mensagem = $"Sucesso: {novoUser.Email} cadastrado com sucesso";

    }
    catch (Exception ex)
    {
      resposta.mensagem = ex.ToString();
    }
    return resposta;
  }

  public DTO_Resposta Logar(DTO_Login crendencias)
  {
    DTO_Resposta resposta = new DTO_Resposta();
    try
    {
      if (crendencias == null)
      {
        resposta.mensagem = "Dados inválidos";
        return resposta;
      }

      var userExisitente = _db.Users.FirstOrDefault(x => x.Password == crendencias.Password && x.Email == crendencias.Email);

      if (userExisitente != null)
      {
        resposta.mensagem = $"Sucesso: {userExisitente.Email} logado";
        resposta.resposta = GenerateJwtToken(userExisitente);
      }

      resposta.mensagem = $"Credenciais inválidas";

    }
    catch (Exception ex)
    {
      resposta.mensagem = ex.ToString();
    }
    return resposta;
  }
}