using SmartOficina.Api.Domain.Interfaces;

namespace SmartOficina.UnitTest.SmartOficina.API.Controllers;

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
    public async Task Deve_retornar_lista_Produtos()
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
 
    private static ProdutoDto CriandoUmProdutoFake(string marca, string nome, string modelo, float valor_compra, float valor_venda)
    {
        return  new ProdutoDto() { Marca = "Unit", Nome = "Teste", Modelo = "Unitario", Valor_Compra = 20, Valor_Venda = 30, PrestadorId = Guid.NewGuid() };
    }
    
    private static ICollection<ProdutoDto> Cria_Lista_ProdutosFake()
    {
        return new List<ProdutoDto>() { new ProdutoDto() { Marca = "Unit", Nome = "Teste", Modelo = "Unitario", Valor_Compra = 20, Valor_Venda = 30, PrestadorId = Guid.NewGuid() }};
    }

}
