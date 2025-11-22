using System;
using TalentVision.Domain.Enums;

namespace TalentVision.Domain.Entities;

public class VagaCompetencia
{
    public Guid IdVaga { get; set; }
    public TipoCompetencia Competencia { get; set; }
    public int Peso { get; set; } // 1 a 10
}