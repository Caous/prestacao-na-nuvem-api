//namespace SmartOficina.UnitTest.Controllers;

//public class PrestadorControllerTest
//{
//    private readonly Mock<IPrestadorRepository> _repository = new();
//    private readonly Mock<IMapper> _mapper = new();

//    [Fact]
//    public async Task Deve_Retornar_ListaDePrestador_Usando()
//    {
//        //Arrange
//        ICollection<Prestador> PrestadorsFake = CriaListaFornecedoresFake();
//        _repository.Setup(s => s.GetAll()).ReturnsAsync(PrestadorsFake);
//        //Act
//        var response = await new PrestadorController(_repository.Object, _mapper.Object).GetAll();
//        var okResult = response as OkObjectResult;
//        var result = okResult.Value as ICollection<Prestador>;
//        //Assert
//        _repository.Verify(s => s.GetAll(), Times.Once());
//        _mapper.Verify(s => s.Map<Prestador>(It.IsAny<PrestadorDto>), Times.Never());
//        Assert.NotNull(result);
//        Assert.Equal(result.First().Telefone, PrestadorsFake.First().Telefone);
//        Assert.Equal(result.First().CPF, PrestadorsFake.First().CPF);
//        Assert.Equal(result.First().DataCadastro, PrestadorsFake.First().DataCadastro);
//        Assert.Equal(result.First().Id, PrestadorsFake.First().Id);
//        Assert.Equal(result.First().Email, PrestadorsFake.First().Email);
//        Assert.Equal(result.First().Endereco, PrestadorsFake.First().Endereco);
//        Assert.Equal(result.First().Nome, PrestadorsFake.First().Nome);
        
//    }

//    [Fact]
//    public async Task Deve_Cadastrar_Um_Prestador_RetornarSucesso()
//    {
//        //Arrange
//        Guid? id = null;
//        Prestador PrestadorFake = CriaFornecedorFake(Guid.NewGuid());
//        PrestadorDto PrestadorDtoFake = CriaFornecedorDtoFake(id);

//        _repository.Setup(s => s.Create(It.IsAny<Prestador>())).ReturnsAsync(PrestadorFake);
//        _mapper.Setup(s => s.Map<Prestador>(It.IsAny<PrestadorDto>())).Returns(PrestadorFake);
//        //Act
//        var response = await new PrestadorController(_repository.Object, _mapper.Object).Add(PrestadorDtoFake);
//        var okResult = response as OkObjectResult;
//        var result = okResult.Value as Prestador;
//        //Assert
//        _repository.Verify(s => s.Create(It.IsAny<Prestador>()), Times.Once());
//        _mapper.Verify(s => s.Map<Prestador>(It.IsAny<PrestadorDto>()), Times.Once());
//        Assert.NotNull(result);
//        Assert.Equal(result.Telefone, PrestadorFake.Telefone);
//        Assert.Equal(result.CPF, PrestadorFake.CPF);
//        Assert.Equal(result.DataCadastro, PrestadorFake.DataCadastro);
//        Assert.Equal(result.Id, PrestadorFake.Id);
//        Assert.Equal(result.Email, PrestadorFake.Email);
//        Assert.Equal(result.Endereco, PrestadorFake.Endereco);
//        Assert.Equal(result.Nome, PrestadorFake.Nome);
//    }

//    [Fact]
//    public async Task Deve_Retornarar_Um_Prestador_RetornarSucesso()
//    {
//        //Arrange
//        Prestador PrestadorFake = CriaFornecedorFake(Guid.NewGuid());
//        PrestadorDto PrestadorDtoFake = CriaFornecedorDtoFake(PrestadorFake.Id);

//        _repository.Setup(s => s.FindById(It.IsAny<Guid>())).ReturnsAsync(PrestadorFake);
//        _mapper.Setup(s => s.Map<Prestador>(It.IsAny<PrestadorDto>())).Returns(PrestadorFake);
//        //Act
//        var response = await new PrestadorController(_repository.Object, _mapper.Object).GetId(PrestadorDtoFake.Id.Value);
//        var okResult = response as OkObjectResult;
//        var result = okResult.Value as Prestador;
//        //Assert
//        _repository.Verify(s => s.FindById(It.IsAny<Guid>()), Times.Once());
//        _mapper.Verify(s => s.Map<Prestador>(It.IsAny<PrestadorDto>()), Times.Never());
//        Assert.NotNull(result);
//        Assert.Equal(result.Telefone, PrestadorFake.Telefone);
//        Assert.Equal(result.CPF, PrestadorFake.CPF);
//        Assert.Equal(result.DataCadastro, PrestadorFake.DataCadastro);
//        Assert.Equal(result.Id, PrestadorFake.Id);
//        Assert.Equal(result.Email, PrestadorFake.Email);
//        Assert.Equal(result.Endereco, PrestadorFake.Endereco);
//        Assert.Equal(result.Nome, PrestadorFake.Nome);
//    }

//    [Fact]
//    public async Task Deve_Atualizar_Um_Prestador_RetornarSucesso()
//    {
//        //Arrange
//        Prestador PrestadorFake = CriaFornecedorFake(Guid.NewGuid());
//        PrestadorDto PrestadorDtoFake = CriaFornecedorDtoFake(Guid.NewGuid());

//        _repository.Setup(s => s.Update(It.IsAny<Prestador>())).ReturnsAsync(PrestadorFake);
//        _mapper.Setup(s => s.Map<Prestador>(It.IsAny<PrestadorDto>())).Returns(PrestadorFake);
//        //Act
//        var response = await new PrestadorController(_repository.Object, _mapper.Object).AtualizarPrestador(PrestadorDtoFake);
//        var okResult = response as OkObjectResult;
//        var result = okResult.Value as Prestador;
//        //Assert
//        _repository.Verify(s => s.Update(It.IsAny<Prestador>()), Times.Once());
//        _mapper.Verify(s => s.Map<Prestador>(It.IsAny<PrestadorDto>()), Times.Once());
//        Assert.NotNull(result);
//        Assert.Equal(result.Telefone, PrestadorFake.Telefone);
//        Assert.Equal(result.CPF, PrestadorFake.CPF);
//        Assert.Equal(result.DataCadastro, PrestadorFake.DataCadastro);
//        Assert.Equal(result.Id, PrestadorFake.Id);
//        Assert.Equal(result.Email, PrestadorFake.Email);
//        Assert.Equal(result.Endereco, PrestadorFake.Endereco);
//        Assert.Equal(result.Nome, PrestadorFake.Nome);
//    }


//    [Fact]
//    public async Task Deve_Desativar_Um_Prestador_RetornarSucesso()
//    {
//        //Arrange
//        Prestador PrestadorFake = CriaFornecedorFake(Guid.NewGuid());
//        PrestadorDto PrestadorDtoFake = CriaFornecedorDtoFake(PrestadorFake.Id);

//        _repository.Setup(s => s.Desabled(It.IsAny<Guid>())).ReturnsAsync(PrestadorFake);
//        _mapper.Setup(s => s.Map<Prestador>(It.IsAny<PrestadorDto>())).Returns(PrestadorFake);
//        //Act
//        var response = await new PrestadorController(_repository.Object, _mapper.Object).DesativarPrestadorServico(PrestadorDtoFake.Id.Value);
//        var okResult = response as OkObjectResult;
//        var result = okResult.Value as Prestador;
//        //Assert
//        _repository.Verify(s => s.Desabled(It.IsAny<Guid>()), Times.Once());
//        _mapper.Verify(s => s.Map<Prestador>(It.IsAny<PrestadorDto>()), Times.Never());
//        Assert.NotNull(result);
//        Assert.Equal(result.Telefone, PrestadorFake.Telefone);
//        Assert.Equal(result.CPF, PrestadorFake.CPF);
//        Assert.Equal(result.DataCadastro, PrestadorFake.DataCadastro);
//        Assert.Equal(result.Id, PrestadorFake.Id);
//        Assert.Equal(result.Email, PrestadorFake.Email);
//        Assert.Equal(result.Endereco, PrestadorFake.Endereco);
//        Assert.Equal(result.Nome, PrestadorFake.Nome);
//    }

//    [Fact]
//    public async Task Deve_Deletar_Um_Prestador_RetornarSucesso()
//    {
//        //Arrange
//        Prestador PrestadorFake = CriaFornecedorFake(Guid.NewGuid());
//        PrestadorDto PrestadorDtoFake = CriaFornecedorDtoFake(PrestadorFake.Id);

//        _repository.Setup(s => s.Delete(It.IsAny<Guid>()));
//        _mapper.Setup(s => s.Map<Prestador>(It.IsAny<PrestadorDto>())).Returns(PrestadorFake);
//        //Act
//        var response = await new PrestadorController(_repository.Object, _mapper.Object).DeletarPrestador(PrestadorDtoFake.Id.Value);

//        //Assert
//        _repository.Verify(s => s.Delete(It.IsAny<Guid>()), Times.Once());
//        _mapper.Verify(s => s.Map<Prestador>(It.IsAny<PrestadorDto>()), Times.Never());
//        Assert.NotNull(response);

//    }
//    private static ICollection<Prestador> CriaListaFornecedoresFake()
//    {
//        return new List<Prestador>() { new Prestador() { CPF = "123456789", Email = "teste@test.com.br", Nome = "Teste", Telefone = "11999999999", Id = Guid.NewGuid() } };
//    }


//    private static Prestador CriaFornecedorFake(Guid? Id)
//    {
//        return new Prestador() { CPF = "123456789", Email = "teste@test.com.br", Nome = "Teste", Telefone = "11999999999", Id = Id.HasValue ? Id.Value : Guid.Empty };
//    }

//    private static PrestadorDto CriaFornecedorDtoFake(Guid? Id)
//    {
//        return new PrestadorDto() { CPF = "123456789", Email = "teste@test.com.br", Nome = "Teste", Telefone = "11999999999", Id = Id.HasValue ? Id.Value : Guid.Empty };
//    }
//}
