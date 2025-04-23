namespace PrestacaoNuvem.Api.Dto;

public class EmailRequestDto
{
    public string? Body { get; set; }
    public string? Subject { get; set; }
    public string? Content { get; set; }
    public string? To { get; set; }
    public string? BoxTo { get; set; }
    public string? Cco { get; set; }
    public string? MessageBox { get; set; }
}
