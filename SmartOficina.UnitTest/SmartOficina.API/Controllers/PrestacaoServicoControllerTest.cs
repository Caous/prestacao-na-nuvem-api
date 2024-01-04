using SmartOficina.Api.Domain.Interfaces;
using SmartOficina.Api.Domain.Model;
using SmartOficina.Api.Domain.Services;
using System.Numerics;

namespace SmartOficina.UnitTest.SmartOficina.API.Controllers;

public class PrestacaoServicoControllerTest
{
    Mock<IPrestacaoServicoService> _serviceMock = new();
    private static DefaultHttpContext CreateFakeClaims(ICollection<PrestacaoServicoDto> prestacaoServico)
    {
        var fakeHttpContext = new DefaultHttpContext();
        ClaimsIdentity identity = new(
            new[] {
                        new Claim("PrestadorId", prestacaoServico.First().PrestadorId.ToString()),
                        new Claim("UserName", "Teste"),
                        new Claim("IdUserLogin", prestacaoServico.First().PrestadorId.ToString())

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
        var okResult = response as OkObjectResult;
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
}
