namespace PrestacaoNuvem.Api.Dto;

public class DashboardDto
{
    public class DashboardOSMesDto
    {
        public long Valor { get; set; }
    }
    public class DashboardProdutosNovos
    {
        public long Valor { get; set; }
    }
    public class DashboardReceitaCategoriaDto
    {
        public string Categoria { get; set; }
        public double Valor { get; set; }
    }
    public class DashboardReceitaNomeProdutoDto
    {
        public string Nome { get; set; }
        public double Valor { get; set; }
    }
    public class DashboardReceitaMarcaProdutoDto
    {
        public string Marca { get; set; }
        public double Valor { get; set; }
    }
    public class DashboardReceitaDiariaDto
    {
        public string DateRef { get; set; }
        public double Valor { get; set; }
    }
    public class DashboardReceitaMesAgrupadoDto
    {
        public string DateRef { get; set; }
        public double Valor { get; set; }
    }
    public class DashboardReceitaMesDto
    {
        public decimal Valor { get; set; }
    }
    public class DashboardReceitaSubCaterogiaDto
    {
        public string Titulo { get; set; }
        public double Valor { get; set; }
    }

    public class DashboardLastServices {

        public string ServicoCategoria { get; set; }
        public float ValorServico { get; set; }
        public float ValorProduto { get; set; }        
        public float Total { get; set; }
        public string Cpf { get; set; }

    }

     public class DashboardBrands
    {
        public string Marca { get; set; }

        public int Quantidade { get; set; }
        
    }

    public class DashboardTypesVehicle
    {
        public EVeiculoTipo Tipo { get; set; }
        public int Quantidade { get; set; }
    }
}
