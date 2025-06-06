﻿using PrestacaoNuvem.Api.Domain.Model.Dashboard;
using static PrestacaoNuvem.Api.Domain.Model.Dashboards;
using static PrestacaoNuvem.Api.Dto.DashboardDto;

namespace PrestacaoNuvem.Api.ResolveMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<ClienteDto, Cliente>().ReverseMap();
        CreateMap<HistoricoClienteDto, HistoricoCliente>().ReverseMap();
        CreateMap<PrestadorDto, Prestador>().ReverseMap();
        CreateMap<VeiculoDto, Veiculo>().ReverseMap();
        CreateMap<PrestacaoServicoDto, PrestacaoServico>().ReverseMap();
        CreateMap<CategoriaServicoDto, CategoriaServico>().ReverseMap();
        CreateMap<SubCategoriaServicoDto, SubCategoriaServico>().ReverseMap();
        CreateMap<ServicoDto, Servico>().ReverseMap();
        CreateMap<ProdutoDto, Produto>().ReverseMap();
        CreateMap<ContratoDto, Contrato>().ReverseMap();
        CreateMap<FuncionarioPrestadorDto, FuncionarioPrestador>().ReverseMap();
        CreateMap<DashboardReceitaCategoriaDto, CategoriaAgrupado>().ReverseMap();
        CreateMap<DashboardReceitaDiariaDto, FaturamentoDiario>().ReverseMap();
        CreateMap<DashboardReceitaDiariaDto, DashboardReceitaDiaria>().ReverseMap();
        CreateMap<DashboardOSMesDto, DashboardOSMes>().ReverseMap();
        CreateMap<DashboardReceitaMesDto, DashboardReceitaMes>().ReverseMap();
        CreateMap<DashboardReceitaSubCaterogiaDto, SubCategoriaAgrupado>().ReverseMap();
        CreateMap<DashboardReceitaNomeProdutoDto, ProdutoAgrupado>().ReverseMap();
        CreateMap<DashboardReceitaMarcaProdutoDto, ProdutoAgrupadoMarca>().ReverseMap();
        CreateMap<FilialDto, Filial>().ReverseMap();
        CreateMap<OrdemVendaDto, OrdemVenda>().ReverseMap();
        CreateMap<DashboardReceitaMesAgrupadoDto, FaturamentoMes>().ReverseMap();
        CreateMap<UserModelDto, UserModel>()
                    .ForMember(p => p.NormalizedEmail, opt => opt.MapFrom(src => src.Email))
                    .ForMember(p => p.Email, opt => opt.MapFrom(src => src.Email))
                    .ReverseMap();

        CreateMap<PrestadorLoginDto, UserModelDto>()
                     .ReverseMap();

        CreateMap<PrestadorCadastroDto, UserModel>()
                    .ReverseMap();

        CreateMap<LeadGoogleDtoResponse, LeadModel>().ReverseMap();
        CreateMap<LeadGoogleDtoRequest, LeadModel>().ReverseMap();
        CreateMap<LeadGoogleDtoRequest, LeadGoogleDtoResponse>().ReverseMap();
        CreateMap<HistoricoLeadDto, HistoricoLead>().ReverseMap();
        CreateMap<HistoricoLeadDto, HistoricoLeadResponse>().ReverseMap();
        CreateMap<HistoricoLeadResponse, HistoricoLead>().ReverseMap();
        CreateMap<MessagesResponseDto, GroupMongo>()
    .ForMember(p => p.Messages, opt => opt.MapFrom(src => src.Messages)).ReverseMap();
        CreateMap<GroupMessageResponseDto, GroupMongoMessage>().ReverseMap();

    }
}
