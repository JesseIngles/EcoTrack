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
    private readonly string secretKey = "Tchilla".PadRight(32, '0'); // Garante que a chave seja igual

    public CAuthRepository(AppDbContext db)
    {
        _db = db;
    }

    private string GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)); // Mesma chave
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim("id", user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
        };

        var token = new JwtSecurityToken(
            issuer: "CodePoint",
            audience: "CodePoint",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30), // Melhor usar UTC
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

    public DTO_Resposta Logar(DTO_Login credenciais)
    {
        DTO_Resposta resposta = new DTO_Resposta();
        try
        {
            if (credenciais == null)
            {
                resposta.mensagem = "Dados inválidos";
                return resposta;
            }

            var userExistente = _db.Users.FirstOrDefault(x => x.Password == credenciais.Password && x.Email == credenciais.Email);

            if (userExistente != null)
            {
                resposta.mensagem = $"Sucesso: {userExistente.Email} logado";
                resposta.resposta = GenerateJwtToken(userExistente);
                return resposta;
            }

            resposta.mensagem = "Credenciais inválidas";
        }
        catch (Exception ex)
        {
            resposta.mensagem = ex.ToString();
        }
        return resposta;
    }
}
