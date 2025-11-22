using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TalentVision.API.Controllers;
using TalentVision.Application.Dtos;
using TalentVision.Application.Services;
using TalentVision.Domain.Entities;


namespace TalentVision.API.Tests;

[TestFixture]
public class CompetenciasControllerTests
{
    [Test]
    public async Task Get_DeveRetornarListaDeCompetencias()
    {
        // Arrange
        var competenciasFake = new List<Competencia>
        {
            new Competencia
            {
                IdCompetencia = Guid.NewGuid(),
                Nome = "Comunicação",
                Categoria = "Soft Skill"
            }
        };

        var serviceMock = new Mock<ICompetenciaService>();
        serviceMock.Setup(s => s.ListarAsync())
            .ReturnsAsync(competenciasFake);

        var controller = new CompetenciasController(serviceMock.Object);

        // Act
        ActionResult<IEnumerable<CompetenciaReadDto>> resultado = await controller.Get();

        // Assert
        Assert.That(resultado, Is.Not.Null);

        var ok = resultado.Result as OkObjectResult;
        Assert.That(ok, Is.Not.Null, "Resultado deve ser OkObjectResult");

        var lista = ok!.Value as IEnumerable<CompetenciaReadDto>;
        Assert.That(lista, Is.Not.Null, "Valor retornado deve ser lista de CompetenciaReadDto");
        Assert.That(lista!, Has.Exactly(1).Items);
    }
}