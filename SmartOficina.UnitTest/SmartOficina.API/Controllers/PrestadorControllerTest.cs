﻿using SmartOficina.Api.Infrastructure.Migrations;

namespace SmartOficina.UnitTest.SmartOficina.API.Controllers;

public class PrestadorControllerTest
{
    private readonly Mock<IPrestadorRepository> _repository = new();
    private readonly Mock<IMapper> _mapper = new();
    private readonly Mock<IFuncionarioPrestadorRepository> _funcionarioRepository = new();

    #region Prestador 
    
    [Fact]
    public async Task Deve_Retornar_ListaDePrestador()
    {
        //Arrange
        ICollection<Prestador> PrestadorsFake = CriaListaFornecedoresFake();
        _repository.Setup(s => s.GetAll()).ReturnsAsync(PrestadorsFake);
        //Act
        var response = await new PrestadorController(_repository.Object, _funcionarioRepository.Object, _mapper.Object).GetAll();
        var okResult = response as OkObjectResult;
        var result = okResult.Value as ICollection<Prestador>;
        //Assert
        _repository.Verify(s => s.GetAll(), Times.Once());
        _mapper.Verify(s => s.Map<Prestador>(It.IsAny<PrestadorDto>), Times.Never());
        Assert.NotNull(result);
        Assert.Equal(result.First().Telefone, PrestadorsFake.First().Telefone);
        Assert.Equal(result.First().CPF, PrestadorsFake.First().CPF);
        Assert.Equal(result.First().DataCadastro, PrestadorsFake.First().DataCadastro);
        Assert.Equal(result.First().Id, PrestadorsFake.First().Id);
        Assert.Equal(result.First().EmailEmpresa, PrestadorsFake.First().EmailEmpresa);
        Assert.Equal(result.First().Endereco, PrestadorsFake.First().Endereco);
        Assert.Equal(result.First().Nome, PrestadorsFake.First().Nome);

    }

    [Fact]
    public async Task Deve_Cadastrar_Um_Prestador_RetornarSucesso()
    {
        //Arrange
        Guid? id = null;
        Prestador PrestadorFake = CriaFornecedorFake(Guid.NewGuid());
        PrestadorDto PrestadorDtoFake = CriaFornecedorDtoFake(id);

        _repository.Setup(s => s.Create(It.IsAny<Prestador>())).ReturnsAsync(PrestadorFake);
        _mapper.Setup(s => s.Map<Prestador>(It.IsAny<PrestadorDto>())).Returns(PrestadorFake);
        //Act
        var response = await new PrestadorController(_repository.Object, _funcionarioRepository.Object, _mapper.Object).Add(PrestadorDtoFake);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as Prestador;
        //Assert
        _repository.Verify(s => s.Create(It.IsAny<Prestador>()), Times.Once());
        _mapper.Verify(s => s.Map<Prestador>(It.IsAny<PrestadorDto>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(result.Telefone, PrestadorFake.Telefone);
        Assert.Equal(result.CPF, PrestadorFake.CPF);
        Assert.Equal(result.DataCadastro, PrestadorFake.DataCadastro);
        Assert.Equal(result.Id, PrestadorFake.Id);
        Assert.Equal(result.EmailEmpresa, PrestadorFake.EmailEmpresa);
        Assert.Equal(result.Endereco, PrestadorFake.Endereco);
        Assert.Equal(result.Nome, PrestadorFake.Nome);
    }

    [Fact]
    public async Task Deve_Retornarar_Um_Prestador_RetornarSucesso()
    {
        //Arrange
        Prestador PrestadorFake = CriaFornecedorFake(Guid.NewGuid());
        PrestadorDto PrestadorDtoFake = CriaFornecedorDtoFake(PrestadorFake.Id);

        _repository.Setup(s => s.FindById(It.IsAny<Guid>())).ReturnsAsync(PrestadorFake);
        _mapper.Setup(s => s.Map<Prestador>(It.IsAny<PrestadorDto>())).Returns(PrestadorFake);
        //Act
        var response = await new PrestadorController(_repository.Object, _funcionarioRepository.Object, _mapper.Object).GetId(PrestadorDtoFake.Id.Value);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as Prestador;
        //Assert
        _repository.Verify(s => s.FindById(It.IsAny<Guid>()), Times.Once());
        _mapper.Verify(s => s.Map<Prestador>(It.IsAny<PrestadorDto>()), Times.Never());
        Assert.NotNull(result);
        Assert.Equal(result.Telefone, PrestadorFake.Telefone);
        Assert.Equal(result.CPF, PrestadorFake.CPF);
        Assert.Equal(result.DataCadastro, PrestadorFake.DataCadastro);
        Assert.Equal(result.Id, PrestadorFake.Id);
        Assert.Equal(result.EmailEmpresa, PrestadorFake.EmailEmpresa);
        Assert.Equal(result.Endereco, PrestadorFake.Endereco);
        Assert.Equal(result.Nome, PrestadorFake.Nome);
    }

    [Fact]
    public async Task Deve_Atualizar_Um_Prestador_RetornarSucesso()
    {
        //Arrange
        Prestador PrestadorFake = CriaFornecedorFake(Guid.NewGuid());
        PrestadorDto PrestadorDtoFake = CriaFornecedorDtoFake(Guid.NewGuid());

        _repository.Setup(s => s.Update(It.IsAny<Prestador>())).ReturnsAsync(PrestadorFake);
        _mapper.Setup(s => s.Map<Prestador>(It.IsAny<PrestadorDto>())).Returns(PrestadorFake);
        //Act
        var response = await new PrestadorController(_repository.Object, _funcionarioRepository.Object, _mapper.Object).AtualizarPrestador(PrestadorDtoFake);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as Prestador;
        //Assert
        _repository.Verify(s => s.Update(It.IsAny<Prestador>()), Times.Once());
        _mapper.Verify(s => s.Map<Prestador>(It.IsAny<PrestadorDto>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(result.Telefone, PrestadorFake.Telefone);
        Assert.Equal(result.CPF, PrestadorFake.CPF);
        Assert.Equal(result.DataCadastro, PrestadorFake.DataCadastro);
        Assert.Equal(result.Id, PrestadorFake.Id);
        Assert.Equal(result.EmailEmpresa, PrestadorFake.EmailEmpresa);
        Assert.Equal(result.Endereco, PrestadorFake.Endereco);
        Assert.Equal(result.Nome, PrestadorFake.Nome);
    }


    [Fact]
    public async Task Deve_Desativar_Um_Prestador_RetornarSucesso()
    {
        //Arrange
        Prestador PrestadorFake = CriaFornecedorFake(Guid.NewGuid());
        PrestadorDto PrestadorDtoFake = CriaFornecedorDtoFake(PrestadorFake.Id);

        _repository.Setup(s => s.Desabled(It.IsAny<Guid>())).ReturnsAsync(PrestadorFake);
        _mapper.Setup(s => s.Map<Prestador>(It.IsAny<PrestadorDto>())).Returns(PrestadorFake);
        //Act
        var response = await new PrestadorController(_repository.Object, _funcionarioRepository.Object, _mapper.Object).DesativarPrestadorServico(PrestadorDtoFake.Id.Value);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as Prestador;
        //Assert
        _repository.Verify(s => s.Desabled(It.IsAny<Guid>()), Times.Once());
        _mapper.Verify(s => s.Map<Prestador>(It.IsAny<PrestadorDto>()), Times.Never());
        Assert.NotNull(result);
        Assert.Equal(result.Telefone, PrestadorFake.Telefone);
        Assert.Equal(result.CPF, PrestadorFake.CPF);
        Assert.Equal(result.DataCadastro, PrestadorFake.DataCadastro);
        Assert.Equal(result.Id, PrestadorFake.Id);
        Assert.Equal(result.EmailEmpresa, PrestadorFake.EmailEmpresa);
        Assert.Equal(result.Endereco, PrestadorFake.Endereco);
        Assert.Equal(result.Nome, PrestadorFake.Nome);
    }

    [Fact]
    public async Task Deve_Deletar_Um_Prestador_RetornarSucesso()
    {
        //Arrange
        Prestador PrestadorFake = CriaFornecedorFake(Guid.NewGuid());
        PrestadorDto PrestadorDtoFake = CriaFornecedorDtoFake(PrestadorFake.Id);

        _repository.Setup(s => s.Delete(It.IsAny<Guid>()));
        _mapper.Setup(s => s.Map<Prestador>(It.IsAny<PrestadorDto>())).Returns(PrestadorFake);
        //Act
        var response = await new PrestadorController(_repository.Object, _funcionarioRepository.Object, _mapper.Object).DeletarPrestador(PrestadorDtoFake.Id.Value);

        //Assert
        _repository.Verify(s => s.Delete(It.IsAny<Guid>()), Times.Once());
        _mapper.Verify(s => s.Map<Prestador>(It.IsAny<PrestadorDto>()), Times.Never());
        Assert.NotNull(response);

    }
    #endregion


    #region Funcionários

    [Fact]
    public async Task Deve_Retornar_ListaDeFuncionarios()
    {
        //Arrange
        ICollection<FuncionarioPrestador> funcionarioFake = CriarListaFuncionarioFake();
        _funcionarioRepository.Setup(s => s.GetAll()).ReturnsAsync(funcionarioFake);
        //Act
        var response = await new PrestadorController(_repository.Object, _funcionarioRepository.Object, _mapper.Object).GetAllFuncionario();
        var okResult = response as OkObjectResult;
        var result = okResult.Value as ICollection<FuncionarioPrestador>;
        //Assert
        _funcionarioRepository.Verify(s => s.GetAll(), Times.Once());
        _mapper.Verify(s => s.Map<FuncionarioPrestador>(It.IsAny<FuncionarioPrestadorDto>), Times.Once());
        _mapper.Verify(s => s.Map<FuncionarioPrestadorDto>(It.IsAny<FuncionarioPrestador>), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(result.First().Telefone, funcionarioFake.First().Telefone);
        Assert.Equal(result.First().CPF, funcionarioFake.First().CPF);
        Assert.Equal(result.First().DataCadastro, funcionarioFake.First().DataCadastro);
        Assert.Equal(result.First().Id, funcionarioFake.First().Id);
        Assert.Equal(result.First().Cargo, funcionarioFake.First().Cargo);
        Assert.Equal(result.First().Email, funcionarioFake.First().Email);
        Assert.Equal(result.First().Endereco, funcionarioFake.First().Endereco);
        Assert.Equal(result.First().Nome, funcionarioFake.First().Nome);
        Assert.Equal(result.First().Prestador.CPF, funcionarioFake.First().Prestador.CPF);
        Assert.Equal(result.First().Prestador.Nome, funcionarioFake.First().Prestador.Nome);

    }

    [Fact]
    public async Task Deve_Cadastrar_Um_FuncionarioPrestador_RetornarSucesso()
    {
        //Arrange
        Guid? id = null;
        FuncionarioPrestador funcionarioFake = CriarFuncionarioFake(Guid.NewGuid());
        FuncionarioPrestadorDto funcionarioDtoFake = CriarFuncionarioDtoFake(id);

        _funcionarioRepository.Setup(s => s.Create(It.IsAny<FuncionarioPrestador>())).ReturnsAsync(funcionarioFake);
        _mapper.Setup(s => s.Map<FuncionarioPrestadorDto>(It.IsAny<FuncionarioPrestador>())).Returns(funcionarioDtoFake);
        _mapper.Setup(s => s.Map<FuncionarioPrestador>(It.IsAny<FuncionarioPrestadorDto>())).Returns(funcionarioFake);
        //Act
        var response = await new PrestadorController(_repository.Object, _funcionarioRepository.Object, _mapper.Object).AddFuncionario(funcionarioDtoFake);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as FuncionarioPrestadorDto;
        //Assert
        _funcionarioRepository.Verify(s => s.Create(It.IsAny<FuncionarioPrestador>()), Times.Once());
        _mapper.Verify(s => s.Map<FuncionarioPrestadorDto>(It.IsAny<FuncionarioPrestador>()), Times.Once());
        _mapper.Verify(s => s.Map<FuncionarioPrestador>(It.IsAny<FuncionarioPrestadorDto>()), Times.Once());

        Assert.NotNull(result);
        Assert.Equal(result.Telefone, funcionarioDtoFake.Telefone);
        Assert.Equal(result.CPF, funcionarioDtoFake.CPF);
        Assert.Equal(result.DataCadastro, funcionarioDtoFake.DataCadastro);
        Assert.Equal(result.Id, funcionarioDtoFake.Id);
        Assert.Equal(result.Email, funcionarioDtoFake.Email);
        Assert.Equal(result.Endereco, funcionarioDtoFake.Endereco);
        Assert.Equal(result.Nome, funcionarioDtoFake.Nome);
    }

    [Fact]
    public async Task Deve_Retornarar_Um_FuncionarioPrestador_RetornarSucesso()
    {
        //Arrange
        Guid? id = null;
        FuncionarioPrestador funcionarioFake = CriarFuncionarioFake(Guid.NewGuid());
        FuncionarioPrestadorDto funcionarioDtoFake = CriarFuncionarioDtoFake(id);

        _funcionarioRepository.Setup(s => s.FindById(It.IsAny<Guid>())).ReturnsAsync(funcionarioFake);
        _mapper.Setup(s => s.Map<FuncionarioPrestadorDto>(It.IsAny<FuncionarioPrestador>())).Returns(funcionarioDtoFake);
        //Act
        var response = await new PrestadorController(_repository.Object, _funcionarioRepository.Object, _mapper.Object).GetIdFuncionario(funcionarioFake.Id);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as FuncionarioPrestadorDto;
        //Assert
        _funcionarioRepository.Verify(s => s.FindById(It.IsAny<Guid>()), Times.Once());
        _mapper.Verify(s => s.Map<FuncionarioPrestadorDto>(It.IsAny<FuncionarioPrestador>()), Times.Never());
        Assert.NotNull(result);
        Assert.Equal(result.Telefone, funcionarioDtoFake.Telefone);
        Assert.Equal(result.CPF, funcionarioDtoFake.CPF);
        Assert.Equal(result.DataCadastro, funcionarioDtoFake.DataCadastro);
        Assert.Equal(result.Id, funcionarioDtoFake.Id);
        Assert.Equal(result.Email, funcionarioDtoFake.Email);
        Assert.Equal(result.Endereco, funcionarioDtoFake.Endereco);
        Assert.Equal(result.Nome, funcionarioDtoFake.Nome);
    }

    [Fact]
    public async Task Deve_Atualizar_Um_Funcionario_RetornarSucesso()
    {
        //Arrange
        Guid? id = null;
        FuncionarioPrestador funcionarioFake = CriarFuncionarioFake(Guid.NewGuid());
        FuncionarioPrestadorDto funcionarioDtoFake = CriarFuncionarioDtoFake(id);

        _funcionarioRepository.Setup(s => s.Update(It.IsAny<FuncionarioPrestador>())).ReturnsAsync(funcionarioFake);
        _mapper.Setup(s => s.Map<FuncionarioPrestador>(It.IsAny<FuncionarioPrestadorDto>())).Returns(funcionarioFake);
        _mapper.Setup(s => s.Map<FuncionarioPrestadorDto>(It.IsAny<FuncionarioPrestador>())).Returns(funcionarioDtoFake);
        //Act
        var response = await new PrestadorController(_repository.Object, _funcionarioRepository.Object, _mapper.Object).AtualizarFuncionario(funcionarioDtoFake);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as FuncionarioPrestadorDto;
        //Assert
        _funcionarioRepository.Verify(s => s.Update(It.IsAny<FuncionarioPrestador>()), Times.Once());
        _mapper.Verify(s => s.Map<FuncionarioPrestador>(It.IsAny<FuncionarioPrestadorDto>()), Times.Once());
        _mapper.Verify(s => s.Map<FuncionarioPrestadorDto>(It.IsAny<FuncionarioPrestador>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(result.Telefone, funcionarioDtoFake.Telefone);
        Assert.Equal(result.CPF, funcionarioDtoFake.CPF);
        Assert.Equal(result.DataCadastro, funcionarioDtoFake.DataCadastro);
        Assert.Equal(result.Id, funcionarioDtoFake.Id);
        Assert.Equal(result.Email, funcionarioDtoFake.Email);
        Assert.Equal(result.Endereco, funcionarioDtoFake.Endereco);
        Assert.Equal(result.Nome, funcionarioDtoFake.Nome);
    }


    [Fact]
    public async Task Deve_Desativar_Um_Funcionario_RetornarSucesso()
    {
        //Arrange
        Guid? id = null;
        FuncionarioPrestador funcionarioFake = CriarFuncionarioFake(Guid.NewGuid());
        FuncionarioPrestadorDto funcionarioDtoFake = CriarFuncionarioDtoFake(id);

        _funcionarioRepository.Setup(s => s.Desabled(It.IsAny<Guid>())).ReturnsAsync(funcionarioFake);
        _mapper.Setup(s => s.Map<FuncionarioPrestador>(It.IsAny<FuncionarioPrestadorDto>())).Returns(funcionarioFake);
        _mapper.Setup(s => s.Map<FuncionarioPrestadorDto>(It.IsAny<FuncionarioPrestador>())).Returns(funcionarioDtoFake);
        //Act
        var response = await new PrestadorController(_repository.Object, _funcionarioRepository.Object, _mapper.Object).DesativarFuncionario(funcionarioFake.Id);
        var okResult = response as OkObjectResult;
        var result = okResult.Value as FuncionarioPrestadorDto;
        //Assert
        _funcionarioRepository.Verify(s => s.Desabled(It.IsAny<Guid>()), Times.Once());
        _mapper.Verify(s => s.Map<Prestador>(It.IsAny<PrestadorDto>()), Times.Never());
        Assert.NotNull(result);
        Assert.Equal(result.Telefone, funcionarioDtoFake.Telefone);
        Assert.Equal(result.CPF, funcionarioDtoFake.CPF);
        Assert.Equal(result.DataCadastro, funcionarioDtoFake.DataCadastro);
        Assert.Equal(result.Id, funcionarioDtoFake.Id);
        Assert.Equal(result.Email, funcionarioDtoFake.Email);
        Assert.Equal(result.Endereco, funcionarioDtoFake.Endereco);
        Assert.Equal(result.Nome, funcionarioDtoFake.Nome);
    }

    [Fact]
    public async Task Deve_Deletar_Um_Funcionario_RetornarSucesso()
    {
        //Arrange
        Guid? id = null;
        FuncionarioPrestador funcionarioFake = CriarFuncionarioFake(Guid.NewGuid());
        FuncionarioPrestadorDto funcionarioDtoFake = CriarFuncionarioDtoFake(id);

        _funcionarioRepository.Setup(s => s.Delete(It.IsAny<Guid>()));
        _mapper.Setup(s => s.Map<FuncionarioPrestadorDto>(It.IsAny<FuncionarioPrestador>())).Returns(funcionarioDtoFake);
        //Act
        var response = await new PrestadorController(_repository.Object, _funcionarioRepository.Object, _mapper.Object).DeletarFuncionario(funcionarioDtoFake.Id.Value);

        //Assert
        _funcionarioRepository.Verify(s => s.Delete(It.IsAny<Guid>()), Times.Once());
        _mapper.Verify(s => s.Map<FuncionarioPrestadorDto>(It.IsAny<FuncionarioPrestador>()), Times.Never());
        Assert.NotNull(response);

    }
    #endregion


    private FuncionarioPrestadorDto CriarFuncionarioDtoFake(Guid? id)
    {
        return new FuncionarioPrestadorDto() { Cargo = "teste", CPF = "123456789", Email = "teste@teste.com.br", Nome = "teste func", RG = "52453", Telefone = "1234556", DataCadastro = DateTime.Now, Endereco = "rua teste", Id = Guid.NewGuid(), UsrCadastro = Guid.NewGuid() };
    }

    private FuncionarioPrestador CriarFuncionarioFake(Guid guid)
    {
        return new FuncionarioPrestador() { Cargo = "teste", CPF = "123456789", Email = "teste@teste.com.br", Nome = "teste func", RG = "52453", Telefone = "1234556", DataCadastro = DateTime.Now, Endereco = "rua teste", Id = Guid.NewGuid(), UsrCadastro = Guid.NewGuid(), Prestador = CriaFornecedorFake(Guid.NewGuid()), PrestadorId = Guid.NewGuid() };
    }

    private ICollection<FuncionarioPrestador> CriarListaFuncionarioFake()
    {
        return new List<FuncionarioPrestador>() { new FuncionarioPrestador() { Cargo = "teste", CPF = "123456789", Email = "teste@teste.com.br", Nome = "teste func", RG = "52453", Telefone = "1234556", DataCadastro = DateTime.Now, Endereco = "rua teste", Id = Guid.NewGuid(), UsrCadastro = Guid.NewGuid(), Prestador = CriaFornecedorFake(Guid.NewGuid()), PrestadorId = Guid.NewGuid() } };
    }

    private static ICollection<Prestador> CriaListaFornecedoresFake()
    {
        return new List<Prestador>() { new Prestador() { CPF = "123456789", EmailEmpresa = "teste@test.com.br", Nome = "Teste", Telefone = "11999999999", Id = Guid.NewGuid() } };
    }


    private static Prestador CriaFornecedorFake(Guid? Id)
    {
        return new Prestador() { CPF = "123456789", EmailEmpresa = "teste@test.com.br", Nome = "Teste", Telefone = "11999999999", Id = Id.HasValue ? Id.Value : Guid.Empty };
    }

    private static PrestadorDto CriaFornecedorDtoFake(Guid? Id)
    {
        return new PrestadorDto() { CPF = "123456789", EmailEmpresa = "teste@test.com.br", Nome = "Teste", Telefone = "11999999999", Id = Id.HasValue ? Id.Value : Guid.Empty };
    }
}
