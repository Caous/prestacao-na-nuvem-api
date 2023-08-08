namespace SmartOficina.UnitTest.SmartOficina.API.Controllers;

public class ClienteControllerTest
{
    private readonly Mock<IClienteRepository> _repository = new();
    private readonly Mock<IMapper> _mapper = new();

    [Fact]
    public async Task Deve_Retornar_ListaDeCliente_Usando_ParametroID()
    {
        //Arrange
        ICollection<Cliente> clientesFake = CriaListaClienteFake();
        _repository.Setup(s => s.GetAll()).ReturnsAsync(clientesFake);
        //Act
        var response = await new ClienteController(_repository.Object, _mapper.Object).GetAll();
        var okResult = response as OkObjectResult;
        var result = okResult.Value as ICollection<Cliente>;
        //Assert
        _repository.Verify(s => s.GetAll(), Times.Once());
        _mapper.Verify(s => s.Map<Cliente>(It.IsAny<ClienteDto>), Times.Never());
        Assert.NotNull(result);
        Assert.Equal(result.First().Telefone, clientesFake.First().Telefone);
        Assert.Equal(result.First().CPF, clientesFake.First().CPF);
        Assert.Equal(result.First().DataCadastro, clientesFake.First().DataCadastro);
        Assert.Equal(result.First().Id, clientesFake.First().Id);
        Assert.Equal(result.First().Email, clientesFake.First().Email);
        Assert.Equal(result.First().Endereco, clientesFake.First().Endereco);
        Assert.Equal(result.First().Nome, clientesFake.First().Nome);
        Assert.Equal(result.First().Rg, clientesFake.First().Rg);
    }

    [Fact]
    public async Task Deve_Cadastrar_Um_Cliente_RetornarSucesso()
    {
        //Arrange
        Guid? id = null;
        Cliente clienteFake = CriaClienteFake(Guid.NewGuid());
        ClienteDto clienteDtoFake = CriaClienteDtoFake(id);

        _repository.Setup(s => s.Create(It.IsAny<Cliente>())).ReturnsAsync(clienteFake);
        _mapper.Setup(s => s.Map<Cliente>(It.IsAny<ClienteDto>())).Returns(clienteFake);
        //Act
        var response = await new ClienteController(_repository.Object, _mapper.Object).AddAsync(clienteDtoFake);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as Cliente;
        //Assert
        _repository.Verify(s => s.Create(It.IsAny<Cliente>()), Times.Once());
        _mapper.Verify(s => s.Map<Cliente>(It.IsAny<ClienteDto>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(result.Telefone, clienteFake.Telefone);
        Assert.Equal(result.CPF, clienteFake.CPF);
        Assert.Equal(result.DataCadastro, clienteFake.DataCadastro);
        Assert.Equal(result.Id, clienteFake.Id);
        Assert.Equal(result.Email, clienteFake.Email);
        Assert.Equal(result.Endereco, clienteFake.Endereco);
        Assert.Equal(result.Nome, clienteFake.Nome);
        Assert.Equal(result.Rg, clienteFake.Rg);
    }

    [Fact]
    public async Task Deve_Retornarar_Um_Cliente_RetornarSucesso()
    {
        //Arrange
        Cliente clienteFake = CriaClienteFake(Guid.NewGuid());
        ClienteDto clienteDtoFake = CriaClienteDtoFake(clienteFake.Id);

        _repository.Setup(s => s.FindById(It.IsAny<Guid>())).ReturnsAsync(clienteFake);
        _mapper.Setup(s => s.Map<Cliente>(It.IsAny<ClienteDto>())).Returns(clienteFake);
        //Act
        var response = await new ClienteController(_repository.Object, _mapper.Object).GetId(clienteDtoFake.Id.Value);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as Cliente;
        //Assert
        _repository.Verify(s => s.FindById(It.IsAny<Guid>()), Times.Once());
        _mapper.Verify(s => s.Map<Cliente>(It.IsAny<ClienteDto>()), Times.Never());
        Assert.NotNull(result);
        Assert.Equal(result.Telefone, clienteFake.Telefone);
        Assert.Equal(result.CPF, clienteFake.CPF);
        Assert.Equal(result.DataCadastro, clienteFake.DataCadastro);
        Assert.Equal(result.Id, clienteFake.Id);
        Assert.Equal(result.Email, clienteFake.Email);
        Assert.Equal(result.Endereco, clienteFake.Endereco);
        Assert.Equal(result.Nome, clienteFake.Nome);
        Assert.Equal(result.Rg, clienteFake.Rg);
    }

    [Fact]
    public async Task Deve_Atualizar_Um_Cliente_RetornarSucesso()
    {
        //Arrange
        Cliente clienteFake = CriaClienteFake(Guid.NewGuid());
        ClienteDto clienteDtoFake = CriaClienteDtoFake(Guid.NewGuid());

        _repository.Setup(s => s.Update(It.IsAny<Cliente>())).ReturnsAsync(clienteFake);
        _mapper.Setup(s => s.Map<Cliente>(It.IsAny<ClienteDto>())).Returns(clienteFake);
        //Act
        var response = await new ClienteController(_repository.Object, _mapper.Object).AtualizarCliente(clienteDtoFake);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as Cliente;
        //Assert
        _repository.Verify(s => s.Update(It.IsAny<Cliente>()), Times.Once());
        _mapper.Verify(s => s.Map<Cliente>(It.IsAny<ClienteDto>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(result.Telefone, clienteFake.Telefone);
        Assert.Equal(result.CPF, clienteFake.CPF);
        Assert.Equal(result.DataCadastro, clienteFake.DataCadastro);
        Assert.Equal(result.Id, clienteFake.Id);
        Assert.Equal(result.Email, clienteFake.Email);
        Assert.Equal(result.Endereco, clienteFake.Endereco);
        Assert.Equal(result.Nome, clienteFake.Nome);
        Assert.Equal(result.Rg, clienteFake.Rg);
    }


    [Fact]
    public async Task Deve_Desativar_Um_Cliente_RetornarSucesso()
    {
        //Arrange
        Cliente clienteFake = CriaClienteFake(Guid.NewGuid());
        ClienteDto clienteDtoFake = CriaClienteDtoFake(clienteFake.Id);

        _repository.Setup(s => s.Desabled(It.IsAny<Guid>())).ReturnsAsync(clienteFake);
        _mapper.Setup(s => s.Map<Cliente>(It.IsAny<ClienteDto>())).Returns(clienteFake);
        //Act
        var response = await new ClienteController(_repository.Object, _mapper.Object).DesativarCliente(clienteDtoFake.Id.Value);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as Cliente;
        //Assert
        _repository.Verify(s => s.Desabled(It.IsAny<Guid>()), Times.Once());
        _mapper.Verify(s => s.Map<Cliente>(It.IsAny<ClienteDto>()), Times.Never());
        Assert.NotNull(result);
        Assert.Equal(result.Telefone, clienteFake.Telefone);
        Assert.Equal(result.CPF, clienteFake.CPF);
        Assert.Equal(result.DataCadastro, clienteFake.DataCadastro);
        Assert.Equal(result.Id, clienteFake.Id);
        Assert.Equal(result.Email, clienteFake.Email);
        Assert.Equal(result.Endereco, clienteFake.Endereco);
        Assert.Equal(result.Nome, clienteFake.Nome);
        Assert.Equal(result.Rg, clienteFake.Rg);
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


    private static Cliente CriaClienteFake(Guid? Id)
    {
        return new Cliente() { CPF = "123456789", Email = "teste@test.com.br", Nome = "Teste", Telefone = "11999999999", Id = Id.HasValue ? Id.Value : Guid.Empty, PrestadorId = Guid.NewGuid() };
    }

    private static ClienteDto CriaClienteDtoFake(Guid? Id)
    {
        return new ClienteDto() { CPF = "123456789", Email = "teste@test.com.br", Nome = "Teste", Telefone = "11999999999", Id = Id.HasValue ? Id.Value : Guid.Empty, PrestadorId = Guid.NewGuid() };
    }
}
