using SmartOficina.Api.Domain.Interfaces;
using System.Net;

namespace SmartOficina.UnitTest.SmartOficina.API.Controllers;

#pragma warning disable 8604, 8602, 8629, 8600, 8620
public class SubServicoControllerTest
{
    Mock<ISubCategoriaServicoService> _serviceMock = new();
    private static DefaultHttpContext CreateFakeClaims(ICollection<SubCategoriaServicoDto> subCategoriaServicos)
    {
        var fakeHttpContext = new DefaultHttpContext();
        ClaimsIdentity identity = new(
            new[] {
                        new Claim("PrestadorId", subCategoriaServicos.First().PrestadorId.ToString()),
                        new Claim("UserName", "Teste"),
                        new Claim("IdUserLogin", subCategoriaServicos.First().PrestadorId.ToString())

            }
        );
        fakeHttpContext.User = new System.Security.Claims.ClaimsPrincipal(identity);
        return fakeHttpContext;
    }

    private SubServicoController CreateFakeController(ICollection<SubCategoriaServicoDto> subCategoriaServicos)
    {
        return new SubServicoController(_serviceMock.Object) { ControllerContext = new ControllerContext() { HttpContext = CreateFakeClaims(subCategoriaServicos) } };
    }

    private ICollection<SubCategoriaServicoDto> RetornaListaSubCategoriaServico(string categoria, string desc)
    {
        return new List<SubCategoriaServicoDto>() { new SubCategoriaServicoDto() { Titulo = categoria, Desc = desc, Id = Guid.NewGuid(), CategoriaId = Guid.NewGuid(), PrestadorId = Guid.NewGuid() } };
    }

    private SubCategoriaServicoDto RetornaSubCategoriaServico(string categoria, string desc)
    {
        return new SubCategoriaServicoDto() { Titulo = categoria, Desc = desc, Id = Guid.NewGuid(), CategoriaId = Guid.NewGuid() };
    }

    [Fact]
    public async Task Deve_Retornar_Lista_SubCategoria_RetornoOk()
    {
        //Arranger
        ICollection<SubCategoriaServicoDto> subCategoriaServicosListaFake = RetornaListaSubCategoriaServico("Teste", "teste desc");
        _serviceMock.Setup(s => s.GetAllSubCategoria(It.IsAny<SubCategoriaServicoDto>())).ReturnsAsync(subCategoriaServicosListaFake);
        //Act
        SubServicoController controllerSubCategoria = CreateFakeController(subCategoriaServicosListaFake);
        var response = await controllerSubCategoria.GetAll(string.Empty, string.Empty);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as ICollection<SubCategoriaServicoDto>;

        //Assert
        _serviceMock.Verify(s => s.GetAllSubCategoria(It.IsAny<SubCategoriaServicoDto>()), Times.Once);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(result.Count, subCategoriaServicosListaFake.Count);
        Assert.Equal(result.First().Titulo, subCategoriaServicosListaFake.First().Titulo);
        Assert.Equal(result.First().Desc, subCategoriaServicosListaFake.First().Desc);
    }

    [Fact]
    public async Task NaoDeve_Retornar_Lista_SubCategoria_RetornoNoContent()
    {
        //Arranger
        ICollection<SubCategoriaServicoDto> subCategoriaServicosListaFake = RetornaListaSubCategoriaServico("Teste", "teste desc");
        ICollection<SubCategoriaServicoDto> subCategoriaServicosFakeNull = null;

        _serviceMock.Setup(s => s.GetAllSubCategoria(It.IsAny<SubCategoriaServicoDto>())).ReturnsAsync(subCategoriaServicosFakeNull);
        //Act
        SubServicoController controllerSubCategoria = CreateFakeController(subCategoriaServicosListaFake);
        var response = await controllerSubCategoria.GetAll(string.Empty, string.Empty);
        var okResult = response as NoContentResult;

        //Assert
        _serviceMock.Verify(s => s.GetAllSubCategoria(It.IsAny<SubCategoriaServicoDto>()), Times.Once);
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);
    }

    [Fact]
    public async Task NaoDeve_Retornar_Um_SubCategoria_RetornoNoContent()
    {
        //Arranger
        ICollection<SubCategoriaServicoDto> subCategoriaServicosListaFake = RetornaListaSubCategoriaServico("Teste", "teste desc");
        SubCategoriaServicoDto subCategoriaServicoFake = RetornaSubCategoriaServico("Teste", "teste desc");
        SubCategoriaServicoDto subCategoriaServicoFakeNull = null;
        _serviceMock.Setup(s => s.FindByIdSubCategoria(It.IsAny<Guid>())).ReturnsAsync(subCategoriaServicoFakeNull);
        //Act
        SubServicoController controllerSubCategoria = CreateFakeController(subCategoriaServicosListaFake);
        var response = await controllerSubCategoria.GetId(subCategoriaServicoFake.Id.Value);
        var okResult = response as NoContentResult;

        //Assert
        _serviceMock.Verify(s => s.FindByIdSubCategoria(It.IsAny<Guid>()), Times.Once);
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);
    }



    [Fact]
    public async Task Deve_Retornar_Um_SubCategoria_RetornoOk()
    {
        //Arranger
        ICollection<SubCategoriaServicoDto> subCategoriaServicosListaFake = RetornaListaSubCategoriaServico("Teste", "teste desc");
        SubCategoriaServicoDto subCategoriaServicoFake = RetornaSubCategoriaServico("Teste", "teste desc");
        _serviceMock.Setup(s => s.FindByIdSubCategoria(It.IsAny<Guid>())).ReturnsAsync(subCategoriaServicoFake);
        //Act
        SubServicoController controllerSubCategoria = CreateFakeController(subCategoriaServicosListaFake);
        var response = await controllerSubCategoria.GetId(subCategoriaServicoFake.Id.Value);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as SubCategoriaServicoDto;

        //Assert
        _serviceMock.Verify(s => s.FindByIdSubCategoria(It.IsAny<Guid>()), Times.Once);
        Assert.NotNull(result);
        Assert.Equal(result.Desc, subCategoriaServicoFake.Desc);
        Assert.Equal(result.Titulo, subCategoriaServicoFake.Titulo);
    }

    [Fact]
    public async Task NaoDeve_Retornar_Um_SubCategoria_RetornoBadRequest()
    {
        //Arranger
        ICollection<SubCategoriaServicoDto> subCategoriaServicosListaFake = RetornaListaSubCategoriaServico("Teste", "teste desc");
        SubCategoriaServicoDto subCategoriaServicoFake = RetornaSubCategoriaServico("Teste", "teste desc");
        _serviceMock.Setup(s => s.FindByIdSubCategoria(It.IsAny<Guid>())).ReturnsAsync(subCategoriaServicoFake);
        SubServicoController controllerSubCategoria = CreateFakeController(subCategoriaServicosListaFake);
        controllerSubCategoria.ModelState.AddModelError("key", "error message");
        //Act
        var response = await controllerSubCategoria.GetId(subCategoriaServicoFake.Id.Value);
        var okResult = response as ObjectResult;

        //Assert
        _serviceMock.Verify(s => s.FindByIdSubCategoria(It.IsAny<Guid>()), Times.Never);
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.BadRequest, okResult.StatusCode);
    }
    [Fact]
    public async Task Deve_Cadastrar_SubCategoria_RetornoOk()
    {
        //Arranger
        ICollection<SubCategoriaServicoDto> subCategoriaServicosListaFake = RetornaListaSubCategoriaServico("Teste", "teste desc");
        SubCategoriaServicoDto subCategoriaServicoFake = RetornaSubCategoriaServico("Teste", "teste desc");
        _serviceMock.Setup(s => s.CreateSubCategoria(It.IsAny<SubCategoriaServicoDto>())).ReturnsAsync(subCategoriaServicoFake);
        SubServicoController controllerSubCategoria = CreateFakeController(subCategoriaServicosListaFake);
        //Act
        var response = await controllerSubCategoria.AddAsync(subCategoriaServicoFake);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as SubCategoriaServicoDto;

        //Assert
        _serviceMock.Verify(s => s.CreateSubCategoria(It.IsAny<SubCategoriaServicoDto>()), Times.Once);
        Assert.NotNull(result);
        Assert.Equal(result.Desc, subCategoriaServicoFake.Desc);
        Assert.Equal(result.Titulo, subCategoriaServicoFake.Titulo);
    }

    [Fact]
    public async Task NaoDeve_Cadastrar_SubCategoria_RetornoNoContent()
    {
        //Arranger
        ICollection<SubCategoriaServicoDto> subCategoriaServicosListaFake = RetornaListaSubCategoriaServico("Teste", "teste desc");
        SubCategoriaServicoDto subCategoriaServicoFake = RetornaSubCategoriaServico("Teste", "teste desc");
        SubCategoriaServicoDto subCategoriaServicoFakeNull = null;

        _serviceMock.Setup(s => s.CreateSubCategoria(It.IsAny<SubCategoriaServicoDto>())).ReturnsAsync(subCategoriaServicoFakeNull);
        //Act
        SubServicoController controllerSubCategoria = CreateFakeController(subCategoriaServicosListaFake);
        var response = await controllerSubCategoria.AddAsync(subCategoriaServicoFake);
        var okResult = response as NoContentResult;

        //Assert
        _serviceMock.Verify(s => s.CreateSubCategoria(It.IsAny<SubCategoriaServicoDto>()), Times.Once);
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);
    }

    [Fact]
    public async Task NaoDeve_Cadastrar_SubCategoria_RetornoBadRequest()
    {
        //Arranger
        ICollection<SubCategoriaServicoDto> subCategoriaServicosListaFake = RetornaListaSubCategoriaServico("Teste", "teste desc");
        SubCategoriaServicoDto subCategoriaServicoFake = RetornaSubCategoriaServico("Teste", "teste desc");
        _serviceMock.Setup(s => s.CreateSubCategoria(It.IsAny<SubCategoriaServicoDto>())).ReturnsAsync(subCategoriaServicoFake);
        SubServicoController controllerSubCategoria = CreateFakeController(subCategoriaServicosListaFake);
        controllerSubCategoria.ModelState.AddModelError("key", "error message");
        //Act
        var response = await controllerSubCategoria.AddAsync(subCategoriaServicoFake);
        var okResult = response as ObjectResult;

        //Assert
        _serviceMock.Verify(s => s.CreateSubCategoria(It.IsAny<SubCategoriaServicoDto>()), Times.Never);
        Assert.NotNull(okResult);
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Deve_Atualizar_Retornar_Veiculo_RetornoOk()
    {
        //Arranger
        ICollection<SubCategoriaServicoDto> subCategoriaServicosListaFake = RetornaListaSubCategoriaServico("Teste", "teste desc");
        SubCategoriaServicoDto subCategoriaServicoFake = RetornaSubCategoriaServico("Teste", "teste desc");
        _serviceMock.Setup(s => s.UpdateSubCategoria(It.IsAny<SubCategoriaServicoDto>())).ReturnsAsync(subCategoriaServicoFake);
        SubServicoController controllerSubCategoria = CreateFakeController(subCategoriaServicosListaFake);
        //Act
        var response = await controllerSubCategoria.AtualizarSubServico(subCategoriaServicoFake);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as SubCategoriaServicoDto;

        //Assert
        _serviceMock.Verify(s => s.UpdateSubCategoria(It.IsAny<SubCategoriaServicoDto>()), Times.Once);
        Assert.NotNull(result);
        Assert.Equal(result.Titulo, subCategoriaServicoFake.Titulo);
        Assert.Equal(result.Desc, subCategoriaServicoFake.Desc);
    }

    [Fact]
    public async Task NaoDeve_Atualizar_Retornar_Veiculo_RetornoNoContent()
    {
        //Arranger
        ICollection<SubCategoriaServicoDto> subCategoriaServicosListaFake = RetornaListaSubCategoriaServico("Teste", "teste desc");
        SubCategoriaServicoDto subCategoriaServicoFake = RetornaSubCategoriaServico("Teste", "teste desc");
        SubCategoriaServicoDto subCategoriaServicoFakeNull = null;
        _serviceMock.Setup(s => s.UpdateSubCategoria(It.IsAny<SubCategoriaServicoDto>())).ReturnsAsync(subCategoriaServicoFakeNull);
        SubServicoController controllerSubCategoria = CreateFakeController(subCategoriaServicosListaFake);
        //Act
        var response = await controllerSubCategoria.AtualizarSubServico(subCategoriaServicoFake);
        var okResult = response as NoContentResult;

        //Assert
        _serviceMock.Verify(s => s.UpdateSubCategoria(It.IsAny<SubCategoriaServicoDto>()), Times.Once);
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);
    }

    [Fact]
    public async Task NaoDeve_Atualizar_Retornar_Veiculo_RetornoBadRequest()
    {
        //Arranger
        ICollection<SubCategoriaServicoDto> subCategoriaServicosListaFake = RetornaListaSubCategoriaServico("Teste", "teste desc");
        SubCategoriaServicoDto subCategoriaServicoFake = RetornaSubCategoriaServico("Teste", "teste desc");
        _serviceMock.Setup(s => s.UpdateSubCategoria(It.IsAny<SubCategoriaServicoDto>())).ReturnsAsync(subCategoriaServicoFake);
        SubServicoController controllerSubCategoria = CreateFakeController(subCategoriaServicosListaFake);
        controllerSubCategoria.ModelState.AddModelError("key", "error message");
        //Act
        var response = await controllerSubCategoria.AtualizarSubServico(subCategoriaServicoFake);
        var okResult = response as ObjectResult;

        //Assert
        _serviceMock.Verify(s => s.UpdateSubCategoria(It.IsAny<SubCategoriaServicoDto>()), Times.Never);
        Assert.NotNull(okResult);
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Deve_Desativar_Retornar_Veiculo_RetornoOk()
    {
        //Arranger
        ICollection<SubCategoriaServicoDto> subCategoriaServicosListaFake = RetornaListaSubCategoriaServico("Teste", "teste desc");
        SubCategoriaServicoDto subCategoriaServicoFake = RetornaSubCategoriaServico("Teste", "teste desc");
        _serviceMock.Setup(s => s.Desabled(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(subCategoriaServicoFake);
        //Act
        SubServicoController controllerSubCategoria = CreateFakeController(subCategoriaServicosListaFake);
        var response = await controllerSubCategoria.DesativarSubServico(subCategoriaServicoFake.Id.Value);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as SubCategoriaServicoDto;

        //Assert
        _serviceMock.Verify(s => s.Desabled(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        Assert.NotNull(result);
        Assert.Equal(result.Titulo, subCategoriaServicoFake.Titulo);
        Assert.Equal(result.Desc, subCategoriaServicoFake.Desc);

    }

    [Fact]
    public async Task NaoDeve_Desativar_Retornar_Veiculo_RetornoNoContent()
    {
        //Arranger
        ICollection<SubCategoriaServicoDto> subCategoriaServicosListaFake = RetornaListaSubCategoriaServico("Teste", "teste desc");
        SubCategoriaServicoDto subCategoriaServicoFake = RetornaSubCategoriaServico("Teste", "teste desc");
        SubCategoriaServicoDto veiculoFakeNull = null;
        _serviceMock.Setup(s => s.Desabled(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(veiculoFakeNull);
        //Act
        SubServicoController controllerSubCategoria = CreateFakeController(subCategoriaServicosListaFake);
        var response = await controllerSubCategoria.DesativarSubServico(subCategoriaServicoFake.Id.Value);
        var okResult = response as NoContentResult;

        //Assert
        _serviceMock.Verify(s => s.Desabled(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);
    }

    [Fact]
    public async Task NaoDeve_Desativar_Retornar_Veiculo_RetornoBadRequest()
    {
        //Arranger
        ICollection<SubCategoriaServicoDto> subCategoriaServicosListaFake = RetornaListaSubCategoriaServico("Teste", "teste desc");
        SubCategoriaServicoDto subCategoriaServicoFake = RetornaSubCategoriaServico("Teste", "teste desc");
        _serviceMock.Setup(s => s.Desabled(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(subCategoriaServicoFake);
        SubServicoController controllerSubCategoria = CreateFakeController(subCategoriaServicosListaFake);
        controllerSubCategoria.ModelState.AddModelError("key", "error message");
        //Act
        var response = await controllerSubCategoria.DesativarSubServico(subCategoriaServicoFake.Id.Value);
        var okResult = response as ObjectResult;

        //Assert
        _serviceMock.Verify(s => s.Desabled(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Never);
        Assert.NotNull(okResult);
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.BadRequest);
    }


    [Fact]
    public async Task Deve_Deletar_Retornar_Veiculo_RetornoOk()
    {
        //Arranger
        ICollection<SubCategoriaServicoDto> subCategoriaServicosListaFake = RetornaListaSubCategoriaServico("Teste", "teste desc");
        SubCategoriaServicoDto subCategoriaServicoFake = RetornaSubCategoriaServico("Teste", "teste desc");
        _serviceMock.Setup(s => s.Delete(It.IsAny<Guid>()));
        //Act
        SubServicoController controllerSubCategoria = CreateFakeController(subCategoriaServicosListaFake);
        var response = await controllerSubCategoria.DeletarSubServico(subCategoriaServicoFake.Id.Value);
        var okResult = response as OkObjectResult;

        //Assert
        _serviceMock.Verify(s => s.Delete(It.IsAny<Guid>()), Times.Once);
        Assert.NotNull(response);
    }

    [Fact]
    public async Task NaoDeve_Deletar_Retornar_Veiculo_RetornoBadRequest()
    {
        //Arranger
        ICollection<SubCategoriaServicoDto> subCategoriaServicosListaFake = RetornaListaSubCategoriaServico("Teste", "teste desc");
        SubCategoriaServicoDto subCategoriaServicoFake = RetornaSubCategoriaServico("Teste", "teste desc");
        _serviceMock.Setup(s => s.Delete(It.IsAny<Guid>()));
        SubServicoController controllerSubCategoria = CreateFakeController(subCategoriaServicosListaFake);
        controllerSubCategoria.ModelState.AddModelError("key", "error message");
        //Act
        var response = await controllerSubCategoria.DeletarSubServico(subCategoriaServicoFake.Id.Value);
        var okResult = response as ObjectResult;

        //Assert
        _serviceMock.Verify(s => s.Delete(It.IsAny<Guid>()), Times.Never);
        Assert.NotNull(okResult);
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.BadRequest);
    }

}
