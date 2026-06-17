namespace RifaManager.Web.Models.Common;

public sealed class ApiErrorResponse
{
    public string? Title { get; set; }
    public int? Status { get; set; }
    public string? Detail { get; set; }
}
