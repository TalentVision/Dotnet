using TalentVision.Domain.Entities;

namespace TalentVision.Application.Services;

public interface ICandidaturaService
{
    Task<IEnumerable<Candidatura>> ListarAsync();
    Task<Candidatura?> ObterPorIdAsync(Guid id);
    Task<Candidatura> CriarAsync(Candidatura candidatura);
    Task<Candidatura?> AtualizarAsync(Guid id, Candidatura candidatura);
    Task<bool> RemoverAsync(Guid id);
}