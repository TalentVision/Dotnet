using Microsoft.EntityFrameworkCore;
using TalentVision.Application.Services;
using TalentVision.Domain.Entities;
using TalentVision.Infrastructure.Data;

namespace TalentVision.Infrastructure.Services;

public class UsuarioService : IUsuarioService
{
    private readonly AppDbContext _context;

    public UsuarioService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Usuario>> ListarAsync()
    {
        return await _context.Usuarios.AsNoTracking().ToListAsync();
    }

    public async Task<Usuario?> ObterPorIdAsync(Guid id)
    {
        return await _context.Usuarios.FindAsync(id);
    }

    public async Task<Usuario> CriarAsync(Usuario usuario)
    {
        usuario.IdUsuario = Guid.NewGuid();
        usuario.CriadoEm = DateTime.UtcNow;

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return usuario;
    }

    public async Task<Usuario?> AtualizarAsync(Guid id, Usuario usuario)
    {
        var existente = await _context.Usuarios.FindAsync(id);
        if (existente is null)
            return null;

        existente.Nome = usuario.Nome;
        existente.Email = usuario.Email;
        existente.SenhaHash = usuario.SenhaHash;
        existente.TipoUsuario = usuario.TipoUsuario;

        await _context.SaveChangesAsync();
        return existente;
    }

    public async Task<bool> RemoverAsync(Guid id)
    {
        var existente = await _context.Usuarios.FindAsync(id);
        if (existente is null)
            return false;

        _context.Usuarios.Remove(existente);
        await _context.SaveChangesAsync();
        return true;
    }
}