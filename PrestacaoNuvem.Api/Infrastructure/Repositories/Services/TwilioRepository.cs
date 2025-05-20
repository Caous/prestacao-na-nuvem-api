
namespace PrestacaoNuvem.Api.Infrastructure.Repositories.Services;

public class TwilioRepository : ITwilioRepository
{
    public Task<BrokerLastMessagesResult> GetMessages(Message? filter)
    {
        throw new NotImplementedException();
    }
}
