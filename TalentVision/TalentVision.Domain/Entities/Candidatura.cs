using System;

namespace TalentVision.Domain.Entities;

public class Candidatura
{
    public Guid IdCandidatura { get; set; } = Guid.NewGuid();
    public Guid IdUsuario { get; set; }
    public Guid IdVaga { get; set; }
    
    public DateTime DataCandidatura { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "pendente"; // pendente/avaliado/reprovado/aceito
    public decimal? Compatibilidade { get; set; }
}