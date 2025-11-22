using TalentVision.Domain.Entities;

namespace TalentVision.Application.Services;

public interface IUsuarioService
{
    Task<IEnumerable<Usuario>> ListarAsync();
    Task<Usuario?> ObterPorIdAsync(Guid id);
    Task<Usuario> CriarAsync(Usuario usuario);
    Task<Usuario?> AtualizarAsync(Guid id, Usuario usuario);
    Task<bool> RemoverAsync(Guid id);
}