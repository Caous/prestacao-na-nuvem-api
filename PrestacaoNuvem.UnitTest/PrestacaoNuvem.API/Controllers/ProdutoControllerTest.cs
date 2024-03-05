using PrestacaoNuvem.Api.Domain.Interfaces;
using System.Net;

namespace PrestacaoNuvem.UnitTest.PrestacaoNuvem.API.Controllers;

#pragma warning disable 8604, 8602, 8629, 8600, 8620
public class ProdutoControllerTest
{
    private readonly Mock<IProdutoService> _produtoMock = new();

    private static DefaultHttpContext CreateFakeClaims(ProdutoDto produtosFake)
    {
        var fakeHttpContext = new DefaultHttpContext();
        ClaimsIdentity identity = new(
            new[] {
                        new Claim("PrestadorId", produtosFake.PrestadorId.ToString()),
                        new Claim("UserName", "Teste"),
                        new Claim("IdUserLogin", produtosFake.PrestadorId.ToString())

            }
        );
        fakeHttpContext.User = new ClaimsPrincipal(identity);
        return fakeHttpContext;
    }
    private ProdutoController CreateFakeController(ProdutoDto produtosFake)
    {
        return new ProdutoController(_produtoMock.Object) { ControllerContext = new ControllerContext() { HttpContext = CreateFakeClaims(produtosFake) } };
    }

    [Fact]
    public async Task Deve_Retornar_Lista_Produtos()
    {
        //Arranger
        ICollection<ProdutoDto> retorno_mock_lista_produtos_Fake = Cria_Lista_ProdutosFake();
        _produtoMock.Setup(c => c.GetAllProduto(It.IsAny<ProdutoDto>())).ReturnsAsync(retorno_mock_lista_produtos_Fake);
        ProdutoDto produtoFake = CriandoUmProdutoFake("Teste", "testando", "testado",10, 20);
        ProdutoController produtoController = CreateFakeController(produtoFake);
       
        //Act
        var response = await produtoController.GetAll(string.Empty, string.Empty, string.Empty);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as ICollection<ProdutoDto>;

        //Assert
        _produtoMock.Verify(c => c.GetAllProduto(It.IsAny<ProdutoDto>()), Times.Once);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(result.First().Nome, retorno_mock_lista_produtos_Fake.First().Nome);
        Assert.Equal(result.First().Marca, retorno_mock_lista_produtos_Fake.First().Marca);
        Assert.Equal(result.First().Modelo, retorno_mock_lista_produtos_Fake.First().Modelo);

    }

    [Fact]
    public async Task Nao_Deve_Retornar_Lista_Produtos_ReturnNull()
    {
        //Arranger
        ICollection<ProdutoDto> retorno_mock_lista_produtos_Fake = Cria_Lista_ProdutosFake();
        retorno_mock_lista_produtos_Fake = null;
        _produtoMock.Setup(c => c.GetAllProduto(It.IsAny<ProdutoDto>())).ReturnsAsync(retorno_mock_lista_produtos_Fake);
        ProdutoDto produtoFake = CriandoUmProdutoFake("Teste", "testando", "testado", 10, 20);
        ProdutoController produtoController = CreateFakeController(produtoFake);

        //Act
        var response = await produtoController.GetAll(string.Empty, string.Empty, string.Empty);
        var okResult = response as NoContentResult;

        //Assert
        _produtoMock.Verify(c => c.GetAllProduto(It.IsAny<ProdutoDto>()), Times.Once);
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);

    }

    [Fact]
    public async Task Deve_Retornar_Um_Produtos()
    {
        //Arranger
        ICollection<ProdutoDto> retorno_mock_lista_produtos_Fake = Cria_Lista_ProdutosFake();
        _produtoMock.Setup(c => c.FindByIdProduto(It.IsAny<Guid>())).ReturnsAsync(retorno_mock_lista_produtos_Fake.First());
        ProdutoDto produtoFake = CriandoUmProdutoFake("Teste", "testando", "testado", 10, 20);
        ProdutoController produtoController = CreateFakeController(produtoFake);

        //Act
        var response = await produtoController.GetId(Guid.NewGuid());
        var okResult = response as OkObjectResult;
        var result = okResult.Value as ProdutoDto;

        //Assert
        _produtoMock.Verify(c => c.FindByIdProduto(It.IsAny<Guid>()), Times.Once);
        Assert.NotNull(result);
        Assert.Equal(result.Nome, retorno_mock_lista_produtos_Fake.First().Nome);
        Assert.Equal(result.Marca, retorno_mock_lista_produtos_Fake.First().Marca);
        Assert.Equal(result.Modelo, retorno_mock_lista_produtos_Fake.First().Modelo);

    }

    [Fact]
    public async Task Nao_Deve_Retornar_Um_Produtos_ReturnNull()
    {
        //Arranger
        ICollection<ProdutoDto> retorno_mock_lista_produtos_Fake = Cria_Lista_ProdutosFake();
        ProdutoDto xptoNull = null;
        _produtoMock.Setup(c => c.FindByIdProduto(It.IsAny<Guid>())).ReturnsAsync(xptoNull);
        ProdutoDto produtoFake = CriandoUmProdutoFake("Teste", "testando", "testado", 10, 20);
        ProdutoController produtoController = CreateFakeController(produtoFake);

        //Act
        var response = await produtoController.GetId(Guid.NewGuid());
        var okResult = response as NoContentResult;

        //Assert
        _produtoMock.Verify(c => c.FindByIdProduto(It.IsAny<Guid>()), Times.Once);
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);

    }

    [Fact]
    public async Task Nao_Deve_Retornar_Um_Produtos_ReturnBadRequest()
    {
        //Arranger
        ICollection<ProdutoDto> retorno_mock_lista_produtos_Fake = Cria_Lista_ProdutosFake();
        _produtoMock.Setup(c => c.FindByIdProduto(It.IsAny<Guid>())).ReturnsAsync(retorno_mock_lista_produtos_Fake.First());
        ProdutoDto produtoFake = CriandoUmProdutoFake("Teste", "testando", "testado", 10, 20);
        ProdutoController produtoController = CreateFakeController(produtoFake);
        produtoController.ModelState.AddModelError("key", "error message");

        //Act
        var response = await produtoController.GetId(Guid.NewGuid());
        var okResult = response as ObjectResult;

        //Assert
        _produtoMock.Verify(c => c.FindByIdProduto(It.IsAny<Guid>()), Times.Never());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.BadRequest, okResult.StatusCode);

    }

    [Fact]
    public async Task Deve_cadastrar_produto()
    {
        //Arrange
        ProdutoDto CadastrandoUmProdutoFake = CriandoUmProdutoFake("Teste", "testando", "testado", 10, 20);
        _produtoMock.Setup(c => c.CreateProduto(It.IsAny<ProdutoDto>())).ReturnsAsync(CadastrandoUmProdutoFake);
        ProdutoController produtoController = CreateFakeController(CadastrandoUmProdutoFake);

        //Act
        var response = await produtoController.Add(CadastrandoUmProdutoFake);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as ProdutoDto;
        
        //Assert
        _produtoMock.Verify(c => c.CreateProduto(It.IsAny<ProdutoDto>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(result.Nome, CadastrandoUmProdutoFake.Nome);
        Assert.Equal(result.Marca, CadastrandoUmProdutoFake.Marca);
        Assert.Equal(result.Modelo, CadastrandoUmProdutoFake.Modelo);
        
    }


    [Fact]
    public async Task Nao_Deve_Cadastrar_Produto_RetornoNoContent()
    {
        //Arrange
        ProdutoDto CadastrandoUmProdutoFake = CriandoUmProdutoFake("Teste", "testando", "testado", 10, 20);
        ProdutoDto CadastrandoUmProdutoFakeNull = null;
        _produtoMock.Setup(c => c.CreateProduto(It.IsAny<ProdutoDto>())).ReturnsAsync(CadastrandoUmProdutoFakeNull);
        ProdutoController produtoController = CreateFakeController(CadastrandoUmProdutoFake);

        //Act
        var response = await produtoController.Add(CadastrandoUmProdutoFake);
        var okResult = response as NoContentResult;
        //Assert
        _produtoMock.Verify(c => c.CreateProduto(It.IsAny<ProdutoDto>()), Times.Once());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);

    }

    [Fact]
    public async Task Nao_Deve_Cadastrar_Produto_RetornoBadRequest()
    {
        //Arrange
        ProdutoDto CadastrandoUmProdutoFake = CriandoUmProdutoFake("Teste", "testando", "testado", 10, 20);
        _produtoMock.Setup(c => c.CreateProduto(It.IsAny<ProdutoDto>())).ReturnsAsync(CadastrandoUmProdutoFake);
        ProdutoController produtoController = CreateFakeController(CadastrandoUmProdutoFake);
        produtoController.ModelState.AddModelError("key", "error message");

        //Act
        var response = await produtoController.Add(CadastrandoUmProdutoFake);
        var okResult = response as ObjectResult;

        //Assert
        _produtoMock.Verify(c => c.CreateProduto(It.IsAny<ProdutoDto>()), Times.Never());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.BadRequest, okResult.StatusCode);

    }

    [Fact]
    public async Task Deve_Cadastrar_Lista_Produto_Arquivo()
    {
        //Arrange
        ProdutoDto CadastrandoUmProdutoFake = CriandoUmProdutoFake("Teste", "testando", "testado", 10, 20);
        ICollection<ProdutoDto> CadastrandoUmProdutoListaFake = Cria_Lista_ProdutosFake();
        _produtoMock.Setup(c => c.MapperProdutoLot(It.IsAny<IFormFile>())).ReturnsAsync(CadastrandoUmProdutoListaFake);
        _produtoMock.Setup(c => c.CreateProdutoLot(It.IsAny<ICollection<ProdutoDto>>())).ReturnsAsync("Cadastrado");
        ProdutoController produtoController = CreateFakeController(CadastrandoUmProdutoFake);
        //Setup mock file using a memory stream
        var content = "Hello World from a Fake File";
        var fileName = "test.pdf";
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(content);
        writer.Flush();
        stream.Position = 0;

        //create FormFile with desired data
        IFormFile file = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);


        //Act
        var response = await produtoController.AddFromExcel(file);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as string;

        //Assert
        _produtoMock.Verify(c => c.CreateProdutoLot(It.IsAny<ICollection<ProdutoDto>>()), Times.Once());
        _produtoMock.Verify(c => c.MapperProdutoLot(It.IsAny<IFormFile>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal("Cadastrado", result);
        
    }


    [Fact]
    public async Task Nao_Deve_Cadastrar_Lista_Produto_RetornoBadRequest_ArquivoNaoEnviado()
    {
        ProdutoDto CadastrandoUmProdutoFake = CriandoUmProdutoFake("Teste", "testando", "testado", 10, 20);
        ICollection<ProdutoDto> CadastrandoUmProdutoListaFake = Cria_Lista_ProdutosFake();
        _produtoMock.Setup(c => c.MapperProdutoLot(It.IsAny<IFormFile>())).ReturnsAsync(CadastrandoUmProdutoListaFake);
        _produtoMock.Setup(c => c.CreateProdutoLot(It.IsAny<ICollection<ProdutoDto>>())).ReturnsAsync("Cadastrado");
        ProdutoController produtoController = CreateFakeController(CadastrandoUmProdutoFake);
        //Setup mock file using a memory stream
        var content = "";
        var fileName = "test.pdf";
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(content);
        writer.Flush();
        stream.Position = 0;

        //create FormFile with desired data
        IFormFile file = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);


        //Act
        var response = await produtoController.AddFromExcel(file);
        var okResult = response as ObjectResult;

        //Assert
        _produtoMock.Verify(c => c.CreateProdutoLot(It.IsAny<ICollection<ProdutoDto>>()), Times.Never());
        _produtoMock.Verify(c => c.MapperProdutoLot(It.IsAny<IFormFile>()), Times.Never());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.BadRequest, okResult.StatusCode);

    }

    [Fact]
    public async Task Nao_Deve_Cadastrar_Lista_Produto_RetornoBadRequest_NaoPossivelMapear()
    {
        ProdutoDto CadastrandoUmProdutoFake = CriandoUmProdutoFake("Teste", "testando", "testado", 10, 20);
        ICollection<ProdutoDto> CadastrandoUmProdutoListaFake = Cria_Lista_ProdutosFake();
        CadastrandoUmProdutoListaFake = null;
        _produtoMock.Setup(c => c.MapperProdutoLot(It.IsAny<IFormFile>())).ReturnsAsync(CadastrandoUmProdutoListaFake);
        _produtoMock.Setup(c => c.CreateProdutoLot(It.IsAny<ICollection<ProdutoDto>>())).ReturnsAsync("Cadastrado");
        ProdutoController produtoController = CreateFakeController(CadastrandoUmProdutoFake);
        //Setup mock file using a memory stream
        var content = "Hello World from a Fake File";
        var fileName = "test.pdf";
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(content);
        writer.Flush();
        stream.Position = 0;

        //create FormFile with desired data
        IFormFile file = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);


        //Act
        var response = await produtoController.AddFromExcel(file);
        var okResult = response as ObjectResult;

        //Assert
        _produtoMock.Verify(c => c.CreateProdutoLot(It.IsAny<ICollection<ProdutoDto>>()), Times.Never());
        _produtoMock.Verify(c => c.MapperProdutoLot(It.IsAny<IFormFile>()), Times.Once());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.BadRequest, okResult.StatusCode);

    }

    [Fact]
    public async Task Nao_Deve_Cadastrar_Lista_Produto_RetornoNoContent()
    {
        ProdutoDto CadastrandoUmProdutoFake = CriandoUmProdutoFake("Teste", "testando", "testado", 10, 20);
        ICollection<ProdutoDto> CadastrandoUmProdutoListaFake = Cria_Lista_ProdutosFake();
        _produtoMock.Setup(c => c.MapperProdutoLot(It.IsAny<IFormFile>())).ReturnsAsync(CadastrandoUmProdutoListaFake);
        string xpto = null;
        _produtoMock.Setup(c => c.CreateProdutoLot(It.IsAny<ICollection<ProdutoDto>>())).ReturnsAsync(xpto);
        ProdutoController produtoController = CreateFakeController(CadastrandoUmProdutoFake);
        //Setup mock file using a memory stream
        var content = "Hello World from a Fake File";
        var fileName = "test.pdf";
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(content);
        writer.Flush();
        stream.Position = 0;

        //create FormFile with desired data
        IFormFile file = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);


        //Act
        var response = await produtoController.AddFromExcel(file);
        var okResult = response as NoContentResult;

        //Assert
        _produtoMock.Verify(c => c.CreateProdutoLot(It.IsAny<ICollection<ProdutoDto>>()), Times.Once());
        _produtoMock.Verify(c => c.MapperProdutoLot(It.IsAny<IFormFile>()), Times.Once());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);

    }

    [Fact]
    public async Task Nao_Deve_Cadastrar_Lista_Produto_RetornoBadRequest()
    {
        //Arrange
        ProdutoDto CadastrandoUmProdutoFake = CriandoUmProdutoFake("Teste", "testando", "testado", 10, 20);
        ICollection<ProdutoDto> CadastrandoUmProdutoListaFake = Cria_Lista_ProdutosFake();
        _produtoMock.Setup(c => c.MapperProdutoLot(It.IsAny<IFormFile>())).ReturnsAsync(CadastrandoUmProdutoListaFake);
        _produtoMock.Setup(c => c.CreateProdutoLot(It.IsAny<ICollection<ProdutoDto>>())).ReturnsAsync("Cadastrado");
        ProdutoController produtoController = CreateFakeController(CadastrandoUmProdutoFake);
        produtoController.ModelState.AddModelError("key", "error message");
        var content = "Hello World from a Fake File";
        var fileName = "test.pdf";
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(content);
        writer.Flush();
        stream.Position = 0;

        //create FormFile with desired data
        IFormFile file = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);

        //Act
        var response = await produtoController.AddFromExcel(file);
        var okResult = response as ObjectResult;

        //Assert
        _produtoMock.Verify(c => c.CreateProdutoLot(It.IsAny<ICollection<ProdutoDto>>()), Times.Never());
        _produtoMock.Verify(c => c.MapperProdutoLot(It.IsAny<IFormFile>()), Times.Never());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.BadRequest, okResult.StatusCode);

    }

    [Fact]
    public async Task Deve_Atualizar_Produto()
    {
        //Arrange
        ProdutoDto CadastrandoUmProdutoFake = CriandoUmProdutoFake("Teste", "testando", "testado", 10, 20);
        CadastrandoUmProdutoFake.Id = Guid.NewGuid();
        _produtoMock.Setup(c => c.UpdateProduto(It.IsAny<ProdutoDto>())).ReturnsAsync(CadastrandoUmProdutoFake);
        ProdutoController produtoController = CreateFakeController(CadastrandoUmProdutoFake);

        //Act
        var response = await produtoController.AtualizarProduto(CadastrandoUmProdutoFake);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as ProdutoDto;

        //Assert
        _produtoMock.Verify(c => c.UpdateProduto(It.IsAny<ProdutoDto>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(result.Nome, CadastrandoUmProdutoFake.Nome);
        Assert.Equal(result.Marca, CadastrandoUmProdutoFake.Marca);
        Assert.Equal(result.Modelo, CadastrandoUmProdutoFake.Modelo);

    }


    [Fact]
    public async Task Nao_Deve_Atualizar_Produto_RetornoNoContent()
    {
        //Arrange
        ProdutoDto cadastrandoUmProdutoFake = CriandoUmProdutoFake("Teste", "testando", "testado", 10, 20);
        cadastrandoUmProdutoFake.Id = Guid.NewGuid();
        ProdutoDto CadastrandoUmProdutoFakeNull = null;
        _produtoMock.Setup(c => c.UpdateProduto(It.IsAny<ProdutoDto>())).ReturnsAsync(CadastrandoUmProdutoFakeNull);
        ProdutoController produtoController = CreateFakeController(cadastrandoUmProdutoFake);

        //Act
        var response = await produtoController.AtualizarProduto(cadastrandoUmProdutoFake);
        var okResult = response as NoContentResult;
        //Assert
        _produtoMock.Verify(c => c.UpdateProduto(It.IsAny<ProdutoDto>()), Times.Once());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);

    }

    [Fact]
    public async Task Nao_Deve_Atualizar_Produto_RetornoBadRequest()
    {
        //Arrange
        ProdutoDto CadastrandoUmProdutoFake = CriandoUmProdutoFake("Teste", "testando", "testado", 10, 20);
        _produtoMock.Setup(c => c.UpdateProduto(It.IsAny<ProdutoDto>())).ReturnsAsync(CadastrandoUmProdutoFake);
        ProdutoController produtoController = CreateFakeController(CadastrandoUmProdutoFake);
        produtoController.ModelState.AddModelError("key", "error message");

        //Act
        var response = await produtoController.AtualizarProduto(CadastrandoUmProdutoFake);
        var okResult = response as ObjectResult;

        //Assert
        _produtoMock.Verify(c => c.UpdateProduto(It.IsAny<ProdutoDto>()), Times.Never());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.BadRequest, okResult.StatusCode);

    }

    [Fact]
    public async Task Deve_Desativar_Produto()
    {
        //Arrange
        
        ProdutoDto CadastrandoUmProdutoFake = CriandoUmProdutoFake("Teste", "testando", "testado", 10, 20);
        CadastrandoUmProdutoFake.Id = Guid.NewGuid();
        _produtoMock.Setup(c => c.Desabled(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(CadastrandoUmProdutoFake);
        ProdutoController produtoController = CreateFakeController(CadastrandoUmProdutoFake);

        //Act
        var response = await produtoController.DesativarProduto(CadastrandoUmProdutoFake.Id.Value);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as ProdutoDto;

        //Assert
        _produtoMock.Verify(c => c.Desabled(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(result.Nome, CadastrandoUmProdutoFake.Nome);
        Assert.Equal(result.Marca, CadastrandoUmProdutoFake.Marca);
        Assert.Equal(result.Modelo, CadastrandoUmProdutoFake.Modelo);

    }


    [Fact]
    public async Task Nao_Deve_Desativar_Produto_RetornoNoContent()
    {
        //Arrange
        ProdutoDto CadastrandoUmProdutoFake = CriandoUmProdutoFake("Teste", "testando", "testado", 10, 20);
        CadastrandoUmProdutoFake.Id = Guid.NewGuid();
        ProdutoDto CadastrandoUmProdutoFakeNull = null;
        _produtoMock.Setup(c => c.Desabled(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(CadastrandoUmProdutoFakeNull);
        ProdutoController produtoController = CreateFakeController(CadastrandoUmProdutoFake);

        //Act
        var response = await produtoController.DesativarProduto(CadastrandoUmProdutoFake.Id.Value);
        var okResult = response as NoContentResult;
        //Assert
        _produtoMock.Verify(c => c.Desabled(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);

    }

    [Fact]
    public async Task Nao_Deve_Desativar_Produto_RetornoBadRequest()
    {
        //Arrange
        ProdutoDto CadastrandoUmProdutoFake = CriandoUmProdutoFake("Teste", "testando", "testado", 10, 20);
        CadastrandoUmProdutoFake.Id = Guid.NewGuid();
        _produtoMock.Setup(c => c.Desabled(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(CadastrandoUmProdutoFake);
        ProdutoController produtoController = CreateFakeController(CadastrandoUmProdutoFake);
        produtoController.ModelState.AddModelError("key", "error message");

        //Act
        var response = await produtoController.DesativarProduto(CadastrandoUmProdutoFake.Id.Value);
        var okResult = response as ObjectResult;

        //Assert
        _produtoMock.Verify(c => c.Desabled(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Never());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.BadRequest, okResult.StatusCode);

    }

    [Fact]
    public async Task Deve_Deletar_Produto()
    {
        //Arrange

        ProdutoDto CadastrandoUmProdutoFake = CriandoUmProdutoFake("Teste", "testando", "testado", 10, 20);
        CadastrandoUmProdutoFake.Id = Guid.NewGuid();
        _produtoMock.Setup(c => c.Delete(It.IsAny<Guid>()));
        ProdutoController produtoController = CreateFakeController(CadastrandoUmProdutoFake);

        //Act
        var response = await produtoController.DeletarProduto(CadastrandoUmProdutoFake.Id.Value);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as string;

        //Assert
        _produtoMock.Verify(c => c.Delete(It.IsAny<Guid>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal("Deletado", result);
       

    }

   

    [Fact]
    public async Task Nao_Deve_Deletar_Produto_RetornoBadRequest()
    {
        //Arrange
        ProdutoDto CadastrandoUmProdutoFake = CriandoUmProdutoFake("Teste", "testando", "testado", 10, 20);
        CadastrandoUmProdutoFake.Id = Guid.NewGuid();
        _produtoMock.Setup(c => c.Delete(It.IsAny<Guid>()));
        ProdutoController produtoController = CreateFakeController(CadastrandoUmProdutoFake);
        produtoController.ModelState.AddModelError("key", "error message");

        //Act
        var response = await produtoController.DeletarProduto(CadastrandoUmProdutoFake.Id.Value);
        var okResult = response as ObjectResult;

        //Assert
        _produtoMock.Verify(c => c.Delete(It.IsAny<Guid>()), Times.Never());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.BadRequest, okResult.StatusCode);

    }

    [Fact]
    public async Task Nao_Deve_Deletar_Produto_RetornoBadRequest_Exception()
    {
        //Arrange
        ProdutoDto CadastrandoUmProdutoFake = CriandoUmProdutoFake("Teste", "testando", "testado", 10, 20);
        CadastrandoUmProdutoFake.Id = Guid.NewGuid();
        _produtoMock.Setup(c => c.Delete(It.IsAny<Guid>())).ThrowsAsync(new Exception("Error"));
        ProdutoController produtoController = CreateFakeController(CadastrandoUmProdutoFake);

        //Act
        var response = await produtoController.DeletarProduto(CadastrandoUmProdutoFake.Id.Value);
        var okResult = response as ObjectResult;

        //Assert
        _produtoMock.Verify(c => c.Delete(It.IsAny<Guid>()), Times.Once());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.BadRequest, okResult.StatusCode);

    }

    private static ProdutoDto CriandoUmProdutoFake(string marca, string nome, string modelo, float valor_compra, float valor_venda)
    {
        return  new ProdutoDto() { Marca = "Unit", Nome = "Teste", Modelo = "Unitario", Valor_Compra = 20, Valor_Venda = 30, PrestadorId = Guid.NewGuid() };
    }
    
    private static ICollection<ProdutoDto> Cria_Lista_ProdutosFake()
    {
        return new List<ProdutoDto>() { new ProdutoDto() { Marca = "Unit", Nome = "Teste", Modelo = "Unitario", Valor_Compra = 20, Valor_Venda = 30, PrestadorId = Guid.NewGuid() }};
    }

}
