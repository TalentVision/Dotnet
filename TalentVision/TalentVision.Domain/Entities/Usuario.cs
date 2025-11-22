using System;

namespace TalentVision.Domain.Entities;

public class Usuario
{
    public Guid IdUsuario { get; set; } = Guid.NewGuid();
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string SenhaHash { get; set; } = string.Empty;
    public string TipoUsuario { get; set; } = string.Empty; // candidato / recrutador
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
}