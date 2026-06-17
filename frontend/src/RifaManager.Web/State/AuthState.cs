namespace RifaManager.Web.State;

public sealed class AuthState
{
    public string? AccessToken { get; private set; }

    public bool IsAuthenticated => !string.IsNullOrWhiteSpace(AccessToken);

    public void SignIn(string accessToken) => AccessToken = accessToken;

    public void SignOut() => AccessToken = null;
}
