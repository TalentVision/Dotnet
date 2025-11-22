using Microsoft.AspNetCore.Mvc;
using TalentVision.Application.Services;

namespace TalentVision.API.Controllers;

[ApiController]
[Route("api/v2/[controller]")]
public class UsuariosV2Controller : ControllerBase
{
    private readonly IUsuarioService _service;

    public UsuariosV2Controller(IUsuarioService service)
    {
        _service = service;
    }

    // GET: api/v2/usuarios
    // Exemplo: retorna visão simplificada
    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> Get()
    {
        var usuarios = await _service.ListarAsync();

        var resultado = usuarios.Select(u => new
        {
            u.Nome,
            u.Email
        });

        return Ok(resultado);
    }
}