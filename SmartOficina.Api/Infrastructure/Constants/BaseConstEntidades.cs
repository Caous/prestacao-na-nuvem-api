using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace SmartOficina.Api.Infrastructure.Constants;

public class BaseConstEntidades
{
    public const string NomeValidation = "Por favor informar o nome";
    public const string EmailValidation = "Por favor informar o e-mail";
    public const string TelefoneValidation = "Por favor informar o telefone";
    public const string TituloValidation = "Por favor informar o título";
    public const string DescricaoValidation = "Por favor informar a descrição";
    public const string RGValidation = "Por favor informar o RG";
    public const string CPFValidation = "Por favor informar o CPF";
    public const string EnderecoValidation = "Por favor informar o endereço";
    public const string CargoValidation = "Por favor informar um cargo valido";
    public const string MarcaValidation = "Por favor informar uma marca valido";
    public const string ModeloValidation = "Por favor informar um modelo valido";
    public const string DataValidadeValidation = "Por favor informar uma data de validade valida";
    public const string GarantiaValidation   = "Por favor informar uma garantia valida";
    public const string ValorCompraValidation = "Por favor informar um valor de compra valido";
    public const string ValorVendaValidation = "Por favor informar um valor de venda valido";
}
