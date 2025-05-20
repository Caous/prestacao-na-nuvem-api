namespace PrestacaoNuvem.Api.Dto;

public class GroupMessageResponseDto
{
    public string Id { get; set; }
    public string ToUser { get; set; }
    public string FromUser { get; set; }
    public DateTime Date { get; set; }
    public string Text { get; set; }
}
