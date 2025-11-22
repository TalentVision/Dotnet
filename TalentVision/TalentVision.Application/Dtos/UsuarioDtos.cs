using System;

namespace TalentVision.Application.Dtos;

public class UsuarioCreateDto
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;      // vamos gravar em SenhaHash
    public string TipoUsuario { get; set; } = string.Empty; // candidato / recrutador
}

public class UsuarioUpdateDto
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public string TipoUsuario { get; set; } = string.Empty;
}

public class UsuarioReadDto
{
    public Guid IdUsuario { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string TipoUsuario { get; set; } = string.Empty;
    public DateTime CriadoEm { get; set; }
}