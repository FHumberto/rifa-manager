namespace RifaManager.Web.Models.Rifas;

public sealed class ListarRifasResponse
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public double ValorBilhete { get; set; }
    public DateOnly DataSorteio { get; set; }
    public string Premio { get; set; } = string.Empty;
    public bool Encerrada { get; set; }
}
