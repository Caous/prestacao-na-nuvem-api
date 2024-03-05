using DocumentFormat.OpenXml;
using Moq;
using PrestacaoNuvem.Api.Controllers;
using PrestacaoNuvem.Api.Domain.Interfaces;
using System.Net;
using static PrestacaoNuvem.Api.Dto.DashboardDto;

namespace PrestacaoNuvem.UnitTest.PrestacaoNuvem.API.Controllers;

public class DashboardControllerTest
{
    private readonly Mock<IDashboardService> _serviceMock = new();
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
    private DashboardController CreateFakeController(ICollection<CategoriaServicoDto> categoriasFake)
    {
        return new DashboardController(_serviceMock.Object) { ControllerContext = new ControllerContext() { HttpContext = CreateFakeClaims(categoriasFake) } };
    }
    private static ICollection<CategoriaServicoDto> RetornarListaCategoriasFake(string titulo, string descricao)
    {
        return new List<CategoriaServicoDto>() { new CategoriaServicoDto() { Titulo = "Titulo Categoria Fake", Desc = "Descricao Categoria Fake", PrestadorId = Guid.NewGuid() } };
    }

    [Fact]
    public async Task Deve_Retornar_Lista_FaturamentoDiario_RetornoOk()
    {
        //Arranger
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
        ICollection<DashboardReceitaDiariaDto> dashboardReceitaDiarias = new List<DashboardReceitaDiariaDto>() { new DashboardReceitaDiariaDto() { DateRef = DateTime.Now.ToString(), Valor = 20 } };
        _serviceMock.Setup(s => s.GetDailySales(It.IsAny<Guid>())).ReturnsAsync(dashboardReceitaDiarias);
        //Act
        DashboardController controller = CreateFakeController(categoriasFake);
        var response = await controller.GetDashboardFaturamentoDiario();
        var okResult = response as OkObjectResult;
        var result = okResult.Value as object[];


        dynamic firstItem = result[0];

        System.Reflection.PropertyInfo pi = firstItem.GetType().GetProperty("Key");
        string data = (string)(pi.GetValue(firstItem, null));

        pi = firstItem.GetType().GetProperty("Count");
        double valor = (double)(pi.GetValue(firstItem, null));

        //Assert
        _serviceMock.Verify(s => s.GetDailySales(It.IsAny<Guid>()), Times.Once);
        Assert.NotNull(result);
        Assert.NotNull(data);
        Assert.NotNull(valor);
    }

    [Fact]
    public async Task Nao_Deve_Retornar_FaturamentoDiario_ReturnoNull()
    {
        //Arranger (parametros)
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
        ICollection<DashboardReceitaDiariaDto> dashboardReceitaDiarias = null;
        _serviceMock.Setup(s => s.GetDailySales(It.IsAny<Guid>())).ReturnsAsync(dashboardReceitaDiarias);

        DashboardController controller = CreateFakeController(categoriasFake);

        //Act (ação/execução)
        var response = await controller.GetDashboardFaturamentoDiario();
        var okResult = response as NoContentResult;

        //Assert (verificação)
        _serviceMock.Verify(s => s.GetDailySales(It.IsAny<Guid>()), Times.Once);
        Assert.NotNull(okResult); //Verifico se é nulo
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);

    }

    [Fact]
    public async Task Nao_Deve_Retornar_FaturamentoDiario_ReturnoBadRequest()
    {
        //Arranger (parametros)
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
        ICollection<DashboardReceitaDiariaDto> dashboardReceitaDiarias = null;
        _serviceMock.Setup(s => s.GetDailySales(It.IsAny<Guid>())).ReturnsAsync(dashboardReceitaDiarias);

        DashboardController controller = CreateFakeController(categoriasFake);
        controller.ModelState.AddModelError("key", "error message");

        //Act (ação/execução)
        var response = await controller.GetDashboardFaturamentoDiario();
        var okResult = response as ObjectResult;

        //Assert (verificação)
        _serviceMock.Verify(s => s.GetDailySales(It.IsAny<Guid>()), Times.Never);
        Assert.NotNull(okResult); //Verifico se é nulo
        Assert.Equal((int)HttpStatusCode.BadRequest, okResult.StatusCode);

    }


    [Fact]
    public async Task Deve_Retornar_Lista_CategoriaVendaAgrupado_RetornoOk()
    {
        //Arranger
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
        ICollection<DashboardReceitaCategoriaDto> dashboardReceitaDiarias = new List<DashboardReceitaCategoriaDto>() { new DashboardReceitaCategoriaDto() { Categoria = "Teste", Valor = 20 } };
        _serviceMock.Setup(s => s.GetServicesGroupByCategoryService(It.IsAny<Guid>())).ReturnsAsync(dashboardReceitaDiarias);
        //Act
        DashboardController controller = CreateFakeController(categoriasFake);
        var response = await controller.GetDashboardCategoriaAgrupado();
        var okResult = response as OkObjectResult;
        var result = okResult.Value as object[];


        dynamic firstItem = result[0];

        System.Reflection.PropertyInfo pi = firstItem.GetType().GetProperty("Key");
        string caterogia = (string)(pi.GetValue(firstItem, null));

        pi = firstItem.GetType().GetProperty("Count");
        string valor = (string)(pi.GetValue(firstItem, null));

        //Assert
        _serviceMock.Verify(s => s.GetServicesGroupByCategoryService(It.IsAny<Guid>()), Times.Once);
        Assert.NotNull(result);
        Assert.NotNull(caterogia);
        Assert.NotNull(valor);
    }

    [Fact]
    public async Task Nao_Deve_Retornar_CategoriaVendaAgrupado_ReturnoNull()
    {
        //Arranger (parametros)
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
        ICollection<DashboardReceitaCategoriaDto> dashboardReceitaDiarias = null;
        _serviceMock.Setup(s => s.GetServicesGroupByCategoryService(It.IsAny<Guid>())).ReturnsAsync(dashboardReceitaDiarias);

        DashboardController controller = CreateFakeController(categoriasFake);

        //Act (ação/execução)
        var response = await controller.GetDashboardCategoriaAgrupado();
        var okResult = response as NoContentResult;

        //Assert (verificação)
        _serviceMock.Verify(s => s.GetServicesGroupByCategoryService(It.IsAny<Guid>()), Times.Once);
        Assert.NotNull(okResult); //Verifico se é nulo
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);

    }

    [Fact]
    public async Task Nao_Deve_Retornar_CategoriaVendaAgrupado_ReturnoBadRequest()
    {
        //Arranger (parametros)
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
        ICollection<DashboardReceitaCategoriaDto> dashboardReceitaDiarias = null;
        _serviceMock.Setup(s => s.GetServicesGroupByCategoryService(It.IsAny<Guid>())).ReturnsAsync(dashboardReceitaDiarias);

        DashboardController controller = CreateFakeController(categoriasFake);
        controller.ModelState.AddModelError("key", "error message");

        //Act (ação/execução)
        var response = await controller.GetDashboardCategoriaAgrupado();
        var okResult = response as ObjectResult;

        //Assert (verificação)
        _serviceMock.Verify(s => s.GetServicesGroupByCategoryService(It.IsAny<Guid>()), Times.Never);
        Assert.NotNull(okResult); //Verifico se é nulo
        Assert.Equal((int)HttpStatusCode.BadRequest, okResult.StatusCode);

    }

    [Fact]
    public async Task Deve_Retornar_Lista_SubCategoriaVendaAgrupado_RetornoOk()
    {
        //Arranger
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
        ICollection<DashboardReceitaSubCaterogiaDto> dashboardReceitaDiarias = new List<DashboardReceitaSubCaterogiaDto>() { new DashboardReceitaSubCaterogiaDto() { Titulo = "Teste", Valor = 20 } };
        _serviceMock.Setup(s => s.GetServicesGroupBySubCategoryService(It.IsAny<Guid>())).ReturnsAsync(dashboardReceitaDiarias);
        //Act
        DashboardController controller = CreateFakeController(categoriasFake);
        var response = await controller.GetDashboardSubCategoriaAgrupado();
        var okResult = response as OkObjectResult;
        var result = okResult.Value as object[];


        dynamic firstItem = result[0];

        System.Reflection.PropertyInfo pi = firstItem.GetType().GetProperty("Key");
        string titulo = (string)(pi.GetValue(firstItem, null));

        pi = firstItem.GetType().GetProperty("Count");
        double valor = (double)(pi.GetValue(firstItem, null));

        //Assert
        _serviceMock.Verify(s => s.GetServicesGroupBySubCategoryService(It.IsAny<Guid>()), Times.Once);
        Assert.NotNull(result);
        Assert.NotNull(titulo);
        Assert.NotNull(valor);
    }

    [Fact]
    public async Task Nao_Deve_Retornar_SubCategoriaVendaAgrupado_ReturnoNull()
    {
        //Arranger (parametros)
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
        ICollection<DashboardReceitaSubCaterogiaDto> dashboardReceitaDiarias = null;
        _serviceMock.Setup(s => s.GetServicesGroupBySubCategoryService(It.IsAny<Guid>())).ReturnsAsync(dashboardReceitaDiarias);

        DashboardController controller = CreateFakeController(categoriasFake);

        //Act (ação/execução)
        var response = await controller.GetDashboardSubCategoriaAgrupado();
        var okResult = response as NoContentResult;

        //Assert (verificação)
        _serviceMock.Verify(s => s.GetServicesGroupBySubCategoryService(It.IsAny<Guid>()), Times.Once);
        Assert.NotNull(okResult); //Verifico se é nulo
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);

    }

    [Fact]
    public async Task Nao_Deve_Retornar_SubCategoriaVendaAgrupado_ReturnoBadRequest()
    {
        //Arranger (parametros)
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
        ICollection<DashboardReceitaSubCaterogiaDto> dashboardReceitaDiarias = null;
        _serviceMock.Setup(s => s.GetServicesGroupBySubCategoryService(It.IsAny<Guid>())).ReturnsAsync(dashboardReceitaDiarias);

        DashboardController controller = CreateFakeController(categoriasFake);
        controller.ModelState.AddModelError("key", "error message");

        //Act (ação/execução)
        var response = await controller.GetDashboardSubCategoriaAgrupado();
        var okResult = response as ObjectResult;

        //Assert (verificação)
        _serviceMock.Verify(s => s.GetServicesGroupBySubCategoryService(It.IsAny<Guid>()), Times.Never);
        Assert.NotNull(okResult); //Verifico se é nulo
        Assert.Equal((int)HttpStatusCode.BadRequest, okResult.StatusCode);

    }


    [Fact]
    public async Task Deve_Retornar_Lista_ReceitaMesa_RetornoOk()
    {
        //Arranger
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
        DashboardReceitaMesDto dashboardReceitaDiarias = new DashboardReceitaMesDto() { Valor = 20 };
        _serviceMock.Setup(s => s.GetSalesMonth(It.IsAny<Guid>())).ReturnsAsync(dashboardReceitaDiarias);
        //Act
        DashboardController controller = CreateFakeController(categoriasFake);
        var response = await controller.GetDashboardReceitaMes();
        var okResult = response as OkObjectResult;
        var result = okResult.Value as DashboardReceitaMesDto;




        //Assert
        _serviceMock.Verify(s => s.GetSalesMonth(It.IsAny<Guid>()), Times.Once);
        Assert.NotNull(result);
        Assert.Equal(20, result.Valor);
    }

    [Fact]
    public async Task Nao_Deve_Retornar_ReceitaMesa_ReturnoNull()
    {
        //Arranger (parametros)
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
        DashboardReceitaMesDto dashboardReceitaDiarias = null;
        _serviceMock.Setup(s => s.GetSalesMonth(It.IsAny<Guid>())).ReturnsAsync(dashboardReceitaDiarias);

        DashboardController controller = CreateFakeController(categoriasFake);

        //Act (ação/execução)
        var response = await controller.GetDashboardReceitaMes();
        var okResult = response as NoContentResult;

        //Assert (verificação)
        _serviceMock.Verify(s => s.GetSalesMonth(It.IsAny<Guid>()), Times.Once);
        Assert.NotNull(okResult); //Verifico se é nulo
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);

    }

    [Fact]
    public async Task Nao_Deve_Retornar_ReceitaMesa_ReturnoBadRequest()
    {
        //Arranger (parametros)
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
        DashboardReceitaMesDto dashboardReceitaDiarias = null;
        _serviceMock.Setup(s => s.GetSalesMonth(It.IsAny<Guid>())).ReturnsAsync(dashboardReceitaDiarias);

        DashboardController controller = CreateFakeController(categoriasFake);
        controller.ModelState.AddModelError("key", "error message");

        //Act (ação/execução)
        var response = await controller.GetDashboardReceitaMes();
        var okResult = response as ObjectResult;

        //Assert (verificação)
        _serviceMock.Verify(s => s.GetSalesMonth(It.IsAny<Guid>()), Times.Never);
        Assert.NotNull(okResult); //Verifico se é nulo
        Assert.Equal((int)HttpStatusCode.BadRequest, okResult.StatusCode);

    }

    [Fact]
    public async Task Deve_Retornar_Lista_ClientesNovos_RetornoOk()
    {
        //Arranger
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
        DashboardClientesNovos dashboardReceitaDiarias = new DashboardClientesNovos() { Valor = 20 };
        _serviceMock.Setup(s => s.GetNewCustomerMonth(It.IsAny<Guid>())).ReturnsAsync(dashboardReceitaDiarias);
        //Act
        DashboardController controller = CreateFakeController(categoriasFake);
        var response = await controller.GetDashboardClientesNovosMes();
        var okResult = response as OkObjectResult;
        var result = okResult.Value as DashboardClientesNovos;




        //Assert
        _serviceMock.Verify(s => s.GetNewCustomerMonth(It.IsAny<Guid>()), Times.Once);
        Assert.NotNull(result);
        Assert.Equal(20, result.Valor);
    }

    [Fact]
    public async Task Nao_Deve_Retornar_ClientesNovos_ReturnoNull()
    {
        //Arranger (parametros)
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
        DashboardClientesNovos dashboardReceitaDiarias = null;
        _serviceMock.Setup(s => s.GetNewCustomerMonth(It.IsAny<Guid>())).ReturnsAsync(dashboardReceitaDiarias);

        DashboardController controller = CreateFakeController(categoriasFake);

        //Act (ação/execução)
        var response = await controller.GetDashboardClientesNovosMes();
        var okResult = response as NoContentResult;

        //Assert (verificação)
        _serviceMock.Verify(s => s.GetNewCustomerMonth(It.IsAny<Guid>()), Times.Once);
        Assert.NotNull(okResult); //Verifico se é nulo
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);

    }

    [Fact]
    public async Task Nao_Deve_Retornar_ClientesNovos_ReturnoBadRequest()
    {
        //Arranger (parametros)
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
        DashboardClientesNovos dashboardReceitaDiarias = null;
        _serviceMock.Setup(s => s.GetNewCustomerMonth(It.IsAny<Guid>())).ReturnsAsync(dashboardReceitaDiarias);

        DashboardController controller = CreateFakeController(categoriasFake);
        controller.ModelState.AddModelError("key", "error message");

        //Act (ação/execução)
        var response = await controller.GetDashboardClientesNovosMes();
        var okResult = response as ObjectResult;

        //Assert (verificação)
        _serviceMock.Verify(s => s.GetNewCustomerMonth(It.IsAny<Guid>()), Times.Never);
        Assert.NotNull(okResult); //Verifico se é nulo
        Assert.Equal((int)HttpStatusCode.BadRequest, okResult.StatusCode);

    }

    [Fact]
    public async Task Deve_Retornar_Lista_ProdutosVendidosMes_RetornoOk()
    {
        //Arranger
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
        DashboardProdutosNovos dashboardReceitaDiarias = new DashboardProdutosNovos() { Valor = 20 };
        _serviceMock.Setup(s => s.GetSalesProductMonth(It.IsAny<Guid>())).ReturnsAsync(dashboardReceitaDiarias);
        //Act
        DashboardController controller = CreateFakeController(categoriasFake);
        var response = await controller.GetDashboardProdutosVendidosMes();
        var okResult = response as OkObjectResult;
        var result = okResult.Value as DashboardProdutosNovos;




        //Assert
        _serviceMock.Verify(s => s.GetSalesProductMonth(It.IsAny<Guid>()), Times.Once);
        Assert.NotNull(result);
        Assert.Equal(20, result.Valor);
    }

    [Fact]
    public async Task Nao_Deve_Retornar_ProdutosVendidosMes_ReturnoNull()
    {
        //Arranger (parametros)
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
        DashboardProdutosNovos dashboardReceitaDiarias = null;
        _serviceMock.Setup(s => s.GetSalesProductMonth(It.IsAny<Guid>())).ReturnsAsync(dashboardReceitaDiarias);

        DashboardController controller = CreateFakeController(categoriasFake);

        //Act (ação/execução)
        var response = await controller.GetDashboardProdutosVendidosMes();
        var okResult = response as NoContentResult;

        //Assert (verificação)
        _serviceMock.Verify(s => s.GetSalesProductMonth(It.IsAny<Guid>()), Times.Once);
        Assert.NotNull(okResult); //Verifico se é nulo
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);

    }

    [Fact]
    public async Task Nao_Deve_Retornar_ProdutosVendidosMes_ReturnoBadRequest()
    {
        //Arranger (parametros)
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
        DashboardProdutosNovos dashboardReceitaDiarias = null;
        _serviceMock.Setup(s => s.GetSalesProductMonth(It.IsAny<Guid>())).ReturnsAsync(dashboardReceitaDiarias);

        DashboardController controller = CreateFakeController(categoriasFake);
        controller.ModelState.AddModelError("key", "error message");

        //Act (ação/execução)
        var response = await controller.GetDashboardProdutosVendidosMes();
        var okResult = response as ObjectResult;

        //Assert (verificação)
        _serviceMock.Verify(s => s.GetSalesProductMonth(It.IsAny<Guid>()), Times.Never);
        Assert.NotNull(okResult); //Verifico se é nulo
        Assert.Equal((int)HttpStatusCode.BadRequest, okResult.StatusCode);

    }

    [Fact]
    public async Task Deve_Retornar_Lista_OSMes_RetornoOk()
    {
        //Arranger
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
        DashboardOSMes dashboardReceitaDiarias = new DashboardOSMes() { Valor = 20 };
        _serviceMock.Setup(s => s.GetOSMonth(It.IsAny<Guid>())).ReturnsAsync(dashboardReceitaDiarias);
        //Act
        DashboardController controller = CreateFakeController(categoriasFake);
        var response = await controller.GetDashboardServicosMes();
        var okResult = response as OkObjectResult;
        var result = okResult.Value as DashboardOSMes;




        //Assert
        _serviceMock.Verify(s => s.GetOSMonth(It.IsAny<Guid>()), Times.Once);
        Assert.NotNull(result);
        Assert.Equal(20, result.Valor);
    }

    [Fact]
    public async Task Nao_Deve_Retornar_OSMes_ReturnoNull()
    {
        //Arranger (parametros)
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
        DashboardOSMes dashboardReceitaDiarias = null;
        _serviceMock.Setup(s => s.GetOSMonth(It.IsAny<Guid>())).ReturnsAsync(dashboardReceitaDiarias);

        DashboardController controller = CreateFakeController(categoriasFake);

        //Act (ação/execução)
        var response = await controller.GetDashboardServicosMes();
        var okResult = response as NoContentResult;

        //Assert (verificação)
        _serviceMock.Verify(s => s.GetOSMonth(It.IsAny<Guid>()), Times.Once);
        Assert.NotNull(okResult); //Verifico se é nulo
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);

    }

    [Fact]
    public async Task Nao_Deve_Retornar_OSMes_ReturnoBadRequest()
    {
        //Arranger (parametros)
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
        DashboardOSMes dashboardReceitaDiarias = null;
        _serviceMock.Setup(s => s.GetOSMonth(It.IsAny<Guid>())).ReturnsAsync(dashboardReceitaDiarias);

        DashboardController controller = CreateFakeController(categoriasFake);
        controller.ModelState.AddModelError("key", "error message");

        //Act (ação/execução)
        var response = await controller.GetDashboardServicosMes();
        var okResult = response as ObjectResult;

        //Assert (verificação)
        _serviceMock.Verify(s => s.GetOSMonth(It.IsAny<Guid>()), Times.Never);
        Assert.NotNull(okResult); //Verifico se é nulo
        Assert.Equal((int)HttpStatusCode.BadRequest, okResult.StatusCode);

    }
}
