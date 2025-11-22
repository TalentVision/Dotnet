using Microsoft.EntityFrameworkCore;
using TalentVision.Application.Services;
using TalentVision.Domain.Entities;
using TalentVision.Infrastructure.Data;

namespace TalentVision.Infrastructure.Services;

public class CandidaturaService : ICandidaturaService
{
    private readonly AppDbContext _context;

    public CandidaturaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Candidatura>> ListarAsync()
    {
        return await _context.Candidaturas.AsNoTracking().ToListAsync();
    }

    public async Task<Candidatura?> ObterPorIdAsync(Guid id)
    {
        return await _context.Candidaturas.FindAsync(id);
    }

    public async Task<Candidatura> CriarAsync(Candidatura candidatura)
    {
        candidatura.IdCandidatura = Guid.NewGuid();
        candidatura.DataCandidatura = DateTime.UtcNow;
        candidatura.Status = candidatura.Status is null or "" ? "pendente" : candidatura.Status;

        _context.Candidaturas.Add(candidatura);
        await _context.SaveChangesAsync();

        return candidatura;
    }

    public async Task<Candidatura?> AtualizarAsync(Guid id, Candidatura candidatura)
    {
        var existente = await _context.Candidaturas.FindAsync(id);
        if (existente is null)
            return null;

        existente.Status = candidatura.Status;
        existente.Compatibilidade = candidatura.Compatibilidade;

        await _context.SaveChangesAsync();
        return existente;
    }

    public async Task<bool> RemoverAsync(Guid id)
    {
        var existente = await _context.Candidaturas.FindAsync(id);
        if (existente is null)
            return false;

        _context.Candidaturas.Remove(existente);
        await _context.SaveChangesAsync();
        return true;
    }
}