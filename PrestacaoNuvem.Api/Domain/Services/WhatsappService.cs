
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace PrestacaoNuvem.Api.Domain.Services
{
    public class WhatsappService : IWhatsappService
    {
        private readonly ITwilioRepository _repositoryTwilio;
        private readonly IMongoRepository _mongoRepository;
        private readonly IMapper _mapper;

        public WhatsappService(ITwilioRepository repositoryTwilio, IMongoRepository mongoRepository, IMapper mapper)
        {
            _repositoryTwilio = repositoryTwilio;
            _mongoRepository = mongoRepository;
            _mapper = mapper;
        }

        public async Task<int?> CountMessagesPending(Message? filter)
        {
            return 1;
        }

        public async Task<int?> CountNewCustomerMonth(Message? filter)
        {
            if (filter == null)
                filter = new Message() { DateCreate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), DateUpdated = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1) };

            var result = await _repositoryTwilio.GetMessages(filter);
            var count = result.Messages.Select(x => x.ConversationId).Distinct().Count();
            return count;
        }

        public async Task<int?> CountNewTicketsSupport(Message? filter)
        {
            return 1;
        }

        public async Task<ICollection<MessagesResponseDto>> GetAllAsync()
        {
            var resultMongo = await _mongoRepository.GetAllAsync();
            var result = _mapper.Map<ICollection<MessagesResponseDto>>(resultMongo);
            foreach (var item in result)
            {
                string pattern = @"whatsapp:\+55(\d{2})(\d{5})(\d{4})";

                var match = Regex.Match(item.ConversationId, pattern);
                if (match.Success)
                {
                    string ddd = match.Groups[1].Value;
                    string part1 = match.Groups[2].Value;
                    string part2 = match.Groups[3].Value;

                    string formattedNumber = $"+55 ({ddd}) {part1}-{part2}";
                    item.PhoneNumber = formattedNumber;
                }
                item.DateConversation = item.Messages.FirstOrDefault().Date.ToString("dd/MM/yyyy");
            }
            return result;
        }

        public async Task<ICollection<MessageDto>> GetLastMessagens(Message? filter)
        {
            var result = await _repositoryTwilio.GetMessages(filter);

            var resultGroup = result.Messages.GroupBy(x => x.ToUser).ToList();

            List<MessageDto> resultFinally = new();

            foreach (var item in resultGroup)
            {
                resultFinally.Add(new MessageDto()
                {
                    CustomerName = item.First().ToUser,
                    DateMessage = item.First().DateCreate.Value,
                    Id = item.First().ConversationId,
                    PhoneNumber = item.Key,
                    ProtocolNumber = "XptoSp",
                    Service = "Suporte",
                    Status = "Encerrado"
                });
            }

            return resultFinally;
        }

        public async Task<BrokerLastMessagesResult> GetMessages(Message? filter)
        {
            return await _repositoryTwilio.GetMessages(filter);
        }

        public async Task<int?> RecurrenceCustomer(Message? filter)
        {
            if (filter == null)
                filter = new Message() { DateCreate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), DateUpdated = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1) };

            var recurrenceCustomer = new List<string>() { "whatsapp:+5511942616650", "whatsapp:+551193143-8599" };

            var result = await _repositoryTwilio.GetMessages(filter);
            var count = result.Messages.Select(x => recurrenceCustomer.Contains(x.ToUser)).Distinct().Count();
            return count;
        }

        public async Task RegisterMessages(ICollection<Message> messages)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Message>> ValitadorMessageAsync(string request)
        {
            if (!string.IsNullOrEmpty(request))
            {
                ICollection<Message> messages = JsonConvert.DeserializeObject<ICollection<Message>>(request);
                return messages;
            }
            return null;
        }
    }
}
