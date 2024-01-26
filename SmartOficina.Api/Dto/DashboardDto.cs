namespace SmartOficina.Api.Dto;

public class DashboardDto
{
    public class DashboardOSMes
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
    public class DashboardReceitaDiariaDto
    {
        public DateTime DateRef { get; set; }
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
}
