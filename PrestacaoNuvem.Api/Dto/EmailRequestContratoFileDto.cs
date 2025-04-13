namespace PrestacaoNuvem.Api.Dto;

public class EmailRequestContratoFileDto: EmailRequestDto
{
    public IFormFile PdfAnexo { get; set; }
}
