namespace PrestacaoNuvem.Api.Infrastructure.Repositories.Interfaces;

public interface ITwilioRepository
{
    Task<BrokerLastMessagesResult> GetMessages(Message? filter);
}
