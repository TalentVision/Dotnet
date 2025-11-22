using Microsoft.AspNetCore.Mvc;
using TalentVision.Application.Dtos;
using TalentVision.Application.Services;
using TalentVision.Domain.Entities;

namespace TalentVision.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CandidaturasController : ControllerBase
{
    private readonly ICandidaturaService _service;

    public CandidaturasController(ICandidaturaService service)
    {
        _service = service;
    }

    // GET: api/candidaturas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CandidaturaReadDto>>> Get()
    {
        var candidaturas = await _service.ListarAsync();

        var dtos = candidaturas.Select(c => new CandidaturaReadDto
        {
            IdCandidatura = c.IdCandidatura,
            IdUsuario = c.IdUsuario,
            IdVaga = c.IdVaga,
            Status = c.Status,
            Compatibilidade = c.Compatibilidade,
            DataCandidatura = c.DataCandidatura
        });

        return Ok(dtos);
    }

    // GET: api/candidaturas/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CandidaturaReadDto>> GetById(Guid id)
    {
        var candidatura = await _service.ObterPorIdAsync(id);
        if (candidatura is null)
            return NotFound();

        var dto = new CandidaturaReadDto
        {
            IdCandidatura = candidatura.IdCandidatura,
            IdUsuario = candidatura.IdUsuario,
            IdVaga = candidatura.IdVaga,
            Status = candidatura.Status,
            Compatibilidade = candidatura.Compatibilidade,
            DataCandidatura = candidatura.DataCandidatura
        };

        return Ok(dto);
    }

    // POST: api/candidaturas
    [HttpPost]
    public async Task<ActionResult<CandidaturaReadDto>> Post(CandidaturaCreateDto dto)
    {
        var entity = new Candidatura
        {
            IdUsuario = dto.IdUsuario,
            IdVaga = dto.IdVaga,
            Status = "pendente"
        };

        var criada = await _service.CriarAsync(entity);

        var readDto = new CandidaturaReadDto
        {
            IdCandidatura = criada.IdCandidatura,
            IdUsuario = criada.IdUsuario,
            IdVaga = criada.IdVaga,
            Status = criada.Status,
            Compatibilidade = criada.Compatibilidade,
            DataCandidatura = criada.DataCandidatura
        };

        return CreatedAtAction(nameof(GetById), new { id = readDto.IdCandidatura }, readDto);
    }

    // PUT: api/candidaturas/{id}
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<CandidaturaReadDto>> Put(Guid id, CandidaturaUpdateDto dto)
    {
        var entity = new Candidatura
        {
            IdCandidatura = id,
            Status = dto.Status,
            Compatibilidade = dto.Compatibilidade
        };

        var atualizada = await _service.AtualizarAsync(id, entity);
        if (atualizada is null)
            return NotFound();

        var readDto = new CandidaturaReadDto
        {
            IdCandidatura = atualizada.IdCandidatura,
            IdUsuario = atualizada.IdUsuario,
            IdVaga = atualizada.IdVaga,
            Status = atualizada.Status,
            Compatibilidade = atualizada.Compatibilidade,
            DataCandidatura = atualizada.DataCandidatura
        };

        return Ok(readDto);
    }

    // DELETE: api/candidaturas/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var ok = await _service.RemoverAsync(id);
        if (!ok)
            return NotFound();

        return NoContent();
    }
}
