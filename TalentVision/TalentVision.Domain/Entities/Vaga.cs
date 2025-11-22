using System;

namespace TalentVision.Domain.Entities;

public class Vaga
{
    public Guid IdVaga { get; set; } = Guid.NewGuid();
    public string Titulo { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public string? Empresa { get; set; }
    public string? Localizacao { get; set; }
    public Guid? IdRecrutador { get; set; }  // FK de Usuario
    public DateTime PublicadoEm { get; set; } = DateTime.UtcNow;
}