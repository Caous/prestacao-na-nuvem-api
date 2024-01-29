namespace SmartOficina.Api.Domain.Model;

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

    public class FaturamentoDiario {

        public string DateRef { get; set; }
        public double Valor { get; set; }
    }
}
