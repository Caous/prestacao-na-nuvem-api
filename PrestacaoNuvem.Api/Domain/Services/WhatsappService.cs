
namespace PrestacaoNuvem.Api.Domain.Services
{
    public class WhatsappService : IWhatsappService
    {
        private readonly ITwilioRepository _repositoryTwilio;
        //private readonly IMongoRepository _mongoRepository;
        private readonly IMapper _mapper;

        //public WhatsappService(ITwilioRepository repositoryTwilio, IMongoRepository mongoRepository, IMapper mapper)
        //{
        //    _repositoryTwilio = repositoryTwilio;
        //    _mongoRepository = mongoRepository;
        //    _mapper = mapper;
        //}

        public Task<int?> CountMessagesPending(Message? filter)
        {
            throw new NotImplementedException();
        }

        public Task<int?> CountNewCustomerMonth(Message? filter)
        {
            throw new NotImplementedException();
        }

        public Task<int?> CountNewTicketsSupport(Message? filter)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<MessagesResponseDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<MessageDto>> GetLastMessagens(Message? filter)
        {
            throw new NotImplementedException();
        }

        public Task<BrokerLastMessagesResult> GetMessages(Message? filter)
        {
            throw new NotImplementedException();
        }

        public Task<int?> RecurrenceCustomer(Message? filter)
        {
            throw new NotImplementedException();
        }

        public Task RegisterMessages(ICollection<Message> messages)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Message>> ValitadorMessageAsync(string request)
        {
            throw new NotImplementedException();
        }
    }
}
