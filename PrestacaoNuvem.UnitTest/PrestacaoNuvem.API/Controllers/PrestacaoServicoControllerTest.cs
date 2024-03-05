using PrestacaoNuvem.Api.Domain.Interfaces;
using System.Net;

namespace PrestacaoNuvem.UnitTest.PrestacaoNuvem.API.Controllers;

#pragma warning disable 8604, 8602, 8629, 8600, 8620
public class PrestacaoServicoControllerTest
{
    readonly Mock<IPrestacaoServicoService> _serviceMock = new();
    private static DefaultHttpContext CreateFakeClaims(ICollection<PrestacaoServicoDto> prestacaoServico)
    {
        var fakeHttpContext = new DefaultHttpContext();
        ClaimsIdentity identity = new(
            new[] {
                        new Claim("PrestadorId", prestacaoServico.First().PrestadorId.ToString() ?? string.Empty),
                        new Claim("UserName", "Teste"),
                        new Claim("IdUserLogin", prestacaoServico.First().PrestadorId.ToString() ?? string.Empty)

            }
        );
        fakeHttpContext.User = new System.Security.Claims.ClaimsPrincipal(identity);
        return fakeHttpContext;
    }

    private PrestacaoServicoController CreateFakeController(ICollection<PrestacaoServicoDto> prestacaoServico)
    {
        return new PrestacaoServicoController(_serviceMock.Object) { ControllerContext = new ControllerContext() { HttpContext = CreateFakeClaims(prestacaoServico) } };
    }

    private ICollection<PrestacaoServicoDto> RetornaListaPrestacaoServico()
    {
        return
            new List<PrestacaoServicoDto>()
            {
                new PrestacaoServicoDto()
                {
                    Cliente = new ClienteDto()
                    {
                        CPF = "123456789",
                        Email = "teste@test.com.br",
                        Nome = "Teste",
                        Telefone = "11999999999",
                        Id = Guid.NewGuid(),
                        PrestadorId = Guid.NewGuid()
                    },
                    ClienteId = Guid.NewGuid(),
                    DataCadastro = DateTime.Now,
                    DataConclusaoServico = DateTime.Now,
                    FuncionarioPrestador = new FuncionarioPrestadorDto()
                    {
                        Cargo = "teste",
                        CPF = "123456789",
                        Email = "teste@teste.com.br",
                        Nome = "teste func",
                        RG = "52453",
                        Telefone = "1234556",
                        DataCadastro = DateTime.Now,
                        Endereco = "rua teste",
                        Id = Guid.NewGuid(),
                        UsrCadastro = Guid.NewGuid(),
                        PrestadorId = Guid.NewGuid()
                    },
                    FuncionarioPrestadorId = Guid.NewGuid(),
                    Id = Guid.NewGuid(),
                    Prestador = new PrestadorDto()
                    {
                        CPF = "123456789",
                        EmailEmpresa = "teste@test.com.br",
                        Nome = "Teste",
                        Telefone = "11999999999",
                        Id = Guid.NewGuid()
                    },
                    PrestadorId = Guid.NewGuid(),
                    Produtos = new List<ProdutoDto>()
                    {
                        new ProdutoDto()
                        {
                            Marca = "Unit",
                            Nome = "Teste",
                            Modelo = "Unitario",
                            Valor_Compra = 20,
                            Valor_Venda = 30,
                            PrestadorId = Guid.NewGuid()
                        }
                    },
                    Referencia = "Teste",
                    Servicos = new List<ServicoDto>()
                    {
                        new ServicoDto()
                        {
                            DataCadastro = DateTime.Now,
                            Descricao = "Teste",
                            Id = Guid.NewGuid(),
                            PrestadorId= Guid.NewGuid(),
                            SubCategoriaServico = new SubCategoriaServicoDto()
                            {
                                Titulo = "Teste",
                                Desc = "Teste Dec",
                                Id = Guid.NewGuid(),
                                CategoriaId = Guid.NewGuid(),
                                PrestadorId = Guid.NewGuid()
                            },
                            SubServicoId= Guid.NewGuid(),
                            UsrCadastro = Guid.NewGuid(),
                            Valor = 1000
                        }
                    },
                    Status = EPrestacaoServicoStatus.Aberto,
                    UsrCadastro = Guid.NewGuid(),
                    Veiculo = new VeiculoDto()
                    {
                        Marca = "teste",
                        Modelo = "teste",
                        Placa = "Teste",
                        Id = Guid.NewGuid(),
                        UsrCadastro = Guid.NewGuid(),
                        PrestadorId = Guid.NewGuid()
                    },
                    VeiculoId = Guid.NewGuid()
                }
            };
    }

    private PrestacaoServicoDto RetornaPrestacaoServico()
    {
        return new PrestacaoServicoDto()
        {
            Cliente = new ClienteDto()
            {
                CPF = "123456789",
                Email = "teste@test.com.br",
                Nome = "Teste",
                Telefone = "11999999999",
                Id = Guid.NewGuid(),
                PrestadorId = Guid.NewGuid()
            },
            ClienteId = Guid.NewGuid(),
            DataCadastro = DateTime.Now,
            DataConclusaoServico = DateTime.Now,
            FuncionarioPrestador = new FuncionarioPrestadorDto()
            {
                Cargo = "teste",
                CPF = "123456789",
                Email = "teste@teste.com.br",
                Nome = "teste func",
                RG = "52453",
                Telefone = "1234556",
                DataCadastro = DateTime.Now,
                Endereco = "rua teste",
                Id = Guid.NewGuid(),
                UsrCadastro = Guid.NewGuid(),
                PrestadorId = Guid.NewGuid()
            },
            FuncionarioPrestadorId = Guid.NewGuid(),
            Id = Guid.NewGuid(),
            Prestador = new PrestadorDto()
            {
                CPF = "123456789",
                EmailEmpresa = "teste@test.com.br",
                Nome = "Teste",
                Telefone = "11999999999",
                Id = Guid.NewGuid()
            },
            PrestadorId = Guid.NewGuid(),
            Produtos = new List<ProdutoDto>()
                    {
                        new ProdutoDto()
                        {
                            Marca = "Unit",
                            Nome = "Teste",
                            Modelo = "Unitario",
                            Valor_Compra = 20,
                            Valor_Venda = 30,
                            PrestadorId = Guid.NewGuid(),
                            Qtd = 1
                        }
                    },
            Referencia = "Teste",
            Servicos = new List<ServicoDto>()
                    {
                        new ServicoDto()
                        {
                            DataCadastro = DateTime.Now,
                            Descricao = "Teste",
                            Id = Guid.NewGuid(),
                            PrestadorId= Guid.NewGuid(),
                            SubCategoriaServico = new SubCategoriaServicoDto()
                            {
                                Titulo = "Teste",
                                Desc = "Teste Dec",
                                Id = Guid.NewGuid(),
                                CategoriaId = Guid.NewGuid(),
                                PrestadorId = Guid.NewGuid()
                            },
                            SubServicoId= Guid.NewGuid(),
                            UsrCadastro = Guid.NewGuid(),
                            Valor = 1000
                        }
                    },
            Status = EPrestacaoServicoStatus.Aberto,
            UsrCadastro = Guid.NewGuid(),
            Veiculo = new VeiculoDto()
            {
                Marca = "teste",
                Modelo = "teste",
                Placa = "Teste",
                Id = Guid.NewGuid(),
                UsrCadastro = Guid.NewGuid(),
                PrestadorId = Guid.NewGuid()
            },
            VeiculoId = Guid.NewGuid()
        };
    }

    [Fact]
    public async Task Deve_Retornar_ListaDePrestacaoServico_RetornoOk()
    {
        //Arrange
        ICollection<PrestacaoServicoDto> prestacaoListaFake = RetornaListaPrestacaoServico();
        PrestacaoServicoDto prestacaoFake = RetornaPrestacaoServico();
        _serviceMock.Setup(s => s.GetAllPrestacaoServico(It.IsAny<PrestacaoServicoDto>())).ReturnsAsync(prestacaoListaFake);
        PrestacaoServicoController controller = CreateFakeController(prestacaoListaFake);
        //Act
        var response = await controller.GetAll();
        var okResult = (OkObjectResult)response;
        var result = okResult.Value as ICollection<PrestacaoServicoDto>;
        //Assert
        _serviceMock.Verify(s => s.GetAllPrestacaoServico(It.IsAny<PrestacaoServicoDto>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(result.First().Referencia, prestacaoListaFake.First().Referencia);
        Assert.Equal(result.First().FuncionarioPrestador, prestacaoListaFake.First().FuncionarioPrestador);
        Assert.Equal(result.First().DataCadastro, prestacaoListaFake.First().DataCadastro);
        Assert.Equal(result.First().Id, prestacaoListaFake.First().Id);
        Assert.Equal(result.First().Prestador, prestacaoListaFake.First().Prestador);
        Assert.Equal(result.First().FuncionarioPrestadorId, prestacaoListaFake.First().FuncionarioPrestadorId);
        Assert.Equal(result.First().Cliente, prestacaoListaFake.First().Cliente);
        Assert.Equal(result.First().Cliente.Telefone, prestacaoListaFake.First().Cliente.Telefone);
        Assert.Equal(prestacaoListaFake?.First()?.Cliente?.CPF, result.First()?.Cliente?.CPF);
        Assert.Equal(result.First().Cliente.DataCadastro, prestacaoListaFake.First().Cliente.DataCadastro);
        Assert.Equal(result.First().Cliente.Id, prestacaoListaFake.First().Cliente.Id);
        Assert.Equal(result.First().Cliente.Email, prestacaoListaFake.First().Cliente.Email);
        Assert.Equal(result.First().Cliente.Endereco, prestacaoListaFake.First().Cliente.Endereco);
        Assert.Equal(result.First().Cliente.Nome, prestacaoListaFake.First().Cliente.Nome);
        Assert.Equal(result.First().Cliente.Rg, prestacaoListaFake.First().Cliente.Rg);
        Assert.Equal(result.First().ClienteId, prestacaoListaFake.First().ClienteId);
        Assert.Equal(result.First().DataCadastro, prestacaoListaFake.First().DataCadastro);
        Assert.Equal(result.First().DataConclusaoServico, prestacaoListaFake.First().DataConclusaoServico);
        Assert.Equal(result.First().FuncionarioPrestador, prestacaoListaFake.First().FuncionarioPrestador);
        Assert.Equal(result.First().FuncionarioPrestador.Telefone, prestacaoListaFake.First().FuncionarioPrestador.Telefone);
        Assert.Equal(result.First().FuncionarioPrestador.CPF, prestacaoListaFake.First().FuncionarioPrestador.CPF);
        Assert.Equal(result.First().FuncionarioPrestador.DataCadastro, prestacaoListaFake.First().FuncionarioPrestador.DataCadastro);
        Assert.Equal(result.First().FuncionarioPrestador.Id, prestacaoListaFake.First().FuncionarioPrestador.Id);
        Assert.Equal(result.First().FuncionarioPrestador.Cargo, prestacaoListaFake.First().FuncionarioPrestador.Cargo);
        Assert.Equal(result.First().FuncionarioPrestador.Email, prestacaoListaFake.First().FuncionarioPrestador.Email);
        Assert.Equal(result.First().FuncionarioPrestador.Endereco, prestacaoListaFake.First().FuncionarioPrestador.Endereco);
        Assert.Equal(result.First().FuncionarioPrestador.Nome, prestacaoListaFake.First().FuncionarioPrestador.Nome);
        Assert.Equal(result.First().FuncionarioPrestador.PrestadorId, prestacaoListaFake.First().FuncionarioPrestador.PrestadorId);
        Assert.Equal(result.First().FuncionarioPrestadorId, prestacaoListaFake.First().FuncionarioPrestadorId);
        Assert.Equal(result.First().PrestadorId, prestacaoListaFake.First().PrestadorId);
        Assert.Equal(result.First().Produtos, prestacaoListaFake.First().Produtos);
        Assert.Equal(result.First().Produtos.First().Garantia, prestacaoListaFake.First().Produtos.First().Garantia);
        Assert.Equal(result.First().Produtos.First().DataCadastro, prestacaoListaFake.First().Produtos.First().DataCadastro);
        Assert.Equal(result.First().Produtos.First().DataDesativacao, prestacaoListaFake.First().Produtos.First().DataDesativacao);
        Assert.Equal(result.First().Produtos.First().Data_validade, prestacaoListaFake.First().Produtos.First().Data_validade);
        Assert.Equal(result.First().Produtos.First().Id, prestacaoListaFake.First().Produtos.First().Id);
        Assert.Equal(result.First().Produtos.First().Marca, prestacaoListaFake.First().Produtos.First().Marca);
        Assert.Equal(result.First().Produtos.First().Modelo, prestacaoListaFake.First().Produtos.First().Modelo);
        Assert.Equal(result.First().Produtos.First().Nome, prestacaoListaFake.First().Produtos.First().Nome);
        Assert.Equal(result.First().Produtos.First().Qtd, prestacaoListaFake.First().Produtos.First().Qtd);
        Assert.Equal(result.First().Produtos.First().TipoMedidaItem, prestacaoListaFake.First().Produtos.First().TipoMedidaItem);
        Assert.Equal(result.First().Produtos.First().Valor_Compra, prestacaoListaFake.First().Produtos.First().Valor_Compra);
        Assert.Equal(result.First().Produtos.First().Valor_Venda, prestacaoListaFake.First().Produtos.First().Valor_Venda);
        Assert.Equal(result.First().Referencia, prestacaoListaFake.First().Referencia);
        Assert.Equal(result.First().Servicos, prestacaoListaFake.First().Servicos);
        Assert.Equal(result.First().Servicos.First().Descricao, prestacaoListaFake.First().Servicos.First().Descricao);
        Assert.Equal(result.First().Servicos.First().Valor, prestacaoListaFake.First().Servicos.First().Valor);
        Assert.Equal(result.First().Status, prestacaoListaFake.First().Status);
        Assert.Equal(result.First().Veiculo, prestacaoListaFake.First().Veiculo);
        Assert.Equal(result.First().Veiculo.Placa, prestacaoListaFake.First().Veiculo.Placa);
        Assert.Equal(result.First().Veiculo.Marca, prestacaoListaFake.First().Veiculo.Marca);
        Assert.Equal(result.First().Veiculo.Ano, prestacaoListaFake.First().Veiculo.Ano);
    }

    [Fact]
    public async Task Deve_Retornar_PrestacaoServico_RetornoOk()
    {
        //Arrange
        ICollection<PrestacaoServicoDto> prestacaoListaFake = RetornaListaPrestacaoServico();
        PrestacaoServicoDto prestacaoFake = RetornaPrestacaoServico();
        _serviceMock.Setup(s => s.FindByIdPrestacaoServico(It.IsAny<Guid>())).ReturnsAsync(prestacaoFake);
        PrestacaoServicoController controller = CreateFakeController(prestacaoListaFake);
        //Act
        var response = await controller.GetId(prestacaoFake.Id.Value);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as PrestacaoServicoDto;
        //Assert
        _serviceMock.Verify(s => s.FindByIdPrestacaoServico(It.IsAny<Guid>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(result.Referencia, prestacaoFake.Referencia);
        Assert.Equal(result.FuncionarioPrestador, prestacaoFake.FuncionarioPrestador);
        Assert.Equal(result.DataCadastro, prestacaoFake.DataCadastro);
        Assert.Equal(result.Id, prestacaoFake.Id);
        Assert.Equal(result.Prestador, prestacaoFake.Prestador);
        Assert.Equal(result.FuncionarioPrestadorId, prestacaoFake.FuncionarioPrestadorId);
        Assert.Equal(result.Cliente, prestacaoFake.Cliente);
        Assert.Equal(result.Cliente.Telefone, prestacaoFake.Cliente.Telefone);
        Assert.Equal(result.Cliente.CPF, prestacaoFake.Cliente.CPF);
        Assert.Equal(result.Cliente.DataCadastro, prestacaoFake.Cliente.DataCadastro);
        Assert.Equal(result.Cliente.Id, prestacaoFake.Cliente.Id);
        Assert.Equal(result.Cliente.Email, prestacaoFake.Cliente.Email);
        Assert.Equal(result.Cliente.Endereco, prestacaoFake.Cliente.Endereco);
        Assert.Equal(result.Cliente.Nome, prestacaoFake.Cliente.Nome);
        Assert.Equal(result.Cliente.Rg, prestacaoFake.Cliente.Rg);
        Assert.Equal(result.ClienteId, prestacaoFake.ClienteId);
        Assert.Equal(result.DataCadastro, prestacaoFake.DataCadastro);
        Assert.Equal(result.DataConclusaoServico, prestacaoFake.DataConclusaoServico);
        Assert.Equal(result.FuncionarioPrestador, prestacaoFake.FuncionarioPrestador);
        Assert.Equal(result.FuncionarioPrestador.Telefone, prestacaoFake.FuncionarioPrestador.Telefone);
        Assert.Equal(result.FuncionarioPrestador.CPF, prestacaoFake.FuncionarioPrestador.CPF);
        Assert.Equal(result.FuncionarioPrestador.DataCadastro, prestacaoFake.FuncionarioPrestador.DataCadastro);
        Assert.Equal(result.FuncionarioPrestador.Id, prestacaoFake.FuncionarioPrestador.Id);
        Assert.Equal(result.FuncionarioPrestador.Cargo, prestacaoFake.FuncionarioPrestador.Cargo);
        Assert.Equal(result.FuncionarioPrestador.Email, prestacaoFake.FuncionarioPrestador.Email);
        Assert.Equal(result.FuncionarioPrestador.Endereco, prestacaoFake.FuncionarioPrestador.Endereco);
        Assert.Equal(result.FuncionarioPrestador.Nome, prestacaoFake.FuncionarioPrestador.Nome);
        Assert.Equal(result.FuncionarioPrestador.PrestadorId, prestacaoFake.FuncionarioPrestador.PrestadorId);
        Assert.Equal(result.FuncionarioPrestadorId, prestacaoFake.FuncionarioPrestadorId);
        Assert.Equal(result.PrestadorId, prestacaoFake.PrestadorId);
        Assert.Equal(result.Produtos, prestacaoFake.Produtos);
        Assert.Equal(result.Produtos.First().Garantia, prestacaoFake.Produtos.First().Garantia);
        Assert.Equal(result.Produtos.First().DataCadastro, prestacaoFake.Produtos.First().DataCadastro);
        Assert.Equal(result.Produtos.First().DataDesativacao, prestacaoFake.Produtos.First().DataDesativacao);
        Assert.Equal(result.Produtos.First().Data_validade, prestacaoFake.Produtos.First().Data_validade);
        Assert.Equal(result.Produtos.First().Id, prestacaoFake.Produtos.First().Id);
        Assert.Equal(result.Produtos.First().Marca, prestacaoFake.Produtos.First().Marca);
        Assert.Equal(result.Produtos.First().Modelo, prestacaoFake.Produtos.First().Modelo);
        Assert.Equal(result.Produtos.First().Nome, prestacaoFake.Produtos.First().Nome);
        Assert.Equal(result.Produtos.First().Qtd, prestacaoFake.Produtos.First().Qtd);
        Assert.Equal(result.Produtos.First().TipoMedidaItem, prestacaoFake.Produtos.First().TipoMedidaItem);
        Assert.Equal(result.Produtos.First().Valor_Compra, prestacaoFake.Produtos.First().Valor_Compra);
        Assert.Equal(result.Produtos.First().Valor_Venda, prestacaoFake.Produtos.First().Valor_Venda);
        Assert.Equal(result.Referencia, prestacaoFake.Referencia);
        Assert.Equal(result.Servicos, prestacaoFake.Servicos);
        Assert.Equal(result.Servicos.First().Descricao, prestacaoFake.Servicos.First().Descricao);
        Assert.Equal(result.Servicos.First().Valor, prestacaoFake.Servicos.First().Valor);
        Assert.Equal(result.Status, prestacaoFake.Status);
        Assert.Equal(result.Veiculo, prestacaoFake.Veiculo);
        Assert.Equal(result.Veiculo.Placa, prestacaoFake.Veiculo.Placa);
        Assert.Equal(result.Veiculo.Marca, prestacaoFake.Veiculo.Marca);
        Assert.Equal(result.Veiculo.Ano, prestacaoFake.Veiculo.Ano);
    }

    [Fact]
    public async Task NaoDeve_Retornar_PrestacaoServico_RetornoNoContent()
    {
        //Arrange
        ICollection<PrestacaoServicoDto> prestacaoListaFake = RetornaListaPrestacaoServico();
        PrestacaoServicoDto prestacaoFake = RetornaPrestacaoServico();
        PrestacaoServicoDto prestacaoFakeNull = null;
        _serviceMock.Setup(s => s.FindByIdPrestacaoServico(It.IsAny<Guid>())).ReturnsAsync(prestacaoFakeNull);
        PrestacaoServicoController controller = CreateFakeController(prestacaoListaFake);
        //Act
        var response = await controller.GetId(prestacaoFake.Id.Value);
        var okResult = response as NoContentResult;

        //Assert
        _serviceMock.Verify(s => s.FindByIdPrestacaoServico(It.IsAny<Guid>()), Times.Once());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);

    }

    [Fact]
    public async Task Deve_Retornar_PrestacaoServico_RetornoBadRequest()
    {
        //Arrange
        ICollection<PrestacaoServicoDto> prestacaoListaFake = RetornaListaPrestacaoServico();
        PrestacaoServicoDto prestacaoFake = RetornaPrestacaoServico();
        _serviceMock.Setup(s => s.FindByIdPrestacaoServico(It.IsAny<Guid>())).ReturnsAsync(prestacaoFake);
        PrestacaoServicoController controller = CreateFakeController(prestacaoListaFake);
        controller.ModelState.AddModelError("key", "error message");
        //Act
        var response = await controller.GetId(prestacaoFake.Id.Value);
        var okResult = response as ObjectResult;

        //Assert
        _serviceMock.Verify(s => s.GetAllPrestacaoServico(It.IsAny<PrestacaoServicoDto>()), Times.Never());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.BadRequest, okResult.StatusCode);

    }

    [Fact]
    public async Task Deve_Cadastrar_PrestacaoServico_RetornoOk()
    {
        //Arrange
        ICollection<PrestacaoServicoDto> prestacaoListaFake = RetornaListaPrestacaoServico();
        PrestacaoServicoDto prestacaoFake = RetornaPrestacaoServico();
        _serviceMock.Setup(s => s.CreatePrestacaoServico(It.IsAny<PrestacaoServicoDto>())).ReturnsAsync(prestacaoFake);
        PrestacaoServicoController controller = CreateFakeController(prestacaoListaFake);
        //Act
        var response = await controller.Add(prestacaoFake);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as PrestacaoServicoDto;
        //Assert
        _serviceMock.Verify(s => s.CreatePrestacaoServico(It.IsAny<PrestacaoServicoDto>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(result.Referencia, prestacaoFake.Referencia);
        Assert.Equal(result.FuncionarioPrestador, prestacaoFake.FuncionarioPrestador);
        Assert.Equal(result.DataCadastro, prestacaoFake.DataCadastro);
        Assert.Equal(result.Id, prestacaoFake.Id);
        Assert.Equal(result.Prestador, prestacaoFake.Prestador);
        Assert.Equal(result.FuncionarioPrestadorId, prestacaoFake.FuncionarioPrestadorId);
        Assert.Equal(result.Cliente, prestacaoFake.Cliente);
        Assert.Equal(result.Cliente.Telefone, prestacaoFake.Cliente.Telefone);
        Assert.Equal(result.Cliente.CPF, prestacaoFake.Cliente.CPF);
        Assert.Equal(result.Cliente.DataCadastro, prestacaoFake.Cliente.DataCadastro);
        Assert.Equal(result.Cliente.Id, prestacaoFake.Cliente.Id);
        Assert.Equal(result.Cliente.Email, prestacaoFake.Cliente.Email);
        Assert.Equal(result.Cliente.Endereco, prestacaoFake.Cliente.Endereco);
        Assert.Equal(result.Cliente.Nome, prestacaoFake.Cliente.Nome);
        Assert.Equal(result.Cliente.Rg, prestacaoFake.Cliente.Rg);
        Assert.Equal(result.ClienteId, prestacaoFake.ClienteId);
        Assert.Equal(result.DataCadastro, prestacaoFake.DataCadastro);
        Assert.Equal(result.DataConclusaoServico, prestacaoFake.DataConclusaoServico);
        Assert.Equal(result.FuncionarioPrestador, prestacaoFake.FuncionarioPrestador);
        Assert.Equal(result.FuncionarioPrestador.Telefone, prestacaoFake.FuncionarioPrestador.Telefone);
        Assert.Equal(result.FuncionarioPrestador.CPF, prestacaoFake.FuncionarioPrestador.CPF);
        Assert.Equal(result.FuncionarioPrestador.DataCadastro, prestacaoFake.FuncionarioPrestador.DataCadastro);
        Assert.Equal(result.FuncionarioPrestador.Id, prestacaoFake.FuncionarioPrestador.Id);
        Assert.Equal(result.FuncionarioPrestador.Cargo, prestacaoFake.FuncionarioPrestador.Cargo);
        Assert.Equal(result.FuncionarioPrestador.Email, prestacaoFake.FuncionarioPrestador.Email);
        Assert.Equal(result.FuncionarioPrestador.Endereco, prestacaoFake.FuncionarioPrestador.Endereco);
        Assert.Equal(result.FuncionarioPrestador.Nome, prestacaoFake.FuncionarioPrestador.Nome);
        Assert.Equal(result.FuncionarioPrestador.PrestadorId, prestacaoFake.FuncionarioPrestador.PrestadorId);
        Assert.Equal(result.FuncionarioPrestadorId, prestacaoFake.FuncionarioPrestadorId);
        Assert.Equal(result.PrestadorId, prestacaoFake.PrestadorId);
        Assert.Equal(result.Produtos, prestacaoFake.Produtos);
        Assert.Equal(result.Produtos.First().Garantia, prestacaoFake.Produtos.First().Garantia);
        Assert.Equal(result.Produtos.First().DataCadastro, prestacaoFake.Produtos.First().DataCadastro);
        Assert.Equal(result.Produtos.First().DataDesativacao, prestacaoFake.Produtos.First().DataDesativacao);
        Assert.Equal(result.Produtos.First().Data_validade, prestacaoFake.Produtos.First().Data_validade);
        Assert.Equal(result.Produtos.First().Id, prestacaoFake.Produtos.First().Id);
        Assert.Equal(result.Produtos.First().Marca, prestacaoFake.Produtos.First().Marca);
        Assert.Equal(result.Produtos.First().Modelo, prestacaoFake.Produtos.First().Modelo);
        Assert.Equal(result.Produtos.First().Nome, prestacaoFake.Produtos.First().Nome);
        Assert.Equal(result.Produtos.First().Qtd, prestacaoFake.Produtos.First().Qtd);
        Assert.Equal(result.Produtos.First().TipoMedidaItem, prestacaoFake.Produtos.First().TipoMedidaItem);
        Assert.Equal(result.Produtos.First().Valor_Compra, prestacaoFake.Produtos.First().Valor_Compra);
        Assert.Equal(result.Produtos.First().Valor_Venda, prestacaoFake.Produtos.First().Valor_Venda);
        Assert.Equal(result.Referencia, prestacaoFake.Referencia);
        Assert.Equal(result.Servicos, prestacaoFake.Servicos);
        Assert.Equal(result.Servicos.First().Descricao, prestacaoFake.Servicos.First().Descricao);
        Assert.Equal(result.Servicos.First().Valor, prestacaoFake.Servicos.First().Valor);
        Assert.Equal(result.Status, prestacaoFake.Status);
        Assert.Equal(result.Veiculo, prestacaoFake.Veiculo);
        Assert.Equal(result.Veiculo.Placa, prestacaoFake.Veiculo.Placa);
        Assert.Equal(result.Veiculo.Marca, prestacaoFake.Veiculo.Marca);
        Assert.Equal(result.Veiculo.Ano, prestacaoFake.Veiculo.Ano);
    }

    [Fact]
    public async Task NaoDeve_Cadastrar_PrestacaoServico_RetornoNoContent()
    {
        //Arrange
        ICollection<PrestacaoServicoDto> prestacaoListaFake = RetornaListaPrestacaoServico();
        PrestacaoServicoDto prestacaoFake = RetornaPrestacaoServico();
        PrestacaoServicoDto prestacaoFakeNull = null;
        _serviceMock.Setup(s => s.CreatePrestacaoServico(It.IsAny<PrestacaoServicoDto>())).ReturnsAsync(prestacaoFakeNull);
        PrestacaoServicoController controller = CreateFakeController(prestacaoListaFake);
        //Act
        var response = await controller.Add(prestacaoFake);
        var okResult = response as NoContentResult;
        //Assert
        _serviceMock.Verify(s => s.CreatePrestacaoServico(It.IsAny<PrestacaoServicoDto>()), Times.Once());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);
      
    }

    [Fact]
    public async Task NaoDeve_Cadastrar_PrestacaoServico_RetornoBadRequest()
    {
        //Arrange
        ICollection<PrestacaoServicoDto> prestacaoListaFake = RetornaListaPrestacaoServico();
        PrestacaoServicoDto prestacaoFake = RetornaPrestacaoServico();
        _serviceMock.Setup(s => s.CreatePrestacaoServico(It.IsAny<PrestacaoServicoDto>())).ReturnsAsync(prestacaoFake);
        PrestacaoServicoController controller = CreateFakeController(prestacaoListaFake);
        controller.ModelState.AddModelError("key", "error message");
        //Act
        var response = await controller.Add(prestacaoFake);
        var okResult = response as ObjectResult;
        //Assert
        _serviceMock.Verify(s => s.CreatePrestacaoServico(It.IsAny<PrestacaoServicoDto>()), Times.Never());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.BadRequest, okResult.StatusCode);

    }

    [Fact]
    public async Task Deve_Retornar_PrestacaoServicoFechadasEConcluidas_RetornoOk()
    {
        //Arrange
        ICollection<PrestacaoServicoDto> prestacaoListaFake = RetornaListaPrestacaoServico();
        _serviceMock.Setup(s => s.GetByPrestacoesServicosStatus(It.IsAny<Guid>(), It.IsAny<ICollection<EPrestacaoServicoStatus>>())).ReturnsAsync(prestacaoListaFake);
        PrestacaoServicoController controller = CreateFakeController(prestacaoListaFake);
        //Act
        var response = await controller.GetByPrestacaoServicoFechadosPrestador();
        var okResult = response as OkObjectResult;
        var result = okResult.Value as ICollection<PrestacaoServicoDto>;
        //Assert
        _serviceMock.Verify(s => s.GetByPrestacoesServicosStatus(It.IsAny<Guid>(), It.IsAny<ICollection<EPrestacaoServicoStatus>>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(result.First().Referencia, prestacaoListaFake.First().Referencia);
        Assert.Equal(result.First().FuncionarioPrestador, prestacaoListaFake.First().FuncionarioPrestador);
        Assert.Equal(result.First().DataCadastro, prestacaoListaFake.First().DataCadastro);
        Assert.Equal(result.First().Id, prestacaoListaFake.First().Id);
        Assert.Equal(result.First().Prestador, prestacaoListaFake.First().Prestador);
        Assert.Equal(result.First().FuncionarioPrestadorId, prestacaoListaFake.First().FuncionarioPrestadorId);
        Assert.Equal(result.First().Cliente, prestacaoListaFake.First().Cliente);
        Assert.Equal(result.First().Cliente.Telefone, prestacaoListaFake.First().Cliente.Telefone);
        Assert.Equal(result.First().Cliente.CPF, prestacaoListaFake.First().Cliente.CPF);
        Assert.Equal(result.First().Cliente.DataCadastro, prestacaoListaFake.First().Cliente.DataCadastro);
        Assert.Equal(result.First().Cliente.Id, prestacaoListaFake.First().Cliente.Id);
        Assert.Equal(result.First().Cliente.Email, prestacaoListaFake.First().Cliente.Email);
        Assert.Equal(result.First().Cliente.Endereco, prestacaoListaFake.First().Cliente.Endereco);
        Assert.Equal(result.First().Cliente.Nome, prestacaoListaFake.First().Cliente.Nome);
        Assert.Equal(result.First().Cliente.Rg, prestacaoListaFake.First().Cliente.Rg);
        Assert.Equal(result.First().ClienteId, prestacaoListaFake.First().ClienteId);
        Assert.Equal(result.First().DataCadastro, prestacaoListaFake.First().DataCadastro);
        Assert.Equal(result.First().DataConclusaoServico, prestacaoListaFake.First().DataConclusaoServico);
        Assert.Equal(result.First().FuncionarioPrestador, prestacaoListaFake.First().FuncionarioPrestador);
        Assert.Equal(result.First().FuncionarioPrestador.Telefone, prestacaoListaFake.First().FuncionarioPrestador.Telefone);
        Assert.Equal(result.First().FuncionarioPrestador.CPF, prestacaoListaFake.First().FuncionarioPrestador.CPF);
        Assert.Equal(result.First().FuncionarioPrestador.DataCadastro, prestacaoListaFake.First().FuncionarioPrestador.DataCadastro);
        Assert.Equal(result.First().FuncionarioPrestador.Id, prestacaoListaFake.First().FuncionarioPrestador.Id);
        Assert.Equal(result.First().FuncionarioPrestador.Cargo, prestacaoListaFake.First().FuncionarioPrestador.Cargo);
        Assert.Equal(result.First().FuncionarioPrestador.Email, prestacaoListaFake.First().FuncionarioPrestador.Email);
        Assert.Equal(result.First().FuncionarioPrestador.Endereco, prestacaoListaFake.First().FuncionarioPrestador.Endereco);
        Assert.Equal(result.First().FuncionarioPrestador.Nome, prestacaoListaFake.First().FuncionarioPrestador.Nome);
        Assert.Equal(result.First().FuncionarioPrestador.PrestadorId, prestacaoListaFake.First().FuncionarioPrestador.PrestadorId);
        Assert.Equal(result.First().FuncionarioPrestadorId, prestacaoListaFake.First().FuncionarioPrestadorId);
        Assert.Equal(result.First().PrestadorId, prestacaoListaFake.First().PrestadorId);
        Assert.Equal(result.First().Produtos, prestacaoListaFake.First().Produtos);
        Assert.Equal(result.First().Produtos.First().Garantia, prestacaoListaFake.First().Produtos.First().Garantia);
        Assert.Equal(result.First().Produtos.First().DataCadastro, prestacaoListaFake.First().Produtos.First().DataCadastro);
        Assert.Equal(result.First().Produtos.First().DataDesativacao, prestacaoListaFake.First().Produtos.First().DataDesativacao);
        Assert.Equal(result.First().Produtos.First().Data_validade, prestacaoListaFake.First().Produtos.First().Data_validade);
        Assert.Equal(result.First().Produtos.First().Id, prestacaoListaFake.First().Produtos.First().Id);
        Assert.Equal(result.First().Produtos.First().Marca, prestacaoListaFake.First().Produtos.First().Marca);
        Assert.Equal(result.First().Produtos.First().Modelo, prestacaoListaFake.First().Produtos.First().Modelo);
        Assert.Equal(result.First().Produtos.First().Nome, prestacaoListaFake.First().Produtos.First().Nome);
        Assert.Equal(result.First().Produtos.First().Qtd, prestacaoListaFake.First().Produtos.First().Qtd);
        Assert.Equal(result.First().Produtos.First().TipoMedidaItem, prestacaoListaFake.First().Produtos.First().TipoMedidaItem);
        Assert.Equal(result.First().Produtos.First().Valor_Compra, prestacaoListaFake.First().Produtos.First().Valor_Compra);
        Assert.Equal(result.First().Produtos.First().Valor_Venda, prestacaoListaFake.First().Produtos.First().Valor_Venda);
        Assert.Equal(result.First().Referencia, prestacaoListaFake.First().Referencia);
        Assert.Equal(result.First().Servicos, prestacaoListaFake.First().Servicos);
        Assert.Equal(result.First().Servicos.First().Descricao, prestacaoListaFake.First().Servicos.First().Descricao);
        Assert.Equal(result.First().Servicos.First().Valor, prestacaoListaFake.First().Servicos.First().Valor);
        Assert.Equal(result.First().Status, prestacaoListaFake.First().Status);
        Assert.Equal(result.First().Veiculo, prestacaoListaFake.First().Veiculo);
        Assert.Equal(result.First().Veiculo.Placa, prestacaoListaFake.First().Veiculo.Placa);
        Assert.Equal(result.First().Veiculo.Marca, prestacaoListaFake.First().Veiculo.Marca);
        Assert.Equal(result.First().Veiculo.Ano, prestacaoListaFake.First().Veiculo.Ano);
    }

    [Fact]
    public async Task NaoDeve_Retornar_ListaPrestacaoServicoFechadoEConcluidos_RetornoNoContent()
    {
        //Arrange
        ICollection<PrestacaoServicoDto> prestacaoListaFake = RetornaListaPrestacaoServico();
        ICollection<PrestacaoServicoDto> prestacaoListaFakeNull = null;
        _serviceMock.Setup(s => s.GetByPrestacoesServicosStatus(It.IsAny<Guid>(), It.IsAny<ICollection<EPrestacaoServicoStatus>>())).ReturnsAsync(prestacaoListaFakeNull);
        PrestacaoServicoController controller = CreateFakeController(prestacaoListaFake);
        //Act
        var response = await controller.GetByPrestacaoServicoFechadosPrestador();
        var okResult = response as NoContentResult;

        //Assert
        _serviceMock.Verify(s => s.GetByPrestacoesServicosStatus(It.IsAny<Guid>(), It.IsAny<ICollection<EPrestacaoServicoStatus>>()), Times.Once());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);

    }

    [Fact]
    public async Task NaoDeve_Retornar_ListaPrestacaoServicoFechadoEConcluidos_BadRequestValidationFails()
    {
        //Arrange
        ICollection<PrestacaoServicoDto> prestacaoListaFake = RetornaListaPrestacaoServico();
        ICollection<PrestacaoServicoDto> prestacaoListaFakeNull = null;
        _serviceMock.Setup(s => s.GetByPrestacoesServicosStatus(It.IsAny<Guid>(), It.IsAny<ICollection<EPrestacaoServicoStatus>>())).ReturnsAsync(prestacaoListaFakeNull);
        PrestacaoServicoController controller = CreateFakeController(prestacaoListaFake);
        //Act
        controller.ModelState.AddModelError("key", "error message");
        var response = await controller.GetByPrestacaoServicoFechadosPrestador();
        var okResult = response as ObjectResult;

        //Assert
        _serviceMock.Verify(s => s.GetByPrestacoesServicosStatus(It.IsAny<Guid>(), It.IsAny<ICollection<EPrestacaoServicoStatus>>()), Times.Never());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.BadRequest, okResult.StatusCode);

    }


    [Fact]
    public async Task Deve_Retornar_ListaPrestacaoServicoFechadoEConcluidos_RetornoBadRequest()
    {
        //Arrange
        ICollection<PrestacaoServicoDto> prestacaoListaFake = RetornaListaPrestacaoServico();
        PrestacaoServicoDto prestacaoFake = RetornaPrestacaoServico();
        _serviceMock.Setup(s => s.GetByPrestacoesServicosStatus(It.IsAny<Guid>(), It.IsAny<ICollection<EPrestacaoServicoStatus>>())).ReturnsAsync(prestacaoListaFake);
        PrestacaoServicoController controller = CreateFakeController(prestacaoListaFake);
        controller.ModelState.AddModelError("key", "error message");
        //Act
        var response = await controller.GetId(prestacaoFake.Id.Value);
        var okResult = response as ObjectResult;

        //Assert
        _serviceMock.Verify(s => s.GetAllPrestacaoServico(It.IsAny<PrestacaoServicoDto>()), Times.Never());
        Assert.NotNull(okResult);
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.BadRequest);

    }

    [Fact]
    public async Task Deve_Retornar_PrestacaoServicoAbertas_RetornoOk()
    {
        //Arrange
        ICollection<PrestacaoServicoDto> prestacaoListaFake = RetornaListaPrestacaoServico();
        _serviceMock.Setup(s => s.GetByPrestacoesServicosStatus(It.IsAny<Guid>(), It.IsAny<ICollection<EPrestacaoServicoStatus>>())).ReturnsAsync(prestacaoListaFake);
        PrestacaoServicoController controller = CreateFakeController(prestacaoListaFake);
        //Act
        var response = await controller.GetByPrestacaoServicoAbertosPrestador();
        var okResult = response as OkObjectResult;
        var result = okResult.Value as ICollection<PrestacaoServicoDto>;
        //Assert
        _serviceMock.Verify(s => s.GetByPrestacoesServicosStatus(It.IsAny<Guid>(), It.IsAny<ICollection<EPrestacaoServicoStatus>>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(result.First().Referencia, prestacaoListaFake.First().Referencia);
        Assert.Equal(result.First().FuncionarioPrestador, prestacaoListaFake.First().FuncionarioPrestador);
        Assert.Equal(result.First().DataCadastro, prestacaoListaFake.First().DataCadastro);
        Assert.Equal(result.First().Id, prestacaoListaFake.First().Id);
        Assert.Equal(result.First().Prestador, prestacaoListaFake.First().Prestador);
        Assert.Equal(result.First().FuncionarioPrestadorId, prestacaoListaFake.First().FuncionarioPrestadorId);
        Assert.Equal(result.First().Cliente, prestacaoListaFake.First().Cliente);
        Assert.Equal(result.First().Cliente.Telefone, prestacaoListaFake.First().Cliente.Telefone);
        Assert.Equal(result.First().Cliente.CPF, prestacaoListaFake.First().Cliente.CPF);
        Assert.Equal(result.First().Cliente.DataCadastro, prestacaoListaFake.First().Cliente.DataCadastro);
        Assert.Equal(result.First().Cliente.Id, prestacaoListaFake.First().Cliente.Id);
        Assert.Equal(result.First().Cliente.Email, prestacaoListaFake.First().Cliente.Email);
        Assert.Equal(result.First().Cliente.Endereco, prestacaoListaFake.First().Cliente.Endereco);
        Assert.Equal(result.First().Cliente.Nome, prestacaoListaFake.First().Cliente.Nome);
        Assert.Equal(result.First().Cliente.Rg, prestacaoListaFake.First().Cliente.Rg);
        Assert.Equal(result.First().ClienteId, prestacaoListaFake.First().ClienteId);
        Assert.Equal(result.First().DataCadastro, prestacaoListaFake.First().DataCadastro);
        Assert.Equal(result.First().DataConclusaoServico, prestacaoListaFake.First().DataConclusaoServico);
        Assert.Equal(result.First().FuncionarioPrestador, prestacaoListaFake.First().FuncionarioPrestador);
        Assert.Equal(result.First().FuncionarioPrestador.Telefone, prestacaoListaFake.First().FuncionarioPrestador.Telefone);
        Assert.Equal(result.First().FuncionarioPrestador.CPF, prestacaoListaFake.First().FuncionarioPrestador.CPF);
        Assert.Equal(result.First().FuncionarioPrestador.DataCadastro, prestacaoListaFake.First().FuncionarioPrestador.DataCadastro);
        Assert.Equal(result.First().FuncionarioPrestador.Id, prestacaoListaFake.First().FuncionarioPrestador.Id);
        Assert.Equal(result.First().FuncionarioPrestador.Cargo, prestacaoListaFake.First().FuncionarioPrestador.Cargo);
        Assert.Equal(result.First().FuncionarioPrestador.Email, prestacaoListaFake.First().FuncionarioPrestador.Email);
        Assert.Equal(result.First().FuncionarioPrestador.Endereco, prestacaoListaFake.First().FuncionarioPrestador.Endereco);
        Assert.Equal(result.First().FuncionarioPrestador.Nome, prestacaoListaFake.First().FuncionarioPrestador.Nome);
        Assert.Equal(result.First().FuncionarioPrestador.PrestadorId, prestacaoListaFake.First().FuncionarioPrestador.PrestadorId);
        Assert.Equal(result.First().FuncionarioPrestadorId, prestacaoListaFake.First().FuncionarioPrestadorId);
        Assert.Equal(result.First().PrestadorId, prestacaoListaFake.First().PrestadorId);
        Assert.Equal(result.First().Produtos, prestacaoListaFake.First().Produtos);
        Assert.Equal(result.First().Produtos.First().Garantia, prestacaoListaFake.First().Produtos.First().Garantia);
        Assert.Equal(result.First().Produtos.First().DataCadastro, prestacaoListaFake.First().Produtos.First().DataCadastro);
        Assert.Equal(result.First().Produtos.First().DataDesativacao, prestacaoListaFake.First().Produtos.First().DataDesativacao);
        Assert.Equal(result.First().Produtos.First().Data_validade, prestacaoListaFake.First().Produtos.First().Data_validade);
        Assert.Equal(result.First().Produtos.First().Id, prestacaoListaFake.First().Produtos.First().Id);
        Assert.Equal(result.First().Produtos.First().Marca, prestacaoListaFake.First().Produtos.First().Marca);
        Assert.Equal(result.First().Produtos.First().Modelo, prestacaoListaFake.First().Produtos.First().Modelo);
        Assert.Equal(result.First().Produtos.First().Nome, prestacaoListaFake.First().Produtos.First().Nome);
        Assert.Equal(result.First().Produtos.First().Qtd, prestacaoListaFake.First().Produtos.First().Qtd);
        Assert.Equal(result.First().Produtos.First().TipoMedidaItem, prestacaoListaFake.First().Produtos.First().TipoMedidaItem);
        Assert.Equal(result.First().Produtos.First().Valor_Compra, prestacaoListaFake.First().Produtos.First().Valor_Compra);
        Assert.Equal(result.First().Produtos.First().Valor_Venda, prestacaoListaFake.First().Produtos.First().Valor_Venda);
        Assert.Equal(result.First().Referencia, prestacaoListaFake.First().Referencia);
        Assert.Equal(result.First().Servicos, prestacaoListaFake.First().Servicos);
        Assert.Equal(result.First().Servicos.First().Descricao, prestacaoListaFake.First().Servicos.First().Descricao);
        Assert.Equal(result.First().Servicos.First().Valor, prestacaoListaFake.First().Servicos.First().Valor);
        Assert.Equal(result.First().Status, prestacaoListaFake.First().Status);
        Assert.Equal(result.First().Veiculo, prestacaoListaFake.First().Veiculo);
        Assert.Equal(result.First().Veiculo.Placa, prestacaoListaFake.First().Veiculo.Placa);
        Assert.Equal(result.First().Veiculo.Marca, prestacaoListaFake.First().Veiculo.Marca);
        Assert.Equal(result.First().Veiculo.Ano, prestacaoListaFake.First().Veiculo.Ano);
    }

    [Fact]
    public async Task NaoDeve_Retornar_ListaPrestacaoServicoAbertos_RetornoNoContent()
    {
        //Arrange
        ICollection<PrestacaoServicoDto> prestacaoListaFake = RetornaListaPrestacaoServico();
        ICollection<PrestacaoServicoDto> prestacaoListaFakeNull = null;
        _serviceMock.Setup(s => s.GetByPrestacoesServicosStatus(It.IsAny<Guid>(), It.IsAny<ICollection<EPrestacaoServicoStatus>>())).ReturnsAsync(prestacaoListaFakeNull);
        PrestacaoServicoController controller = CreateFakeController(prestacaoListaFake);
        //Act
        var response = await controller.GetByPrestacaoServicoAbertosPrestador();
        var okResult = response as NoContentResult;

        //Assert
        _serviceMock.Verify(s => s.GetByPrestacoesServicosStatus(It.IsAny<Guid>(), It.IsAny<ICollection<EPrestacaoServicoStatus>>()), Times.Once());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);

    }

    [Fact]
    public async Task Deve_Retornar_ListaPrestacaoServicoAbertos_RetornoBadRequest()
    {
        //Arrange
        ICollection<PrestacaoServicoDto> prestacaoListaFake = RetornaListaPrestacaoServico();
        PrestacaoServicoDto prestacaoFake = RetornaPrestacaoServico();
        _serviceMock.Setup(s => s.GetByPrestacoesServicosStatus(It.IsAny<Guid>(), It.IsAny<ICollection<EPrestacaoServicoStatus>>())).ReturnsAsync(prestacaoListaFake);
        PrestacaoServicoController controller = CreateFakeController(prestacaoListaFake);
        controller.ModelState.AddModelError("key", "error message");
        //Act
        var response = await controller.GetByPrestacaoServicoAbertosPrestador();
        var okResult = response as ObjectResult;

        //Assert
        _serviceMock.Verify(s => s.GetAllPrestacaoServico(It.IsAny<PrestacaoServicoDto>()), Times.Never());
        Assert.NotNull(okResult);
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.BadRequest);

    }

    [Fact]
    public async Task Deve_Retornar_PrestacaoServicoEnriquecido_RetornoOk()
    {
        //Arrange
        ICollection<PrestacaoServicoDto> prestacaoListaFake = RetornaListaPrestacaoServico();
        PrestacaoServicoDto prestacaoFake = RetornaPrestacaoServico();
        _serviceMock.Setup(s => s.GetByPrestador(It.IsAny<Guid>())).ReturnsAsync(prestacaoListaFake);
        PrestacaoServicoController controller = CreateFakeController(prestacaoListaFake);
        //Act
        var response = await controller.GetByPrestacaoServicoEnriquecidoPrestador();
        var okResult = response as OkObjectResult;
        var result = okResult.Value as ICollection<PrestacaoServicoDto>;
        //Assert
        _serviceMock.Verify(s => s.GetByPrestador(It.IsAny<Guid>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(result.First().Referencia, prestacaoListaFake.First().Referencia);
        Assert.Equal(result.First().FuncionarioPrestador, prestacaoListaFake.First().FuncionarioPrestador);
        Assert.Equal(result.First().DataCadastro, prestacaoListaFake.First().DataCadastro);
        Assert.Equal(result.First().Id, prestacaoListaFake.First().Id);
        Assert.Equal(result.First().Prestador, prestacaoListaFake.First().Prestador);
        Assert.Equal(result.First().FuncionarioPrestadorId, prestacaoListaFake.First().FuncionarioPrestadorId);
        Assert.Equal(result.First().Cliente, prestacaoListaFake.First().Cliente);
        Assert.Equal(result.First().Cliente.Telefone, prestacaoListaFake.First().Cliente.Telefone);
        Assert.Equal(result.First().Cliente.CPF, prestacaoListaFake.First().Cliente.CPF);
        Assert.Equal(result.First().Cliente.DataCadastro, prestacaoListaFake.First().Cliente.DataCadastro);
        Assert.Equal(result.First().Cliente.Id, prestacaoListaFake.First().Cliente.Id);
        Assert.Equal(result.First().Cliente.Email, prestacaoListaFake.First().Cliente.Email);
        Assert.Equal(result.First().Cliente.Endereco, prestacaoListaFake.First().Cliente.Endereco);
        Assert.Equal(result.First().Cliente.Nome, prestacaoListaFake.First().Cliente.Nome);
        Assert.Equal(result.First().Cliente.Rg, prestacaoListaFake.First().Cliente.Rg);
        Assert.Equal(result.First().ClienteId, prestacaoListaFake.First().ClienteId);
        Assert.Equal(result.First().DataCadastro, prestacaoListaFake.First().DataCadastro);
        Assert.Equal(result.First().DataConclusaoServico, prestacaoListaFake.First().DataConclusaoServico);
        Assert.Equal(result.First().FuncionarioPrestador, prestacaoListaFake.First().FuncionarioPrestador);
        Assert.Equal(result.First().FuncionarioPrestador.Telefone, prestacaoListaFake.First().FuncionarioPrestador.Telefone);
        Assert.Equal(result.First().FuncionarioPrestador.CPF, prestacaoListaFake.First().FuncionarioPrestador.CPF);
        Assert.Equal(result.First().FuncionarioPrestador.DataCadastro, prestacaoListaFake.First().FuncionarioPrestador.DataCadastro);
        Assert.Equal(result.First().FuncionarioPrestador.Id, prestacaoListaFake.First().FuncionarioPrestador.Id);
        Assert.Equal(result.First().FuncionarioPrestador.Cargo, prestacaoListaFake.First().FuncionarioPrestador.Cargo);
        Assert.Equal(result.First().FuncionarioPrestador.Email, prestacaoListaFake.First().FuncionarioPrestador.Email);
        Assert.Equal(result.First().FuncionarioPrestador.Endereco, prestacaoListaFake.First().FuncionarioPrestador.Endereco);
        Assert.Equal(result.First().FuncionarioPrestador.Nome, prestacaoListaFake.First().FuncionarioPrestador.Nome);
        Assert.Equal(result.First().FuncionarioPrestador.PrestadorId, prestacaoListaFake.First().FuncionarioPrestador.PrestadorId);
        Assert.Equal(result.First().FuncionarioPrestadorId, prestacaoListaFake.First().FuncionarioPrestadorId);
        Assert.Equal(result.First().PrestadorId, prestacaoListaFake.First().PrestadorId);
        Assert.Equal(result.First().Produtos, prestacaoListaFake.First().Produtos);
        Assert.Equal(result.First().Produtos.First().Garantia, prestacaoListaFake.First().Produtos.First().Garantia);
        Assert.Equal(result.First().Produtos.First().DataCadastro, prestacaoListaFake.First().Produtos.First().DataCadastro);
        Assert.Equal(result.First().Produtos.First().DataDesativacao, prestacaoListaFake.First().Produtos.First().DataDesativacao);
        Assert.Equal(result.First().Produtos.First().Data_validade, prestacaoListaFake.First().Produtos.First().Data_validade);
        Assert.Equal(result.First().Produtos.First().Id, prestacaoListaFake.First().Produtos.First().Id);
        Assert.Equal(result.First().Produtos.First().Marca, prestacaoListaFake.First().Produtos.First().Marca);
        Assert.Equal(result.First().Produtos.First().Modelo, prestacaoListaFake.First().Produtos.First().Modelo);
        Assert.Equal(result.First().Produtos.First().Nome, prestacaoListaFake.First().Produtos.First().Nome);
        Assert.Equal(result.First().Produtos.First().Qtd, prestacaoListaFake.First().Produtos.First().Qtd);
        Assert.Equal(result.First().Produtos.First().TipoMedidaItem, prestacaoListaFake.First().Produtos.First().TipoMedidaItem);
        Assert.Equal(result.First().Produtos.First().Valor_Compra, prestacaoListaFake.First().Produtos.First().Valor_Compra);
        Assert.Equal(result.First().Produtos.First().Valor_Venda, prestacaoListaFake.First().Produtos.First().Valor_Venda);
        Assert.Equal(result.First().Referencia, prestacaoListaFake.First().Referencia);
        Assert.Equal(result.First().Servicos, prestacaoListaFake.First().Servicos);
        Assert.Equal(result.First().Servicos.First().Descricao, prestacaoListaFake.First().Servicos.First().Descricao);
        Assert.Equal(result.First().Servicos.First().Valor, prestacaoListaFake.First().Servicos.First().Valor);
        Assert.Equal(result.First().Status, prestacaoListaFake.First().Status);
        Assert.Equal(result.First().Veiculo, prestacaoListaFake.First().Veiculo);
        Assert.Equal(result.First().Veiculo.Placa, prestacaoListaFake.First().Veiculo.Placa);
        Assert.Equal(result.First().Veiculo.Marca, prestacaoListaFake.First().Veiculo.Marca);
        Assert.Equal(result.First().Veiculo.Ano, prestacaoListaFake.First().Veiculo.Ano);
    }

    [Fact]
    public async Task NaoDeve_Retornar_PrestacaoServicoEnriquecido_RetornoNoContent()
    {
        //Arrange
        ICollection<PrestacaoServicoDto> prestacaoListaFake = RetornaListaPrestacaoServico();
        ICollection<PrestacaoServicoDto> prestacaoListaFakeNull = null;
        PrestacaoServicoDto prestacaoFake = RetornaPrestacaoServico();
        _serviceMock.Setup(s => s.GetByPrestador(It.IsAny<Guid>())).ReturnsAsync(prestacaoListaFakeNull);
        PrestacaoServicoController controller = CreateFakeController(prestacaoListaFake);
        //Act
        var response = await controller.GetByPrestacaoServicoEnriquecidoPrestador();
        var okResult = response as NoContentResult;

        //Assert
        _serviceMock.Verify(s => s.GetByPrestador(It.IsAny<Guid>()), Times.Once());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);

    }

    [Fact]
    public async Task Deve_Retornar_PrestacaoServicoEnriquecido_RetornoBadRequest()
    {
        //Arrange
        ICollection<PrestacaoServicoDto> prestacaoListaFake = RetornaListaPrestacaoServico();
        PrestacaoServicoDto prestacaoFake = RetornaPrestacaoServico();
        _serviceMock.Setup(s => s.GetByPrestador(It.IsAny<Guid>())).ReturnsAsync(prestacaoListaFake);
        PrestacaoServicoController controller = CreateFakeController(prestacaoListaFake);
        controller.ModelState.AddModelError("key", "error message");
        //Act
        var response = await controller.GetByPrestacaoServicoEnriquecidoPrestador();
        var okResult = response as ObjectResult;

        //Assert
        _serviceMock.Verify(s => s.GetByPrestador(It.IsAny<Guid>()), Times.Never());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.BadRequest, okResult.StatusCode);

    }

    [Fact]
    public async Task Deve_Atualizar_PrestacaoServico_RetornoOk()
    {
        //Arrange
        ICollection<PrestacaoServicoDto> prestacaoListaFake = RetornaListaPrestacaoServico();
        PrestacaoServicoDto prestacaoFake = RetornaPrestacaoServico();
        _serviceMock.Setup(s => s.UpdatePrestacaoServico(It.IsAny<PrestacaoServicoDto>())).ReturnsAsync(prestacaoFake);
        PrestacaoServicoController controller = CreateFakeController(prestacaoListaFake);
        //Act
        var response = await controller.AtualizarPrestacaoServico(prestacaoFake);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as PrestacaoServicoDto;
        //Assert
        _serviceMock.Verify(s => s.UpdatePrestacaoServico(It.IsAny<PrestacaoServicoDto>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(result.Referencia, prestacaoFake.Referencia);
        Assert.Equal(result.FuncionarioPrestador, prestacaoFake.FuncionarioPrestador);
        Assert.Equal(result.DataCadastro, prestacaoFake.DataCadastro);
        Assert.Equal(result.Id, prestacaoFake.Id);
        Assert.Equal(result.Prestador, prestacaoFake.Prestador);
        Assert.Equal(result.FuncionarioPrestadorId, prestacaoFake.FuncionarioPrestadorId);
        Assert.Equal(result.Cliente, prestacaoFake.Cliente);
        Assert.Equal(result.Cliente.Telefone, prestacaoFake.Cliente.Telefone);
        Assert.Equal(result.Cliente.CPF, prestacaoFake.Cliente.CPF);
        Assert.Equal(result.Cliente.DataCadastro, prestacaoFake.Cliente.DataCadastro);
        Assert.Equal(result.Cliente.Id, prestacaoFake.Cliente.Id);
        Assert.Equal(result.Cliente.Email, prestacaoFake.Cliente.Email);
        Assert.Equal(result.Cliente.Endereco, prestacaoFake.Cliente.Endereco);
        Assert.Equal(result.Cliente.Nome, prestacaoFake.Cliente.Nome);
        Assert.Equal(result.Cliente.Rg, prestacaoFake.Cliente.Rg);
        Assert.Equal(result.ClienteId, prestacaoFake.ClienteId);
        Assert.Equal(result.DataCadastro, prestacaoFake.DataCadastro);
        Assert.Equal(result.DataConclusaoServico, prestacaoFake.DataConclusaoServico);
        Assert.Equal(result.FuncionarioPrestador, prestacaoFake.FuncionarioPrestador);
        Assert.Equal(result.FuncionarioPrestador.Telefone, prestacaoFake.FuncionarioPrestador.Telefone);
        Assert.Equal(result.FuncionarioPrestador.CPF, prestacaoFake.FuncionarioPrestador.CPF);
        Assert.Equal(result.FuncionarioPrestador.DataCadastro, prestacaoFake.FuncionarioPrestador.DataCadastro);
        Assert.Equal(result.FuncionarioPrestador.Id, prestacaoFake.FuncionarioPrestador.Id);
        Assert.Equal(result.FuncionarioPrestador.Cargo, prestacaoFake.FuncionarioPrestador.Cargo);
        Assert.Equal(result.FuncionarioPrestador.Email, prestacaoFake.FuncionarioPrestador.Email);
        Assert.Equal(result.FuncionarioPrestador.Endereco, prestacaoFake.FuncionarioPrestador.Endereco);
        Assert.Equal(result.FuncionarioPrestador.Nome, prestacaoFake.FuncionarioPrestador.Nome);
        Assert.Equal(result.FuncionarioPrestador.PrestadorId, prestacaoFake.FuncionarioPrestador.PrestadorId);
        Assert.Equal(result.FuncionarioPrestadorId, prestacaoFake.FuncionarioPrestadorId);
        Assert.Equal(result.PrestadorId, prestacaoFake.PrestadorId);
        Assert.Equal(result.Produtos, prestacaoFake.Produtos);
        Assert.Equal(result.Produtos.First().Garantia, prestacaoFake.Produtos.First().Garantia);
        Assert.Equal(result.Produtos.First().DataCadastro, prestacaoFake.Produtos.First().DataCadastro);
        Assert.Equal(result.Produtos.First().DataDesativacao, prestacaoFake.Produtos.First().DataDesativacao);
        Assert.Equal(result.Produtos.First().Data_validade, prestacaoFake.Produtos.First().Data_validade);
        Assert.Equal(result.Produtos.First().Id, prestacaoFake.Produtos.First().Id);
        Assert.Equal(result.Produtos.First().Marca, prestacaoFake.Produtos.First().Marca);
        Assert.Equal(result.Produtos.First().Modelo, prestacaoFake.Produtos.First().Modelo);
        Assert.Equal(result.Produtos.First().Nome, prestacaoFake.Produtos.First().Nome);
        Assert.Equal(result.Produtos.First().Qtd, prestacaoFake.Produtos.First().Qtd);
        Assert.Equal(result.Produtos.First().TipoMedidaItem, prestacaoFake.Produtos.First().TipoMedidaItem);
        Assert.Equal(result.Produtos.First().Valor_Compra, prestacaoFake.Produtos.First().Valor_Compra);
        Assert.Equal(result.Produtos.First().Valor_Venda, prestacaoFake.Produtos.First().Valor_Venda);
        Assert.Equal(result.Referencia, prestacaoFake.Referencia);
        Assert.Equal(result.Servicos, prestacaoFake.Servicos);
        Assert.Equal(result.Servicos.First().Descricao, prestacaoFake.Servicos.First().Descricao);
        Assert.Equal(result.Servicos.First().Valor, prestacaoFake.Servicos.First().Valor);
        Assert.Equal(result.Status, prestacaoFake.Status);
        Assert.Equal(result.Veiculo, prestacaoFake.Veiculo);
        Assert.Equal(result.Veiculo.Placa, prestacaoFake.Veiculo.Placa);
        Assert.Equal(result.Veiculo.Marca, prestacaoFake.Veiculo.Marca);
        Assert.Equal(result.Veiculo.Ano, prestacaoFake.Veiculo.Ano);
    }

    [Fact]
    public async Task NaoDeve_Atualizar_PrestacaoServico_RetornoNoContent()
    {
        //Arrange
        ICollection<PrestacaoServicoDto> prestacaoListaFake = RetornaListaPrestacaoServico();
        PrestacaoServicoDto prestacaoFake = RetornaPrestacaoServico();
        PrestacaoServicoDto prestacaoFakeNull = null;
        _serviceMock.Setup(s => s.UpdatePrestacaoServico(It.IsAny<PrestacaoServicoDto>())).ReturnsAsync(prestacaoFakeNull);
        PrestacaoServicoController controller = CreateFakeController(prestacaoListaFake);
        //Act
        var response = await controller.AtualizarPrestacaoServico(prestacaoFake);
        var okResult = response as NoContentResult;

        //Assert
        _serviceMock.Verify(s => s.UpdatePrestacaoServico(It.IsAny<PrestacaoServicoDto>()), Times.Once());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);
    }

    [Fact]
    public async Task NaoDeve_Atualizar_PrestacaoServico_RetornoBadRequest()
    {
        //Arrange
        ICollection<PrestacaoServicoDto> prestacaoListaFake = RetornaListaPrestacaoServico();
        PrestacaoServicoDto prestacaoFake = RetornaPrestacaoServico();
        _serviceMock.Setup(s => s.UpdatePrestacaoServico(It.IsAny<PrestacaoServicoDto>())).ReturnsAsync(prestacaoFake);
        PrestacaoServicoController controller = CreateFakeController(prestacaoListaFake);
        controller.ModelState.AddModelError("key", "error message");
        //Act
        var response = await controller.AtualizarPrestacaoServico(prestacaoFake);
        var okResult = response as ObjectResult;
        //Assert
        _serviceMock.Verify(s => s.UpdatePrestacaoServico(It.IsAny<PrestacaoServicoDto>()), Times.Never());
        Assert.NotNull(okResult);
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.BadRequest);

    }

    [Fact]
    public async Task NaoDeve_Atualizar_PrestacaoServico_RetornoBadRequest_RequestComErro()
    {
        //Arrange
        ICollection<PrestacaoServicoDto> prestacaoListaFake = RetornaListaPrestacaoServico();
        PrestacaoServicoDto prestacaoFake = RetornaPrestacaoServico();
        _serviceMock.Setup(s => s.UpdatePrestacaoServico(It.IsAny<PrestacaoServicoDto>())).ReturnsAsync(prestacaoFake);
        PrestacaoServicoController controller = CreateFakeController(prestacaoListaFake);
        prestacaoFake.Id = null;
        //Act
        var response = await controller.AtualizarPrestacaoServico(prestacaoFake);
        var okResult = response as ObjectResult;
        //Assert
        _serviceMock.Verify(s => s.UpdatePrestacaoServico(It.IsAny<PrestacaoServicoDto>()), Times.Never());
        Assert.NotNull(okResult);
        Assert.Equal(okResult.StatusCode, (int)HttpStatusCode.BadRequest);

    }

    [Fact]
    public async Task Deve_Desativar_PrestacaoServico_RetornoOk()
    {
        //Arrange
        ICollection<PrestacaoServicoDto> prestacaoListaFake = RetornaListaPrestacaoServico();
        PrestacaoServicoDto prestacaoFake = RetornaPrestacaoServico();
        _serviceMock.Setup(s => s.Desabled(It.IsAny<Guid>(),It.IsAny<Guid>())).ReturnsAsync(prestacaoFake);
        PrestacaoServicoController controller = CreateFakeController(prestacaoListaFake);
        //Act
        var response = await controller.DesativarPrestadorServico(prestacaoFake.Id.Value);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as PrestacaoServicoDto;
        //Assert
        _serviceMock.Verify(s => s.Desabled(It.IsAny<Guid>(),It.IsAny<Guid>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(result.Referencia, prestacaoFake.Referencia);
        Assert.Equal(result.FuncionarioPrestador, prestacaoFake.FuncionarioPrestador);
        Assert.Equal(result.DataCadastro, prestacaoFake.DataCadastro);
        Assert.Equal(result.Id, prestacaoFake.Id);
        Assert.Equal(result.Prestador, prestacaoFake.Prestador);
        Assert.Equal(result.FuncionarioPrestadorId, prestacaoFake.FuncionarioPrestadorId);
        Assert.Equal(result.Cliente, prestacaoFake.Cliente);
        Assert.Equal(result.Cliente.Telefone, prestacaoFake.Cliente.Telefone);
        Assert.Equal(result.Cliente.CPF, prestacaoFake.Cliente.CPF);
        Assert.Equal(result.Cliente.DataCadastro, prestacaoFake.Cliente.DataCadastro);
        Assert.Equal(result.Cliente.Id, prestacaoFake.Cliente.Id);
        Assert.Equal(result.Cliente.Email, prestacaoFake.Cliente.Email);
        Assert.Equal(result.Cliente.Endereco, prestacaoFake.Cliente.Endereco);
        Assert.Equal(result.Cliente.Nome, prestacaoFake.Cliente.Nome);
        Assert.Equal(result.Cliente.Rg, prestacaoFake.Cliente.Rg);
        Assert.Equal(result.ClienteId, prestacaoFake.ClienteId);
        Assert.Equal(result.DataCadastro, prestacaoFake.DataCadastro);
        Assert.Equal(result.DataConclusaoServico, prestacaoFake.DataConclusaoServico);
        Assert.Equal(result.FuncionarioPrestador, prestacaoFake.FuncionarioPrestador);
        Assert.Equal(result.FuncionarioPrestador.Telefone, prestacaoFake.FuncionarioPrestador.Telefone);
        Assert.Equal(result.FuncionarioPrestador.CPF, prestacaoFake.FuncionarioPrestador.CPF);
        Assert.Equal(result.FuncionarioPrestador.DataCadastro, prestacaoFake.FuncionarioPrestador.DataCadastro);
        Assert.Equal(result.FuncionarioPrestador.Id, prestacaoFake.FuncionarioPrestador.Id);
        Assert.Equal(result.FuncionarioPrestador.Cargo, prestacaoFake.FuncionarioPrestador.Cargo);
        Assert.Equal(result.FuncionarioPrestador.Email, prestacaoFake.FuncionarioPrestador.Email);
        Assert.Equal(result.FuncionarioPrestador.Endereco, prestacaoFake.FuncionarioPrestador.Endereco);
        Assert.Equal(result.FuncionarioPrestador.Nome, prestacaoFake.FuncionarioPrestador.Nome);
        Assert.Equal(result.FuncionarioPrestador.PrestadorId, prestacaoFake.FuncionarioPrestador.PrestadorId);
        Assert.Equal(result.FuncionarioPrestadorId, prestacaoFake.FuncionarioPrestadorId);
        Assert.Equal(result.PrestadorId, prestacaoFake.PrestadorId);
        Assert.Equal(result.Produtos, prestacaoFake.Produtos);
        Assert.Equal(result.Produtos.First().Garantia, prestacaoFake.Produtos.First().Garantia);
        Assert.Equal(result.Produtos.First().DataCadastro, prestacaoFake.Produtos.First().DataCadastro);
        Assert.Equal(result.Produtos.First().DataDesativacao, prestacaoFake.Produtos.First().DataDesativacao);
        Assert.Equal(result.Produtos.First().Data_validade, prestacaoFake.Produtos.First().Data_validade);
        Assert.Equal(result.Produtos.First().Id, prestacaoFake.Produtos.First().Id);
        Assert.Equal(result.Produtos.First().Marca, prestacaoFake.Produtos.First().Marca);
        Assert.Equal(result.Produtos.First().Modelo, prestacaoFake.Produtos.First().Modelo);
        Assert.Equal(result.Produtos.First().Nome, prestacaoFake.Produtos.First().Nome);
        Assert.Equal(result.Produtos.First().Qtd, prestacaoFake.Produtos.First().Qtd);
        Assert.Equal(result.Produtos.First().TipoMedidaItem, prestacaoFake.Produtos.First().TipoMedidaItem);
        Assert.Equal(result.Produtos.First().Valor_Compra, prestacaoFake.Produtos.First().Valor_Compra);
        Assert.Equal(result.Produtos.First().Valor_Venda, prestacaoFake.Produtos.First().Valor_Venda);
        Assert.Equal(result.Referencia, prestacaoFake.Referencia);
        Assert.Equal(result.Servicos, prestacaoFake.Servicos);
        Assert.Equal(result.Servicos.First().Descricao, prestacaoFake.Servicos.First().Descricao);
        Assert.Equal(result.Servicos.First().Valor, prestacaoFake.Servicos.First().Valor);
        Assert.Equal(result.Status, prestacaoFake.Status);
        Assert.Equal(result.Veiculo, prestacaoFake.Veiculo);
        Assert.Equal(result.Veiculo.Placa, prestacaoFake.Veiculo.Placa);
        Assert.Equal(result.Veiculo.Marca, prestacaoFake.Veiculo.Marca);
        Assert.Equal(result.Veiculo.Ano, prestacaoFake.Veiculo.Ano);
    }

    [Fact]
    public async Task NaoDeve_Desativar_PrestacaoServico_RetornoNoContent()
    {
        //Arrange
        ICollection<PrestacaoServicoDto> prestacaoListaFake = RetornaListaPrestacaoServico();
        PrestacaoServicoDto prestacaoFake = RetornaPrestacaoServico();
        PrestacaoServicoDto prestacaoFakeNull = null;
        _serviceMock.Setup(s => s.Desabled(It.IsAny<Guid>(),It.IsAny<Guid>())).ReturnsAsync(prestacaoFakeNull);
        PrestacaoServicoController controller = CreateFakeController(prestacaoListaFake);
        //Act
        var response = await controller.DesativarPrestadorServico(prestacaoFake.Id.Value);
        var okResult = response as NoContentResult;

        //Assert
        _serviceMock.Verify(s => s.Desabled(It.IsAny<Guid>(),It.IsAny<Guid>()), Times.Once());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);
    }

    [Fact]
    public async Task NaoDeve_Desativar_PrestacaoServico_RetornoBadRequest()
    {
        //Arrange
        ICollection<PrestacaoServicoDto> prestacaoListaFake = RetornaListaPrestacaoServico();
        PrestacaoServicoDto prestacaoFake = RetornaPrestacaoServico();
        _serviceMock.Setup(s => s.Desabled(It.IsAny<Guid>(),It.IsAny<Guid>())).ReturnsAsync(prestacaoFake);
        PrestacaoServicoController controller = CreateFakeController(prestacaoListaFake);
        controller.ModelState.AddModelError("key", "error message");
        //Act
        var response = await controller.DesativarPrestadorServico(prestacaoFake.Id.Value);
        var okResult = response as ObjectResult;
        //Assert
        _serviceMock.Verify(s => s.Desabled(It.IsAny<Guid>(),It.IsAny<Guid>()), Times.Never());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.BadRequest, okResult.StatusCode);

    }

    [Fact]
    public async Task Deve_Deletar_PrestacaoServico_RetornoOk()
    {
        //Arrange
        ICollection<PrestacaoServicoDto> prestacaoListaFake = RetornaListaPrestacaoServico();
        PrestacaoServicoDto prestacaoFake = RetornaPrestacaoServico();
        _serviceMock.Setup(s => s.Delete(It.IsAny<Guid>()));
        PrestacaoServicoController controller = CreateFakeController(prestacaoListaFake);
        //Act
        var response = await controller.DeletarPrestador(prestacaoFake.Id.Value);
        var okResult = response as OkObjectResult;
        //Assert
        _serviceMock.Verify(s => s.Delete(It.IsAny<Guid>()), Times.Once());
        Assert.NotNull(okResult);      
        Assert.Equal(okResult.Value, "Deletado");
    }

    [Fact]
    public async Task NaoDeve_Deletar_PrestacaoServico_RetornoBadRequest()
    {
        //Arrange
        ICollection<PrestacaoServicoDto> prestacaoListaFake = RetornaListaPrestacaoServico();
        PrestacaoServicoDto prestacaoFake = RetornaPrestacaoServico();
        _serviceMock.Setup(s => s.Delete(It.IsAny<Guid>()));

        PrestacaoServicoController controller = CreateFakeController(prestacaoListaFake);
        controller.ModelState.AddModelError("key", "error message");
        //Act
        var response = await controller.DeletarPrestador(prestacaoFake.Id.Value);
        var okResult = response as ObjectResult;
        //Assert
        _serviceMock.Verify(s => s.Delete(It.IsAny<Guid>()), Times.Never());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.BadRequest, okResult.StatusCode);

    }

    [Fact]
    public async Task NaoDeve_Deletar_PrestacaoServico_RetornoBadRequestExecption()
    {
        //Arrange
        ICollection<PrestacaoServicoDto> prestacaoListaFake = RetornaListaPrestacaoServico();
        PrestacaoServicoDto prestacaoFake = RetornaPrestacaoServico();
        _serviceMock.Setup(s => s.Delete(It.IsAny<Guid>())).ThrowsAsync(new Exception("Error"));

        PrestacaoServicoController controller = CreateFakeController(prestacaoListaFake);
        //Act
        var response = await controller.DeletarPrestador(prestacaoFake.Id.Value);
        var okResult = response as ObjectResult;
        //Assert
        _serviceMock.Verify(s => s.Delete(It.IsAny<Guid>()), Times.Once());
        Assert.NotNull(okResult);
        Assert.Equal((int)HttpStatusCode.BadRequest, okResult.StatusCode);

    }

    [Fact]
    public async Task Deve_Mudar_Status_PrestacaoServico_RetornoOk()
    {
        //Arrange
        ICollection<PrestacaoServicoDto> prestacaoListaFake = RetornaListaPrestacaoServico();
        PrestacaoServicoDto prestacaoFake = RetornaPrestacaoServico();
        _serviceMock.Setup(s => s.ChangeStatus(It.IsAny<Guid>(), It.IsAny<EPrestacaoServicoStatus>()));
        PrestacaoServicoController controller = CreateFakeController(prestacaoListaFake);
        //Act
        var response = await controller.ChangeStatus(prestacaoFake.Id.Value, EPrestacaoServicoStatus.Concluido);
        //Assert
        _serviceMock.Verify(s => s.ChangeStatus(It.IsAny<Guid>(), It.IsAny<EPrestacaoServicoStatus>()), Times.Once());
        Assert.NotNull(response);        
    }

    [Fact]
    public async Task Deve_Mudar_Status_PrestacaoServico_RetornoBadRequest()
    {
        //Arrange
        ICollection<PrestacaoServicoDto> prestacaoListaFake = RetornaListaPrestacaoServico();
        PrestacaoServicoDto prestacaoFake = RetornaPrestacaoServico();
        _serviceMock.Setup(s => s.ChangeStatus(It.IsAny<Guid>(), It.IsAny<EPrestacaoServicoStatus>()));
        PrestacaoServicoController controller = CreateFakeController(prestacaoListaFake);
        controller.ModelState.AddModelError("key", "error message"); 
        //Act
        var response = await controller.ChangeStatus(prestacaoFake.Id.Value, EPrestacaoServicoStatus.Concluido);
        //Assert
        _serviceMock.Verify(s => s.ChangeStatus(It.IsAny<Guid>(), It.IsAny<EPrestacaoServicoStatus>()), Times.Never());
        Assert.NotNull(response);
    }
}
