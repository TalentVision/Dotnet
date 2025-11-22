using TalentVision.Domain.Entities;

namespace TalentVision.Application.Services;

public interface ILogAuditoriaService
{
    Task<IEnumerable<LogAuditoria>> ListarAsync();

    Task RegistrarAsync(
        string tabela,
        string operacao,
        Guid? idUsuarioAcao,
        string? descricao);
}