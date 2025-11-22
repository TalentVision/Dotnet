using System;

namespace TalentVision.Application.Dtos;

public class CompetenciaCreateDto
{
    public string Nome { get; set; } = string.Empty;
    public string? Categoria { get; set; }
}

public class CompetenciaUpdateDto
{
    public string Nome { get; set; } = string.Empty;
    public string? Categoria { get; set; }
}

public class CompetenciaReadDto
{
    public Guid IdCompetencia { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Categoria { get; set; }
}