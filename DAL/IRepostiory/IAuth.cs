using EcoTrack.DTO;

namespace EcoTrack.DAL.IRepository;

public interface IAuth
{
    DTO_Resposta Cadastrar(DTO_User novoUser);
    DTO_Resposta Logar(DTO_Login crendencias);
}