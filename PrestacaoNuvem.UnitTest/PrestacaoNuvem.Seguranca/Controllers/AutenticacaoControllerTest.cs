using PrestacaoNuvem.Seguranca.Controllers;
using PrestacaoNuvem.Seguranca.Domain.Model;
using PrestacaoNuvem.Seguranca.Dto;
using PrestacaoNuvem.Seguranca.Infrastructure.Configurations.Repositories.Interfaces;
using PrestacaoNuvem.Seguranca.Infrastructure.Constants;

namespace PrestacaoNuvem.UnitTest.PrestacaoNuvem.Seguranca.Controllers;

#pragma warning disable 8604, 8602, 8629, 8600, 8620
public class AutenticacaoControllerTest
{
    private readonly Mock<IAcessoManager> _acessoManager = new();
    private readonly Mock<IMapper> _mapper = new();

    [Fact]
    public async Task Deve_Retornar_TokenPrestador_TodosParamentrosPreenchidos()
    {
        //Arranger
        Token tokenFake = GeradorTokenFake(autenticado: true);
        _acessoManager.Setup(x => x.ValidarCredenciais(It.IsAny<UserModelDto>())).ReturnsAsync(tokenFake);
        //Act
        AutenticacaoController controllerCategoria = new AutenticacaoController(_acessoManager.Object, _mapper.Object);
        var response = await controllerCategoria.LoginPrestador(GeradorPrestadorLoginDtoFake(email: "teste@teste.com.br", pass: "Teste@teste123", user: "Teste"));
        var okResult = response as OkObjectResult;
        var result = okResult?.Value as Token;
        //Assert
        _acessoManager.Verify(x => x.ValidarCredenciais(It.IsAny<UserModelDto>()), Times.Once);
        _mapper.Verify(x => x.Map<UserModel>(It.IsAny<PrestadorLoginDto>()), Times.Never);
        Assert.NotNull(result);
        Assert.Equal(result.AccessToken, tokenFake.AccessToken);
        Assert.Equal(result.Authenticated, tokenFake.Authenticated);
        Assert.Equal(result.Created, tokenFake.Created);
        Assert.Equal(result.Expiration, tokenFake.Expiration);
        Assert.Equal(result.Message, tokenFake.Message);

    }

    [Fact]
    public async Task Deve_Retornar_TokenPrestador_ComEmailVazio()
    {
        //Arranger
        Token tokenFake = GeradorTokenFake(autenticado: true);
        _acessoManager.Setup(x => x.ValidarCredenciais(It.IsAny<UserModelDto>())).ReturnsAsync(tokenFake);
        //Act
        AutenticacaoController controllerCategoria = new AutenticacaoController(_acessoManager.Object, _mapper.Object);
        var response = await controllerCategoria.LoginPrestador(GeradorPrestadorLoginDtoFake(email: string.Empty, pass: "Teste@teste123", user: "Teste"));
        var okResult = response as OkObjectResult;
        var result = okResult?.Value as Token;
        //Assert
        _acessoManager.Verify(x => x.ValidarCredenciais(It.IsAny<UserModelDto>()), Times.Once);
        _mapper.Verify(x => x.Map<UserModel>(It.IsAny<PrestadorLoginDto>()), Times.Never);
        Assert.NotNull(result);
        Assert.Equal(result.AccessToken, tokenFake.AccessToken);
        Assert.Equal(result.Authenticated, tokenFake.Authenticated);
        Assert.Equal(result.Created, tokenFake.Created);
        Assert.Equal(result.Expiration, tokenFake.Expiration);
        Assert.Equal(result.Message, tokenFake.Message);

    }

    [Fact]
    public async Task Deve_Retornar_TokenPrestador_ComUserVazio()
    {
        //Arranger
        Token tokenFake = GeradorTokenFake(autenticado: true);
        _acessoManager.Setup(x => x.ValidarCredenciais(It.IsAny<UserModelDto>())).ReturnsAsync(tokenFake);
        //Act
        AutenticacaoController controllerCategoria = new AutenticacaoController(_acessoManager.Object, _mapper.Object);
        var response = await controllerCategoria.LoginPrestador(GeradorPrestadorLoginDtoFake(email: "teste@teste.com.br", pass: "Teste@teste123", user: string.Empty));
        var okResult = response as OkObjectResult;
        var result = okResult?.Value as Token;
        //Assert
        _acessoManager.Verify(x => x.ValidarCredenciais(It.IsAny<UserModelDto>()), Times.Once);
        _mapper.Verify(x => x.Map<UserModel>(It.IsAny<PrestadorLoginDto>()), Times.Never);
        Assert.NotNull(result);
        Assert.Equal(result.AccessToken, tokenFake.AccessToken);
        Assert.Equal(result.Authenticated, tokenFake.Authenticated);
        Assert.Equal(result.Created, tokenFake.Created);
        Assert.Equal(result.Expiration, tokenFake.Expiration);
        Assert.Equal(result.Message, tokenFake.Message);

    }

    [Fact]
    public async Task NaoDeve_Retornar_TokenPrestador_ParametroEmailVazioUserVazio()
    {
        //Arranger
        Token tokenFake = GeradorTokenFake(autenticado: true);
        _acessoManager.Setup(x => x.ValidarCredenciais(It.IsAny<UserModelDto>())).ReturnsAsync(tokenFake);
        //Act
        AutenticacaoController controllerCategoria = new AutenticacaoController(_acessoManager.Object, _mapper.Object);
        var response = await controllerCategoria.LoginPrestador(GeradorPrestadorLoginDtoFake(email: string.Empty, pass: string.Empty, user: string.Empty));
        var okResult = response as BadRequestObjectResult;
        var result = okResult?.Value as string;
        //Assert
        _acessoManager.Verify(x => x.ValidarCredenciais(It.IsAny<UserModelDto>()), Times.Never);
        _mapper.Verify(x => x.Map<UserModel>(It.IsAny<PrestadorLoginDto>()), Times.Never);
        Assert.NotNull(result);
        Assert.Equal( $"{PrestadorConst.PrestadorEmailVazio} ou {PrestadorConst.PrestadorNomeUsarioVazio}", result);

    }

    [Fact]
    public async Task NaoDeve_Retornar_TokenPrestador_UsuarioNaoEncontrador()
    {
        //Arranger
        Token tokenFake = GeradorTokenFake(autenticado: false);
        _acessoManager.Setup(x => x.ValidarCredenciais(It.IsAny<UserModelDto>())).ReturnsAsync(tokenFake);
        //Act
        AutenticacaoController controllerCategoria = new AutenticacaoController(_acessoManager.Object, _mapper.Object);
        var response = await controllerCategoria.LoginPrestador(GeradorPrestadorLoginDtoFake(email: "teste@teste.com.br", pass: string.Empty, user: string.Empty));
        var okResult = response as ForbidResult;

        //Assert
        _acessoManager.Verify(x => x.ValidarCredenciais(It.IsAny<UserModelDto>()), Times.Once);
        _mapper.Verify(x => x.Map<UserModel>(It.IsAny<PrestadorLoginDto>()), Times.Never);
        Assert.NotNull(okResult);
        Asset.Equals(okResult.AuthenticationSchemes.First(), "Não autenticado");

    }


    [Fact]
    public async Task Deve_Criar_Prestador_TodosParamentrosPreenchidos()
    {
        //Arranger
        _acessoManager.Setup(x => x.CriarPrestador(It.IsAny<PrestadorCadastroDto>())).ReturnsAsync(true);
        _acessoManager.Setup(x => x.GetUserPorEmail(It.IsAny<string>())).ReturnsAsync(CriarUserModelFake());
        //Act
        AutenticacaoController controllerCategoria = new AutenticacaoController(_acessoManager.Object, _mapper.Object);
        var response = await controllerCategoria.Post(CriarPrestadorCadastroFake(
                                                                                    id: Guid.NewGuid(), email: "teste@teste.com.br", userName: "teste", date: DateTime.Now, pass: "teste", usrCad: Guid.NewGuid(), usuDescricaoCad: "teste"));
        var okResult = response as OkResult;

        //Assert
        _acessoManager.Verify(x => x.CriarPrestador(It.IsAny<PrestadorCadastroDto>()), Times.Once);
        _acessoManager.Verify(x => x.GetUserPorEmail(It.IsAny<string>()), Times.Never);
        _mapper.Verify(x => x.Map<UserModel>(It.IsAny<PrestadorLoginDto>()), Times.Never);
        Assert.NotNull(okResult);
        Asset.Equals(okResult.StatusCode.ToString(), "200");

    }

    [Fact]
    public async Task Deve_Criar_Prestador_SoComUsuarioDescPreenchido()
    {
        //Arranger
        _acessoManager.Setup(x => x.CriarPrestador(It.IsAny<PrestadorCadastroDto>())).ReturnsAsync(true);
        _acessoManager.Setup(x => x.GetUserPorEmail(It.IsAny<string>())).ReturnsAsync(CriarUserModelFake());
        //Act
        AutenticacaoController controllerCategoria = new AutenticacaoController(_acessoManager.Object, _mapper.Object);
        var response = await controllerCategoria.Post(CriarPrestadorCadastroFake(
                                                                                    id: Guid.NewGuid(), email: "teste@teste.com.br", userName: "teste", date: DateTime.Now, pass: "teste", usrCad: null, usuDescricaoCad: "teste"));
        var okResult = response as OkResult;

        //Assert
        _acessoManager.Verify(x => x.CriarPrestador(It.IsAny<PrestadorCadastroDto>()), Times.Once);
        _acessoManager.Verify(x => x.GetUserPorEmail(It.IsAny<string>()), Times.Once);
        _mapper.Verify(x => x.Map<UserModel>(It.IsAny<PrestadorLoginDto>()), Times.Never);
        _acessoManager.Verify(x => x.CriarPrestador(It.IsAny<PrestadorCadastroDto>()), Times.Once);
        Assert.NotNull(okResult);
        Asset.Equals(okResult.StatusCode.ToString(), "200");

    }


    [Fact]
    public async Task Deve_Criar_Prestador_SoComUsuCadPreenchido()
    {
        //Arranger
        _acessoManager.Setup(x => x.CriarPrestador(It.IsAny<PrestadorCadastroDto>())).ReturnsAsync(true);
        _acessoManager.Setup(x => x.GetUserPorEmail(It.IsAny<string>())).ReturnsAsync(CriarUserModelFake());
        //Act
        AutenticacaoController controllerCategoria = new AutenticacaoController(_acessoManager.Object, _mapper.Object);
        var response = await controllerCategoria.Post(CriarPrestadorCadastroFake(
                                                                                    id: Guid.NewGuid(), email: "teste@teste.com.br", userName: "teste", date: DateTime.Now, pass: "teste", usrCad: Guid.NewGuid(), usuDescricaoCad: string.Empty));
        var okResult = response as OkResult;

        //Assert
        _acessoManager.Verify(x => x.CriarPrestador(It.IsAny<PrestadorCadastroDto>()), Times.Once);
        _acessoManager.Verify(x => x.GetUserPorEmail(It.IsAny<string>()), Times.Once);
        _mapper.Verify(x => x.Map<UserModel>(It.IsAny<PrestadorLoginDto>()), Times.Never);
        _acessoManager.Verify(x => x.CriarPrestador(It.IsAny<PrestadorCadastroDto>()), Times.Once);
        Assert.NotNull(okResult);
        Asset.Equals(okResult.StatusCode.ToString(), "200");

    }


    [Fact]
    public async Task NaoDeve_Criar_Prestador_Quando_RetornarFalse()
    {
        //Arranger
        _acessoManager.Setup(x => x.CriarPrestador(It.IsAny<PrestadorCadastroDto>())).ReturnsAsync(false);
        _acessoManager.Setup(x => x.GetUserPorEmail(It.IsAny<string>())).ReturnsAsync(CriarUserModelFake());
        //Act
        AutenticacaoController controllerCategoria = new AutenticacaoController(_acessoManager.Object, _mapper.Object);
        var response = await controllerCategoria.Post(CriarPrestadorCadastroFake(
                                                                                    id: Guid.NewGuid(), email: "teste@teste.com.br", userName: "teste", date: DateTime.Now, pass: "teste", usrCad: Guid.NewGuid(), usuDescricaoCad: string.Empty));
        var okResult = response as BadRequestResult;

        //Assert
        _acessoManager.Verify(x => x.CriarPrestador(It.IsAny<PrestadorCadastroDto>()), Times.Once);
        _acessoManager.Verify(x => x.GetUserPorEmail(It.IsAny<string>()), Times.Once);
        _mapper.Verify(x => x.Map<UserModel>(It.IsAny<PrestadorLoginDto>()), Times.Never);
        _acessoManager.Verify(x => x.CriarPrestador(It.IsAny<PrestadorCadastroDto>()), Times.Once);
        Assert.NotNull(okResult);
        Asset.Equals(okResult.StatusCode.ToString(), "400");

    }


    [Fact]
    public async Task Deve_Retornar_Prestador_ComTodosCamposPrenchidos()
    {
        //Arranger
        var xpto = CriarUserModelFake();
        _acessoManager.Setup(x => x.GetUserPorEmail(It.IsAny<string>())).ReturnsAsync(xpto);
        //Act
        AutenticacaoController controllerCategoria = new AutenticacaoController(_acessoManager.Object, _mapper.Object);
        var response = await controllerCategoria.GetPrestadorUser(email: "teste.com.br", id: Guid.NewGuid());
        var okResult = response as OkObjectResult;
        var result = okResult.Value as UserModel;
        //Assert
        _acessoManager.Verify(x => x.GetUserPorEmail(It.IsAny<string>()), Times.Once);
        _mapper.Verify(x => x.Map<UserModel>(It.IsAny<PrestadorLoginDto>()), Times.Never);
        Assert.NotNull(result);
        Asset.Equals(result.Email, xpto.Email);
        Asset.Equals(result.UserName, xpto.UserName);
        Asset.Equals(result.Id, xpto.Id);

    }

    [Fact]
    public async Task NaoDeve_Retornar_Prestador_QuandoNaoAcharUsuario()
    {
        //Arranger
        UserModel xpto = null;
        _acessoManager.Setup(x => x.GetUserPorEmail(It.IsAny<string>())).ReturnsAsync(xpto);
        //Act
        AutenticacaoController controllerCategoria = new AutenticacaoController(_acessoManager.Object, _mapper.Object);
        var response = await controllerCategoria.GetPrestadorUser(email: "teste.com.br", id: Guid.NewGuid());
        var okResult = response as BadRequestObjectResult;
        var result = okResult.Value as string;
        //Assert
        _acessoManager.Verify(x => x.GetUserPorEmail(It.IsAny<string>()), Times.Once);
        _mapper.Verify(x => x.Map<UserModel>(It.IsAny<PrestadorLoginDto>()), Times.Never);
        Assert.NotNull(result);
        Asset.Equals(result, "User not found!");

    }

    private static UserModel CriarUserModelFake()
    {
        return new UserModel() { Email = "teste@teste", UserName = "teste", Id = Guid.NewGuid().ToString() };
    }

    private static PrestadorCadastroDto CriarPrestadorCadastroFake(Guid? id, string email, string userName, DateTime date, string pass, Guid? usrCad, string usuDescricaoCad)
    {
        return new PrestadorCadastroDto() { Id = id.HasValue ? id.Value : Guid.Empty, Email = email, UserName = userName, DataCadastro = date, Password = pass, UsrCadastro = usrCad.HasValue? id.Value : Guid.Empty, UsrDescricaoCadastro = usuDescricaoCad };
    }

    private static PrestadorLoginDto GeradorPrestadorLoginDtoFake(string email, string pass, string user)
    {
        return new PrestadorLoginDto() { Email = email, Password = pass, UserName = user };
    }

    private static Token GeradorTokenFake(bool autenticado)
    {
        return new Token() { AccessToken = "AcessTokenTeste", Authenticated = autenticado, Created = DateTime.Now.ToString(), Expiration = DateTime.Now.ToString(), Message = "Token Validado" };
    }
}
