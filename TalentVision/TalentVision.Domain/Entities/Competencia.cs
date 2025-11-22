using System;

namespace TalentVision.Domain.Entities;

public class Competencia
{
    public Guid IdCompetencia { get; set; } = Guid.NewGuid();
    public string Nome { get; set; } = string.Empty;
    public string? Categoria { get; set; }
}