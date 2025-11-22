using Microsoft.AspNetCore.Mvc;
using TalentVision.Application.Dtos;
using TalentVision.Application.Services;
using TalentVision.Domain.Entities;

namespace TalentVision.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioService _service;

    public UsuariosController(IUsuarioService service)
    {
        _service = service;
    }

    // GET: api/v1/usuarios?page=1&pageSize=10
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioReadDto>>> Get(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        if (page <= 0) page = 1;
        if (pageSize <= 0 || pageSize > 100) pageSize = 10;

        var usuarios = await _service.ListarAsync();

        var total = usuarios.Count();
        var itens = usuarios
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var dtos = itens.Select(u => new UsuarioReadDto
        {
            IdUsuario = u.IdUsuario,
            Nome = u.Nome,
            Email = u.Email,
            TipoUsuario = u.TipoUsuario,
            CriadoEm = u.CriadoEm
        });

        Response.Headers["X-Total-Count"] = total.ToString();

        return Ok(dtos);
    }

    // GET: api/v1/usuarios/paginado?page=1&pageSize=10 (com HATEOAS)
    [HttpGet("paginado")]
    public async Task<ActionResult<object>> GetPaginado(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        if (page <= 0) page = 1;
        if (pageSize <= 0 || pageSize > 100) pageSize = 10;

        var usuarios = await _service.ListarAsync();

        var total = usuarios.Count();
        var itens = usuarios
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var dtos = itens.Select(u => new UsuarioReadDto
        {
            IdUsuario = u.IdUsuario,
            Nome = u.Nome,
            Email = u.Email,
            TipoUsuario = u.TipoUsuario,
            CriadoEm = u.CriadoEm
        });

        var baseUrl = $"{Request.Scheme}://{Request.Host}/api/v1/usuarios/paginado";

        var result = new
        {
            data = dtos,
            pagination = new
            {
                page,
                pageSize,
                total
            },
            links = new
            {
                self = $"{baseUrl}?page={page}&pageSize={pageSize}",
                next = (page * pageSize < total)
                    ? $"{baseUrl}?page={page + 1}&pageSize={pageSize}"
                    : null,
                prev = (page > 1)
                    ? $"{baseUrl}?page={page - 1}&pageSize={pageSize}"
                    : null
            }
        };

        return Ok(result);
    }

    // GET: api/v1/usuarios/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UsuarioReadDto>> GetById(Guid id)
    {
        var usuario = await _service.ObterPorIdAsync(id);
        if (usuario is null)
            return NotFound();

        var dto = new UsuarioReadDto
        {
            IdUsuario = usuario.IdUsuario,
            Nome = usuario.Nome,
            Email = usuario.Email,
            TipoUsuario = usuario.TipoUsuario,
            CriadoEm = usuario.CriadoEm
        };

        return Ok(dto);
    }

    // POST: api/v1/usuarios
    [HttpPost]
    public async Task<ActionResult<UsuarioReadDto>> Post(UsuarioCreateDto dto)
    {
        var entity = new Usuario
        {
            Nome = dto.Nome,
            Email = dto.Email,
            SenhaHash = dto.Senha,
            TipoUsuario = dto.TipoUsuario
        };

        var criado = await _service.CriarAsync(entity);

        var readDto = new UsuarioReadDto
        {
            IdUsuario = criado.IdUsuario,
            Nome = criado.Nome,
            Email = criado.Email,
            TipoUsuario = criado.TipoUsuario,
            CriadoEm = criado.CriadoEm
        };

        return CreatedAtAction(nameof(GetById), new { id = readDto.IdUsuario }, readDto);
    }

    // PUT: api/v1/usuarios/{id}
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<UsuarioReadDto>> Put(Guid id, UsuarioUpdateDto dto)
    {
        var entity = new Usuario
        {
            IdUsuario = id,
            Nome = dto.Nome,
            Email = dto.Email,
            SenhaHash = dto.Senha,
            TipoUsuario = dto.TipoUsuario
        };

        var atualizado = await _service.AtualizarAsync(id, entity);
        if (atualizado is null)
            return NotFound();

        var readDto = new UsuarioReadDto
        {
            IdUsuario = atualizado.IdUsuario,
            Nome = atualizado.Nome,
            Email = atualizado.Email,
            TipoUsuario = atualizado.TipoUsuario,
            CriadoEm = atualizado.CriadoEm
        };

        return Ok(readDto);
    }

    // DELETE: api/v1/usuarios/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var ok = await _service.RemoverAsync(id);
        if (!ok)
            return NotFound();

        return NoContent();
    }
}
