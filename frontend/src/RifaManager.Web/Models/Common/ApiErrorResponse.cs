namespace RifaManager.Web.Models.Common;

public sealed class ApiErrorResponse
{
    public string? Title { get; set; }
    public int? Status { get; set; }
    public string? Detail { get; set; }
    public Dictionary<string, string[]>? Errors { get; set; }

    public string GetDisplayMessage(string fallback)
    {
        if (Errors is { Count: > 0 })
        {
            return string.Join(" ", Errors.SelectMany(error => error.Value));
        }

        return Detail ?? Title ?? fallback;
    }
}
