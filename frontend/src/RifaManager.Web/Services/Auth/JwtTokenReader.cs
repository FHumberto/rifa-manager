using System.Text;
using System.Text.Json;

namespace RifaManager.Web.Services.Auth;

public static class JwtTokenReader
{
    private static readonly string[] UserIdClaimNames =
    [
        "sub",
        "id",
        "userId",
        "usuarioId",
        "nameid",
        "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
    ];

    public static Guid? GetUserId(string accessToken)
    {
        string[] tokenParts = accessToken.Split('.');

        if (tokenParts.Length < 2)
        {
            return null;
        }

        byte[] payloadBytes = Convert.FromBase64String(NormalizeBase64Url(tokenParts[1]));
        using JsonDocument payload = JsonDocument.Parse(payloadBytes);

        foreach (string claimName in UserIdClaimNames)
        {
            if (payload.RootElement.TryGetProperty(claimName, out JsonElement claim)
                && claim.ValueKind == JsonValueKind.String
                && Guid.TryParse(claim.GetString(), out Guid userId))
            {
                return userId;
            }
        }

        return null;
    }

    private static string NormalizeBase64Url(string value)
    {
        StringBuilder builder = new(value.Replace('-', '+').Replace('_', '/'));

        while (builder.Length % 4 != 0)
        {
            builder.Append('=');
        }

        return builder.ToString();
    }
}
