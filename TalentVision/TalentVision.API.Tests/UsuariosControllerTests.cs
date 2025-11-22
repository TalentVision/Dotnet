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
public class UsuariosControllerTests
{
    [Test]
    public async Task Get_DeveRetornarOkComListaDeUsuarios()
    {
        // Arrange
        var usuariosFake = new List<Usuario>
        {
            new Usuario
            {
                IdUsuario = Guid.NewGuid(),
                Nome = "Teste",
                Email = "teste@teste.com",
                SenhaHash = "hash",
                TipoUsuario = "candidato",
                CriadoEm = DateTime.UtcNow
            }
        };

        var serviceMock = new Mock<IUsuarioService>();
        serviceMock.Setup(s => s.ListarAsync())
            .ReturnsAsync(usuariosFake);

        var controller = new UsuariosController(serviceMock.Object)
        {
            // ⚠️ Isso aqui é o que faltava:
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        // Act
        ActionResult<IEnumerable<UsuarioReadDto>> resultado = await controller.Get();

        // Assert
        Assert.That(resultado, Is.Not.Null);

        var okResult = resultado.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null, "Resultado deve ser OkObjectResult");

        var dtos = okResult!.Value as IEnumerable<UsuarioReadDto>;
        Assert.That(dtos, Is.Not.Null, "Valor retornado deve ser lista de UsuarioReadDto");

        Assert.That(dtos!, Has.Exactly(1).Items);
    }
}