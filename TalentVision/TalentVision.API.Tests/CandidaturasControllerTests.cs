using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TalentVision.API.Controllers;
using TalentVision.Application.Dtos;
using TalentVision.Application.Services;
using TalentVision.Domain.Entities;

namespace TalentVision.API.Tests;

[TestFixture]
public class CandidaturasControllerTests
{
    [Test]
    public async Task Post_DeveCriarCandidaturaERetornarCreated()
    {
        // Arrange
        var dto = new CandidaturaCreateDto
        {
            IdUsuario = Guid.NewGuid(),
            IdVaga = Guid.NewGuid()
        };

        var entidadeCriada = new Candidatura
        {
            IdCandidatura = Guid.NewGuid(),
            IdUsuario = dto.IdUsuario,
            IdVaga = dto.IdVaga,
            DataCandidatura = DateTime.UtcNow,
            Status = "pendente",
            Compatibilidade = null
        };

        var serviceMock = new Mock<ICandidaturaService>();
        serviceMock.Setup(s => s.CriarAsync(It.IsAny<Candidatura>()))
            .ReturnsAsync(entidadeCriada);

        var controller = new CandidaturasController(serviceMock.Object);

        // Act
        ActionResult<CandidaturaReadDto> resultado = await controller.Post(dto);

        // Assert
        Assert.That(resultado, Is.Not.Null);

        var created = resultado.Result as CreatedAtActionResult;
        Assert.That(created, Is.Not.Null, "Resultado deve ser CreatedAtActionResult");

        var readDto = created!.Value as CandidaturaReadDto;
        Assert.That(readDto, Is.Not.Null, "Valor retornado deve ser CandidaturaReadDto");
        Assert.That(readDto!.IdUsuario, Is.EqualTo(dto.IdUsuario));
        Assert.That(readDto.IdVaga, Is.EqualTo(dto.IdVaga));
    }
}