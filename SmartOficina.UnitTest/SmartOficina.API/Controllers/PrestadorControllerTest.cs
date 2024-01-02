using SmartOficina.Api.Domain.Interfaces;
using System.Net;

namespace SmartOficina.UnitTest.SmartOficina.API.Controllers;

public class PrestadorControllerTest
{
    private readonly Mock<IPrestadorService> _prestadorService = new();
    private readonly Mock<IFuncionarioService> _funcionarioService = new();

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
        return new PrestadorController(_prestadorService.Object, _funcionarioService.Object) { ControllerContext = new ControllerContext() { HttpContext = CreateFakeClaims(prestadorDtos) } };
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
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.NoContent);
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
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.NoContent);
      
    }

    [Fact]
    public async Task NaoDeve_Cadastrar_Um_Prestador_RetornarBadRequest()
    {
        //Arrange
        ICollection<PrestadorDto> prestadoresFake =  CriaListaFornecedoresFake();
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
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.NoContent);
       
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
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.NoContent);
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
        _prestadorService.Setup(s => s.Desabled(It.IsAny<Guid>())).ReturnsAsync(prestadoresFake.First());
        PrestadorController controller = GenerateControllerFake(new List<PrestadorDto>() { prestadorDtoFake });
        //Act
        var response = await controller.DesativarPrestadorServico(prestadorDtoFake.Id.Value);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as PrestadorDto;
        //Assert
        _prestadorService.Verify(s => s.Desabled(It.IsAny<Guid>()), Times.Once());
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
        _prestadorService.Setup(s => s.Desabled(It.IsAny<Guid>())).ReturnsAsync(prestadorFakeNull);
        PrestadorController controller = GenerateControllerFake(new List<PrestadorDto>() { prestadorDtoFake });
        //Act
        var response = await controller.DesativarPrestadorServico(prestadorDtoFake.Id.Value);
        var okResult = response as NoContentResult;
        
        //Assert
        _prestadorService.Verify(s => s.Desabled(It.IsAny<Guid>()), Times.Once());
        Assert.NotNull(okResult);
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task NaoDeve_Desativar_Um_Prestador_RetornarBadRequest()
    {
        //Arrange
        ICollection<PrestadorDto> prestadoresFake = CriaListaFornecedoresFake();
        PrestadorDto prestadorFakeNull = null;
        PrestadorDto prestadorDtoFake = new PrestadorDto() { Id = Guid.NewGuid(), PrestadorId = Guid.NewGuid() };
        _prestadorService.Setup(s => s.Desabled(It.IsAny<Guid>())).ReturnsAsync(prestadorFakeNull);
        PrestadorController controller = GenerateControllerFake(new List<PrestadorDto>() { prestadorDtoFake });
        controller.ModelState.AddModelError("key", "error message");
        //Act
        var response = await controller.DesativarPrestadorServico(prestadorDtoFake.Id.Value);
        var okResult = response as ObjectResult;

        //Assert
        _prestadorService.Verify(s => s.Desabled(It.IsAny<Guid>()), Times.Never());
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


    //#region Funcionários

    //private static DefaultHttpContext CreateFakeClaimsFuncionario(ICollection<FuncionarioPrestadorDto> funcionario)
    //{
    //    var fakeHttpContext = new DefaultHttpContext();
    //    ClaimsIdentity identity = new(
    //        new[] {
    //                    new Claim("PrestadorId", funcionario.First().PrestadorId.ToString()),
    //                    new Claim("UserName", "Teste"),
    //                    new Claim("IdUserLogin", funcionario.First().PrestadorId.ToString())

    //        }
    //    );
    //    fakeHttpContext.User = new System.Security.Claims.ClaimsPrincipal(identity);
    //    return fakeHttpContext;
    //}

    //private PrestadorController GenerateControllerFakeFuncionario(ICollection<FuncionarioPrestadorDto> funcionario)
    //{
    //    return new PrestadorController(_prestadorService.Object, _funcionarioService.Object) { ControllerContext = new ControllerContext() { HttpContext = CreateFakeClaimsFuncionario(funcionario) } };
    //}

    //[Fact]
    //public async Task Deve_Retornar_ListaDeFuncionarios()
    //{
    //    //Arrange
    //    ICollection<FuncionarioPrestador> funcionarioFake = CriarListaFuncionarioFake();
    //    _funcionarioService.Setup(s => s.GetAll(It.IsAny<Guid>(), It.IsAny<FuncionarioPrestador>())).ReturnsAsync(funcionarioFake);
    //    List<FuncionarioPrestadorDto> prestadorDtoFakeList = new List<FuncionarioPrestadorDto>();
    //    //Act
    //    var response = await new PrestadorController(_prestadorService.Object, _funcionarioService.Object, _mapper.Object).GetAllFuncionario(string.Empty, string.Empty, string.Empty);
    //    var okResult = response as OkObjectResult;
    //    var result = okResult.Value as ICollection<FuncionarioPrestadorDto>;
    //    //Assert
    //    _funcionarioService.Verify(s => s.GetAll(It.IsAny<Guid>(), It.IsAny<FuncionarioPrestador>()), Times.Once());
    //    _mapper.Verify(s => s.Map<FuncionarioPrestador>(It.IsAny<FuncionarioPrestadorDto>), Times.Once());
    //    _mapper.Verify(s => s.Map<FuncionarioPrestadorDto>(It.IsAny<FuncionarioPrestador>), Times.Once());
    //    Assert.NotNull(result);
    //    Assert.Equal(result.First().Telefone, prestadorDtoFakeList.First().Telefone);
    //    Assert.Equal(result.First().CPF, prestadorDtoFakeList.First().CPF);
    //    Assert.Equal(result.First().DataCadastro, prestadorDtoFakeList.First().DataCadastro);
    //    Assert.Equal(result.First().Id, prestadorDtoFakeList.First().Id);
    //    Assert.Equal(result.First().Cargo, prestadorDtoFakeList.First().Cargo);
    //    Assert.Equal(result.First().Email, prestadorDtoFakeList.First().Email);
    //    Assert.Equal(result.First().Endereco, prestadorDtoFakeList.First().Endereco);
    //    Assert.Equal(result.First().Nome, prestadorDtoFakeList.First().Nome);

    //}

    //[Fact]
    //public async Task Deve_Cadastrar_Um_FuncionarioPrestador_RetornarSucesso()
    //{
    //    //Arrange
    //    Guid? id = null;
    //    FuncionarioPrestador funcionarioFake = CriarFuncionarioFake(Guid.NewGuid());
    //    FuncionarioPrestadorDto funcionarioDtoFake = CriarFuncionarioDtoFake(id);

    //    _funcionarioService.Setup(s => s.Create(It.IsAny<FuncionarioPrestador>())).ReturnsAsync(funcionarioFake);
    //    _mapper.Setup(s => s.Map<FuncionarioPrestadorDto>(It.IsAny<FuncionarioPrestador>())).Returns(funcionarioDtoFake);
    //    var controller = GenerateControllerFakeFuncionario(new List<FuncionarioPrestadorDto>() { funcionarioDtoFake });
    //    //Act
    //    var response = await controller.AddFuncionario(funcionarioDtoFake);
    //    var okResult = response as OkObjectResult;
    //    var result = okResult.Value as FuncionarioPrestadorDto;
    //    //Assert
    //    _funcionarioService.Verify(s => s.Create(It.IsAny<FuncionarioPrestador>()), Times.Once());
    //    _mapper.Verify(s => s.Map<FuncionarioPrestadorDto>(It.IsAny<FuncionarioPrestador>()), Times.Once());

    //    Assert.NotNull(result);
    //    Assert.Equal(result.Telefone, funcionarioDtoFake.Telefone);
    //    Assert.Equal(result.CPF, funcionarioDtoFake.CPF);
    //    Assert.Equal(result.DataCadastro, funcionarioDtoFake.DataCadastro);
    //    Assert.Equal(result.Id, funcionarioDtoFake.Id);
    //    Assert.Equal(result.Email, funcionarioDtoFake.Email);
    //    Assert.Equal(result.Endereco, funcionarioDtoFake.Endereco);
    //    Assert.Equal(result.Nome, funcionarioDtoFake.Nome);
    //}

    //[Fact]
    //public async Task Deve_Retornarar_Um_FuncionarioPrestador_RetornarSucesso()
    //{
    //    //Arrange
    //    Guid? id = null;
    //    FuncionarioPrestador funcionarioFake = CriarFuncionarioFake(Guid.NewGuid());
    //    FuncionarioPrestadorDto funcionarioDtoFake = CriarFuncionarioDtoFake(id);

    //    _funcionarioService.Setup(s => s.FindById(It.IsAny<Guid>())).ReturnsAsync(funcionarioFake);
    //    _mapper.Setup(s => s.Map<FuncionarioPrestadorDto>(It.IsAny<FuncionarioPrestador>())).Returns(funcionarioDtoFake);
    //    var controller = GenerateControllerFakeFuncionario(new List<FuncionarioPrestadorDto>() { funcionarioDtoFake });
    //    //Act
    //    var response = await controller.GetIdFuncionario(funcionarioFake.Id);
    //    var okResult = response as OkObjectResult;
    //    var result = okResult.Value as FuncionarioPrestadorDto;
    //    //Assert
    //    _funcionarioService.Verify(s => s.FindById(It.IsAny<Guid>()), Times.Once());
    //    _mapper.Verify(s => s.Map<FuncionarioPrestadorDto>(It.IsAny<FuncionarioPrestador>()), Times.Once());
    //    Assert.NotNull(result);
    //    Assert.Equal(result.Telefone, funcionarioDtoFake.Telefone);
    //    Assert.Equal(result.CPF, funcionarioDtoFake.CPF);
    //    Assert.Equal(result.DataCadastro, funcionarioDtoFake.DataCadastro);
    //    Assert.Equal(result.Id, funcionarioDtoFake.Id);
    //    Assert.Equal(result.Email, funcionarioDtoFake.Email);
    //    Assert.Equal(result.Endereco, funcionarioDtoFake.Endereco);
    //    Assert.Equal(result.Nome, funcionarioDtoFake.Nome);
    //}

    //[Fact]
    //public async Task Deve_Atualizar_Um_Funcionario_RetornarSucesso()
    //{
    //    //Arrange
    //    Guid? id = null;
    //    FuncionarioPrestador funcionarioFake = CriarFuncionarioFake(Guid.NewGuid());
    //    FuncionarioPrestadorDto funcionarioDtoFake = CriarFuncionarioDtoFake(id);

    //    _funcionarioService.Setup(s => s.Update(It.IsAny<FuncionarioPrestador>())).ReturnsAsync(funcionarioFake);
    //    _mapper.Setup(s => s.Map<FuncionarioPrestadorDto>(It.IsAny<FuncionarioPrestador>())).Returns(funcionarioDtoFake);
    //    var controller = GenerateControllerFakeFuncionario(new List<FuncionarioPrestadorDto>() { funcionarioDtoFake });
    //    //Act
    //    var response = await controller.AtualizarFuncionario(funcionarioDtoFake);
    //    var okResult = response as OkObjectResult;
    //    var result = okResult.Value as FuncionarioPrestadorDto;
    //    //Assert
    //    _funcionarioService.Verify(s => s.Update(It.IsAny<FuncionarioPrestador>()), Times.Once());
    //    _mapper.Verify(s => s.Map<FuncionarioPrestadorDto>(It.IsAny<FuncionarioPrestador>()), Times.Once());
    //    Assert.NotNull(result);
    //    Assert.Equal(result.Telefone, funcionarioDtoFake.Telefone);
    //    Assert.Equal(result.CPF, funcionarioDtoFake.CPF);
    //    Assert.Equal(result.DataCadastro, funcionarioDtoFake.DataCadastro);
    //    Assert.Equal(result.Id, funcionarioDtoFake.Id);
    //    Assert.Equal(result.Email, funcionarioDtoFake.Email);
    //    Assert.Equal(result.Endereco, funcionarioDtoFake.Endereco);
    //    Assert.Equal(result.Nome, funcionarioDtoFake.Nome);
    //}


    //[Fact]
    //public async Task Deve_Desativar_Um_Funcionario_RetornarSucesso()
    //{
    //    //Arrange
    //    Guid? id = null;
    //    FuncionarioPrestador funcionarioFake = CriarFuncionarioFake(Guid.NewGuid());
    //    FuncionarioPrestadorDto funcionarioDtoFake = CriarFuncionarioDtoFake(id);

    //    _funcionarioService.Setup(s => s.Desabled(It.IsAny<Guid>())).ReturnsAsync(funcionarioFake);
    //    _mapper.Setup(s => s.Map<FuncionarioPrestador>(It.IsAny<FuncionarioPrestadorDto>())).Returns(funcionarioFake);
    //    _mapper.Setup(s => s.Map<FuncionarioPrestadorDto>(It.IsAny<FuncionarioPrestador>())).Returns(funcionarioDtoFake);
    //    //Act
    //    var response = await new PrestadorController(_prestadorService.Object, _funcionarioService.Object, _mapper.Object).DesativarFuncionario(funcionarioFake.Id);
    //    var okResult = response as OkObjectResult;
    //    var result = okResult.Value as FuncionarioPrestadorDto;
    //    //Assert
    //    _funcionarioService.Verify(s => s.Desabled(It.IsAny<Guid>()), Times.Once());
    //    _mapper.Verify(s => s.Map<Prestador>(It.IsAny<PrestadorDto>()), Times.Never());
    //    Assert.NotNull(result);
    //    Assert.Equal(result.Telefone, funcionarioDtoFake.Telefone);
    //    Assert.Equal(result.CPF, funcionarioDtoFake.CPF);
    //    Assert.Equal(result.DataCadastro, funcionarioDtoFake.DataCadastro);
    //    Assert.Equal(result.Id, funcionarioDtoFake.Id);
    //    Assert.Equal(result.Email, funcionarioDtoFake.Email);
    //    Assert.Equal(result.Endereco, funcionarioDtoFake.Endereco);
    //    Assert.Equal(result.Nome, funcionarioDtoFake.Nome);
    //}

    //[Fact]
    //public async Task Deve_Deletar_Um_Funcionario_RetornarSucesso()
    //{
    //    //Arrange
    //    Guid? id = null;
    //    FuncionarioPrestador funcionarioFake = CriarFuncionarioFake(Guid.NewGuid());
    //    FuncionarioPrestadorDto funcionarioDtoFake = CriarFuncionarioDtoFake(id);

    //    _funcionarioService.Setup(s => s.Delete(It.IsAny<Guid>()));
    //    _mapper.Setup(s => s.Map<FuncionarioPrestadorDto>(It.IsAny<FuncionarioPrestador>())).Returns(funcionarioDtoFake);
    //    //Act
    //    var response = await new PrestadorController(_prestadorService.Object, _funcionarioService.Object, _mapper.Object).DeletarFuncionario(funcionarioDtoFake.Id.Value);

    //    //Assert
    //    _funcionarioService.Verify(s => s.Delete(It.IsAny<Guid>()), Times.Once());
    //    _mapper.Verify(s => s.Map<FuncionarioPrestadorDto>(It.IsAny<FuncionarioPrestador>()), Times.Never());
    //    Assert.NotNull(response);

    //}
    //#endregion


    private FuncionarioPrestadorDto CriarFuncionarioDtoFake(Guid? id)
    {
        return new FuncionarioPrestadorDto() { Cargo = "teste", CPF = "123456789", Email = "teste@teste.com.br", Nome = "teste func", RG = "52453", Telefone = "1234556", DataCadastro = DateTime.Now, Endereco = "rua teste", Id = Guid.NewGuid(), UsrCadastro = Guid.NewGuid(), PrestadorId = Guid.NewGuid() };
    }

    private FuncionarioPrestador CriarFuncionarioFake(Guid guid)
    {
        return new FuncionarioPrestador() { Cargo = "teste", CPF = "123456789", Email = "teste@teste.com.br", Nome = "teste func", RG = "52453", Telefone = "1234556", DataCadastro = DateTime.Now, Endereco = "rua teste", Id = Guid.NewGuid(), UsrCadastro = Guid.NewGuid(), Prestador = CriaFornecedorFake(Guid.NewGuid()), PrestadorId = Guid.NewGuid() };
    }

    private ICollection<FuncionarioPrestador> CriarListaFuncionarioFake()
    {
        return new List<FuncionarioPrestador>() { new FuncionarioPrestador() { Cargo = "teste", CPF = "123456789", Email = "teste@teste.com.br", Nome = "teste func", RG = "52453", Telefone = "1234556", DataCadastro = DateTime.Now, Endereco = "rua teste", Id = Guid.NewGuid(), UsrCadastro = Guid.NewGuid(), Prestador = CriaFornecedorFake(Guid.NewGuid()), PrestadorId = Guid.NewGuid() } };
    }

    private ICollection<FuncionarioPrestadorDto> CriarListaFuncionarioDtoFake()
    {
        return new List<FuncionarioPrestadorDto>() { new FuncionarioPrestadorDto() { Cargo = "teste", CPF = "123456789", Email = "teste@teste.com.br", Nome = "teste func", RG = "52453", Telefone = "1234556", DataCadastro = DateTime.Now, Endereco = "rua teste", Id = Guid.NewGuid(), UsrCadastro = Guid.NewGuid(), PrestadorId = Guid.NewGuid() } };
    }

    private static ICollection<PrestadorDto> CriaListaFornecedoresFake()
    {
        return new List<PrestadorDto>() { new PrestadorDto() { CPF = "123456789", EmailEmpresa = "teste@test.com.br", Nome = "Teste", Telefone = "11999999999", Id = Guid.NewGuid() } };
    }

    private static ICollection<PrestadorDto> CriaListaFornecedoresDtoFake()
    {
        return new List<PrestadorDto>() { new PrestadorDto() { CPF = "123456789", EmailEmpresa = "teste@test.com.br", Nome = "Teste", Telefone = "11999999999", Id = Guid.NewGuid() } };
    }

    private static Prestador CriaFornecedorFake(Guid? Id)
    {
        return new Prestador() { CPF = "123456789", EmailEmpresa = "teste@test.com.br", Nome = "Teste", Telefone = "11999999999", Id = Id.HasValue ? Id.Value : Guid.Empty };
    }

    private static PrestadorDto CriaFornecedorDtoFake(Guid? Id)
    {
        return new PrestadorDto() { CPF = "123456789", EmailEmpresa = "teste@test.com.br", Nome = "Teste", Telefone = "11999999999", Id = Id.HasValue ? Id.Value : Guid.Empty };
    }
}
