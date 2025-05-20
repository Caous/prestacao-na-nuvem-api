namespace PrestacaoNuvem.Api.Domain.Model;

public class Message
{
    public Message()
    {
        Id = Guid.NewGuid().ToString();
    }

    public string Id { get; set; }
    public string ConversationId { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateSend { get; set; }
    public DateTime? DateUpdated { get; set; }
    public string FromUser { get; set; }
    public string ToUser { get; set; }
    public string Text { get; set; }
    public bool NoReply { get; set; }
}
