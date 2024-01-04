using SmartOficina.Api.Domain.Interfaces;
using System.Net;

namespace SmartOficina.UnitTest.SmartOficina.API.Controllers;

public class CategoriaServicoControllerTest
{
    private readonly Mock<ICategoriaService> _serviceMock = new();

    private static DefaultHttpContext CreateFakeClaims(ICollection<CategoriaServicoDto> categoriasFake)
    {
        var fakeHttpContext = new DefaultHttpContext();
        ClaimsIdentity identity = new(
            new[] {
                        new Claim("PrestadorId", categoriasFake.First().PrestadorId.ToString()),
                        new Claim("UserName", "Teste"),
                        new Claim("IdUserLogin", categoriasFake.First().PrestadorId.ToString())

            }
        );
        fakeHttpContext.User = new System.Security.Claims.ClaimsPrincipal(identity);
        return fakeHttpContext;
    }
    private CategoriaServicoController CreateFakeController(ICollection<CategoriaServicoDto> categoriasFake)
    {
        return new CategoriaServicoController(_serviceMock.Object) { ControllerContext = new ControllerContext() { HttpContext = CreateFakeClaims(categoriasFake) } };
    }

    [Fact]
    public async Task Deve_Retornar_Lista_CategoriaServico()
    {
        //Arranger
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
        _serviceMock.Setup(s => s.GetAllCategoria(It.IsAny<CategoriaServicoDto>())).ReturnsAsync(categoriasFake);
        //Act
        CategoriaServicoController controllerCategoria = CreateFakeController(categoriasFake);
        var response = await controllerCategoria.GetAll(string.Empty, string.Empty);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as ICollection<CategoriaServicoDto>;

        //Assert
        _serviceMock.Verify(s => s.GetAllCategoria(It.IsAny<CategoriaServicoDto>()), Times.Once);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(result.Count, categoriasFake.Count);
        Assert.Equal(result.First().Desc, categoriasFake.First().Desc);
        Assert.Equal(result.First().Titulo, categoriasFake.First().Titulo);
    }

    [Fact]
    public async Task Nao_Deve_Retornar_ListaCategoriaService()
    {
        //Arranger (parametros)
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");

        List<CategoriaServicoDto> categoriaServicoFake = null;
        _serviceMock.Setup(c => c.GetAllCategoria(It.IsAny<CategoriaServicoDto>())).ReturnsAsync(categoriaServicoFake);
        CategoriaServicoController categoriaServicoControler = CreateFakeController(categoriasFake);

        //Act (ação/execução)
        var response = await categoriaServicoControler.GetAll(string.Empty, string.Empty);
        var okResult = response as NoContentResult;


        //Assert (verificação)
        _serviceMock.Verify(v => v.GetAllCategoria(It.IsAny<CategoriaServicoDto>()), Times.Once); //Verifco se ele foi chamado apenas uma vez :)
        Assert.NotNull(okResult); //Verifico se é nulo
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.NoContent);

    }

    [Fact]
    public async Task Deve_Retornar_BadRequest_MetodoGetAll()
    {
        //Arranger
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");

        List<CategoriaServicoDto> categoriaServicoFake = null;
        _serviceMock.Setup(c => c.GetAllCategoria(It.IsAny<CategoriaServicoDto>())).ReturnsAsync(categoriaServicoFake);
        CategoriaServicoController categoriaServicoControler = CreateFakeController(categoriasFake);
        categoriaServicoControler.ModelState.AddModelError("key", "error message");
        //Act
        var response = await categoriaServicoControler.GetAll(string.Empty, string.Empty);
        var okResult = response as ObjectResult;


        //Assert (verificação)
        _serviceMock.Verify(v => v.GetAllCategoria(It.IsAny<CategoriaServicoDto>()), Times.Never);
        Assert.NotNull(okResult);
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.BadRequest);

    }

    [Fact]
    public async Task Deve_Cadastrar_Uma_Categoria_RetornarOk()
    {
        //Arrange
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
        CategoriaServicoDto categoriaDtoFake = RetornarCategoriaDtoFake("Teste Titulo", "teste");
        _serviceMock.Setup(s => s.CreateCategoria(It.IsAny<CategoriaServicoDto>())).ReturnsAsync(categoriaDtoFake);
        CategoriaServicoController categoriaServicoControler = CreateFakeController(categoriasFake);
        //Act
        var response = await categoriaServicoControler.AddAsync(new CategoriaServicoDto());
        var okResult = response as OkObjectResult;
        var result = okResult.Value as CategoriaServicoDto;
        //Assert
        _serviceMock.Verify(x => x.CreateCategoria(It.IsAny<CategoriaServicoDto>()), Times.Once);
        Assert.NotNull(result);
        Equals(result.Id, categoriaDtoFake.Id);
        Equals(result.DataCadastro, categoriaDtoFake.DataCadastro);
        Equals(result.UsrCadastro, categoriaDtoFake.UsrCadastro);
        Equals(result.Desc, categoriaDtoFake.Desc);
        Equals(result.Titulo, categoriaDtoFake.Titulo);
    }

    [Fact]
    public async Task NaoDeve_Cadastrar_Uma_Categoria_NoContent()
    {
        //Arrange
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
        CategoriaServicoDto categoriaDtoFake = null;
        _serviceMock.Setup(s => s.CreateCategoria(It.IsAny<CategoriaServicoDto>())).ReturnsAsync(categoriaDtoFake);
        CategoriaServicoController categoriaServicoControler = CreateFakeController(categoriasFake);
        //Act
        var response = await categoriaServicoControler.AddAsync(new CategoriaServicoDto());
        var okResult = response as NoContentResult;

        //Assert (verificação)
        _serviceMock.Verify(v => v.CreateCategoria(It.IsAny<CategoriaServicoDto>()), Times.Once);
        Assert.NotNull(okResult); //Verifico se é nulo
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Deve_Retornar_BadRequest_MetodoCreateCategoria()
    {
        //Arranger
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
        CategoriaServicoDto categoriaDtoFake = RetornarCategoriaDtoFake("Teste Titulo", "teste");
        _serviceMock.Setup(s => s.CreateCategoria(It.IsAny<CategoriaServicoDto>())).ReturnsAsync(categoriaDtoFake);
        CategoriaServicoController categoriaServicoControler = CreateFakeController(categoriasFake);

        //Act
        categoriaServicoControler.ModelState.AddModelError("key", "error message");
        var response = await categoriaServicoControler.AddAsync(new CategoriaServicoDto());
        var okResult = response as ObjectResult;


        //Assert (verificação)
        _serviceMock.Verify(v => v.CreateCategoria(It.IsAny<CategoriaServicoDto>()), Times.Never);
        Assert.NotNull(okResult);
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.BadRequest);

    }

    [Fact]
    public async Task Deve_Atualizar_Uma_Categoria_RetornarOk()
    {
        //Arrange
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
        CategoriaServicoDto categoriaDtoFake = RetornarCategoriaDtoFake("Teste Titulo", "teste");
        _serviceMock.Setup(s => s.UpdateCategoria(It.IsAny<CategoriaServicoDto>())).ReturnsAsync(categoriaDtoFake);
        CategoriaServicoController controllerCategoria = CreateFakeController(categoriasFake);
        //Act
        var response = await controllerCategoria.AtualizarCategoria(categoriaDtoFake);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as CategoriaServicoDto;
        //Assert
        _serviceMock.Verify(s => s.UpdateCategoria(It.IsAny<CategoriaServicoDto>()), Times.Once);
        Assert.NotNull(result);
        Equals(result.Id, categoriaDtoFake.Id);
        Equals(result.DataCadastro, categoriaDtoFake.DataCadastro);
        Equals(result.UsrCadastro, categoriaDtoFake.UsrCadastro);
        Equals(result.Desc, categoriaDtoFake.Desc);
        Equals(result.Titulo, categoriaDtoFake.Titulo);
    }

    [Fact]
    public async Task NaoDeve_Atualizar_Uma_Categoria_RetornarStatusBadRequest()
    {
        //Arrange
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
        CategoriaServicoDto categoriaDtoFake = RetornarCategoriaDtoFake("Teste Titulo", "teste");
        _serviceMock.Setup(s => s.UpdateCategoria(It.IsAny<CategoriaServicoDto>())).ReturnsAsync(categoriaDtoFake);
        CategoriaServicoController controllerCategoria = CreateFakeController(categoriasFake);
        controllerCategoria.ModelState.AddModelError("key", "error message");
        //Act
        var response = await controllerCategoria.AtualizarCategoria(categoriaDtoFake);
        var okResult = response as ObjectResult;
        //Assert
        _serviceMock.Verify(s => s.UpdateCategoria(It.IsAny<CategoriaServicoDto>()), Times.Never);
        Assert.NotNull(okResult);
        Equals(okResult.StatusCode, (int)HttpStatusCode.BadRequest);

    }

    [Fact]
    public async Task NaoDeve_Atualizar_Uma_Categoria_RetornarStatusBadRequest_PassandoIdNullo()
    {
        //Arrange
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
        CategoriaServicoDto categoriaDtoFake = RetornarCategoriaDtoFake("Teste Titulo", "teste");
        categoriaDtoFake.Id = null;
        _serviceMock.Setup(s => s.UpdateCategoria(It.IsAny<CategoriaServicoDto>())).ReturnsAsync(categoriaDtoFake);
        CategoriaServicoController controllerCategoria = CreateFakeController(categoriasFake);
        //Act
        var response = await controllerCategoria.AtualizarCategoria(categoriaDtoFake);
        var okResult = response as ObjectResult;
        //Assert
        _serviceMock.Verify(s => s.UpdateCategoria(It.IsAny<CategoriaServicoDto>()), Times.Never);
        Assert.NotNull(okResult);
        Equals(okResult.StatusCode, (int)HttpStatusCode.BadRequest);

    }

    [Fact]
    public async Task NaoDeve_Atualizar_Uma_Categoria_RetornarStatusNoContent()
    {
        //Arrange
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
        CategoriaServicoDto categoriaDtoFake = null;
        _serviceMock.Setup(s => s.UpdateCategoria(It.IsAny<CategoriaServicoDto>())).ReturnsAsync(categoriaDtoFake);
        CategoriaServicoController controllerCategoria = CreateFakeController(categoriasFake);
        //Act
        var response = await controllerCategoria.AtualizarCategoria(new CategoriaServicoDto() { Id = Guid.NewGuid() });
        var okResult = response as NoContentResult;
        //Assert
        _serviceMock.Verify(s => s.UpdateCategoria(It.IsAny<CategoriaServicoDto>()), Times.Once);
        Assert.NotNull(okResult);
        Equals(okResult.StatusCode, (int)HttpStatusCode.NoContent);

    }

    [Fact]
    public async Task Deve_Retornar_Uma_Categoria_PassandoId()
    {
        //Arrange
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
        CategoriaServicoDto categoriaDtoFake = RetornarCategoriaDtoFake("Teste Titulo", "teste");
        _serviceMock.Setup(s => s.FindByIdCategoria(It.IsAny<Guid>())).ReturnsAsync(categoriaDtoFake);
        CategoriaServicoController controllerCategoria = CreateFakeController(categoriasFake);
        //Act

        var response = await controllerCategoria.GetId(categoriaDtoFake.Id.Value);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as CategoriaServicoDto;
        //Assert
        _serviceMock.Verify(x => x.FindByIdCategoria(It.IsAny<Guid>()), Times.Once);
        Assert.NotNull(result);
        Equals(result.Id, categoriaDtoFake.Id);
        Equals(result.DataCadastro, categoriaDtoFake.DataCadastro);
        Equals(result.UsrCadastro, categoriaDtoFake.UsrCadastro);
        Equals(result.Desc, categoriaDtoFake.Desc);
        Equals(result.Titulo, categoriaDtoFake.Titulo);

    }

    [Fact]
    public async Task NaoDeve_Retornar_Uma_Categoria_PassandoId()
    {
        //Arrange
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
        CategoriaServicoDto categoriaDtoFake = null;
        _serviceMock.Setup(s => s.FindByIdCategoria(It.IsAny<Guid>())).ReturnsAsync(categoriaDtoFake);
        CategoriaServicoController controllerCategoria = CreateFakeController(categoriasFake);
        //Act

        var response = await controllerCategoria.GetId(Guid.NewGuid());
        var okResult = response as NoContentResult;
        //Assert
        _serviceMock.Verify(x => x.FindByIdCategoria(It.IsAny<Guid>()), Times.Once);
        Assert.NotNull(okResult);
        Equals(okResult.StatusCode, (int)HttpStatusCode.NoContent);

    }
    [Fact]
    public async Task NaoDeve_Retornar_Uma_Categoria_RetornoBadRequest()
    {
        //Arrange
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
        CategoriaServicoDto categoriaDtoFake = null;
        _serviceMock.Setup(s => s.FindByIdCategoria(It.IsAny<Guid>())).ReturnsAsync(categoriaDtoFake);
        CategoriaServicoController controllerCategoria = CreateFakeController(categoriasFake);
        controllerCategoria.ModelState.AddModelError("key", "error message");
        //Act

        var response = await controllerCategoria.GetId(Guid.NewGuid());
        var okResult = response as ObjectResult;
        //Assert
        _serviceMock.Verify(x => x.FindByIdCategoria(It.IsAny<Guid>()), Times.Never);
        Assert.NotNull(okResult);
        Equals(okResult.StatusCode, (int)HttpStatusCode.BadRequest);

    }

    [Fact]
    public async Task Deve_Desativar_Uma_Categoria_RetornarOk()
    {
        //Arrange
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
        CategoriaServicoDto categoriaDtoFake = RetornarCategoriaDtoFake("Teste Titulo", "teste");
        _serviceMock.Setup(s => s.Desabled(It.IsAny<Guid>())).ReturnsAsync(categoriaDtoFake);
        CategoriaServicoController controllerCategoria = CreateFakeController(categoriasFake);
        //Act

        var response = await controllerCategoria.DesativarCategoria(categoriaDtoFake.Id.Value);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as CategoriaServicoDto;
        //Assert
        _serviceMock.Verify(x => x.Desabled(It.IsAny<Guid>()), Times.Once);
        Assert.NotNull(result);
        Equals(result.Id, categoriaDtoFake.Id);
        Equals(result.DataCadastro, categoriaDtoFake.DataCadastro);
        Equals(result.UsrCadastro, categoriaDtoFake.UsrCadastro);
        Equals(result.Desc, categoriaDtoFake.Desc);
        Equals(result.Titulo, categoriaDtoFake.Titulo);

    }

    [Fact]
    public async Task NaoDeve_Desativar_Uma_Categoria_RetornaBadRequest()
    {
        //Arrange
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
        CategoriaServicoDto categoriaDtoFake = RetornarCategoriaDtoFake("Teste Titulo", "teste");
        _serviceMock.Setup(s => s.Desabled(It.IsAny<Guid>())).ReturnsAsync(categoriaDtoFake);
        CategoriaServicoController controllerCategoria = CreateFakeController(categoriasFake);
        controllerCategoria.ModelState.AddModelError("key", "error message");
        //Act

        var response = await controllerCategoria.DesativarCategoria(categoriaDtoFake.Id.Value);
        var okResult = response as ObjectResult;

        //Assert
        _serviceMock.Verify(x => x.Desabled(It.IsAny<Guid>()), Times.Never);
        Assert.NotNull(okResult);
        Equals(okResult.StatusCode, (int)HttpStatusCode.BadRequest);

    }

    [Fact]
    public async Task NaoDeve_Desativar_Uma_Categoria_RetornaNoContet()
    {
        //Arrange
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
        CategoriaServicoDto categoriaDtoFake = null;
        _serviceMock.Setup(s => s.Desabled(It.IsAny<Guid>())).ReturnsAsync(categoriaDtoFake);
        CategoriaServicoController controllerCategoria = CreateFakeController(categoriasFake);
        //Act

        var response = await controllerCategoria.DesativarCategoria(Guid.NewGuid());
        var okResult = response as NoContentResult;
        //Assert
        _serviceMock.Verify(x => x.Desabled(It.IsAny<Guid>()), Times.Once);
        Assert.NotNull(okResult);
        Equals(okResult.StatusCode, (int)HttpStatusCode.NoContent);

    }

    [Fact]
    public async Task NaoDeve_Deletar_Uma_Categoria_RetornarBadRequest()
    {
        //Arrange
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
        CategoriaServicoDto categoriaDtoFake = null;
        _serviceMock.Setup(s => s.Delete(It.IsAny<Guid>()));
        CategoriaServicoController controllerCategoria = CreateFakeController(categoriasFake);
        controllerCategoria.ModelState.AddModelError("key", "error message");
        //Act
        var response = await controllerCategoria.DeletarCategoria(Guid.NewGuid());
        var okResult = response as ObjectResult;

        //Assert
        _serviceMock.Verify(x => x.Desabled(It.IsAny<Guid>()), Times.Never);
        Assert.NotNull(okResult);
        Equals(okResult.StatusCode, (int)HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task NaoDeve_Deletar_Uma_Categoria_RetornarBadRequest_DevidoErro()
    {
        //Arrange
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
        CategoriaServicoDto categoriaDtoFake = null;
        _serviceMock.Setup(s => s.Delete(It.IsAny<Guid>()));
        CategoriaServicoController controllerCategoria = CreateFakeController(categoriasFake);
        controllerCategoria.ModelState.AddModelError("key", "error message");
        //Act
        var exp = Assert.ThrowsAsync<Exception>(() => controllerCategoria.DeletarCategoria(Guid.NewGuid()));
        //Assert
    }

    [Fact]
    public async Task Deve_Deletar_Uma_Categoria_RetornarOk()
    {
        //Arrange
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
         CategoriaServicoDto categoriaDtoFake = RetornarCategoriaDtoFake("Teste Titulo", "teste");
        _serviceMock.Setup(s => s.Delete(It.IsAny<Guid>()));
        CategoriaServicoController controllerCategoria = CreateFakeController(categoriasFake);
        //Act
        await controllerCategoria.DeletarCategoria(Guid.NewGuid());
        //Assert
        _serviceMock.Verify(x => x.Delete(It.IsAny<Guid>()), Times.Once);
        
    }

    private static CategoriaServicoDto RetornarCategoriaDtoFake(string titulo, string desc)
    {
        return new CategoriaServicoDto() { Desc = titulo, Titulo = desc, Id = Guid.NewGuid() };
    }

    private static ICollection<CategoriaServicoDto> RetornarListaCategoriasFake(string titulo, string descricao)
    {
        return new List<CategoriaServicoDto>() { new CategoriaServicoDto() { Titulo = "Titulo Categoria Fake", Desc = "Descricao Categoria Fake", PrestadorId = Guid.NewGuid() } };
    }
}
