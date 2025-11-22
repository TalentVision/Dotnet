using Microsoft.EntityFrameworkCore;
using TalentVision.Application.Services;
using TalentVision.Domain.Entities;
using TalentVision.Infrastructure.Data;

namespace TalentVision.Infrastructure.Services;

public class CompetenciaService : ICompetenciaService
{
    private readonly AppDbContext _context;

    public CompetenciaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Competencia>> ListarAsync()
    {
        return await _context.Competencias.AsNoTracking().ToListAsync();
    }

    public async Task<Competencia?> ObterPorIdAsync(Guid id)
    {
        return await _context.Competencias.FindAsync(id);
    }

    public async Task<Competencia> CriarAsync(Competencia competencia)
    {
        competencia.IdCompetencia = Guid.NewGuid();
        _context.Competencias.Add(competencia);
        await _context.SaveChangesAsync();
        return competencia;
    }

    public async Task<Competencia?> AtualizarAsync(Guid id, Competencia competencia)
    {
        var existente = await _context.Competencias.FindAsync(id);
        if (existente is null)
            return null;

        existente.Nome = competencia.Nome;
        existente.Categoria = competencia.Categoria;

        await _context.SaveChangesAsync();
        return existente;
    }

    public async Task<bool> RemoverAsync(Guid id)
    {
        var existente = await _context.Competencias.FindAsync(id);
        if (existente is null)
            return false;

        _context.Competencias.Remove(existente);
        await _context.SaveChangesAsync();
        return true;
    }
}