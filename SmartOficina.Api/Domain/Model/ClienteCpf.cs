namespace SmartOficina.Api.Domain.Model;

public class ClienteCpf
{
    public ClienteCpf()
    {

    }
    public ClienteCpf(string cpf, string nome, string email, string rg, IEnumerable<Endereco> enderecos, DateTime dataNascimento, string uf, string cidade, string sexo, string estadoCivil, string pis, string graduacao, IEnumerable<Telefone> telefones, string pai, string mae)
    {
        Cpf = new Cpf(cpf);
        Nome = nome;
        Email = new Email(email);
        Rg = rg;
        Enderecos = enderecos;
        DataNascimento = dataNascimento;
        Naturalidade = new Naturalidade(uf, cidade);
        Sexo = new Sexo(sexo);
        EstadoCivil = new EstadoCivil(estadoCivil);
        Pis = new Pis(pis);
        Graduacao = new Graduacao(graduacao);
        Telefones = telefones;
        Pais = new Pais(pai, mae);
    }

    public Cpf Cpf { get; private set; }
    public string Nome { get; private set; }
    public Email Email { get; private set; }
    public string Rg { get; private set; }
    public IEnumerable<Endereco> Enderecos { get; private set; }
    public DateTime DataNascimento { get; private set; }
    public Naturalidade Naturalidade { get; private set; }
    public Sexo Sexo { get; private set; }
    public EstadoCivil EstadoCivil { get; private set; }
    public Pis Pis { get; private set; }
    public Graduacao Graduacao { get; private set; }
    public IEnumerable<Telefone> Telefones { get; private set; }
    public Pais Pais { get; private set; }
}
