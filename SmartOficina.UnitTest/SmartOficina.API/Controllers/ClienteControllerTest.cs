namespace SmartOficina.UnitTest.SmartOficina.API.Controllers;

public class ClienteControllerTest
{
    private readonly Mock<IClienteRepository> _repository = new();
    private readonly Mock<IMapper> _mapper = new();

    private static DefaultHttpContext CreateFakeClaims(ICollection<Cliente> clientes)
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

    private ClienteController GenerateControllerFake(ICollection<Cliente> clientesFake)
    {
        return new ClienteController(_repository.Object, _mapper.Object) { ControllerContext = new ControllerContext() { HttpContext = CreateFakeClaims(clientesFake) } };
    }

    [Fact]
    public async Task Deve_Retornar_ListaDeCliente_Usando_ParametroID()
    {
        //Arrange
        ICollection<Cliente> clientesFake = CriaListaClienteFake();
        List<ClienteDto> clientesDtoFake = new List<ClienteDto>() { new ClienteDto() { CPF = "", Email = "", Nome = "", Telefone = "" } };
        _repository.Setup(s => s.GetAll(It.IsAny<Guid>(), It.IsAny<Cliente>())).ReturnsAsync(clientesFake);
        //Act
        var response = await new ClienteController(_repository.Object, _mapper.Object).GetAll(string.Empty, string.Empty, string.Empty);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as ICollection<ClienteDto>;
        //Assert
        _repository.Verify(s => s.GetAll(It.IsAny<Guid>(), It.IsAny<Cliente>()), Times.Once());
        _mapper.Verify(s => s.Map<Cliente>(It.IsAny<ClienteDto>), Times.Never());
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
    public async Task Deve_Cadastrar_Um_Cliente_RetornarSucesso()
    {
        //Arrange
        Guid? id = null;
        Cliente clienteFake = CriaClienteFake(Guid.NewGuid());
        ClienteDto clienteDtoFake = CriaClienteDtoFake(id);

        _repository.Setup(s => s.Create(It.IsAny<Cliente>())).ReturnsAsync(clienteFake);
        _mapper.Setup(s => s.Map<ClienteDto>(It.IsAny<Cliente>())).Returns(clienteDtoFake);
        ClienteController controller = GenerateControllerFake(new List<Cliente>() { clienteFake });
        //Act
        var response = await controller.AddAsync(clienteDtoFake);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as ClienteDto;
        //Assert
        _repository.Verify(s => s.Create(It.IsAny<Cliente>()), Times.Once());
        _mapper.Verify(s => s.Map<ClienteDto>(It.IsAny<Cliente>()), Times.Once());
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
    public async Task Deve_Retornarar_Um_Cliente_RetornarSucesso()
    {
        //Arrange
        Cliente clienteFake = CriaClienteFake(Guid.NewGuid());
        ClienteDto clienteDtoFake = CriaClienteDtoFake(clienteFake.Id);

        _repository.Setup(s => s.FindById(It.IsAny<Guid>())).ReturnsAsync(clienteFake);
        _mapper.Setup(s => s.Map<ClienteDto>(It.IsAny<Cliente>())).Returns(clienteDtoFake);
        ClienteController controller = GenerateControllerFake(new List<Cliente>() { clienteFake });
        //Act
        var response = await controller.GetId(clienteDtoFake.Id.Value);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as ClienteDto;
        //Assert
        _repository.Verify(s => s.FindById(It.IsAny<Guid>()), Times.Once());
        _mapper.Verify(s => s.Map<ClienteDto>(It.IsAny<Cliente>()), Times.Once());
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
    public async Task Deve_Atualizar_Um_Cliente_RetornarSucesso()
    {
        //Arrange
        Cliente clienteFake = CriaClienteFake(Guid.NewGuid());
        ClienteDto clienteDtoFake = CriaClienteDtoFake(Guid.NewGuid());

        _repository.Setup(s => s.Update(It.IsAny<Cliente>())).ReturnsAsync(clienteFake);
        _mapper.Setup(s => s.Map<ClienteDto>(It.IsAny<Cliente>())).Returns(clienteDtoFake);
        ClienteController controller = GenerateControllerFake(new List<Cliente>() { clienteFake });
        //Act
        var response = await controller.AtualizarCliente(clienteDtoFake);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as ClienteDto;
        //Assert
        _repository.Verify(s => s.Update(It.IsAny<Cliente>()), Times.Once());
        _mapper.Verify(s => s.Map<ClienteDto>(It.IsAny<Cliente>()), Times.Once());
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
    public async Task Deve_Desativar_Um_Cliente_RetornarSucesso()
    {
        //Arrange
        Cliente clienteFake = CriaClienteFake(Guid.NewGuid());
        ClienteDto clienteDtoFake = CriaClienteDtoFake(clienteFake.Id);

        _repository.Setup(s => s.Desabled(It.IsAny<Guid>())).ReturnsAsync(clienteFake);
        _mapper.Setup(s => s.Map<ClienteDto>(It.IsAny<Cliente>())).Returns(clienteDtoFake);
        ClienteController controller = GenerateControllerFake(new List<Cliente>() { clienteFake });
        //Act
        var response = await controller.DesativarCliente(clienteDtoFake.Id.Value);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as ClienteDto;
        //Assert
        _repository.Verify(s => s.Desabled(It.IsAny<Guid>()), Times.Once());
        _mapper.Verify(s => s.Map<ClienteDto>(It.IsAny<Cliente>()), Times.Once());
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
    public async Task Deve_Deletar_Um_Cliente_RetornarSucesso()
    {
        //Arrange
        Cliente clienteFake = CriaClienteFake(Guid.NewGuid());
        ClienteDto clienteDtoFake = CriaClienteDtoFake(clienteFake.Id);

        _repository.Setup(s => s.Delete(It.IsAny<Guid>()));
        _mapper.Setup(s => s.Map<Cliente>(It.IsAny<ClienteDto>())).Returns(clienteFake);
        //Act
        var response = await new ClienteController(_repository.Object, _mapper.Object).DeletarCliente(clienteDtoFake.Id.Value);

        //Assert
        _repository.Verify(s => s.Delete(It.IsAny<Guid>()), Times.Once());
        _mapper.Verify(s => s.Map<Cliente>(It.IsAny<ClienteDto>()), Times.Never());
        Assert.NotNull(response);

    }
    private static ICollection<Cliente> CriaListaClienteFake()
    {
        return new List<Cliente>() { new Cliente() { CPF = "123456789", Email = "teste@test.com.br", Nome = "Teste", Telefone = "11999999999", Id = Guid.NewGuid(), PrestadorId = Guid.NewGuid() } };
    }
    private static ICollection<ClienteDto> CriaListaClienteDtoFake()
    {
        return new List<ClienteDto>() { new ClienteDto() { CPF = "123456789", Email = "teste@test.com.br", Nome = "Teste", Telefone = "11999999999", Id = Guid.NewGuid(), PrestadorId = Guid.NewGuid() } };
    }


    private static Cliente CriaClienteFake(Guid? Id)
    {
        return new Cliente() { CPF = "123456789", Email = "teste@test.com.br", Nome = "Teste", Telefone = "11999999999", Id = Id.HasValue ? Id.Value : Guid.Empty, PrestadorId = Guid.NewGuid() };
    }

    private static ClienteDto CriaClienteDtoFake(Guid? Id)
    {
        return new ClienteDto() { CPF = "123456789", Email = "teste@test.com.br", Nome = "Teste", Telefone = "11999999999", Id = Id.HasValue ? Id.Value : Guid.Empty, PrestadorId = Guid.NewGuid() };
    }
}
