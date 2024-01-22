namespace SmartOficina.Api.Domain.Services;

public class PrestacaoServicoService : IPrestacaoServicoService
{
    private readonly IPrestacaoServicoRepository _repository;
    private readonly IMapper _mapper;

    public PrestacaoServicoService(IPrestacaoServicoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task ChangeStatus(Guid id, EPrestacaoServicoStatus status)
    {
        await _repository.ChangeStatus(id, status);
    }

    public async Task<PrestacaoServicoDto> CreatePrestacaoServico(PrestacaoServicoDto prestacaoServico)
    {
        if (!prestacaoServico.PrestadorId.HasValue)
            prestacaoServico.PrestadorId = prestacaoServico.PrestadorId;

        if (prestacaoServico.Produtos != null)
        {
            foreach (var item in prestacaoServico.Produtos)
            {
                item.PrestadorId = prestacaoServico.PrestadorId.Value;
                item.UsrCadastro = prestacaoServico.UsrCadastro;
                item.UsrCadastroDesc = prestacaoServico.UsrCadastroDesc;
            }
        }

        if (prestacaoServico.Veiculo != null)
        {
            prestacaoServico.Veiculo.PrestadorId = prestacaoServico.PrestadorId.Value;
            prestacaoServico.UsrCadastro = prestacaoServico.UsrCadastro;
            prestacaoServico.UsrCadastroDesc = prestacaoServico.UsrCadastroDesc;
        }

        if (prestacaoServico.Cliente != null)
        {
            prestacaoServico.Cliente.PrestadorId = prestacaoServico.PrestadorId.Value;
            prestacaoServico.UsrCadastro = prestacaoServico.UsrCadastro;
            prestacaoServico.UsrCadastroDesc = prestacaoServico.UsrCadastroDesc;
        }

        if (prestacaoServico.Servicos != null)
        {
            foreach (var item in prestacaoServico.Servicos)
            {
                item.PrestadorId = prestacaoServico.PrestadorId.Value;
                item.UsrCadastro = prestacaoServico.UsrCadastro;
                item.UsrCadastroDesc = prestacaoServico.UsrCadastroDesc;
            }
        }


        prestacaoServico.UsrCadastroDesc = prestacaoServico.UsrCadastroDesc;
        prestacaoServico.UsrCadastro = prestacaoServico.UsrCadastro;


        var produtos = new List<ProdutoDto>();

        if (prestacaoServico.Produtos != null)
        {
            foreach (var prod in prestacaoServico.Produtos)
            {
                for (int i = 0; i < prod.Qtd; i++)
                {
                    produtos.Add(prod);
                }
            }
        }

        prestacaoServico.Produtos = produtos;

        var result = await _repository.Create(_mapper.Map<PrestacaoServico>(prestacaoServico));

        return _mapper.Map<PrestacaoServicoDto>(result);
    }

    public async Task Delete(Guid id)
    {
        await _repository.Delete(id);
    }

    public async Task<PrestacaoServicoDto> Desabled(Guid id, Guid userDesabled)
    {

        var result = await _repository.Desabled(id, userDesabled);
        return _mapper.Map<PrestacaoServicoDto>(result);
    }

    public async Task<PrestacaoServicoDto> FindByIdPrestacaoServico(Guid id)
    {
        var result = await _repository.FindById(id);

        return _mapper.Map<PrestacaoServicoDto>(result);
    }

    public async Task<ICollection<PrestacaoServicoDto>> GetAllPrestacaoServico(PrestacaoServicoDto item)
    {
        var result = await _repository.GetAll(item.PrestadorId.Value, _mapper.Map<PrestacaoServico>(item));

        return _mapper.Map<ICollection<PrestacaoServicoDto>>(result);
    }

    public async Task<ICollection<PrestacaoServicoDto>> GetByPrestacoesServicosStatus(Guid prestadorId, ICollection<EPrestacaoServicoStatus> statusPrestacao)
    {
        List<EPrestacaoServicoStatus> status = new List<EPrestacaoServicoStatus>() { EPrestacaoServicoStatus.Concluido, EPrestacaoServicoStatus.Rejeitado };
        return _mapper.Map<ICollection<PrestacaoServicoDto>>(await _repository.GetByPrestacoesServicosStatus(prestadorId, status));
    }

    public async Task<ICollection<PrestacaoServicoDto>> GetByPrestador(Guid prestadorId)
    {
        return _mapper.Map<ICollection<PrestacaoServicoDto>>(await _repository.GetByPrestador(prestadorId));
    }

    public async Task<PrestacaoServicoDto> UpdatePrestacaoServico(PrestacaoServicoDto prestacaoServico)
    {
        if (!prestacaoServico.PrestadorId.HasValue)
            prestacaoServico.PrestadorId = prestacaoServico.PrestadorId;

        if (prestacaoServico.Produtos != null)
        {
            foreach (var item in prestacaoServico.Produtos)
            {
                item.PrestadorId = prestacaoServico.PrestadorId.Value;
                item.UsrCadastro = prestacaoServico.UsrCadastro;
                item.UsrCadastroDesc = prestacaoServico.UsrCadastroDesc;
            }
        }

        if (prestacaoServico.Servicos != null)
        {

            foreach (var item in prestacaoServico.Servicos)
            {
                item.PrestadorId = prestacaoServico.PrestadorId.Value;
                item.UsrCadastro = prestacaoServico.UsrCadastro;
                item.UsrCadastroDesc = prestacaoServico.UsrCadastroDesc;
            }
        }

        if (prestacaoServico.Veiculo != null)
            prestacaoServico.Veiculo.PrestadorId = prestacaoServico.PrestadorId.Value;

        if (prestacaoServico.Cliente != null)
            prestacaoServico.Cliente.PrestadorId = prestacaoServico.PrestadorId.Value;


        var produtos = new List<ProdutoDto>();

        if (prestacaoServico.Produtos != null)
        {
            foreach (var prod in prestacaoServico.Produtos)
            {
                if (prod.Qtd == 0)
                    produtos.Add(prod);

                for (int i = 0; i < prod.Qtd; i++)
                {
                    produtos.Add(prod);
                }
            }
        }

        prestacaoServico.Produtos = produtos;

        var result = await _repository.Update(_mapper.Map<PrestacaoServico>(prestacaoServico));

        return _mapper.Map<PrestacaoServicoDto>(result);
    }
}
