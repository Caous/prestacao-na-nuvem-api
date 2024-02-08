using static PrestacaoNuvem.Api.Domain.Model.Dashboards;
using static PrestacaoNuvem.Api.Dto.DashboardDto;

namespace PrestacaoNuvem.Api.ResolveMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<ClienteDto, Cliente>().ReverseMap();
        CreateMap<PrestadorDto, Prestador>().ReverseMap();
        CreateMap<VeiculoDto, Veiculo>().ReverseMap();
        CreateMap<PrestacaoServicoDto, PrestacaoServico>().ReverseMap();
        CreateMap<CategoriaServicoDto, CategoriaServico>().ReverseMap();
        CreateMap<SubCategoriaServicoDto, SubCategoriaServico>().ReverseMap();
        CreateMap<ServicoDto, Servico>().ReverseMap();
        CreateMap<ProdutoDto, Produto>().ReverseMap();
        CreateMap<FuncionarioPrestadorDto, FuncionarioPrestador>().ReverseMap();
        CreateMap<DashboardReceitaCategoriaDto, CategoriaAgrupado>().ReverseMap();
        CreateMap<DashboardReceitaDiariaDto, FaturamentoDiario>().ReverseMap();
        CreateMap<DashboardReceitaSubCaterogiaDto, SubCategoriaAgrupado>().ReverseMap();

    }
}
