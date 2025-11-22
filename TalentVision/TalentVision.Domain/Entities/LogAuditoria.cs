using System;

namespace TalentVision.Domain.Entities;

public class LogAuditoria
{
    public Guid IdLog { get; set; } = Guid.NewGuid();
    public string? TabelaAfetada { get; set; }
    public string? Operacao { get; set; }  
    public DateTime DataOperacao { get; set; } = DateTime.UtcNow;
    public Guid? IdUsuarioAcao { get; set; }
    public string? Descricao { get; set; }
}