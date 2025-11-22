using System;

namespace TalentVision.Application.Dtos;

public class CandidaturaCreateDto
{
    public Guid IdUsuario { get; set; }
    public Guid IdVaga { get; set; }
}

public class CandidaturaUpdateDto
{
    public string Status { get; set; } = "pendente"; // pendente / avaliado / reprovado / aceito
    public decimal? Compatibilidade { get; set; }
}

public class CandidaturaReadDto
{
    public Guid IdCandidatura { get; set; }
    public Guid IdUsuario { get; set; }
    public Guid IdVaga { get; set; }
    public string Status { get; set; } = "pendente";
    public decimal? Compatibilidade { get; set; }
    public DateTime DataCandidatura { get; set; }
}