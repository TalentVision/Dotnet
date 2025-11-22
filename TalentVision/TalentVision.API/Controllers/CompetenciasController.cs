using Microsoft.AspNetCore.Mvc;
using TalentVision.Application.Dtos;
using TalentVision.Application.Services;
using TalentVision.Domain.Entities;

namespace TalentVision.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CompetenciasController : ControllerBase
{
    private readonly ICompetenciaService _service;

    public CompetenciasController(ICompetenciaService service)
    {
        _service = service;
    }

    // GET: api/competencias
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompetenciaReadDto>>> Get()
    {
        var competencias = await _service.ListarAsync();

        var dtos = competencias.Select(c => new CompetenciaReadDto
        {
            IdCompetencia = c.IdCompetencia,
            Nome = c.Nome,
            Categoria = c.Categoria
        });

        return Ok(dtos);
    }

    // GET: api/competencias/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CompetenciaReadDto>> GetById(Guid id)
    {
        var competencia = await _service.ObterPorIdAsync(id);
        if (competencia is null)
            return NotFound();

        var dto = new CompetenciaReadDto
        {
            IdCompetencia = competencia.IdCompetencia,
            Nome = competencia.Nome,
            Categoria = competencia.Categoria
        };

        return Ok(dto);
    }

    // POST: api/competencias
    [HttpPost]
    public async Task<ActionResult<CompetenciaReadDto>> Post(CompetenciaCreateDto dto)
    {
        var entity = new Competencia
        {
            Nome = dto.Nome,
            Categoria = dto.Categoria
        };

        var criada = await _service.CriarAsync(entity);

        var readDto = new CompetenciaReadDto
        {
            IdCompetencia = criada.IdCompetencia,
            Nome = criada.Nome,
            Categoria = criada.Categoria
        };

        return CreatedAtAction(nameof(GetById), new { id = readDto.IdCompetencia }, readDto);
    }

    // PUT: api/competencias/{id}
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<CompetenciaReadDto>> Put(Guid id, CompetenciaUpdateDto dto)
    {
        var entity = new Competencia
        {
            IdCompetencia = id,
            Nome = dto.Nome,
            Categoria = dto.Categoria
        };

        var atualizada = await _service.AtualizarAsync(id, entity);
        if (atualizada is null)
            return NotFound();

        var readDto = new CompetenciaReadDto
        {
            IdCompetencia = atualizada.IdCompetencia,
            Nome = atualizada.Nome,
            Categoria = atualizada.Categoria
        };

        return Ok(readDto);
    }

    // DELETE: api/competencias/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var ok = await _service.RemoverAsync(id);
        if (!ok)
            return NotFound();

        return NoContent();
    }
}
