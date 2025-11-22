using System;

namespace TalentVision.Application.Dtos;

public class VagaCreateDto
{
    public string Titulo { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public string? Empresa { get; set; }
    public string? Localizacao { get; set; }
    public Guid? IdRecrutador { get; set; }
}

public class VagaUpdateDto
{
    public string Titulo { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public string? Empresa { get; set; }
    public string? Localizacao { get; set; }
    public Guid? IdRecrutador { get; set; }
}

public class VagaReadDto
{
    public Guid IdVaga { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Empresa { get; set; }
    public string? Localizacao { get; set; }
    public Guid? IdRecrutador { get; set; }
    public DateTime PublicadoEm { get; set; }
}