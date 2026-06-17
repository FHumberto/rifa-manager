namespace RifaManager.Web.Models.Rifas;

public sealed class SortearRifaResponse
{
    public Guid RifaId { get; set; }
    public Guid BilheteId { get; set; }
    public int NumeroBilhete { get; set; }
    public Guid ParticipanteId { get; set; }
    public string ParticipanteNome { get; set; } = string.Empty;
    public string ParticipanteTelefone { get; set; } = string.Empty;
}
