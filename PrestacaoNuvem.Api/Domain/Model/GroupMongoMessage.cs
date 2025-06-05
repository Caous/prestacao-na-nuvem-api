namespace PrestacaoNuvem.Api.Domain.Model;

public class GroupMongoMessage
{
    public string Id { get; set; }
    public string ToUser { get; set; }
    public string FromUser { get; set; }
    public DateTime Date { get; set; }
    public string Text { get; set; }
}
