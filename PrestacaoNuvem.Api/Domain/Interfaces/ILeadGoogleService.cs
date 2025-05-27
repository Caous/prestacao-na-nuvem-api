using MongoDB.Bson;

namespace PrestacaoNuvem.Api.Domain.Interfaces;

public interface ILeadGoogleService
{
    Task<ICollection<LeadGoogleDtoResponse>> GetAllAsync(LeadGoogleDtoRequest filter);
    Task<LeadGoogleDtoResponse> GetAllLeadGoogleAsync(LeadGoogleDtoRequest request);
    Task<LeadGoogleDtoResponse> PostLeadAsync(LeadGoogleDtoRequest request);
    Task<bool> DeleteLeadGoogle(ObjectId Id);
    Task<bool> PostLeadEmailAsync(EmailRequestDto request);
    Task<LeadGoogleDtoResponse> PutLeadAsync(LeadGoogleDtoRequest request);
    Task<int> GetLeadsCountByMonthAsync();
    Task<int> GetLeadsCountByWeekAsync();
    Task<int> GetMeetingsCountAsync();
    Task<(int enviados, int abertos, int respondidos)> GetEmailStatsAsync();
    Task<string?> GetTopCategoriaAsync();
    Task<TimeSpan?> GetAverageConversionTimeAsync();
    Task<int> GetLeadsCountByStatusAsync(int status);

}
