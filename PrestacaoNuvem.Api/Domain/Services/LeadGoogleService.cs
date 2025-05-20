using MongoDB.Bson;
using PrestacaoNuvem.Api.Domain.Interfacesk;

namespace PrestacaoNuvem.Api.Domain.Services;

public class LeadGoogleService : ILeadGoogleService
{
    private readonly ILeadRepository _leadGoogleRepository;
    private readonly IMapper _mapper;
    private readonly IEmailManager _emailManager;

    public LeadGoogleService(ILeadRepository leadRepository, IMapper mapper, IEmailManager emailManager)
    {
        _leadGoogleRepository = leadRepository;
        _mapper = mapper;
        _emailManager = emailManager;
    }

    public async Task<bool> DeleteLeadGoogle(ObjectId Id)
    {
        return await _leadGoogleRepository.DeleteLeadAsync(Id);
    }

    public async Task<ICollection<LeadGoogleDtoResponse>> GetAllAsync()
    {
        var result = await _leadGoogleRepository.GetLeadsAsync();
        return _mapper.Map<ICollection<LeadGoogleDtoResponse>>(result);
    }

    public async Task<LeadGoogleDtoResponse> GetAllLeadGoogleAsync(LeadGoogleDtoRequest request)
    {
        var result = await _leadGoogleRepository.GetLeadByFilterId(_mapper.Map<LeadModel>(request));
        return _mapper.Map<LeadGoogleDtoResponse>(result);
    }

    public async Task<LeadGoogleDtoResponse> PostLeadAsync(LeadGoogleDtoRequest request)
    {
        var mapperRequest = _mapper.Map<LeadModel>(request);
        mapperRequest.DataCriacao = DateTime.Now;
        var result = await _leadGoogleRepository.PostLeadAsync(mapperRequest);
        return _mapper.Map<LeadGoogleDtoResponse>(result);
    }

    public async Task<bool> PostLeadEmailAsync(EmailRequestDto request)
    {
        return await _emailManager.SendEmailSmtpAsync(new Email());
    }

    public async Task<LeadGoogleDtoResponse> PutLeadAsync(LeadGoogleDtoRequest request)
    {
        var mapperRequest = _mapper.Map<LeadModel>(request);
        if (mapperRequest != null && mapperRequest.Historico != null && mapperRequest.Historico.Any())
        {
            foreach (var item in mapperRequest.Historico)
            {
                if (item.DataAtualizacao == null && string.IsNullOrEmpty(item.DataAtualizacao.ToString()))
                    item.DataAtualizacao = DateTime.Now;
            }
        }
        var result = await _leadGoogleRepository.PutLeadAsync(new ObjectId(request.Id), mapperRequest);
        return _mapper.Map<LeadGoogleDtoResponse>(result);
    }

    public async Task<int> GetLeadsCountByMonthAsync()
    {
        return await _leadGoogleRepository.GetLeadsCountByMonthAsync();
    }

    public async Task<int> GetLeadsCountByWeekAsync()
    {
        return await _leadGoogleRepository.GetLeadsCountByWeekAsync();
    }

    public async Task<int> GetMeetingsCountAsync()
    {
        return await _leadGoogleRepository.GetMeetingsCountAsync();
    }

    public async Task<(int enviados, int abertos, int respondidos)> GetEmailStatsAsync()
    {
        return await _leadGoogleRepository.GetEmailStatsAsync();
    }

    public async Task<string?> GetTopCategoriaAsync()
    {
        return await _leadGoogleRepository.GetTopCategoriaAsync();
    }

    public async Task<TimeSpan?> GetAverageConversionTimeAsync()
    {
        return await _leadGoogleRepository.GetAverageConversionTimeAsync();
    }
    public async Task<int> GetLeadsCountByStatusAsync(int status)
    {
        return await _leadGoogleRepository.GetLeadsCountByStatusAsync(status);
    }
}
