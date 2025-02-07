using EcoTrack.DAL.Database.AppDbContexts;
using EcoTrack.DAL.Database.Entities;
using EcoTrack.DAL.IRepository;
using EcoTrack.DTO;

namespace EcoTrack.DAL.CRepository;

public class CSustainableActionRepository : ISustainableAction
{
  private readonly AppDbContext _db;
  public CSustainableActionRepository(AppDbContext db)
  {
    _db = db;
  }

  public DTO_Resposta Atualizar(Guid id, DTO_SustainableAction sustainableAction)
  {
    DTO_Resposta resposta = new DTO_Resposta();
    try
    {
      var SustainableAction = _db.SustainableActions.FirstOrDefault(x => x.Id == id);
      if (sustainableAction == null)
      {
        resposta.mensagem = "Dados inválidos";
        return resposta;
      }
      SustainableAction.Points = sustainableAction.Points;
      SustainableAction.Description = sustainableAction.Description;
      SustainableAction.Title = sustainableAction.Title;
      SustainableAction.Category = sustainableAction.Category;
      SustainableAction.Description = sustainableAction.Description;

      _db.SustainableActions.Update(SustainableAction);
      _db.SaveChanges();

      resposta.mensagem = $"Sucesso: {sustainableAction.Title} atualizado com sucesso";

    }
    catch (Exception ex)
    {
      resposta.mensagem = ex.ToString();
    }
    return resposta;
  }

  public DTO_Resposta Cadastrar(DTO_SustainableAction sustainableAction)
  {
    DTO_Resposta resposta = new DTO_Resposta();
    try
    {
      if (sustainableAction == null)
      {
        resposta.mensagem = "Dados inválidos";
        return resposta;
      }

      SustainableAction SustainableAction = new SustainableAction
      {
        Points = sustainableAction.Points,
        Title = sustainableAction.Title,
        Description = sustainableAction.Description,
        UserId = sustainableAction.UserId,
        Category = sustainableAction.Category,

      };

      _db.SustainableActions.Add(SustainableAction);
      _db.SaveChanges();

      resposta.mensagem = $"Sucesso: {sustainableAction.Title} cadastrado com sucesso";

    }
    catch (Exception ex)
    {
      resposta.mensagem = ex.ToString();
    }
    return resposta;
  }

  public DTO_Resposta Eliminar(Guid id)
  {
    DTO_Resposta resposta = new DTO_Resposta();
    try
    {
      var sustainableAction = _db.SustainableActions.FirstOrDefault(x => x.Id == id);
      if (sustainableAction == null)
      {
        resposta.mensagem = "Dados inválidos";
        return resposta;
      }

      _db.SustainableActions.Remove(sustainableAction);
      _db.SaveChanges();

      resposta.mensagem = $"Sucesso: {sustainableAction.Title} cadastrado com sucesso";

    }
    catch (Exception ex)
    {
      resposta.mensagem = ex.ToString();
    }
    return resposta;
  }

  public DTO_Resposta Listar()
  {
    DTO_Resposta resposta = new DTO_Resposta();
    try
    {
      resposta.resposta = _db.SustainableActions.ToList();
      resposta.mensagem = "Todas Acções sustentavéis";
    }
    catch (Exception ex)
    {
      resposta.mensagem = ex.ToString();
    }
    return resposta;
  }
}