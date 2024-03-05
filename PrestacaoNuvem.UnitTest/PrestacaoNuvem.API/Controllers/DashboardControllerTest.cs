using Newtonsoft.Json;
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
    public async Task Deve_Retornar_Lista_CategoriaServico()
    {
        //Arranger
        ICollection<CategoriaServicoDto> categoriasFake = RetornarListaCategoriasFake("Teste Titulo", "teste");
        ICollection<DashboardReceitaDiariaDto> dashboardReceitaDiarias = new List<DashboardReceitaDiariaDto>() { new DashboardReceitaDiariaDto() { DateRef = DateTime.Now.ToString(), Valor = 20 } };
        _serviceMock.Setup(s => s.GetDailySales(It.IsAny<Guid>())).ReturnsAsync(dashboardReceitaDiarias);
        //Act
        DashboardController controller = CreateFakeController(categoriasFake);
        var response = await controller.GetDashboardFaturamentoDiario();
        var okResult = response as OkObjectResult;
        var result = JsonConvert.DeserializeObject<ICollection<DashboardReceitaDiariaDto>>(okResult.Value.ToString());

        //Assert
        _serviceMock.Verify(s => s.GetDailySales(It.IsAny<Guid>()), Times.Once);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(result.Count, dashboardReceitaDiarias.Count);
        Assert.Equal(result.First().Valor, dashboardReceitaDiarias.First().Valor);
        Assert.Equal(result.First().DateRef, dashboardReceitaDiarias.First().DateRef);
    }

    [Fact]
    public async Task Nao_Deve_Retornar_ListaCategoriaService_ReturnoNull()
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

}
