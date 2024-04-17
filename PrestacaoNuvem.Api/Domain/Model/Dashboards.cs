namespace PrestacaoNuvem.Api.Domain.Model;

public class Dashboards
{
    public class CategoriaAgrupado
    {
        public string Categoria { get; set; }
        public double Valor { get; set; }
    }

    public class SubCategoriaAgrupado
    {
        public string Titulo { get; set; }
        public double Valor { get; set; }
    }

    public class FaturamentoDiario
    {

        public string DateRef { get; set; }
        public double Valor { get; set; }
    }

    public class FaturamentoMes
    {
        public string DateRef { get; set; }
        public double Valor { get; set; }
    }

    public class ProdutoAgrupado
    {
        public string Nome { get; set; }
        public double Valor { get; set; }
    }
    public class ProdutoAgrupadoMarca
    {
        public string Marca { get; set; }
        public double Valor { get; set; }
    }
}
