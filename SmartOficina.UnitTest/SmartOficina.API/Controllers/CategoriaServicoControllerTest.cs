namespace SmartOficina.UnitTest.SmartOficina.API.Controllers;

public class CategoriaServicoControllerTest
{
    private readonly Mock<ICategoriaServicoRepository> _repositoryMock = new();
    private readonly Mock<IMapper> _mapper = new();

    [Fact]
    public async Task Deve_Retornar_Lista_CategoriaServico()
    {
        //Arranger
        ICollection<CategoriaServico> categoriasFake = RetornarListaCategoriasFake();
        _repositoryMock.Setup(s => s.GetAll(It.IsAny<Guid>())).ReturnsAsync(categoriasFake);
        //Act
        CategoriaServicoController controllerCategoria = new CategoriaServicoController(_repositoryMock.Object, _mapper.Object);
        var response = await controllerCategoria.GetAll();
        var okResult = response as OkObjectResult;
        var result = okResult.Value as ICollection<CategoriaServico>;

        //Assert
        _repositoryMock.Verify(x => x.GetAll(Guid.NewGuid()), Times.Once);
        _mapper.Verify(x => x.Map<CategoriaServicoDto>(It.IsAny<CategoriaServicoDto>()), Times.Never);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(result.Count, categoriasFake.Count);
        Assert.Equal(result.First().Desc, categoriasFake.First().Desc);
        Assert.Equal(result.First().Titulo, categoriasFake.First().Titulo);
    }

    [Fact]
    public async Task Deve_Cadastrar_Uma_Categoria_RetornarOk()
    {
        //Arrange
        CategoriaServico categoriaFake = RetornarCategoriaFake(Guid.NewGuid());
        CategoriaServicoDto categoriaDtoFake = RetornarCategoriaDtoFake(Guid.Empty);
        _mapper.Setup(s => s.Map<CategoriaServico>(categoriaDtoFake)).Returns(categoriaFake);
        _repositoryMock.Setup(s => s.Create(It.IsAny<CategoriaServico>())).ReturnsAsync(categoriaFake);
        //Act
        CategoriaServicoController controllerCategoria = new CategoriaServicoController(_repositoryMock.Object, _mapper.Object);
        var response = await controllerCategoria.AddAsync(new CategoriaServicoDto());
        var okResult = response as OkObjectResult;
        var result = okResult.Value as CategoriaServico;
        //Assert
        _repositoryMock.Verify(x => x.Create(It.IsAny<CategoriaServico>()), Times.Once);
        _mapper.Verify(x => x.Map<CategoriaServico>(It.IsAny<CategoriaServicoDto>()), Times.Once);
        Assert.NotNull(result);
        Equals(result.Id, categoriaFake.Id);
        Equals(result.DataCadastro, categoriaFake.DataCadastro);
        Equals(result.UsrCadastro, categoriaFake.UsrCadastro);
        Equals(result.Desc, categoriaFake.Desc);
        Equals(result.Titulo, categoriaFake.Titulo);
    }

    [Fact]
    public async Task Deve_Atualizar_Uma_Categoria_RetornarOk()
    {
        //Arrange
        CategoriaServico categoriaFake = RetornarCategoriaFake(Guid.NewGuid());
        CategoriaServicoDto categoriaDtoFake = RetornarCategoriaDtoFake(Guid.NewGuid());
        _mapper.Setup(s => s.Map<CategoriaServico>(categoriaDtoFake)).Returns(categoriaFake);
        _repositoryMock.Setup(s => s.Update(It.IsAny<CategoriaServico>())).ReturnsAsync(categoriaFake);
        //Act
        CategoriaServicoController controllerCategoria = new CategoriaServicoController(_repositoryMock.Object, _mapper.Object);
        var response = await controllerCategoria.AtualizarCategoria(categoriaDtoFake);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as CategoriaServico;
        //Assert
        _repositoryMock.Verify(x => x.Update(It.IsAny<CategoriaServico>()), Times.Once);
        _mapper.Verify(x => x.Map<CategoriaServico>(It.IsAny<CategoriaServicoDto>()), Times.Once);
        Assert.NotNull(result);
        Equals(result.Id, categoriaFake.Id);
        Equals(result.DataCadastro, categoriaFake.DataCadastro);
        Equals(result.UsrCadastro, categoriaFake.UsrCadastro);
        Equals(result.Desc, categoriaFake.Desc);
        Equals(result.Titulo, categoriaFake.Titulo);
    }

    [Fact]
    public async Task NaoDeve_Atualizar_Uma_Categoria_RetornarStatusCodeQuatroCentos()
    {
        //Arrange
        CategoriaServico categoriaFake = RetornarCategoriaFake(Guid.NewGuid());
        CategoriaServicoDto categoriaDtoFake = RetornarCategoriaDtoFake(Guid.Empty);
        _mapper.Setup(s => s.Map<CategoriaServico>(categoriaDtoFake)).Returns(categoriaFake);
        _repositoryMock.Setup(s => s.Update(It.IsAny<CategoriaServico>())).ReturnsAsync(categoriaFake);
        //Act
        CategoriaServicoController controllerCategoria = new CategoriaServicoController(_repositoryMock.Object, _mapper.Object);
        var response = await controllerCategoria.AtualizarCategoria(new CategoriaServicoDto() { Desc = "Teste", Titulo = "Teste" });
        var okResult = response as BadRequestObjectResult;
        var result = okResult.Value as CategoriaServico;
        //Assert
        _repositoryMock.Verify(x => x.Update(It.IsAny<CategoriaServico>()), Times.Never);
        _mapper.Verify(x => x.Map<CategoriaServico>(It.IsAny<CategoriaServicoDto>()), Times.Never);
        Assert.Null(result);

    }

    [Fact]
    public async Task Deve_Retornar_Uma_Categoria_RetornarOk()
    {
        //Arrange
        CategoriaServico categoriaFake = RetornarCategoriaFake(Guid.NewGuid());
        CategoriaServicoDto categoriaDtoFake = RetornarCategoriaDtoFake(categoriaFake.Id);
        _mapper.Setup(s => s.Map<CategoriaServico>(categoriaDtoFake)).Returns(categoriaFake);
        _repositoryMock.Setup(s => s.FindById(It.IsAny<Guid>())).ReturnsAsync(categoriaFake);
        //Act
        CategoriaServicoController controllerCategoria = new CategoriaServicoController(_repositoryMock.Object, _mapper.Object);
        var response = await controllerCategoria.GetId(categoriaDtoFake.Id.Value);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as CategoriaServico;
        //Assert
        _repositoryMock.Verify(x => x.FindById(It.IsAny<Guid>()), Times.Once);
        _mapper.Verify(x => x.Map<CategoriaServico>(It.IsAny<CategoriaServicoDto>()), Times.Never);
        Assert.NotNull(result);
        Equals(result.Id, categoriaFake.Id);
        Equals(result.DataCadastro, categoriaFake.DataCadastro);
        Equals(result.UsrCadastro, categoriaFake.UsrCadastro);
        Equals(result.Desc, categoriaFake.Desc);
        Equals(result.Titulo, categoriaFake.Titulo);

    }

    [Fact]
    public async Task Deve_Desativar_Uma_Categoria_RetornarOk()
    {
        //Arrange
        CategoriaServico categoriaFake = RetornarCategoriaFake(Guid.NewGuid());
        CategoriaServicoDto categoriaDtoFake = RetornarCategoriaDtoFake(categoriaFake.Id);
        _mapper.Setup(s => s.Map<CategoriaServico>(categoriaDtoFake)).Returns(categoriaFake);
        _repositoryMock.Setup(s => s.Desabled(It.IsAny<Guid>())).ReturnsAsync(categoriaFake);
        //Act
        CategoriaServicoController controllerCategoria = new CategoriaServicoController(_repositoryMock.Object, _mapper.Object);
        var response = await controllerCategoria.DesativarCategoria(categoriaDtoFake.Id.Value);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as CategoriaServico;
        //Assert
        _repositoryMock.Verify(x => x.Desabled(It.IsAny<Guid>()), Times.Once);
        _mapper.Verify(x => x.Map<CategoriaServico>(It.IsAny<CategoriaServicoDto>()), Times.Never);
        Assert.NotNull(result);
        Equals(result.Id, categoriaFake.Id);
        Equals(result.DataCadastro, categoriaFake.DataCadastro);
        Equals(result.UsrCadastro, categoriaFake.UsrCadastro);
        Equals(result.Desc, categoriaFake.Desc);
        Equals(result.Titulo, categoriaFake.Titulo);

    }

    [Fact]
    public async Task Deve_Deletar_Uma_Categoria_RetornarOk()
    {
        //Arrange
        CategoriaServico categoriaFake = RetornarCategoriaFake(Guid.NewGuid());
        CategoriaServicoDto categoriaDtoFake = RetornarCategoriaDtoFake(categoriaFake.Id);
        _mapper.Setup(s => s.Map<CategoriaServico>(categoriaDtoFake)).Returns(categoriaFake);
        _repositoryMock.Setup(s => s.Delete(It.IsAny<Guid>()));
        //Act
        CategoriaServicoController controllerCategoria = new CategoriaServicoController(_repositoryMock.Object, _mapper.Object);
        var response = await controllerCategoria.DeletarCategoria(categoriaDtoFake.Id.Value);

        //Assert
        _repositoryMock.Verify(x => x.Delete(It.IsAny<Guid>()), Times.Once);
        _mapper.Verify(x => x.Map<CategoriaServico>(It.IsAny<CategoriaServicoDto>()), Times.Never);
        Assert.NotNull(response);


    }

    private static CategoriaServicoDto RetornarCategoriaDtoFake(Guid? Id)
    {
        return new CategoriaServicoDto() { Desc = "Descricao teste", Titulo = "Titulo teste", Id = Id.HasValue ? Id.Value : Guid.Empty };
    }
    private static CategoriaServico RetornarCategoriaFake(Guid? Id)
    {
        return new CategoriaServico() { Desc = "Descricao teste", Titulo = "Titulo teste", DataCadastro = DateTime.Now, Id = Id.HasValue ? Id.Value : Guid.Empty, UsrCadastro = Guid.NewGuid(), PrestadorId = Guid.NewGuid() };
    }

    private static ICollection<CategoriaServico> RetornarListaCategoriasFake()
    {
        return new List<CategoriaServico>() { new CategoriaServico() { Titulo = "Titulo Categoria Fake", Desc = "Descricao Categoria Fake", PrestadorId = Guid.NewGuid() } };
    }
}
