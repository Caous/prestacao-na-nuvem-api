namespace SmartOficina.UnitTest.SmartOficina.API.Controllers;

public class PrestadorControllerTest
{
    private readonly Mock<IPrestadorRepository> _repository = new();
    private readonly Mock<IMapper> _mapper = new();
    private readonly Mock<IFuncionarioPrestadorRepository> _funcionarioRepository = new();

    #region Prestador 

    private static DefaultHttpContext CreateFakeClaimsPrestador(ICollection<PrestadorDto> prestador)
    {
        var fakeHttpContext = new DefaultHttpContext();
        ClaimsIdentity identity = new(
            new[] {
                        new Claim("PrestadorId", prestador.First().Id.ToString()),
                        new Claim("UserName", "Teste"),
                        new Claim("IdUserLogin", prestador.First().Id.ToString())

            }
        );
        fakeHttpContext.User = new System.Security.Claims.ClaimsPrincipal(identity);
        return fakeHttpContext;
    }
    private PrestadorController GenerateControllerFake(ICollection<PrestadorDto> PrestadorDtoFake)
    {
        return new PrestadorController(_repository.Object, _funcionarioRepository.Object, _mapper.Object) { ControllerContext = new ControllerContext() { HttpContext = CreateFakeClaimsPrestador(PrestadorDtoFake) } };
    }


    [Fact]
    public async Task Deve_Retornar_ListaDePrestador()
    {
        //Arrange
        ICollection<Prestador> PrestadorsFake = CriaListaFornecedoresFake();
        ICollection<PrestadorDto> PrestadorDtoFake = CriaListaFornecedoresDtoFake();
        _repository.Setup(s => s.GetAll(It.IsAny<Guid>())).ReturnsAsync(PrestadorsFake);
        _mapper.Setup(x => x.Map<ICollection<PrestadorDto>>(It.IsAny<ICollection<Prestador>>())).Returns(PrestadorDtoFake);
        PrestadorController controller = GenerateControllerFake(PrestadorDtoFake);
        //Act
        var response = await controller.GetAll();
        var okResult = response as OkObjectResult;
        var result = okResult.Value as ICollection<PrestadorDto>;
        //Assert
        _repository.Verify(s => s.GetAll(It.IsAny<Guid>()), Times.Once());
        _mapper.Verify(s => s.Map<ICollection<PrestadorDto>>(It.IsAny<ICollection<Prestador>>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(result.First().Telefone, PrestadorDtoFake.First().Telefone);
        Assert.Equal(result.First().CPF, PrestadorDtoFake.First().CPF);
        Assert.Equal(result.First().DataCadastro, PrestadorDtoFake.First().DataCadastro);
        Assert.Equal(result.First().Id, PrestadorDtoFake.First().Id);
        Assert.Equal(result.First().EmailEmpresa, PrestadorDtoFake.First().EmailEmpresa);
        Assert.Equal(result.First().Endereco, PrestadorDtoFake.First().Endereco);
        Assert.Equal(result.First().Nome, PrestadorDtoFake.First().Nome);

    }


    [Fact]
    public async Task Deve_Cadastrar_Um_Prestador_RetornarSucesso()
    {
        //Arrange
        Guid? id = null;
        Prestador prestadorFake = CriaFornecedorFake(Guid.NewGuid());
        PrestadorDto prestadorDtoFake = CriaFornecedorDtoFake(id);

        _repository.Setup(s => s.Create(It.IsAny<Prestador>())).ReturnsAsync(prestadorFake);
        _mapper.Setup(s => s.Map<PrestadorDto>(It.IsAny<Prestador>())).Returns(prestadorDtoFake);
        PrestadorController controller = GenerateControllerFake(new List<PrestadorDto>() { prestadorDtoFake });
        //Act
        var response = await controller.Add(prestadorDtoFake);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as PrestadorDto;
        //Assert
        _repository.Verify(s => s.Create(It.IsAny<Prestador>()), Times.Once());
        _mapper.Verify(s => s.Map<PrestadorDto>(It.IsAny<Prestador>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(result.Telefone, prestadorDtoFake.Telefone);
        Assert.Equal(result.CPF, prestadorDtoFake.CPF);
        Assert.Equal(result.DataCadastro, prestadorDtoFake.DataCadastro);
        Assert.Equal(result.Id, prestadorDtoFake.Id);
        Assert.Equal(result.EmailEmpresa, prestadorDtoFake.EmailEmpresa);
        Assert.Equal(result.Endereco, prestadorDtoFake.Endereco);
        Assert.Equal(result.Nome, prestadorDtoFake.Nome);
    }

    [Fact]
    public async Task Deve_Retornarar_Um_Prestador_RetornarSucesso()
    {
        //Arrange
        Prestador prestadorFake = CriaFornecedorFake(Guid.NewGuid());
        PrestadorDto prestadorDtoFake = CriaFornecedorDtoFake(prestadorFake.Id);

        _repository.Setup(s => s.FindById(It.IsAny<Guid>())).ReturnsAsync(prestadorFake);
        _mapper.Setup(s => s.Map<PrestadorDto>(It.IsAny<Prestador>())).Returns(prestadorDtoFake);
        PrestadorController controller = GenerateControllerFake(new List<PrestadorDto>() { prestadorDtoFake });
        //Act
        var response = await controller.GetId(prestadorFake.Id);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as PrestadorDto;
        //Assert
        _repository.Verify(s => s.FindById(It.IsAny<Guid>()), Times.Once());
        _mapper.Verify(s => s.Map<PrestadorDto>(It.IsAny<Prestador>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(result.Telefone, prestadorDtoFake.Telefone);
        Assert.Equal(result.CPF, prestadorDtoFake.CPF);
        Assert.Equal(result.DataCadastro, prestadorDtoFake.DataCadastro);
        Assert.Equal(result.Id, prestadorDtoFake.Id);
        Assert.Equal(result.EmailEmpresa, prestadorDtoFake.EmailEmpresa);
        Assert.Equal(result.Endereco, prestadorDtoFake.Endereco);
        Assert.Equal(result.Nome, prestadorDtoFake.Nome);
    }

    [Fact]
    public async Task Deve_Atualizar_Um_Prestador_RetornarSucesso()
    {
        //Arrange
        Prestador prestadorFake = CriaFornecedorFake(Guid.NewGuid());
        PrestadorDto prestadorDtoFake = CriaFornecedorDtoFake(Guid.NewGuid());

        _repository.Setup(s => s.Update(It.IsAny<Prestador>())).ReturnsAsync(prestadorFake);
        _mapper.Setup(s => s.Map<PrestadorDto>(It.IsAny<Prestador>())).Returns(prestadorDtoFake);
        PrestadorController controller = GenerateControllerFake(new List<PrestadorDto>() { prestadorDtoFake });
        //Act
        var response = await controller.AtualizarPrestador(prestadorDtoFake);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as PrestadorDto;
        //Assert
        _repository.Verify(s => s.Update(It.IsAny<Prestador>()), Times.Once());
        _mapper.Verify(s => s.Map<PrestadorDto>(It.IsAny<Prestador>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(result.Telefone, prestadorDtoFake.Telefone);
        Assert.Equal(result.CPF, prestadorDtoFake.CPF);
        Assert.Equal(result.DataCadastro, prestadorDtoFake.DataCadastro);
        Assert.Equal(result.Id, prestadorDtoFake.Id);
        Assert.Equal(result.EmailEmpresa, prestadorDtoFake.EmailEmpresa);
        Assert.Equal(result.Endereco, prestadorDtoFake.Endereco);
        Assert.Equal(result.Nome, prestadorDtoFake.Nome);
    }


    [Fact]
    public async Task Deve_Desativar_Um_Prestador_RetornarSucesso()
    {
        //Arrange
        Prestador prestadorFake = CriaFornecedorFake(Guid.NewGuid());
        PrestadorDto prestadorDtoFake = CriaFornecedorDtoFake(prestadorFake.Id);

        _repository.Setup(s => s.Desabled(It.IsAny<Guid>())).ReturnsAsync(prestadorFake);
        _mapper.Setup(s => s.Map<PrestadorDto>(It.IsAny<Prestador>())).Returns(prestadorDtoFake);
        PrestadorController controller = GenerateControllerFake(new List<PrestadorDto>() { prestadorDtoFake });
        //Act
        var response = await controller.DesativarPrestadorServico(prestadorDtoFake.Id.Value);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as PrestadorDto;
        //Assert
        _repository.Verify(s => s.Desabled(It.IsAny<Guid>()), Times.Once());
        _mapper.Verify(s => s.Map<PrestadorDto>(It.IsAny<Prestador>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(result.Telefone, prestadorDtoFake.Telefone);
        Assert.Equal(result.CPF, prestadorDtoFake.CPF);
        Assert.Equal(result.DataCadastro, prestadorDtoFake.DataCadastro);
        Assert.Equal(result.Id, prestadorDtoFake.Id);
        Assert.Equal(result.EmailEmpresa, prestadorDtoFake.EmailEmpresa);
        Assert.Equal(result.Endereco, prestadorDtoFake.Endereco);
        Assert.Equal(result.Nome, prestadorDtoFake.Nome);
    }

    [Fact]
    public async Task Deve_Deletar_Um_Prestador_RetornarSucesso()
    {
        //Arrange
        Prestador PrestadorFake = CriaFornecedorFake(Guid.NewGuid());
        PrestadorDto PrestadorDtoFake = CriaFornecedorDtoFake(PrestadorFake.Id);

        _repository.Setup(s => s.Delete(It.IsAny<Guid>()));
        _mapper.Setup(s => s.Map<Prestador>(It.IsAny<PrestadorDto>())).Returns(PrestadorFake);
        //Act
        var response = await new PrestadorController(_repository.Object, _funcionarioRepository.Object, _mapper.Object).DeletarPrestador(PrestadorDtoFake.Id.Value);

        //Assert
        _repository.Verify(s => s.Delete(It.IsAny<Guid>()), Times.Once());
        _mapper.Verify(s => s.Map<Prestador>(It.IsAny<PrestadorDto>()), Times.Never());
        Assert.NotNull(response);

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
        return new PrestadorController(_repository.Object, _funcionarioRepository.Object, _mapper.Object) { ControllerContext = new ControllerContext() { HttpContext = CreateFakeClaimsFuncionario(funcionario) } };
    }

    [Fact]
    public async Task Deve_Retornar_ListaDeFuncionarios()
    {
        //Arrange
        ICollection<FuncionarioPrestador> funcionarioFake = CriarListaFuncionarioFake();
        ICollection<FuncionarioPrestadorDto> funcionarioDtoFake = CriarListaFuncionarioDtoFake();
        _funcionarioRepository.Setup(s => s.GetAll(It.IsAny<Guid>())).ReturnsAsync(funcionarioFake);
        _mapper.Setup(x => x.Map<ICollection<FuncionarioPrestadorDto>>(It.IsAny<ICollection<FuncionarioPrestador>>())).Returns(funcionarioDtoFake);
        var controller = GenerateControllerFakeFuncionario(funcionarioDtoFake);
        //Act
        var response = await controller.GetAllFuncionario();
        var okResult = response as OkObjectResult;
        var result = okResult.Value as ICollection<FuncionarioPrestadorDto>;
        //Assert
        _funcionarioRepository.Verify(s => s.GetAll(It.IsAny<Guid>()), Times.Once());
        _mapper.Verify(s => s.Map<ICollection<FuncionarioPrestadorDto>>(It.IsAny<ICollection<FuncionarioPrestador>>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(result.First().Telefone, funcionarioDtoFake.First().Telefone);
        Assert.Equal(result.First().CPF, funcionarioDtoFake.First().CPF);
        Assert.Equal(result.First().DataCadastro, funcionarioDtoFake.First().DataCadastro);
        Assert.Equal(result.First().Id, funcionarioDtoFake.First().Id);
        Assert.Equal(result.First().Cargo, funcionarioDtoFake.First().Cargo);
        Assert.Equal(result.First().Email, funcionarioDtoFake.First().Email);
        Assert.Equal(result.First().Endereco, funcionarioDtoFake.First().Endereco);
        Assert.Equal(result.First().Nome, funcionarioDtoFake.First().Nome);

    }

    [Fact]
    public async Task Deve_Cadastrar_Um_FuncionarioPrestador_RetornarSucesso()
    {
        //Arrange
        Guid? id = null;
        FuncionarioPrestador funcionarioFake = CriarFuncionarioFake(Guid.NewGuid());
        FuncionarioPrestadorDto funcionarioDtoFake = CriarFuncionarioDtoFake(id);

        _funcionarioRepository.Setup(s => s.Create(It.IsAny<FuncionarioPrestador>())).ReturnsAsync(funcionarioFake);
        _mapper.Setup(s => s.Map<FuncionarioPrestadorDto>(It.IsAny<FuncionarioPrestador>())).Returns(funcionarioDtoFake);
        var controller = GenerateControllerFakeFuncionario(new List<FuncionarioPrestadorDto>() { funcionarioDtoFake });
        //Act
        var response = await controller.AddFuncionario(funcionarioDtoFake);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as FuncionarioPrestadorDto;
        //Assert
        _funcionarioRepository.Verify(s => s.Create(It.IsAny<FuncionarioPrestador>()), Times.Once());
        _mapper.Verify(s => s.Map<FuncionarioPrestadorDto>(It.IsAny<FuncionarioPrestador>()), Times.Once());

        Assert.NotNull(result);
        Assert.Equal(result.Telefone, funcionarioDtoFake.Telefone);
        Assert.Equal(result.CPF, funcionarioDtoFake.CPF);
        Assert.Equal(result.DataCadastro, funcionarioDtoFake.DataCadastro);
        Assert.Equal(result.Id, funcionarioDtoFake.Id);
        Assert.Equal(result.Email, funcionarioDtoFake.Email);
        Assert.Equal(result.Endereco, funcionarioDtoFake.Endereco);
        Assert.Equal(result.Nome, funcionarioDtoFake.Nome);
    }

    [Fact]
    public async Task Deve_Retornarar_Um_FuncionarioPrestador_RetornarSucesso()
    {
        //Arrange
        Guid? id = null;
        FuncionarioPrestador funcionarioFake = CriarFuncionarioFake(Guid.NewGuid());
        FuncionarioPrestadorDto funcionarioDtoFake = CriarFuncionarioDtoFake(id);

        _funcionarioRepository.Setup(s => s.FindById(It.IsAny<Guid>())).ReturnsAsync(funcionarioFake);
        _mapper.Setup(s => s.Map<FuncionarioPrestadorDto>(It.IsAny<FuncionarioPrestador>())).Returns(funcionarioDtoFake);
        var controller = GenerateControllerFakeFuncionario(new List<FuncionarioPrestadorDto>() { funcionarioDtoFake });
        //Act
        var response = await controller.GetIdFuncionario(funcionarioFake.Id);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as FuncionarioPrestadorDto;
        //Assert
        _funcionarioRepository.Verify(s => s.FindById(It.IsAny<Guid>()), Times.Once());
        _mapper.Verify(s => s.Map<FuncionarioPrestadorDto>(It.IsAny<FuncionarioPrestador>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(result.Telefone, funcionarioDtoFake.Telefone);
        Assert.Equal(result.CPF, funcionarioDtoFake.CPF);
        Assert.Equal(result.DataCadastro, funcionarioDtoFake.DataCadastro);
        Assert.Equal(result.Id, funcionarioDtoFake.Id);
        Assert.Equal(result.Email, funcionarioDtoFake.Email);
        Assert.Equal(result.Endereco, funcionarioDtoFake.Endereco);
        Assert.Equal(result.Nome, funcionarioDtoFake.Nome);
    }

    [Fact]
    public async Task Deve_Atualizar_Um_Funcionario_RetornarSucesso()
    {
        //Arrange
        Guid? id = null;
        FuncionarioPrestador funcionarioFake = CriarFuncionarioFake(Guid.NewGuid());
        FuncionarioPrestadorDto funcionarioDtoFake = CriarFuncionarioDtoFake(id);

        _funcionarioRepository.Setup(s => s.Update(It.IsAny<FuncionarioPrestador>())).ReturnsAsync(funcionarioFake);
        _mapper.Setup(s => s.Map<FuncionarioPrestadorDto>(It.IsAny<FuncionarioPrestador>())).Returns(funcionarioDtoFake);
        var controller = GenerateControllerFakeFuncionario(new List<FuncionarioPrestadorDto>() { funcionarioDtoFake });
        //Act
        var response = await controller.AtualizarFuncionario(funcionarioDtoFake);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as FuncionarioPrestadorDto;
        //Assert
        _funcionarioRepository.Verify(s => s.Update(It.IsAny<FuncionarioPrestador>()), Times.Once());
        _mapper.Verify(s => s.Map<FuncionarioPrestadorDto>(It.IsAny<FuncionarioPrestador>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(result.Telefone, funcionarioDtoFake.Telefone);
        Assert.Equal(result.CPF, funcionarioDtoFake.CPF);
        Assert.Equal(result.DataCadastro, funcionarioDtoFake.DataCadastro);
        Assert.Equal(result.Id, funcionarioDtoFake.Id);
        Assert.Equal(result.Email, funcionarioDtoFake.Email);
        Assert.Equal(result.Endereco, funcionarioDtoFake.Endereco);
        Assert.Equal(result.Nome, funcionarioDtoFake.Nome);
    }


    [Fact]
    public async Task Deve_Desativar_Um_Funcionario_RetornarSucesso()
    {
        //Arrange
        Guid? id = null;
        FuncionarioPrestador funcionarioFake = CriarFuncionarioFake(Guid.NewGuid());
        FuncionarioPrestadorDto funcionarioDtoFake = CriarFuncionarioDtoFake(id);

        _funcionarioRepository.Setup(s => s.Desabled(It.IsAny<Guid>())).ReturnsAsync(funcionarioFake);
        _mapper.Setup(s => s.Map<FuncionarioPrestador>(It.IsAny<FuncionarioPrestadorDto>())).Returns(funcionarioFake);
        _mapper.Setup(s => s.Map<FuncionarioPrestadorDto>(It.IsAny<FuncionarioPrestador>())).Returns(funcionarioDtoFake);
        //Act
        var response = await new PrestadorController(_repository.Object, _funcionarioRepository.Object, _mapper.Object).DesativarFuncionario(funcionarioFake.Id);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as FuncionarioPrestadorDto;
        //Assert
        _funcionarioRepository.Verify(s => s.Desabled(It.IsAny<Guid>()), Times.Once());
        _mapper.Verify(s => s.Map<Prestador>(It.IsAny<PrestadorDto>()), Times.Never());
        Assert.NotNull(result);
        Assert.Equal(result.Telefone, funcionarioDtoFake.Telefone);
        Assert.Equal(result.CPF, funcionarioDtoFake.CPF);
        Assert.Equal(result.DataCadastro, funcionarioDtoFake.DataCadastro);
        Assert.Equal(result.Id, funcionarioDtoFake.Id);
        Assert.Equal(result.Email, funcionarioDtoFake.Email);
        Assert.Equal(result.Endereco, funcionarioDtoFake.Endereco);
        Assert.Equal(result.Nome, funcionarioDtoFake.Nome);
    }

    [Fact]
    public async Task Deve_Deletar_Um_Funcionario_RetornarSucesso()
    {
        //Arrange
        Guid? id = null;
        FuncionarioPrestador funcionarioFake = CriarFuncionarioFake(Guid.NewGuid());
        FuncionarioPrestadorDto funcionarioDtoFake = CriarFuncionarioDtoFake(id);

        _funcionarioRepository.Setup(s => s.Delete(It.IsAny<Guid>()));
        _mapper.Setup(s => s.Map<FuncionarioPrestadorDto>(It.IsAny<FuncionarioPrestador>())).Returns(funcionarioDtoFake);
        //Act
        var response = await new PrestadorController(_repository.Object, _funcionarioRepository.Object, _mapper.Object).DeletarFuncionario(funcionarioDtoFake.Id.Value);

        //Assert
        _funcionarioRepository.Verify(s => s.Delete(It.IsAny<Guid>()), Times.Once());
        _mapper.Verify(s => s.Map<FuncionarioPrestadorDto>(It.IsAny<FuncionarioPrestador>()), Times.Never());
        Assert.NotNull(response);

    }
    #endregion


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

    private static ICollection<Prestador> CriaListaFornecedoresFake()
    {
        return new List<Prestador>() { new Prestador() { CPF = "123456789", EmailEmpresa = "teste@test.com.br", Nome = "Teste", Telefone = "11999999999", Id = Guid.NewGuid() } };
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
