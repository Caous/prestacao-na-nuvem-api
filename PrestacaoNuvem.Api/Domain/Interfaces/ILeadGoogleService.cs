using MongoDB.Bson;

namespace PrestacaoNuvem.Api.Domain.Interfaces;

public interface ILeadGoogleService
{
    Task<ICollection<LeadGoogleDtoResponse>> GetAllAsync();
    Task<LeadGoogleDtoResponse> GetAllLeadGoogleAsync(LeadGoogleDtoRequest request);
    Task<LeadGoogleDtoResponse> PostLeadAsync(LeadGoogleDtoRequest request);
    Task<bool> DeleteLeadGoogle(ObjectId Id);
    Task<bool> PostLeadEmailAsync(EmailRequestDto request);
    Task<LeadGoogleDtoResponse> PutLeadAsync(LeadGoogleDtoRequest request);
}
