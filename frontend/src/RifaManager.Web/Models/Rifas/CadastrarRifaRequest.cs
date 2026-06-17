using System.ComponentModel.DataAnnotations;

namespace RifaManager.Web.Models.Rifas;

public sealed class CadastrarRifaRequest
{
    [Required(ErrorMessage = "Informe o nome da rifa.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe a descrição da rifa.")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o valor do bilhete.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor do bilhete deve ser maior que zero.")]
    public double? ValorBilhete { get; set; }

    [Required(ErrorMessage = "Informe a data do sorteio.")]
    public DateTime? DataSorteio { get; set; }

    [Required(ErrorMessage = "Informe o prêmio da rifa.")]
    public string Premio { get; set; } = string.Empty;
}
