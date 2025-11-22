using Microsoft.EntityFrameworkCore;
using TalentVision.Application.Services;
using TalentVision.Domain.Entities;
using TalentVision.Infrastructure.Data;

namespace TalentVision.Infrastructure.Services;

public class VagaService : IVagaService
{
    private readonly AppDbContext _context;

    public VagaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Vaga>> ListarAsync()
    {
        return await _context.Vagas.AsNoTracking().ToListAsync();
    }

    public async Task<Vaga?> ObterPorIdAsync(Guid id)
    {
        return await _context.Vagas.FindAsync(id);
    }

    public async Task<Vaga> CriarAsync(Vaga vaga)
    {
        vaga.IdVaga = Guid.NewGuid();
        vaga.PublicadoEm = DateTime.UtcNow;

        _context.Vagas.Add(vaga);
        await _context.SaveChangesAsync();

        return vaga;
    }

    public async Task<Vaga?> AtualizarAsync(Guid id, Vaga vaga)
    {
        var existente = await _context.Vagas.FindAsync(id);
        if (existente is null)
            return null;

        existente.Titulo = vaga.Titulo;
        existente.Descricao = vaga.Descricao;
        existente.Empresa = vaga.Empresa;
        existente.Localizacao = vaga.Localizacao;
        existente.IdRecrutador = vaga.IdRecrutador;

        await _context.SaveChangesAsync();
        return existente;
    }

    public async Task<bool> RemoverAsync(Guid id)
    {
        var existente = await _context.Vagas.FindAsync(id);
        if (existente is null)
            return false;

        _context.Vagas.Remove(existente);
        await _context.SaveChangesAsync();
        return true;
    }
}