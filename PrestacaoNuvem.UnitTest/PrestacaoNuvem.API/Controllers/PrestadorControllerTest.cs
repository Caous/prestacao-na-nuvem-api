using FluentValidation;
using PrestacaoNuvem.Api.Domain.Interfaces;
using System.Net;

namespace PrestacaoNuvem.UnitTest.PrestacaoNuvem.API.Controllers;

#pragma warning disable 8604, 8602, 8629, 8600, 8620
public class PrestadorControllerTest
{
    private readonly Mock<IPrestadorService> _prestadorService = new();
    private readonly Mock<IFuncionarioService> _funcionarioService = new();
    private readonly Mock<IValidator<PrestadorDto>> _validator = new();
    private readonly Mock<IValidator<FuncionarioPrestadorDto>> _validatorFuncionario = new();

    private static DefaultHttpContext CreateFakeClaims(ICollection<PrestadorDto> prestadores)
    {
        var fakeHttpContext = new DefaultHttpContext();
        ClaimsIdentity identity = new(
            new[] {
                        new Claim("PrestadorId", prestadores.First().PrestadorId.ToString()),
                        new Claim("UserName", "Teste"),
                        new Claim("IdUserLogin", prestadores.First().PrestadorId.ToString())

            }
        );
        fakeHttpContext.User = new System.Security.Claims.ClaimsPrincipal(identity);
        return fakeHttpContext;
    }
    private PrestadorController GenerateControllerFake(List<PrestadorDto> prestadorDtos)
    {
        return new PrestadorController(_prestadorService.Object, _funcionarioService.Object, _validator.Object, _validatorFuncionario.Object) { ControllerContext = new ControllerContext() { HttpContext = CreateFakeClaims(prestadorDtos) } };
    }

    #region Prestador 

    [Fact]
    public async Task Deve_Retornar_ListaDePrestador_RetornoOk()
    {
        //Arrange
        ICollection<PrestadorDto> prestadoresFake = CriaListaFornecedoresFake();
        PrestadorDto prestadorDtoFake = new PrestadorDto() { Id = Guid.NewGuid(), PrestadorId = Guid.NewGuid() };
        _prestadorService.Setup(s => s.GetAllPrestador(It.IsAny<PrestadorDto>())).ReturnsAsync(prestadoresFake);
        PrestadorController controller = GenerateControllerFake(new List<PrestadorDto>() { prestadorDtoFake });
        //Act
        var response = await controller.GetAll();
        var okResult = response as OkObjectResult;
        var result = okResult.Value as ICollection<PrestadorDto>;
        //Assert
        _prestadorService.Verify(s => s.GetAllPrestador(It.IsAny<PrestadorDto>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(result.First().Telefone, prestadoresFake.First().Telefone);
        Assert.Equal(result.First().CPF, prestadoresFake.First().CPF);
        Assert.Equal(result.First().DataCadastro, prestadoresFake.First().DataCadastro);
        Assert.Equal(result.First().Id, prestadoresFake.First().Id);
        Assert.Equal(result.First().EmailEmpresa, prestadoresFake.First().EmailEmpresa);
        Assert.Equal(result.First().Endereco, prestadoresFake.First().Endereco);
        Assert.Equal(result.First().Nome, prestadoresFake.First().Nome);

    }
    [Fact]
    public async Task NaoDeve_Retornar_ListaDePrestadorNoContent()
    {
        //Arrange
        ICollection<PrestadorDto> prestadoresFake = null;
        PrestadorDto prestadorDtoFake = new PrestadorDto() { Id = Guid.NewGuid(), PrestadorId = Guid.NewGuid() };
        _prestadorService.Setup(s => s.GetAllPrestador(It.IsAny<PrestadorDto>())).ReturnsAsync(prestadoresFake);
        PrestadorController controller = GenerateControllerFake(new List<PrestadorDto>() { prestadorDtoFake });
        //Act
        var response = await controller.GetAll();
        var okResult = response as NoContentResult;

        //Assert
        _prestadorService.Verify(s => s.GetAllPrestador(It.IsAny<PrestadorDto>()), Times.Once());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);
    }

    [Fact]
    public async Task Deve_Cadastrar_Um_Prestador_RetornarOk()
    {
        //Arrange
        ICollection<PrestadorDto> prestadoresFake = CriaListaFornecedoresFake();
        PrestadorDto prestadorDtoFake = new PrestadorDto() { Id = Guid.NewGuid(), PrestadorId = Guid.NewGuid() };
        _prestadorService.Setup(s => s.CreatePrestador(It.IsAny<PrestadorDto>())).ReturnsAsync(prestadoresFake.First());
        PrestadorController controller = GenerateControllerFake(new List<PrestadorDto>() { prestadorDtoFake });
        //Act
        var response = await controller.Add(prestadorDtoFake);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as PrestadorDto;
        //Assert
        _prestadorService.Verify(s => s.CreatePrestador(It.IsAny<PrestadorDto>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(result.Telefone, prestadoresFake.First().Telefone);
        Assert.Equal(result.CPF, prestadoresFake.First().CPF);
        Assert.Equal(result.DataCadastro, prestadoresFake.First().DataCadastro);
        Assert.Equal(result.Id, prestadoresFake.First().Id);
        Assert.Equal(result.EmailEmpresa, prestadoresFake.First().EmailEmpresa);
        Assert.Equal(result.Endereco, prestadoresFake.First().Endereco);
        Assert.Equal(result.Nome, prestadoresFake.First().Nome);
    }

    [Fact]
    public async Task NaoDeve_Cadastrar_Um_Prestador_RetornarNoContent()
    {
        //Arrange
        ICollection<PrestadorDto> prestadoresFake = CriaListaFornecedoresFake();
        PrestadorDto prestadorDtoNull = null;
        PrestadorDto prestadorDtoFake = new PrestadorDto() { Id = Guid.NewGuid(), PrestadorId = Guid.NewGuid() };
        _prestadorService.Setup(s => s.CreatePrestador(It.IsAny<PrestadorDto>())).ReturnsAsync(prestadorDtoNull);
        PrestadorController controller = GenerateControllerFake(new List<PrestadorDto>() { prestadorDtoFake });
        //Act
        var response = await controller.Add(prestadorDtoFake);
        var okResult = response as NoContentResult;
        //Assert
        _prestadorService.Verify(s => s.CreatePrestador(It.IsAny<PrestadorDto>()), Times.Once());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);

    }

    [Fact]
    public async Task NaoDeve_Cadastrar_Um_Prestador_RetornarBadRequest()
    {
        //Arrange
        ICollection<PrestadorDto> prestadoresFake = CriaListaFornecedoresFake();
        PrestadorDto prestadorDtoFake = new PrestadorDto() { Id = Guid.NewGuid(), PrestadorId = Guid.NewGuid() };
        _prestadorService.Setup(s => s.CreatePrestador(It.IsAny<PrestadorDto>())).ReturnsAsync(prestadoresFake.FirstOrDefault());
        PrestadorController controller = GenerateControllerFake(new List<PrestadorDto>() { prestadorDtoFake });
        controller.ModelState.AddModelError("key", "error message");
        //Act
        var response = await controller.Add(prestadorDtoFake);
        var okResult = response as ObjectResult;
        //Assert
        _prestadorService.Verify(s => s.CreatePrestador(It.IsAny<PrestadorDto>()), Times.Never());
        Assert.NotNull(okResult);
        Equals(okResult.StatusCode, (int)HttpStatusCode.BadRequest);

    }

    [Fact]
    public async Task Deve_Retornarar_Um_Prestador_RetornarOk()
    {
        //Arrange
        ICollection<PrestadorDto> prestadoresFake = CriaListaFornecedoresFake();
        PrestadorDto prestadorDtoFake = new PrestadorDto() { Id = Guid.NewGuid(), PrestadorId = Guid.NewGuid() };
        _prestadorService.Setup(s => s.FindByIdPrestador(It.IsAny<Guid>())).ReturnsAsync(prestadoresFake.First());
        PrestadorController controller = GenerateControllerFake(new List<PrestadorDto>() { prestadorDtoFake });
        //Act
        var response = await controller.GetId(prestadorDtoFake.Id.Value);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as PrestadorDto;
        //Assert
        _prestadorService.Verify(s => s.FindByIdPrestador(It.IsAny<Guid>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(result.Telefone, prestadoresFake.First().Telefone);
        Assert.Equal(result.CPF, prestadoresFake.First().CPF);
        Assert.Equal(result.DataCadastro, prestadoresFake.First().DataCadastro);
        Assert.Equal(result.Id, prestadoresFake.First().Id);
        Assert.Equal(result.EmailEmpresa, prestadoresFake.First().EmailEmpresa);
        Assert.Equal(result.Endereco, prestadoresFake.First().Endereco);
        Assert.Equal(result.Nome, prestadoresFake.First().Nome);
    }


    [Fact]
    public async Task Deve_Retornarar_Um_Prestador_RetornarNoContent()
    {
        //Arrange
        ICollection<PrestadorDto> prestadoresFake = CriaListaFornecedoresFake();
        PrestadorDto prestadorFake = null;
        PrestadorDto prestadorDtoFake = new PrestadorDto() { Id = Guid.NewGuid(), PrestadorId = Guid.NewGuid() };
        _prestadorService.Setup(s => s.FindByIdPrestador(It.IsAny<Guid>())).ReturnsAsync(prestadorFake);
        PrestadorController controller = GenerateControllerFake(new List<PrestadorDto>() { prestadorDtoFake });
        //Act
        var response = await controller.GetId(prestadorDtoFake.Id.Value);
        var okResult = response as NoContentResult;
        //Assert
        _prestadorService.Verify(s => s.FindByIdPrestador(It.IsAny<Guid>()), Times.Once());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);

    }

    [Fact]
    public async Task NaoDeve_Retornarar_Um_Prestador_RetornaBadRequest()
    {
        //Arrange
        ICollection<PrestadorDto> prestadoresFake = CriaListaFornecedoresFake();
        PrestadorDto prestadorDtoFake = new PrestadorDto() { Id = Guid.NewGuid(), PrestadorId = Guid.NewGuid() };
        _prestadorService.Setup(s => s.FindByIdPrestador(It.IsAny<Guid>())).ReturnsAsync(prestadoresFake.First());
        PrestadorController controller = GenerateControllerFake(new List<PrestadorDto>() { prestadorDtoFake });
        controller.ModelState.AddModelError("key", "error message");
        //Act
        var response = await controller.GetId(prestadorDtoFake.Id.Value);
        var okResult = response as ObjectResult;
        //Assert
        _prestadorService.Verify(s => s.FindByIdPrestador(It.IsAny<Guid>()), Times.Never());
        Assert.NotNull(okResult);
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Deve_Atualizar_Um_Prestador_RetornarSucesso()
    {
        //Arrange
        ICollection<PrestadorDto> prestadoresFake = CriaListaFornecedoresFake();
        PrestadorDto prestadorDtoFake = new PrestadorDto() { Id = Guid.NewGuid(), PrestadorId = Guid.NewGuid() };
        _prestadorService.Setup(s => s.UpdatePrestador(It.IsAny<PrestadorDto>())).ReturnsAsync(prestadoresFake.First());
        PrestadorController controller = GenerateControllerFake(new List<PrestadorDto>() { prestadorDtoFake });
        //Act
        var response = await controller.AtualizarPrestador(prestadorDtoFake);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as PrestadorDto;
        //Assert
        _prestadorService.Verify(s => s.UpdatePrestador(It.IsAny<PrestadorDto>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(result.Telefone, prestadoresFake.First().Telefone);
        Assert.Equal(result.CPF, prestadoresFake.First().CPF);
        Assert.Equal(result.DataCadastro, prestadoresFake.First().DataCadastro);
        Assert.Equal(result.Id, prestadoresFake.First().Id);
        Assert.Equal(result.EmailEmpresa, prestadoresFake.First().EmailEmpresa);
        Assert.Equal(result.Endereco, prestadoresFake.First().Endereco);
        Assert.Equal(result.Nome, prestadoresFake.First().Nome);
    }

    [Fact]
    public async Task NaoDeve_Atualizar_Um_Prestador_RetornarNoContent()
    {
        //Arrange
        ICollection<PrestadorDto> prestadoresFake = CriaListaFornecedoresFake();
        PrestadorDto prestadorDtoFakeNull = null;
        PrestadorDto prestadorDtoFake = new PrestadorDto() { Id = Guid.NewGuid(), PrestadorId = Guid.NewGuid() };
        _prestadorService.Setup(s => s.UpdatePrestador(It.IsAny<PrestadorDto>())).ReturnsAsync(prestadorDtoFakeNull);
        PrestadorController controller = GenerateControllerFake(new List<PrestadorDto>() { prestadorDtoFake });
        //Act
        var response = await controller.AtualizarPrestador(prestadorDtoFake);
        var okResult = response as NoContentResult;
        //Assert
        _prestadorService.Verify(s => s.UpdatePrestador(It.IsAny<PrestadorDto>()), Times.Once());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);
    }

    [Fact]
    public async Task NaoDeve_Atualizar_Um_Prestador_RetornarBadRequest()
    {
        //Arrange
        ICollection<PrestadorDto> prestadoresFake = CriaListaFornecedoresFake();
        PrestadorDto prestadorDtoFake = new PrestadorDto() { Id = Guid.NewGuid(), PrestadorId = Guid.NewGuid() };
        _prestadorService.Setup(s => s.UpdatePrestador(It.IsAny<PrestadorDto>())).ReturnsAsync(prestadoresFake.First());
        PrestadorController controller = GenerateControllerFake(new List<PrestadorDto>() { prestadorDtoFake });
        controller.ModelState.AddModelError("key", "error message");
        //Act
        var response = await controller.AtualizarPrestador(prestadorDtoFake);
        var okResult = response as ObjectResult;
        //Assert
        _prestadorService.Verify(s => s.UpdatePrestador(It.IsAny<PrestadorDto>()), Times.Never());
        Assert.NotNull(okResult);
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Deve_Desativar_Um_Prestador_RetornarSucesso()
    {
        //Arrange
        ICollection<PrestadorDto> prestadoresFake = CriaListaFornecedoresFake();
        PrestadorDto prestadorDtoFake = new PrestadorDto() { Id = Guid.NewGuid(), PrestadorId = Guid.NewGuid() };
        _prestadorService.Setup(s => s.Desabled(It.IsAny<Guid>(),It.IsAny<Guid>())).ReturnsAsync(prestadoresFake.First());
        PrestadorController controller = GenerateControllerFake(new List<PrestadorDto>() { prestadorDtoFake });
        //Act
        var response = await controller.DesativarPrestadorServico(prestadorDtoFake.Id.Value, prestadorDtoFake.Id.Value);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as PrestadorDto;
        //Assert
        _prestadorService.Verify(s => s.Desabled(It.IsAny<Guid>(),It.IsAny<Guid>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(result.Telefone, prestadoresFake.First().Telefone);
        Assert.Equal(result.CPF, prestadoresFake.First().CPF);
        Assert.Equal(result.DataCadastro, prestadoresFake.First().DataCadastro);
        Assert.Equal(result.Id, prestadoresFake.First().Id);
        Assert.Equal(result.EmailEmpresa, prestadoresFake.First().EmailEmpresa);
        Assert.Equal(result.Endereco, prestadoresFake.First().Endereco);
        Assert.Equal(result.Nome, prestadoresFake.First().Nome);
    }

    [Fact]
    public async Task NaoDeve_Desativar_Um_Prestador_RetornarNoContent()
    {
        //Arrange
        ICollection<PrestadorDto> prestadoresFake = CriaListaFornecedoresFake();
        PrestadorDto prestadorFakeNull = null;
        PrestadorDto prestadorDtoFake = new PrestadorDto() { Id = Guid.NewGuid(), PrestadorId = Guid.NewGuid() };
        _prestadorService.Setup(s => s.Desabled(It.IsAny<Guid>(),It.IsAny<Guid>())).ReturnsAsync(prestadorFakeNull);
        PrestadorController controller = GenerateControllerFake(new List<PrestadorDto>() { prestadorDtoFake });
        //Act
        var response = await controller.DesativarPrestadorServico(prestadorDtoFake.Id.Value, prestadorDtoFake.Id.Value);
        var okResult = response as NoContentResult;

        //Assert
        _prestadorService.Verify(s => s.Desabled(It.IsAny<Guid>(),It.IsAny<Guid>()), Times.Once());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);
    }

    [Fact]
    public async Task NaoDeve_Desativar_Um_Prestador_RetornarBadRequest()
    {
        //Arrange
        ICollection<PrestadorDto> prestadoresFake = CriaListaFornecedoresFake();
        PrestadorDto prestadorFakeNull = null;
        PrestadorDto prestadorDtoFake = new PrestadorDto() { Id = Guid.NewGuid(), PrestadorId = Guid.NewGuid() };
        _prestadorService.Setup(s => s.Desabled(It.IsAny<Guid>(),It.IsAny<Guid>())).ReturnsAsync(prestadorFakeNull);
        PrestadorController controller = GenerateControllerFake(new List<PrestadorDto>() { prestadorDtoFake });
        controller.ModelState.AddModelError("key", "error message");
        //Act
        var response = await controller.DesativarPrestadorServico(prestadorDtoFake.Id.Value, prestadorDtoFake.Id.Value);
        var okResult = response as ObjectResult;

        //Assert
        _prestadorService.Verify(s => s.Desabled(It.IsAny<Guid>(),It.IsAny<Guid>()), Times.Never());
        Assert.NotNull(okResult);
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Deve_Deletar_Um_Prestador_RetornarSucesso()
    {
        //Arrange
        ICollection<PrestadorDto> prestadoresFake = CriaListaFornecedoresFake();
        PrestadorDto prestadorDtoFake = new PrestadorDto() { Id = Guid.NewGuid(), PrestadorId = Guid.NewGuid() };
        _prestadorService.Setup(s => s.Delete(It.IsAny<Guid>()));
        PrestadorController controller = GenerateControllerFake(new List<PrestadorDto>() { prestadorDtoFake });
        //Act
        var response = await controller.DeletarPrestador(prestadorDtoFake.Id.Value);

        //Assert
        _prestadorService.Verify(s => s.Delete(It.IsAny<Guid>()), Times.Once());
        Assert.NotNull(response);

    }

    [Fact]
    public async Task NaoDeve_Deletar_Um_Prestador_RetornarBadRequest()
    {
        //Arrange
        ICollection<PrestadorDto> prestadoresFake = CriaListaFornecedoresFake();
        PrestadorDto prestadorDtoFake = new PrestadorDto() { Id = Guid.NewGuid(), PrestadorId = Guid.NewGuid() };
        _prestadorService.Setup(s => s.Delete(It.IsAny<Guid>()));
        PrestadorController controller = GenerateControllerFake(new List<PrestadorDto>() { prestadorDtoFake });
        controller.ModelState.AddModelError("key", "error message");
        //Act
        var response = await controller.DeletarPrestador(prestadorDtoFake.Id.Value);
        var okResult = response as ObjectResult;
        //Assert
        _prestadorService.Verify(s => s.Delete(It.IsAny<Guid>()), Times.Never());
        Assert.NotNull(okResult);
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.BadRequest);

    }

    #endregion


    #region Funcionários

    private static DefaultHttpContext CreateFakeClaimsFuncionario(ICollection<FuncionarioPrestadorDto> funcionario)
    {
        var fakeHttpContext = new DefaultHttpContext();
        ClaimsIdentity identity = new(
            new[] {
                        new Claim("PrestadorId", funcionario.First().PrestadorId.ToString()),
                        new Claim("UserName", "Teste"),
                        new Claim("IdUserLogin", funcionario.First().PrestadorId.ToString())

            }
        );
        fakeHttpContext.User = new System.Security.Claims.ClaimsPrincipal(identity);
        return fakeHttpContext;
    }

    private PrestadorController GenerateControllerFakeFuncionario(ICollection<FuncionarioPrestadorDto> funcionario)
    {
        return new PrestadorController(_prestadorService.Object, _funcionarioService.Object, _validator.Object, _validatorFuncionario.Object) { ControllerContext = new ControllerContext() { HttpContext = CreateFakeClaimsFuncionario(funcionario) } };
    }

    [Fact]
    public async Task Deve_Retornar_ListaDeFuncionarios_RetornoOk()
    {
        //Arrange
        ICollection<FuncionarioPrestadorDto> funcionarioFake = CriarListaFuncionarioFake();
        _funcionarioService.Setup(s => s.GetAllFuncionario(It.IsAny<FuncionarioPrestadorDto>())).ReturnsAsync(funcionarioFake);
        PrestadorController controller = GenerateControllerFakeFuncionario(funcionarioFake);
        //Act
        var response = await controller.GetAllFuncionario(string.Empty, string.Empty, string.Empty);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as ICollection<FuncionarioPrestadorDto>;
        //Assert
        _funcionarioService.Verify(s => s.GetAllFuncionario(It.IsAny<FuncionarioPrestadorDto>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(result.First().Telefone, funcionarioFake.First().Telefone);
        Assert.Equal(result.First().CPF, funcionarioFake.First().CPF);
        Assert.Equal(result.First().DataCadastro, funcionarioFake.First().DataCadastro);
        Assert.Equal(result.First().Id, funcionarioFake.First().Id);
        Assert.Equal(result.First().Cargo, funcionarioFake.First().Cargo);
        Assert.Equal(result.First().Email, funcionarioFake.First().Email);
        Assert.Equal(result.First().Endereco, funcionarioFake.First().Endereco);
        Assert.Equal(result.First().Nome, funcionarioFake.First().Nome);

    }

    [Fact]
    public async Task NaoDeve_Retornar_ListaDeFuncionarios_RetornoNoContent()
    {
        //Arrange
        ICollection<FuncionarioPrestadorDto> funcionarioFake = CriarListaFuncionarioFake();
        ICollection<FuncionarioPrestadorDto> funcionarioFakeLista = null;
        _funcionarioService.Setup(s => s.GetAllFuncionario(It.IsAny<FuncionarioPrestadorDto>())).ReturnsAsync(funcionarioFakeLista);
        PrestadorController controller = GenerateControllerFakeFuncionario(funcionarioFake);
        //Act
        var response = await controller.GetAllFuncionario(string.Empty, string.Empty, string.Empty);
        var okResult = response as NoContentResult;
        //Assert
        _funcionarioService.Verify(s => s.GetAllFuncionario(It.IsAny<FuncionarioPrestadorDto>()), Times.Once());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);

    }

    [Fact]
    public async Task Deve_Cadastrar_Um_FuncionarioPrestador_RetornarSucesso()
    {
        //Arrange
        FuncionarioPrestadorDto funcionarioFake = CriarFuncionarioDtoFake(Guid.NewGuid());
        ICollection<FuncionarioPrestadorDto> funcionarioFakeLista = CriarListaFuncionarioFake();
        _funcionarioService.Setup(s => s.CreateFuncionario(It.IsAny<FuncionarioPrestadorDto>())).ReturnsAsync(funcionarioFake);
        PrestadorController controller = GenerateControllerFakeFuncionario(funcionarioFakeLista);
        //Act
        var response = await controller.AddFuncionario(funcionarioFake);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as FuncionarioPrestadorDto;
        //Assert
        _funcionarioService.Verify(s => s.CreateFuncionario(It.IsAny<FuncionarioPrestadorDto>()), Times.Once());

        Assert.NotNull(result);
        Assert.Equal(result.Telefone, funcionarioFake.Telefone);
        Assert.Equal(result.CPF, funcionarioFake.CPF);
        Assert.Equal(result.DataCadastro, funcionarioFake.DataCadastro);
        Assert.Equal(result.Id, funcionarioFake.Id);
        Assert.Equal(result.Email, funcionarioFake.Email);
        Assert.Equal(result.Endereco, funcionarioFake.Endereco);
        Assert.Equal(result.Nome, funcionarioFake.Nome);
    }

    [Fact]
    public async Task NaoDeve_Cadastrar_Um_FuncionarioPrestador_RetornarNoContent()
    {
        //Arrange
        FuncionarioPrestadorDto funcionarioFake = CriarFuncionarioDtoFake(Guid.NewGuid());
        FuncionarioPrestadorDto funcionarioFakeNull = null;
        ICollection<FuncionarioPrestadorDto> funcionarioFakeLista = CriarListaFuncionarioFake();
        _funcionarioService.Setup(s => s.CreateFuncionario(It.IsAny<FuncionarioPrestadorDto>())).ReturnsAsync(funcionarioFakeNull);
        PrestadorController controller = GenerateControllerFakeFuncionario(funcionarioFakeLista);
        //Act
        var response = await controller.AddFuncionario(funcionarioFake);
        var okResult = response as NoContentResult;
        //Assert
        _funcionarioService.Verify(s => s.CreateFuncionario(It.IsAny<FuncionarioPrestadorDto>()), Times.Once());

        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);

    }

    [Fact]
    public async Task NaoDeve_Cadastrar_Um_FuncionarioPrestador_RetornarBadRequest()
    {
        //Arrange
        FuncionarioPrestadorDto funcionarioFake = null;
        ICollection<FuncionarioPrestadorDto> funcionarioFakeLista = CriarListaFuncionarioFake();
        _funcionarioService.Setup(s => s.CreateFuncionario(It.IsAny<FuncionarioPrestadorDto>())).ReturnsAsync(funcionarioFake);
        List<FuncionarioPrestadorDto> prestadorDtoFakeList = new List<FuncionarioPrestadorDto>();
        PrestadorController controller = GenerateControllerFakeFuncionario(funcionarioFakeLista);
        controller.ModelState.AddModelError("key", "error message");
        //Act
        var response = await controller.AddFuncionario(funcionarioFake);
        var okResult = response as ObjectResult;
        //Assert
        _funcionarioService.Verify(s => s.CreateFuncionario(It.IsAny<FuncionarioPrestadorDto>()), Times.Never());

        Assert.NotNull(okResult);
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.BadRequest);

    }


    [Fact]
    public async Task Deve_Retornarar_Um_FuncionarioPrestador_RetornarSucesso()
    {
        //Arrange
        FuncionarioPrestadorDto funcionarioFake = CriarFuncionarioDtoFake(Guid.NewGuid());
        ICollection<FuncionarioPrestadorDto> funcionarioFakeLista = CriarListaFuncionarioFake();
        _funcionarioService.Setup(s => s.FindByIdFuncionario(It.IsAny<Guid>())).ReturnsAsync(funcionarioFake);
        List<FuncionarioPrestadorDto> prestadorDtoFakeList = new List<FuncionarioPrestadorDto>();
        PrestadorController controller = GenerateControllerFakeFuncionario(funcionarioFakeLista);
        //Act
        var response = await controller.GetIdFuncionario(funcionarioFake.Id.Value);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as FuncionarioPrestadorDto;
        //Assert
        _funcionarioService.Verify(s => s.FindByIdFuncionario(It.IsAny<Guid>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(result.Telefone, funcionarioFake.Telefone);
        Assert.Equal(result.CPF, funcionarioFake.CPF);
        Assert.Equal(result.DataCadastro, funcionarioFake.DataCadastro);
        Assert.Equal(result.Id, funcionarioFake.Id);
        Assert.Equal(result.Email, funcionarioFake.Email);
        Assert.Equal(result.Endereco, funcionarioFake.Endereco);
        Assert.Equal(result.Nome, funcionarioFake.Nome);
    }

    [Fact]
    public async Task NaoDeve_Retornarar_Um_FuncionarioPrestador_RetornarNoContent()
    {
        //Arrange
        FuncionarioPrestadorDto funcionarioFake = CriarFuncionarioDtoFake(Guid.NewGuid());
        FuncionarioPrestadorDto funcionarioFakeNull = null;
        ICollection<FuncionarioPrestadorDto> funcionarioFakeLista = CriarListaFuncionarioFake();
        _funcionarioService.Setup(s => s.FindByIdFuncionario(It.IsAny<Guid>())).ReturnsAsync(funcionarioFakeNull);
        List<FuncionarioPrestadorDto> prestadorDtoFakeList = new List<FuncionarioPrestadorDto>();
        PrestadorController controller = GenerateControllerFakeFuncionario(funcionarioFakeLista);
        //Act
        var response = await controller.GetIdFuncionario(funcionarioFake.Id.Value);
        var okResult = response as NoContentResult;
        //Assert
        _funcionarioService.Verify(s => s.FindByIdFuncionario(It.IsAny<Guid>()), Times.Once());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);
    }

    [Fact]
    public async Task NaoDeve_Retornarar_Um_FuncionarioPrestador_RetornarBadRequest()
    {
        //Arrange
        FuncionarioPrestadorDto funcionarioFake = CriarFuncionarioDtoFake(Guid.NewGuid());
        ICollection<FuncionarioPrestadorDto> funcionarioFakeLista = CriarListaFuncionarioFake();
        _funcionarioService.Setup(s => s.FindByIdFuncionario(It.IsAny<Guid>())).ReturnsAsync(funcionarioFake);
        List<FuncionarioPrestadorDto> prestadorDtoFakeList = new List<FuncionarioPrestadorDto>();
        PrestadorController controller = GenerateControllerFakeFuncionario(funcionarioFakeLista);
        controller.ModelState.AddModelError("key", "error message");
        //Act
        var response = await controller.GetIdFuncionario(funcionarioFake.Id.Value);
        var okResult = response as ObjectResult;
        //Assert
        _funcionarioService.Verify(s => s.FindByIdFuncionario(It.IsAny<Guid>()), Times.Never());
        Assert.NotNull(okResult);
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Deve_Atualizar_Um_Funcionario_RetornarSucesso()
    {
        //Arrange
        ICollection<FuncionarioPrestadorDto> funcionarioFakeLista = CriarListaFuncionarioFake();
        FuncionarioPrestadorDto funcionarioFake = CriarFuncionarioDtoFake(Guid.NewGuid());
        _funcionarioService.Setup(s => s.UpdateFuncionario(It.IsAny<FuncionarioPrestadorDto>())).ReturnsAsync(funcionarioFake);
        List<FuncionarioPrestadorDto> prestadorDtoFakeList = new List<FuncionarioPrestadorDto>();
        PrestadorController controller = GenerateControllerFakeFuncionario(funcionarioFakeLista);
        //Act
        var response = await controller.AtualizarFuncionario(funcionarioFake);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as FuncionarioPrestadorDto;
        //Assert
        _funcionarioService.Verify(s => s.UpdateFuncionario(It.IsAny<FuncionarioPrestadorDto>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(result.Telefone, funcionarioFake.Telefone);
        Assert.Equal(result.CPF, funcionarioFake.CPF);
        Assert.Equal(result.DataCadastro, funcionarioFake.DataCadastro);
        Assert.Equal(result.Id, funcionarioFake.Id);
        Assert.Equal(result.Email, funcionarioFake.Email);
        Assert.Equal(result.Endereco, funcionarioFake.Endereco);
        Assert.Equal(result.Nome, funcionarioFake.Nome);
    }

    [Fact]
    public async Task NaoDeve_Atualizar_Um_Funcionario_RetornaNoContent()
    {
        //Arrange
        ICollection<FuncionarioPrestadorDto> funcionarioFakeLista = CriarListaFuncionarioFake();
        FuncionarioPrestadorDto funcionarioFake = CriarFuncionarioDtoFake(Guid.NewGuid());
        FuncionarioPrestadorDto funcionarioFakeNull = null;
        _funcionarioService.Setup(s => s.UpdateFuncionario(It.IsAny<FuncionarioPrestadorDto>())).ReturnsAsync(funcionarioFakeNull);
        List<FuncionarioPrestadorDto> prestadorDtoFakeList = new List<FuncionarioPrestadorDto>();
        PrestadorController controller = GenerateControllerFakeFuncionario(funcionarioFakeLista);
        //Act
        var response = await controller.AtualizarFuncionario(funcionarioFake);
        var okResult = response as NoContentResult;
        //Assert
        _funcionarioService.Verify(s => s.UpdateFuncionario(It.IsAny<FuncionarioPrestadorDto>()), Times.Once());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);
    }

    [Fact]
    public async Task NaoDeve_Atualizar_Um_Funcionario_RetornaBadRequest()
    {
        //Arrange
        ICollection<FuncionarioPrestadorDto> funcionarioFakeLista = CriarListaFuncionarioFake();
        FuncionarioPrestadorDto funcionarioFake = CriarFuncionarioDtoFake(Guid.NewGuid());
        _funcionarioService.Setup(s => s.UpdateFuncionario(It.IsAny<FuncionarioPrestadorDto>())).ReturnsAsync(funcionarioFake);
        List<FuncionarioPrestadorDto> prestadorDtoFakeList = new List<FuncionarioPrestadorDto>();
        PrestadorController controller = GenerateControllerFakeFuncionario(funcionarioFakeLista);
        controller.ModelState.AddModelError("key", "error message");
        //Act
        var response = await controller.AtualizarFuncionario(funcionarioFake);
        var okResult = response as ObjectResult;
        //Assert
        _funcionarioService.Verify(s => s.UpdateFuncionario(It.IsAny<FuncionarioPrestadorDto>()), Times.Never());
        Assert.NotNull(okResult);
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.BadRequest);
    }


    [Fact]
    public async Task Deve_Desativar_Um_Funcionario_RetornarSucesso()
    {
        //Arrange
        ICollection<FuncionarioPrestadorDto> funcionarioFakeLista = CriarListaFuncionarioFake();
        FuncionarioPrestadorDto funcionarioFake = CriarFuncionarioDtoFake(Guid.NewGuid());
        _funcionarioService.Setup(s => s.Desabled(It.IsAny<Guid>(),It.IsAny<Guid>())).ReturnsAsync(funcionarioFake);
        PrestadorController controller = GenerateControllerFakeFuncionario(funcionarioFakeLista);
        //Act
        var response = await controller.DesativarFuncionario(funcionarioFake.Id.Value);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as FuncionarioPrestadorDto;
        //Assert
        _funcionarioService.Verify(s => s.Desabled(It.IsAny<Guid>(),It.IsAny<Guid>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(result.Telefone, funcionarioFake.Telefone);
        Assert.Equal(result.CPF, funcionarioFake.CPF);
        Assert.Equal(result.DataCadastro, funcionarioFake.DataCadastro);
        Assert.Equal(result.Id, funcionarioFake.Id);
        Assert.Equal(result.Email, funcionarioFake.Email);
        Assert.Equal(result.Endereco, funcionarioFake.Endereco);
        Assert.Equal(result.Nome, funcionarioFake.Nome);
    }


    [Fact]
    public async Task NaoDeve_Desativar_Um_Funcionario_RetornarNoContent()
    {
        //Arrange
        ICollection<FuncionarioPrestadorDto> funcionarioFakeLista = CriarListaFuncionarioFake();
        FuncionarioPrestadorDto funcionarioFake = CriarFuncionarioDtoFake(Guid.NewGuid());
        FuncionarioPrestadorDto funcionarioFakeNull = null;
        _funcionarioService.Setup(s => s.Desabled(It.IsAny<Guid>(),It.IsAny<Guid>())).ReturnsAsync(funcionarioFakeNull);
        List<FuncionarioPrestadorDto> prestadorDtoFakeList = new List<FuncionarioPrestadorDto>();
        PrestadorController controller = GenerateControllerFakeFuncionario(funcionarioFakeLista);
        //Act
        var response = await controller.DesativarFuncionario(funcionarioFake.Id.Value);
        var okResult = response as NoContentResult;
        
        //Assert
        _funcionarioService.Verify(s => s.Desabled(It.IsAny<Guid>(),It.IsAny<Guid>()), Times.Once());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);
    }

    [Fact]
    public async Task NaoDeve_Desativar_Um_Funcionario_RetornarBadRequest()
    {
        //Arrange
        ICollection<FuncionarioPrestadorDto> funcionarioFakeLista = CriarListaFuncionarioFake();
        FuncionarioPrestadorDto funcionarioFake = CriarFuncionarioDtoFake(Guid.NewGuid());
        _funcionarioService.Setup(s => s.Desabled(It.IsAny<Guid>(),It.IsAny<Guid>())).ReturnsAsync(funcionarioFake);
        List<FuncionarioPrestadorDto> prestadorDtoFakeList = new List<FuncionarioPrestadorDto>();
        PrestadorController controller = GenerateControllerFakeFuncionario(funcionarioFakeLista);
        controller.ModelState.AddModelError("key", "error message");
        //Act
        var response = await controller.DesativarFuncionario(funcionarioFake.Id.Value);
        var okResult = response as ObjectResult;

        //Assert
        _funcionarioService.Verify(s => s.Desabled(It.IsAny<Guid>(),It.IsAny<Guid>()), Times.Never());
        Assert.NotNull(okResult);
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.BadRequest);
    }


    [Fact]
    public async Task Deve_Deletar_Um_Funcionario_RetornarSucesso()
    {
        //Arrange
        ICollection<FuncionarioPrestadorDto> funcionarioFakeLista = CriarListaFuncionarioFake();
        FuncionarioPrestadorDto funcionarioFake = CriarFuncionarioDtoFake(Guid.NewGuid());
        _funcionarioService.Setup(s => s.Delete(It.IsAny<Guid>()));
        List<FuncionarioPrestadorDto> prestadorDtoFakeList = new List<FuncionarioPrestadorDto>();
        PrestadorController controller = GenerateControllerFakeFuncionario(funcionarioFakeLista);
        //Act
        var response = await controller.DeletarFuncionario(funcionarioFake.Id.Value);

        //Assert
        _funcionarioService.Verify(s => s.Delete(It.IsAny<Guid>()), Times.Once());        
        Assert.NotNull(response);

    }


    [Fact]
    public async Task NaoDeve_Deletar_Um_Funcionario_RetornarBadRequest()
    {
        //Arrange
        ICollection<FuncionarioPrestadorDto> funcionarioFakeLista = CriarListaFuncionarioFake();
        FuncionarioPrestadorDto funcionarioFake = CriarFuncionarioDtoFake(Guid.NewGuid());
        _funcionarioService.Setup(s => s.Delete(It.IsAny<Guid>()));
        List<FuncionarioPrestadorDto> prestadorDtoFakeList = new List<FuncionarioPrestadorDto>();
        PrestadorController controller = GenerateControllerFakeFuncionario(funcionarioFakeLista);
        controller.ModelState.AddModelError("key", "error message");
        //Act
        var response = await controller.DeletarFuncionario(funcionarioFake.Id.Value);

        //Assert
        _funcionarioService.Verify(s => s.Delete(It.IsAny<Guid>()), Times.Never());
        Assert.NotNull(response);

    }
    #endregion


    private FuncionarioPrestadorDto CriarFuncionarioDtoFake(Guid? id)
    {
        return new FuncionarioPrestadorDto() { Cargo = "teste", CPF = "40608550817", Email = "teste@teste.com.br", Nome = "teste func", RG = "52453", Telefone = "1234556", DataCadastro = DateTime.Now, Endereco = "rua teste", Id = Guid.NewGuid(), UsrCadastro = Guid.NewGuid(), PrestadorId = Guid.NewGuid() };
    }

    private ICollection<FuncionarioPrestadorDto> CriarListaFuncionarioFake()
    {
        return new List<FuncionarioPrestadorDto>() { new FuncionarioPrestadorDto() { Cargo = "teste", CPF = "123456789", Email = "teste@teste.com.br", Nome = "teste func", RG = "52453", Telefone = "1234556", DataCadastro = DateTime.Now, Endereco = "rua teste", Id = Guid.NewGuid(), UsrCadastro = Guid.NewGuid(), PrestadorId = Guid.NewGuid() } };
    }

    private static ICollection<PrestadorDto> CriaListaFornecedoresFake()
    {
        return new List<PrestadorDto>() { new PrestadorDto() { CPF = "40608550817", EmailEmpresa = "teste@test.com.br", Nome = "Teste", Telefone = "11999999999", Id = Guid.NewGuid() } };
    }

}
