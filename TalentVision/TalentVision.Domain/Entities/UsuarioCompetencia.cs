using System;
using TalentVision.Domain.Enums;

namespace TalentVision.Domain.Entities;

public class UsuarioCompetencia
{
    public Guid IdUsuario { get; set; }
    public TipoCompetencia Competencia { get; set; }
    public int Nivel { get; set; } // 1 a 5
}