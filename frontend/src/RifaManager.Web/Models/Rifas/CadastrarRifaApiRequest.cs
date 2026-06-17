using System.Text.Json.Serialization;

namespace RifaManager.Web.Models.Rifas;

public sealed class CadastrarRifaApiRequest
{
    [JsonPropertyName("nome")]
    public string Nome { get; set; } = string.Empty;

    [JsonPropertyName("descricao")]
    public string Descricao { get; set; } = string.Empty;

    [JsonPropertyName("valorBilhete")]
    public double ValorBilhete { get; set; }

    [JsonPropertyName("dataSorteio")]
    public string DataSorteio { get; set; } = string.Empty;

    [JsonPropertyName("premio")]
    public string Premio { get; set; } = string.Empty;
}
