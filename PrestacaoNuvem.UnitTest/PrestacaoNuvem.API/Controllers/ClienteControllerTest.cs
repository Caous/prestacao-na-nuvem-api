using FluentValidation;
using PrestacaoNuvem.Api.Domain.Interfaces;
using System.Net;

namespace PrestacaoNuvem.UnitTest.PrestacaoNuvem.API.Controllers;

#pragma warning disable 8604, 8602, 8629, 8600, 8620
public class ClienteControllerTest
{
    private readonly Mock<IClienteService> _serviceMock = new();
    private readonly Mock<IValidator<ClienteDto>> _validationMock = new();

    private static DefaultHttpContext CreateFakeClaims(ICollection<ClienteDto> clientes)
    {
        var fakeHttpContext = new DefaultHttpContext();
        ClaimsIdentity identity = new(
            new[] {
                        new Claim("PrestadorId", clientes.First().PrestadorId.ToString()),
                        new Claim("UserName", "Teste"),
                        new Claim("IdUserLogin", clientes.First().PrestadorId.ToString())

            }
        );
        fakeHttpContext.User = new System.Security.Claims.ClaimsPrincipal(identity);
        return fakeHttpContext;
    }

    private ClienteController GenerateControllerFake(ICollection<ClienteDto> clientesFake)
    {
        return new ClienteController(_serviceMock.Object, _validationMock.Object) { ControllerContext = new ControllerContext() { HttpContext = CreateFakeClaims(clientesFake) } };
    }

    [Fact]
    public async Task Deve_Retornar_ListaDeCliente_Usando_ParametroID()
    {
        //Arrange
        ICollection<ClienteDto> clientesDtoFake = CriaListaClienteDtoFake();
        _serviceMock.Setup(s => s.GetAllCliente(It.IsAny<ClienteDto>())).ReturnsAsync(clientesDtoFake);
        ClienteController controllerCliente = GenerateControllerFake(clientesDtoFake);
        //Act
        var response = await controllerCliente.GetAll(string.Empty, string.Empty, string.Empty);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as ICollection<ClienteDto>;
        //Assert
        _serviceMock.Verify(s => s.GetAllCliente(It.IsAny<ClienteDto>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(result.First().Telefone, clientesDtoFake.First().Telefone);
        Assert.Equal(result.First().CPF, clientesDtoFake.First().CPF);
        Assert.Equal(result.First().DataCadastro, clientesDtoFake.First().DataCadastro);
        Assert.Equal(result.First().Id, clientesDtoFake.First().Id);
        Assert.Equal(result.First().Email, clientesDtoFake.First().Email);
        Assert.Equal(result.First().Endereco, clientesDtoFake.First().Endereco);
        Assert.Equal(result.First().Nome, clientesDtoFake.First().Nome);
        Assert.Equal(result.First().Rg, clientesDtoFake.First().Rg);
    }

    [Fact]
    public async Task NaoDeve_Retornar_ListaDeCliente_Usando_ParametroID()
    {
        //Arrange
        ICollection<ClienteDto> clientesDtoFake = CriaListaClienteDtoFake();
        ICollection<ClienteDto> clientesDtoFakeLista = null;
        _serviceMock.Setup(s => s.GetAllCliente(It.IsAny<ClienteDto>())).ReturnsAsync(clientesDtoFakeLista);
        ClienteController controllerCliente = GenerateControllerFake(clientesDtoFake);
        //Act
        var response = await controllerCliente.GetAll(string.Empty, string.Empty, string.Empty);
        var okResult = response as NoContentResult;

        //Assert
        _serviceMock.Verify(s => s.GetAllCliente(It.IsAny<ClienteDto>()), Times.Once());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);

    }


    [Fact]
    public async Task NaoDeve_Retornar_ListaDeCliente_RetornoBadRequest()
    {
        //Arrange
        ICollection<ClienteDto> clientesDtoFake = CriaListaClienteDtoFake();
        _serviceMock.Setup(s => s.GetAllCliente(It.IsAny<ClienteDto>())).ReturnsAsync(clientesDtoFake);
        ClienteController controllerCliente = GenerateControllerFake(clientesDtoFake);
        controllerCliente.ModelState.AddModelError("key", "error message");
        //Act
        var response = await controllerCliente.GetAll(string.Empty, string.Empty, string.Empty);
        var okResult = response as ObjectResult;

        //Assert
        _serviceMock.Verify(s => s.GetAllCliente(It.IsAny<ClienteDto>()), Times.Never());
        Assert.NotNull(okResult);
        Equals(okResult.StatusCode, (int)HttpStatusCode.BadRequest);

    }


    [Fact]
    public async Task Deve_Cadastrar_Um_Cliente_RetornarSucesso()
    {
        //Arrange
        ICollection<ClienteDto> clientesDtoFake = CriaListaClienteDtoFake();
        _serviceMock.Setup(s => s.CreateCliente(It.IsAny<ClienteDto>())).ReturnsAsync(clientesDtoFake.First());
        ClienteController controllerCliente = GenerateControllerFake(clientesDtoFake);
        //Act
        var response = await controllerCliente.AddAsync(clientesDtoFake.First());
        var okResult = response as OkObjectResult;
        var result = okResult.Value as ClienteDto;
        //Assert
        _serviceMock.Verify(s => s.CreateCliente(It.IsAny<ClienteDto>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(result.Telefone, clientesDtoFake.First().Telefone);
        Assert.Equal(result.CPF, clientesDtoFake.First().CPF);
        Assert.Equal(result.DataCadastro, clientesDtoFake.First().DataCadastro);
        Assert.Equal(result.Id, clientesDtoFake.First().Id);
        Assert.Equal(result.Email, clientesDtoFake.First().Email);
        Assert.Equal(result.Endereco, clientesDtoFake.First().Endereco);
        Assert.Equal(result.Nome, clientesDtoFake.First().Nome);
        Assert.Equal(result.Rg, clientesDtoFake.First().Rg);
    }

    [Fact]
    public async Task NaoDeve_Cadastrar_Um_Cliente_RetornarNoContent()
    {
        //Arrange
        ICollection<ClienteDto> clientesDtoFake = CriaListaClienteDtoFake();
        ClienteDto clienteDtoFake = null;
        _serviceMock.Setup(s => s.CreateCliente(It.IsAny<ClienteDto>())).ReturnsAsync(clienteDtoFake);
        ClienteController controllerCliente = GenerateControllerFake(clientesDtoFake);
        //Act
        var response = await controllerCliente.AddAsync(clientesDtoFake.First());
        var okResult = response as NoContentResult;

        //Assert
        _serviceMock.Verify(s => s.CreateCliente(It.IsAny<ClienteDto>()), Times.Once());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);
    }

    [Fact]
    public async Task NaoDeve_AdicionarCliente_RetornoBadRequest()
    {
        //Arrange
        ICollection<ClienteDto> clientesDtoFake = CriaListaClienteDtoFake();
        ClienteDto clienteDtoFake = null;
        _serviceMock.Setup(s => s.CreateCliente(It.IsAny<ClienteDto>())).ReturnsAsync(clienteDtoFake);
        ClienteController controllerCliente = GenerateControllerFake(clientesDtoFake);
        controllerCliente.ModelState.AddModelError("key", "error message");
        //Act
        var response = await controllerCliente.AddAsync(clienteDtoFake);
        var okResult = response as ObjectResult;

        //Assert
        _serviceMock.Verify(s => s.CreateCliente(It.IsAny<ClienteDto>()), Times.Never());
        Assert.NotNull(okResult);
        Equals(okResult.StatusCode, (int)HttpStatusCode.BadRequest);

    }

    [Fact]
    public async Task Deve_Retornarar_Um_Cliente_RetornarSucesso()
    {
        //Arrange
        ICollection<ClienteDto> clientesDtoFake = CriaListaClienteDtoFake();
        ClienteDto clienteDtoFake = CriaClienteDtoFake(Guid.NewGuid());
        _serviceMock.Setup(s => s.FindByIdCliente(It.IsAny<Guid>())).ReturnsAsync(clienteDtoFake);
        ClienteController controllerCliente = GenerateControllerFake(clientesDtoFake);
        //Act
        var response = await controllerCliente.GetId(Guid.NewGuid());
        var okResult = response as OkObjectResult;
        var result = okResult.Value as ClienteDto;
        //Assert
        _serviceMock.Verify(s => s.FindByIdCliente(It.IsAny<Guid>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(result.Telefone, clienteDtoFake.Telefone);
        Assert.Equal(result.CPF, clienteDtoFake.CPF);
        Assert.Equal(result.DataCadastro, clienteDtoFake.DataCadastro);
        Assert.Equal(result.Id, clienteDtoFake.Id);
        Assert.Equal(result.Email, clienteDtoFake.Email);
        Assert.Equal(result.Endereco, clienteDtoFake.Endereco);
        Assert.Equal(result.Nome, clienteDtoFake.Nome);
        Assert.Equal(result.Rg, clienteDtoFake.Rg);
    }

    [Fact]
    public async Task NaoDeve_Retornarar_Um_Cliente_RetornarNoContet()
    {
        //Arrange
        ICollection<ClienteDto> clientesDtoFake = CriaListaClienteDtoFake();
        ClienteDto clienteDtoFake = null;
        _serviceMock.Setup(s => s.FindByIdCliente(It.IsAny<Guid>())).ReturnsAsync(clienteDtoFake);
        ClienteController controllerCliente = GenerateControllerFake(clientesDtoFake);
        //Act
        var response = await controllerCliente.GetId(Guid.NewGuid());
        var okResult = response as NoContentResult;
        //Assert
        _serviceMock.Verify(s => s.FindByIdCliente(It.IsAny<Guid>()), Times.Once());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);
    }

    [Fact]
    public async Task NaoDeve_Retornarar_Um_Cliente_RetornaBadRequest()
    {
        //Arrange
        ICollection<ClienteDto> clientesDtoFake = CriaListaClienteDtoFake();
        ClienteDto clienteDtoFake = null;
        _serviceMock.Setup(s => s.FindByIdCliente(It.IsAny<Guid>())).ReturnsAsync(clienteDtoFake);
        ClienteController controllerCliente = GenerateControllerFake(clientesDtoFake);
        controllerCliente.ModelState.AddModelError("key", "error message");
        //Act
        var response = await controllerCliente.GetId(Guid.NewGuid());
        var okResult = response as ObjectResult;

        //Assert
        _serviceMock.Verify(s => s.CreateCliente(It.IsAny<ClienteDto>()), Times.Never());
        Assert.NotNull(okResult);
        Equals((int)HttpStatusCode.BadRequest, okResult.StatusCode);
    }

    [Fact]
    public async Task Deve_Atualizar_Um_Cliente_RetornarSucesso()
    {
        //Arrange
        ICollection<ClienteDto> clientesDtoFake = CriaListaClienteDtoFake();
        ClienteDto clienteDtoFake = CriaClienteDtoFake(Guid.NewGuid());
        _serviceMock.Setup(s => s.UpdateCliente(It.IsAny<ClienteDto>())).ReturnsAsync(clienteDtoFake);
        ClienteController controllerCliente = GenerateControllerFake(clientesDtoFake);
        //Act
        var response = await controllerCliente.AtualizarCliente(clienteDtoFake);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as ClienteDto;
        //Assert
        _serviceMock.Verify(s => s.UpdateCliente(It.IsAny<ClienteDto>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(result.Telefone, clienteDtoFake.Telefone);
        Assert.Equal(result.CPF, clienteDtoFake.CPF);
        Assert.Equal(result.DataCadastro, clienteDtoFake.DataCadastro);
        Assert.Equal(result.Id, clienteDtoFake.Id);
        Assert.Equal(result.Email, clienteDtoFake.Email);
        Assert.Equal(result.Endereco, clienteDtoFake.Endereco);
        Assert.Equal(result.Nome, clienteDtoFake.Nome);
        Assert.Equal(result.Rg, clienteDtoFake.Rg);
    }

    [Fact]
    public async Task NaoDeve_Atualizar_Um_Cliente_RetornarNoContent()
    {
        //Arrange
        ICollection<ClienteDto> clientesDtoFake = CriaListaClienteDtoFake();
        ClienteDto clienteDtoFake = CriaClienteDtoFake(Guid.NewGuid());
        ClienteDto clienteDtoFakeNull = null;
        _serviceMock.Setup(s => s.UpdateCliente(It.IsAny<ClienteDto>())).ReturnsAsync(clienteDtoFakeNull);
        ClienteController controllerCliente = GenerateControllerFake(clientesDtoFake);
        //Act
        var response = await controllerCliente.AtualizarCliente(clienteDtoFake);
        var okResult = response as NoContentResult;

        //Assert
        _serviceMock.Verify(s => s.UpdateCliente(It.IsAny<ClienteDto>()), Times.Once());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);

    }


    [Fact]
    public async Task NaoDeve_Atualizar_Um_Cliente_RetornarBadRequest()
    {
        //Arrange
        ICollection<ClienteDto> clientesDtoFake = CriaListaClienteDtoFake();
        ClienteDto clienteDtoFake = CriaClienteDtoFake(Guid.NewGuid());
        _serviceMock.Setup(s => s.UpdateCliente(It.IsAny<ClienteDto>())).ReturnsAsync(clienteDtoFake);
        ClienteController controllerCliente = GenerateControllerFake(clientesDtoFake);
        //Act
        controllerCliente.ModelState.AddModelError("key", "error message");
        var response = await controllerCliente.AtualizarCliente(clienteDtoFake);
        var okResult = response as ObjectResult;

        //Assert
        _serviceMock.Verify(s => s.UpdateCliente(It.IsAny<ClienteDto>()), Times.Never());
        Assert.NotNull(okResult);
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.BadRequest);

    }

    [Fact]
    public async Task NaoDeve_Atualizar_Um_Cliente_RetornarBadRequest_PassandoIdNull()
    {
        //Arrange
        ICollection<ClienteDto> clientesDtoFake = CriaListaClienteDtoFake();
        ClienteDto clienteDtoFake = CriaClienteDtoFake(Guid.NewGuid());
        clienteDtoFake.Id = null;
        _serviceMock.Setup(s => s.UpdateCliente(It.IsAny<ClienteDto>())).ReturnsAsync(clienteDtoFake);
        ClienteController controllerCliente = GenerateControllerFake(clientesDtoFake);
        //Act
        var response = await controllerCliente.AtualizarCliente(clienteDtoFake);
        var okResult = response as ObjectResult;

        //Assert
        _serviceMock.Verify(s => s.UpdateCliente(It.IsAny<ClienteDto>()), Times.Never());
        Assert.NotNull(okResult);
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.BadRequest);

    }

    [Fact]
    public async Task Deve_Deletar_Um_Cliente_RetornarSucesso()
    {
        //Arrange
        ICollection<ClienteDto> clientesDtoFake = CriaListaClienteDtoFake();
        ClienteDto clienteDtoFake = CriaClienteDtoFake(Guid.NewGuid());
        _serviceMock.Setup(s => s.Delete(It.IsAny<Guid>()));
        ClienteController controllerCliente = GenerateControllerFake(clientesDtoFake);

        //Act
        var response = await controllerCliente.DeletarCliente(clienteDtoFake.Id.Value);

        //Assert
        _serviceMock.Verify(s => s.Delete(It.IsAny<Guid>()), Times.Once());
        Assert.NotNull(response);

    }

    [Fact]
    public async Task NaoDeve_Deletar_Um_Cliente_RetornarBadRequest()
    {
        //Arrange
        ICollection<ClienteDto> clientesDtoFake = CriaListaClienteDtoFake();
        ClienteDto clienteDtoFake = CriaClienteDtoFake(Guid.NewGuid());
        _serviceMock.Setup(s => s.Delete(It.IsAny<Guid>()));
        ClienteController controllerCliente = GenerateControllerFake(clientesDtoFake);
        //Act
        controllerCliente.ModelState.AddModelError("key", "error message");
        var response = await controllerCliente.DeletarCliente(clienteDtoFake.Id.Value);
        var okResult = response as ObjectResult;

        //Assert
        _serviceMock.Verify(s => s.Delete(It.IsAny<Guid>()), Times.Never());
        Assert.NotNull(okResult);
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.BadRequest);

    }

    [Fact]
    public async Task NaoDeve_DesativarUmCliente_RetornoBadRequest()
    {
        //Arrange
        ICollection<ClienteDto> clientesDtoFake = CriaListaClienteDtoFake();
        ClienteDto clienteDtoFake = CriaClienteDtoFake(Guid.NewGuid());
        _serviceMock.Setup(s => s.Desabled(It.IsAny<Guid>(),It.IsAny<Guid>())).ReturnsAsync(clienteDtoFake);
        ClienteController controllerCliente = GenerateControllerFake(clientesDtoFake);
        //Act
        controllerCliente.ModelState.AddModelError("key", "error message");
        var response = await controllerCliente.DesativarCliente(clienteDtoFake.Id.Value);
        var okResult = response as ObjectResult;

        //Assert
        _serviceMock.Verify(s => s.Desabled(It.IsAny<Guid>(),It.IsAny<Guid>()), Times.Never());
        Assert.NotNull(okResult);
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.BadRequest);

    }

    [Fact]
    public async Task NaoDeve_DesativarUmCliente_RetornoNoContent()
    {
        //Arrange
        ICollection<ClienteDto> clientesDtoFake = CriaListaClienteDtoFake();
        ClienteDto clienteDtoFake = CriaClienteDtoFake(Guid.NewGuid());
        ClienteDto clienteDtoFakeNull = null;

        _serviceMock.Setup(s => s.Desabled(It.IsAny<Guid>(),It.IsAny<Guid>())).ReturnsAsync(clienteDtoFakeNull);
        ClienteController controllerCliente = GenerateControllerFake(clientesDtoFake);
        //Act
        var response = await controllerCliente.DesativarCliente(clienteDtoFake.Id.Value);
        var okResult = response as NoContentResult;

        //Assert
        _serviceMock.Verify(s => s.Desabled(It.IsAny<Guid>(),It.IsAny<Guid>()), Times.Once());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);

    }

    [Fact]
    public async Task Deve_DesativarUmCliente_RetornoOk()
    {
        //Arrange
        ICollection<ClienteDto> clientesDtoFake = CriaListaClienteDtoFake();
        ClienteDto clienteDtoFake = CriaClienteDtoFake(Guid.NewGuid());

        _serviceMock.Setup(s => s.Desabled(It.IsAny<Guid>(),It.IsAny<Guid>())).ReturnsAsync(clienteDtoFake);
        ClienteController controllerCliente = GenerateControllerFake(clientesDtoFake);
        //Act
        var response = await controllerCliente.DesativarCliente(clienteDtoFake.Id.Value);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as ClienteDto;
        //Assert
        _serviceMock.Verify(s => s.Desabled(It.IsAny<Guid>(),It.IsAny<Guid>()), Times.Once());
        Assert.NotNull(result);

    }


    private static ICollection<ClienteDto> CriaListaClienteDtoFake()
    {
        return new List<ClienteDto>() { new ClienteDto() { CPF = "123456789", Email = "teste@test.com.br", Nome = "Teste", Telefone = "11999999999", Id = Guid.NewGuid(), PrestadorId = Guid.NewGuid() } };
    }
    private static ClienteDto CriaClienteDtoFake(Guid? Id)
    {
        return new ClienteDto() { CPF = "123456789", Email = "teste@test.com.br", Nome = "Teste", Telefone = "11999999999", Id = Id.HasValue ? Id.Value : Guid.Empty, PrestadorId = Guid.NewGuid() };
    }
}
