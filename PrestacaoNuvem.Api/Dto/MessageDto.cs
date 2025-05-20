namespace PrestacaoNuvem.Api.Dto;

public class MessageDto
{
    public string PhoneNumber { get; set; }
    public string CustomerName { get; set; }
    public string ProtocolNumber { get; set; }
    public DateTime DateMessage { get; set; }
    public string Service { get; set; }
    public string Status { get; set; }
    public string Id { get; set; }
}
