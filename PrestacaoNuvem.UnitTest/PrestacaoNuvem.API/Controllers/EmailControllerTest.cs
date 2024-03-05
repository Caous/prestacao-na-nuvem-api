using Microsoft.Extensions.Configuration;
using PrestacaoNuvem.Api.Domain.Interfacesk;

namespace PrestacaoNuvem.UnitTest.PrestacaoNuvem.API.Controllers;

public class EmailControllerTest
{
    private readonly Mock<IEmailManager> _managerEmail = new();

    private static DefaultHttpContext CreateFakeClaims(ICollection<PrestadorDto> prestadorDto)
    {
        var fakeHttpContext = new DefaultHttpContext();
        ClaimsIdentity identity = new(
            new[] {
                        new Claim("PrestadorId", prestadorDto.First().PrestadorId.ToString()),
                        new Claim("UserName", "Teste"),
                        new Claim("IdUserLogin", prestadorDto.First().PrestadorId.ToString())

            }
        );
        fakeHttpContext.User = new System.Security.Claims.ClaimsPrincipal(identity);
        return fakeHttpContext;
    }


    [Fact]
    public async Task Deve_Enviar_Email_RetornarTrue()
    {
        //Arrange
        var inMemorySettings = new Dictionary<string, string> {
            {"EmailConfiguration:Host", "Teste" },
            {"EmailConfiguration:Port", "8080" },
            {"EmailConfiguration:UserName", "Teste" },
            {"EmailConfiguration:Password", "Teste" },
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        ICollection<PrestadorDto> prestadoresFake = CriaListaFornecedoresFake();
        _managerEmail.Setup(s => s.SendEmailSmtpAsync(It.IsAny<Email>())).ReturnsAsync(true);
        EmailController controller = new EmailController(_managerEmail.Object, configuration) { ControllerContext = new ControllerContext() { HttpContext = CreateFakeClaims(prestadoresFake) } };
        //Act
        var response = await controller.SendAsyncEmail();
        var okResult = response as OkObjectResult;
        var result = okResult.Value;
        //Assert
        _managerEmail.Verify(s => s.SendEmailSmtpAsync(It.IsAny<Email>()), Times.Once());
        Assert.NotNull(result);
    }

    private static ICollection<PrestadorDto> CriaListaFornecedoresFake()
    {
        return new List<PrestadorDto>() { new PrestadorDto() { CPF = "40608550817", EmailEmpresa = "teste@test.com.br", Nome = "Teste", Telefone = "11999999999", Id = Guid.NewGuid(), PrestadorId = Guid.NewGuid() } };
    }
}
