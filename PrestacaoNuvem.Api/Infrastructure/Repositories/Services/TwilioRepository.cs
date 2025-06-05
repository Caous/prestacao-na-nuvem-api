
using System.Globalization;
using Twilio;
using Twilio.Base;
using Twilio.Rest.Api.V2010.Account;


namespace PrestacaoNuvem.Api.Infrastructure.Repositories.Services;

public class TwilioRepository : ITwilioRepository
{
    private readonly IConfiguration _configuration;
    private TwilioOptions _twilioOptions;
    public TwilioRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        ConfigurationTwilio();
    }

    private void ConfigurationTwilio()
    {
        _twilioOptions = new TwilioOptions() { Number = _configuration["TwilioConfiguration:Number"].ToString(), Password = _configuration["TwilioConfiguration:Password"].ToString(), Username = _configuration["TwilioConfiguration:UserName"].ToString() };
    }

    public async Task<BrokerLastMessagesResult> GetMessages(Message? filter)
    {
        string? dataInicial = filter == null ? "" : filter.DateCreate.HasValue ? filter.DateCreate.Value.ToString() : "";
        string? dataFinal = filter == null ? "" : filter.DateUpdated.HasValue ? filter.DateUpdated.Value.ToString() : "";

        TwilioClient.Init(_twilioOptions.Username, _twilioOptions.Password);

        (DateTime dataInicialTratada, DateTime dataFinalTratada) = ConverterStringsParaDateTime(dataInicial, dataFinal);

        ResourceSet<MessageResource> recordsTwilio = await MessageResource.ReadAsync(dateSentAfter: dataInicialTratada, dateSentBefore: dataFinalTratada);

        return ObterListaConvertidaMessage(recordsTwilio, _twilioOptions.Number);
    }
    static (DateTime dataInicial, DateTime dataFinal) ConverterStringsParaDateTime(string dataInicialString, string dataFinalString, string formato = "dd/MM/yyyy")
    {
        DateTime dataInicial, dataFinal;

        if (string.IsNullOrEmpty(dataInicialString) && string.IsNullOrEmpty(dataFinalString))
        {

            if (!DateTime.TryParseExact(dataInicialString, formato, CultureInfo.InvariantCulture, DateTimeStyles.None, out dataInicial))
            {
                dataInicial = DateTime.Now.AddDays(-120);
            }

            if (!DateTime.TryParseExact(dataFinalString, formato, CultureInfo.InvariantCulture, DateTimeStyles.None, out dataFinal))
            {
                dataFinal = DateTime.Now.AddDays(1);
            }
            return (dataInicial, dataFinal);
        }

        return (Convert.ToDateTime(dataInicialString), Convert.ToDateTime(dataFinalString));
    }

    static BrokerLastMessagesResult ObterListaConvertidaMessage(ResourceSet<MessageResource> recordsTwilio, string twilioOptionsNumber)
    {
        BrokerLastMessagesResult result = new BrokerLastMessagesResult()
        {
            DateSentAfter = DateTime.UtcNow,
            DateSentBefore = DateTime.UtcNow
        };

        foreach (var record in recordsTwilio)
        {
            if (record.From.ToString() == $"whatsapp:{twilioOptionsNumber}" || record.To == $"whatsapp:{twilioOptionsNumber}")
            {
                var isBotNumberFrom = record.From.ToString() == $"whatsapp:{twilioOptionsNumber}";
                var userNumber = isBotNumberFrom ? record.To : record.From.ToString();
                var conversationId = $"{record.DateSent?.ToString("yyMMdd")}-{userNumber}";

                result.Messages.Add(new Message()
                {
                    Id = record.Sid,
                    ConversationId = conversationId,
                    FromUser = record.From.ToString(),
                    ToUser = record.To,
                    DateSend = record.DateSent,
                    DateCreate = record.DateCreated,
                    DateUpdated = record.DateUpdated,
                    Text = record.Body
                });
            }
        }

        result.Messages.Select(m => m.ConversationId).Distinct();

        result.Messages.Reverse();

        return result;
    }

    public async Task<int?> CountNewCustomerMonth()
    {
        TwilioClient.Init(_twilioOptions.Username, _twilioOptions.Password);

        DateTime dataInicial = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        DateTime dataFinal = dataInicial.AddMonths(1).AddDays(-1);

        ResourceSet<MessageResource> recordsTwilio = await MessageResource.ReadAsync(dateSentAfter: dataInicial, dateSentBefore: dataFinal);

        if (recordsTwilio == null)
            return 0;

        return recordsTwilio.Count();

    }
}
