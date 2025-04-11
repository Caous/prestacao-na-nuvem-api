namespace PrestacaoNuvem.Api.Dto
{
    public class ContratoRequestDto
    {
        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }
        public string EnderecoEmpresa { get; set; }
        public string RepresentanteLegal { get; set; }
        public string CpfRepresentante { get; set; }
        public string EnderecoRepresentante { get; set; }
        public string TituloServico { get; set; }
        public ServicoDto[] Servicos { get; set; }
    }
}
