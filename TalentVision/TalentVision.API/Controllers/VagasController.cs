using Microsoft.AspNetCore.Mvc;
using TalentVision.Application.Dtos;
using TalentVision.Application.Services;
using TalentVision.Domain.Entities;

namespace TalentVision.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class VagasController : ControllerBase
{
    private readonly IVagaService _service;

    public VagasController(IVagaService service)
    {
        _service = service;
    }

    // GET: api/vagas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VagaReadDto>>> Get()
    {
        var vagas = await _service.ListarAsync();

        var dtos = vagas.Select(v => new VagaReadDto
        {
            IdVaga = v.IdVaga,
            Titulo = v.Titulo,
            Empresa = v.Empresa,
            Localizacao = v.Localizacao,
            IdRecrutador = v.IdRecrutador,
            PublicadoEm = v.PublicadoEm
        });

        return Ok(dtos);
    }

    // GET: api/vagas/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<VagaReadDto>> GetById(Guid id)
    {
        var vaga = await _service.ObterPorIdAsync(id);
        if (vaga is null)
            return NotFound();

        var dto = new VagaReadDto
        {
            IdVaga = vaga.IdVaga,
            Titulo = vaga.Titulo,
            Empresa = vaga.Empresa,
            Localizacao = vaga.Localizacao,
            IdRecrutador = vaga.IdRecrutador,
            PublicadoEm = vaga.PublicadoEm
        };

        return Ok(dto);
    }

    // POST: api/vagas
    [HttpPost]
    public async Task<ActionResult<VagaReadDto>> Post(VagaCreateDto dto)
    {
        var entity = new Vaga
        {
            Titulo = dto.Titulo,
            Descricao = dto.Descricao,
            Empresa = dto.Empresa,
            Localizacao = dto.Localizacao,
            IdRecrutador = dto.IdRecrutador
        };

        var criada = await _service.CriarAsync(entity);

        var readDto = new VagaReadDto
        {
            IdVaga = criada.IdVaga,
            Titulo = criada.Titulo,
            Empresa = criada.Empresa,
            Localizacao = criada.Localizacao,
            IdRecrutador = criada.IdRecrutador,
            PublicadoEm = criada.PublicadoEm
        };

        return CreatedAtAction(nameof(GetById), new { id = readDto.IdVaga }, readDto);
    }

    // PUT: api/vagas/{id}
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<VagaReadDto>> Put(Guid id, VagaUpdateDto dto)
    {
        var entity = new Vaga
        {
            IdVaga = id,
            Titulo = dto.Titulo,
            Descricao = dto.Descricao,
            Empresa = dto.Empresa,
            Localizacao = dto.Localizacao,
            IdRecrutador = dto.IdRecrutador
        };

        var atualizada = await _service.AtualizarAsync(id, entity);
        if (atualizada is null)
            return NotFound();

        var readDto = new VagaReadDto
        {
            IdVaga = atualizada.IdVaga,
            Titulo = atualizada.Titulo,
            Empresa = atualizada.Empresa,
            Localizacao = atualizada.Localizacao,
            IdRecrutador = atualizada.IdRecrutador,
            PublicadoEm = atualizada.PublicadoEm
        };

        return Ok(readDto);
    }

    // DELETE: api/vagas/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var ok = await _service.RemoverAsync(id);
        if (!ok)
            return NotFound();

        return NoContent();
    }
}
