using PrestacaoNuvem.Api.Enumerations;

namespace PrestacaoNuvem.Api.Domain.Model;
public class Endereco
{
    public Endereco(string logradouro, string cidade, string cep, string estado, string numero, string bairro, string complemento, ETipoEndereco tipoEndereco)
    {
        Validation(logradouro, cidade, cep, estado, numero, bairro);
        Logradouro = logradouro;
        Cidade = cidade;
        Cep = cep;
        Estado = estado;
        Numero = numero;
        Bairro = bairro;
        Complemento = complemento;
        TipoEndereco = tipoEndereco;
    }

    private void Validation(string logradouro, string cidade, string cep, string estado, string numero, string bairro)
    {
        if (logradouro.IsMissing())
            throw new ArgumentException("Logradouro não informado");
        if (cidade.IsMissing())
            throw new ArgumentException("Cidade não informado");
        if (cep.IsMissing())
            throw new ArgumentException("CEP não informado");
        if (estado.IsMissing())
            throw new ArgumentException("Estado não informado");
        if (numero.IsMissing())
            throw new ArgumentException("Número não informado");
        if (bairro.IsMissing())
            throw new ArgumentException("Bairro não informado");
    }

    public string Logradouro { get; private set; }
    public string Cidade { get; private set; }
    public string Cep { get; private set; }
    public string Estado { get; private set; }
    public string Numero { get; private set; }
    public string Bairro { get; private set; }
    public string Complemento { get; private set; }
    public ETipoEndereco TipoEndereco { get; private set; }
}
