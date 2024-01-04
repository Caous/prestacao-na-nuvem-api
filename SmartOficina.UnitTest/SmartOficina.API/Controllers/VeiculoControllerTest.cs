using SmartOficina.Api.Domain.Interfaces;
using System.Net;

namespace SmartOficina.UnitTest.SmartOficina.API.Controllers;

public class VeiculoControllerTest
{
    Mock<IVeiculoService> _serviceMock = new();
    private static DefaultHttpContext CreateFakeClaims(ICollection<VeiculoDto> veiculoFake)
    {
        var fakeHttpContext = new DefaultHttpContext();
        ClaimsIdentity identity = new(
            new[] {
                        new Claim("PrestadorId", veiculoFake.First().PrestadorId.ToString()),
                        new Claim("UserName", "Teste"),
                        new Claim("IdUserLogin", veiculoFake.First().PrestadorId.ToString())

            }
        );
        fakeHttpContext.User = new System.Security.Claims.ClaimsPrincipal(identity);
        return fakeHttpContext;
    }
    private VeiculoController CreateFakeController(ICollection<VeiculoDto> categoriasFake)
    {
        return new VeiculoController(_serviceMock.Object) { ControllerContext = new ControllerContext() { HttpContext = CreateFakeClaims(categoriasFake) } };
    }

    [Fact]
    public async Task Deve_Retornar_Lista_Veiculo_RetornoOk()
    {
        //Arranger
        ICollection<VeiculoDto> veiculosFake = RetornaListaVeiculoFake("Hyundai", "i30", "ebv7898");
        _serviceMock.Setup(s => s.GetAllVeiculos(It.IsAny<VeiculoDto>())).ReturnsAsync(veiculosFake);
        //Act
        VeiculoController controllerVeiculo = CreateFakeController(veiculosFake);
        var response = await controllerVeiculo.GetAll();
        var okResult = response as OkObjectResult;
        var result = okResult.Value as ICollection<VeiculoDto>;

        //Assert
        _serviceMock.Verify(s => s.GetAllVeiculos(It.IsAny<VeiculoDto>()), Times.Once);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(result.Count, veiculosFake.Count);
        Assert.Equal(result.First().Marca, veiculosFake.First().Marca);
        Assert.Equal(result.First().Modelo, veiculosFake.First().Modelo);
        Assert.Equal(result.First().Placa, veiculosFake.First().Placa);
    }

      [Fact]
    public async Task NaoDeve_Retornar_Lista_Veiculo_RetornoNoContent()
    {
        //Arranger
        ICollection<VeiculoDto> veiculosFake = RetornaListaVeiculoFake("Hyundai", "i30", "ebv7898");
        ICollection<VeiculoDto> veiculosFakeNull = null;

        _serviceMock.Setup(s => s.GetAllVeiculos(It.IsAny<VeiculoDto>())).ReturnsAsync(veiculosFakeNull);
        //Act
        VeiculoController controllerVeiculo = CreateFakeController(veiculosFake);
        var response = await controllerVeiculo.GetAll();
        var okResult = response as NoContentResult;

        //Assert
        _serviceMock.Verify(s => s.GetAllVeiculos(It.IsAny<VeiculoDto>()), Times.Once);
        Assert.NotNull(okResult);
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.NoContent);
    }
    [Fact]
    public async Task NaoDeve_Retornar_Um_Veiculo_RetornoNoContent()
    {
        //Arranger
        ICollection<VeiculoDto> veiculosFake = RetornaListaVeiculoFake("Hyundai", "i30", "ebv7898");
        VeiculoDto veiculoFake = RetornaVeiculoFake("Hyundai", "i30", "ebv7898");
        VeiculoDto veiculosFakeNull = null;
        _serviceMock.Setup(s => s.FindByIdVeiculos(It.IsAny<Guid>())).ReturnsAsync(veiculosFakeNull);
        //Act
        VeiculoController controllerVeiculo = CreateFakeController(veiculosFake);
        var response = await controllerVeiculo.GetId(veiculoFake.Id.Value);
        var okResult = response as NoContentResult;

        //Assert
        _serviceMock.Verify(s => s.FindByIdVeiculos(It.IsAny<Guid>()), Times.Once);
        Assert.NotNull(okResult);
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Deve_Retornar_Um_Veiculo_RetornoOk()
    {
        //Arranger
        ICollection<VeiculoDto> veiculosFake = RetornaListaVeiculoFake("Hyundai", "i30", "ebv7898");
        VeiculoDto veiculoFake = RetornaVeiculoFake("Hyundai", "i30", "ebv7898");
        _serviceMock.Setup(s => s.FindByIdVeiculos(It.IsAny<Guid>())).ReturnsAsync(veiculoFake);
        //Act
        VeiculoController controllerVeiculo = CreateFakeController(veiculosFake);
        var response = await controllerVeiculo.GetId(veiculoFake.Id.Value);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as VeiculoDto;

        //Assert
        _serviceMock.Verify(s => s.FindByIdVeiculos(It.IsAny<Guid>()), Times.Once);
        Assert.NotNull(result);
        Assert.Equal(result.Marca, veiculoFake.Marca);
        Assert.Equal(result.Modelo, veiculoFake.Modelo);
        Assert.Equal(result.Placa, veiculoFake.Placa);
    }

    [Fact]
    public async Task NaoDeve_Retornar_Um_Veiculo_RetornoBadRequest()
    {
        //Arranger
        ICollection<VeiculoDto> veiculosFake = RetornaListaVeiculoFake("Hyundai", "i30", "ebv7898");
        VeiculoDto veiculoFake = RetornaVeiculoFake("Hyundai", "i30", "ebv7898");
        _serviceMock.Setup(s => s.FindByIdVeiculos(It.IsAny<Guid>())).ReturnsAsync(veiculoFake);
        VeiculoController controllerVeiculo = CreateFakeController(veiculosFake);
        controllerVeiculo.ModelState.AddModelError("key", "error message");
        //Act
        var response = await controllerVeiculo.GetId(veiculoFake.Id.Value);
        var okResult = response as ObjectResult;

        //Assert
        _serviceMock.Verify(s => s.FindByIdVeiculos(It.IsAny<Guid>()), Times.Never);
        Assert.NotNull(okResult);
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.BadRequest);
    }
    [Fact]
    public async Task Deve_Retornar_Veiculo_RetornoOk()
    {
        //Arranger
        ICollection<VeiculoDto> veiculosFake = RetornaListaVeiculoFake("Hyundai", "i30", "ebv7898");
        VeiculoDto veiculoFake = RetornaVeiculoFake("Hyundai", "i30", "ebv7898");
        _serviceMock.Setup(s => s.CreateVeiculos(It.IsAny<VeiculoDto>())).ReturnsAsync(veiculoFake);
        //Act
        VeiculoController controllerVeiculo = CreateFakeController(veiculosFake);
        var response = await controllerVeiculo.Add(veiculoFake);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as VeiculoDto;

        //Assert
        _serviceMock.Verify(s => s.CreateVeiculos(It.IsAny<VeiculoDto>()), Times.Once);
        Assert.NotNull(result);
        Assert.Equal(result.Marca, veiculoFake.Marca);
        Assert.Equal(result.Modelo, veiculoFake.Modelo);
        Assert.Equal(result.Placa, veiculoFake.Placa);
    }

    [Fact]
    public async Task NaoDeve_Retornar_Veiculo_RetornoNoContent()
    {
        //Arranger
        ICollection<VeiculoDto> veiculosFake = RetornaListaVeiculoFake("Hyundai", "i30", "ebv7898");
        VeiculoDto veiculoFake = RetornaVeiculoFake("Hyundai", "i30", "ebv7898");
        VeiculoDto veiculoFakeNull = null;
        _serviceMock.Setup(s => s.CreateVeiculos(It.IsAny<VeiculoDto>())).ReturnsAsync(veiculoFakeNull);
        //Act
        VeiculoController controllerVeiculo = CreateFakeController(veiculosFake);
        var response = await controllerVeiculo.Add(veiculoFake);
        var okResult = response as NoContentResult;

        //Assert
        _serviceMock.Verify(s => s.CreateVeiculos(It.IsAny<VeiculoDto>()), Times.Once);
        Assert.NotNull(okResult);
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task NaoDeve_Retornar_Veiculo_RetornoBadRequest()
    {
        //Arranger
        ICollection<VeiculoDto> veiculosFake = RetornaListaVeiculoFake("Hyundai", "i30", "ebv7898");
        VeiculoDto veiculoFake = RetornaVeiculoFake("Hyundai", "i30", "ebv7898");
        _serviceMock.Setup(s => s.CreateVeiculos(It.IsAny<VeiculoDto>())).ReturnsAsync(veiculoFake);
        VeiculoController controllerVeiculo = CreateFakeController(veiculosFake);
        controllerVeiculo.ModelState.AddModelError("key", "error message");
        //Act
        var response = await controllerVeiculo.Add(veiculoFake);
        var okResult = response as ObjectResult;

        //Assert
        _serviceMock.Verify(s => s.CreateVeiculos(It.IsAny<VeiculoDto>()), Times.Never);
        Assert.NotNull(okResult);
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Deve_Atualizar_Retornar_Veiculo_RetornoOk()
    {
        //Arranger
        ICollection<VeiculoDto> veiculosFake = RetornaListaVeiculoFake("Hyundai", "i30", "ebv7898");
        VeiculoDto veiculoFake = RetornaVeiculoFake("Hyundai", "i30", "ebv7898");
        _serviceMock.Setup(s => s.UpdateVeiculos(It.IsAny<VeiculoDto>())).ReturnsAsync(veiculoFake);
        //Act
        VeiculoController controllerVeiculo = CreateFakeController(veiculosFake);
        var response = await controllerVeiculo.AtualizarVeiculo(veiculoFake);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as VeiculoDto;

        //Assert
        _serviceMock.Verify(s => s.UpdateVeiculos(It.IsAny<VeiculoDto>()), Times.Once);
        Assert.NotNull(result);
        Assert.Equal(result.Marca, veiculoFake.Marca);
        Assert.Equal(result.Modelo, veiculoFake.Modelo);
        Assert.Equal(result.Placa, veiculoFake.Placa);
    }

    [Fact]
    public async Task NaoDeve_Atualizar_Retornar_Veiculo_RetornoNoContent()
    {
        //Arranger
        ICollection<VeiculoDto> veiculosFake = RetornaListaVeiculoFake("Hyundai", "i30", "ebv7898");
        VeiculoDto veiculoFake = RetornaVeiculoFake("Hyundai", "i30", "ebv7898");
        VeiculoDto veiculoFakeNull = null;
        _serviceMock.Setup(s => s.UpdateVeiculos(It.IsAny<VeiculoDto>())).ReturnsAsync(veiculoFakeNull);
        //Act
        VeiculoController controllerVeiculo = CreateFakeController(veiculosFake);
        var response = await controllerVeiculo.AtualizarVeiculo(veiculoFake);
        var okResult = response as NoContentResult;

        //Assert
        _serviceMock.Verify(s => s.UpdateVeiculos(It.IsAny<VeiculoDto>()), Times.Once);
        Assert.NotNull(okResult);
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task NaoDeve_Atualizar_Retornar_Veiculo_RetornoBadRequest()
    {
        //Arranger
        ICollection<VeiculoDto> veiculosFake = RetornaListaVeiculoFake("Hyundai", "i30", "ebv7898");
        VeiculoDto veiculoFake = RetornaVeiculoFake("Hyundai", "i30", "ebv7898");
        _serviceMock.Setup(s => s.UpdateVeiculos(It.IsAny<VeiculoDto>())).ReturnsAsync(veiculoFake);
        VeiculoController controllerVeiculo = CreateFakeController(veiculosFake);
        controllerVeiculo.ModelState.AddModelError("key", "error message");
        //Act
        var response = await controllerVeiculo.AtualizarVeiculo(veiculoFake);
        var okResult = response as ObjectResult;

        //Assert
        _serviceMock.Verify(s => s.UpdateVeiculos(It.IsAny<VeiculoDto>()), Times.Never);
        Assert.NotNull(okResult);
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Deve_Desativar_Retornar_Veiculo_RetornoOk()
    {
        //Arranger
        ICollection<VeiculoDto> veiculosFake = RetornaListaVeiculoFake("Hyundai", "i30", "ebv7898");
        VeiculoDto veiculoFake = RetornaVeiculoFake("Hyundai", "i30", "ebv7898");
        _serviceMock.Setup(s => s.Desabled(It.IsAny<Guid>())).ReturnsAsync(veiculoFake);
        //Act
        VeiculoController controllerVeiculo = CreateFakeController(veiculosFake);
        var response = await controllerVeiculo.DesativarVeiculo(veiculoFake.Id.Value);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as VeiculoDto;

        //Assert
        _serviceMock.Verify(s => s.Desabled(It.IsAny<Guid>()), Times.Once);
        Assert.NotNull(result);
        Assert.Equal(result.Marca, veiculoFake.Marca);
        Assert.Equal(result.Modelo, veiculoFake.Modelo);
        Assert.Equal(result.Placa, veiculoFake.Placa);
    }

    [Fact]
    public async Task NaoDeve_Desativar_Retornar_Veiculo_RetornoNoContent()
    {
        //Arranger
        ICollection<VeiculoDto> veiculosFake = RetornaListaVeiculoFake("Hyundai", "i30", "ebv7898");
        VeiculoDto veiculoFake = RetornaVeiculoFake("Hyundai", "i30", "ebv7898");
        VeiculoDto veiculoFakeNull = null;
        _serviceMock.Setup(s => s.Desabled(It.IsAny<Guid>())).ReturnsAsync(veiculoFakeNull);
        //Act
        VeiculoController controllerVeiculo = CreateFakeController(veiculosFake);
        var response = await controllerVeiculo.DesativarVeiculo(veiculoFake.Id.Value);
        var okResult = response as NoContentResult;

        //Assert
        _serviceMock.Verify(s => s.Desabled(It.IsAny<Guid>()), Times.Once);
        Assert.NotNull(okResult);
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task NaoDeve_Desativar_Retornar_Veiculo_RetornoBadRequest()
    {
        //Arranger
        ICollection<VeiculoDto> veiculosFake = RetornaListaVeiculoFake("Hyundai", "i30", "ebv7898");
        VeiculoDto veiculoFake = RetornaVeiculoFake("Hyundai", "i30", "ebv7898");
        _serviceMock.Setup(s => s.Desabled(It.IsAny<Guid>())).ReturnsAsync(veiculoFake);
        VeiculoController controllerVeiculo = CreateFakeController(veiculosFake);
        controllerVeiculo.ModelState.AddModelError("key", "error message");
        //Act
        var response = await controllerVeiculo.DesativarVeiculo(veiculoFake.Id.Value);
        var okResult = response as ObjectResult;

        //Assert
        _serviceMock.Verify(s => s.Desabled(It.IsAny<Guid>()), Times.Never);
        Assert.NotNull(okResult);
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.BadRequest);
    }


    [Fact]
    public async Task Deve_Deletar_Retornar_Veiculo_RetornoOk()
    {
        //Arranger
        ICollection<VeiculoDto> veiculosFake = RetornaListaVeiculoFake("Hyundai", "i30", "ebv7898");
        VeiculoDto veiculoFake = RetornaVeiculoFake("Hyundai", "i30", "ebv7898");
        _serviceMock.Setup(s => s.Delete(It.IsAny<Guid>()));
        //Act
        VeiculoController controllerVeiculo = CreateFakeController(veiculosFake);
        var response = await controllerVeiculo.DeletarVeiculo(veiculoFake.Id.Value);
        var okResult = response as OkObjectResult;

        //Assert
        _serviceMock.Verify(s => s.Delete(It.IsAny<Guid>()), Times.Once);
        Assert.NotNull(response);
    }

    [Fact]
    public async Task NaoDeve_Deletar_Retornar_Veiculo_RetornoBadRequest()
    {
        //Arranger
        ICollection<VeiculoDto> veiculosFake = RetornaListaVeiculoFake("Hyundai", "i30", "ebv7898");
        VeiculoDto veiculoFake = RetornaVeiculoFake("Hyundai", "i30", "ebv7898");
        _serviceMock.Setup(s => s.Delete(It.IsAny<Guid>()));
        VeiculoController controllerVeiculo = CreateFakeController(veiculosFake);
        controllerVeiculo.ModelState.AddModelError("key", "error message");
        //Act
        var response = await controllerVeiculo.DeletarVeiculo(veiculoFake.Id.Value);
        var okResult = response as ObjectResult;

        //Assert
        _serviceMock.Verify(s => s.Delete(It.IsAny<Guid>()), Times.Never);
        Assert.NotNull(okResult);
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.BadRequest);
    }

    private VeiculoDto RetornaVeiculoFake(string marca, string modelo, string placa)
    {
        return  new VeiculoDto() { Marca = marca, Modelo = modelo, Placa = placa, Id = Guid.NewGuid(), UsrCadastro = Guid.NewGuid(), PrestadorId = Guid.NewGuid() } ;
    }

    private ICollection<VeiculoDto> RetornaListaVeiculoFake(string marca, string modelo, string placa)
    {
        return new List<VeiculoDto>() { new VeiculoDto() { Marca = marca, Modelo = modelo, Placa = placa, Id = Guid.NewGuid(), UsrCadastro = Guid.NewGuid(), PrestadorId = Guid.NewGuid() } };
    }
}
