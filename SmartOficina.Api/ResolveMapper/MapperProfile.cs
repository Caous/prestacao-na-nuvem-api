namespace SmartOficina.Api.ResolveMapper;

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
        CreateMap<UserAutenticationDto, UserAutentication>()
                .ForMember(p => p.NormalizedEmail, opt => opt.MapFrom(src => src.Email))
                .ForMember(p => p.Email, opt => opt.MapFrom(src => src.Email))
                .ReverseMap();

    }
}
