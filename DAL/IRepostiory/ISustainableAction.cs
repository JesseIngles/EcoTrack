using EcoTrack.DTO;

namespace EcoTrack.DAL.IRepository;

public interface ISustainableAction
{
    DTO_Resposta Atualizar(Guid id, DTO_SustainableAction sustainableAction);
    DTO_Resposta Cadastrar(DTO_SustainableAction sustainableAction, Guid userId);
    DTO_Resposta Eliminar(Guid id);
    DTO_Resposta Listar();
}