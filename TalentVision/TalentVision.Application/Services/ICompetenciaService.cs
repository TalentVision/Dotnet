using TalentVision.Domain.Entities;

namespace TalentVision.Application.Services;

public interface ICompetenciaService
{
    Task<IEnumerable<Competencia>> ListarAsync();
    Task<Competencia?> ObterPorIdAsync(Guid id);
    Task<Competencia> CriarAsync(Competencia competencia);
    Task<Competencia?> AtualizarAsync(Guid id, Competencia competencia);
    Task<bool> RemoverAsync(Guid id);
}