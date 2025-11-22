using TalentVision.Domain.Entities;

namespace TalentVision.Application.Services;

public interface IVagaService
{
    Task<IEnumerable<Vaga>> ListarAsync();
    Task<Vaga?> ObterPorIdAsync(Guid id);
    Task<Vaga> CriarAsync(Vaga vaga);
    Task<Vaga?> AtualizarAsync(Guid id, Vaga vaga);
    Task<bool> RemoverAsync(Guid id);
}