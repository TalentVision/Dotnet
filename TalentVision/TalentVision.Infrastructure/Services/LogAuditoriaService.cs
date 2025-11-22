using Microsoft.EntityFrameworkCore;
using TalentVision.Application.Services;
using TalentVision.Domain.Entities;
using TalentVision.Infrastructure.Data;

namespace TalentVision.Infrastructure.Services;

public class LogAuditoriaService : ILogAuditoriaService
{
    private readonly AppDbContext _context;

    public LogAuditoriaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<LogAuditoria>> ListarAsync()
    {
        return await _context.LogsAuditoria
            .AsNoTracking()
            .OrderByDescending(l => l.DataOperacao)
            .ToListAsync();
    }

    public async Task RegistrarAsync(string tabela, string operacao, Guid? idUsuarioAcao, string? descricao)
    {
        var log = new LogAuditoria
        {
            IdLog = Guid.NewGuid(),
            TabelaAfetada = tabela,
            Operacao = operacao,
            DataOperacao = DateTime.UtcNow,
            IdUsuarioAcao = idUsuarioAcao,
            Descricao = descricao
        };

        _context.LogsAuditoria.Add(log);
        await _context.SaveChangesAsync();
    }
}