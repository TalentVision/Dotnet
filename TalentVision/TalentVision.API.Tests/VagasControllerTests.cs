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
public class VagasControllerTests
{
    [Test]
    public async Task Get_DeveRetornarOkComListaDeVagas()
    {
        // Arrange
        var vagasFake = new List<Vaga>
        {
            new Vaga
            {
                IdVaga = Guid.NewGuid(),
                Titulo = "Desenvolvedor .NET",
                Descricao = "Criar APIs REST",
                Empresa = "TalentVision Tech",
                Localizacao = "São Paulo - SP",
                IdRecrutador = Guid.NewGuid(),
                PublicadoEm = DateTime.UtcNow
            }
        };

        var serviceMock = new Mock<IVagaService>();
        serviceMock.Setup(s => s.ListarAsync())
            .ReturnsAsync(vagasFake);

        var controller = new VagasController(serviceMock.Object)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        // Act
        ActionResult<IEnumerable<VagaReadDto>> resultado = await controller.Get();

        // Assert
        Assert.That(resultado, Is.Not.Null);

        var ok = resultado.Result as OkObjectResult;
        Assert.That(ok, Is.Not.Null, "Resultado deve ser OkObjectResult");

        var dtos = ok!.Value as IEnumerable<VagaReadDto>;
        Assert.That(dtos, Is.Not.Null, "Valor retornado deve ser lista de VagaReadDto");
        Assert.That(dtos!, Has.Exactly(1).Items);
    }
}