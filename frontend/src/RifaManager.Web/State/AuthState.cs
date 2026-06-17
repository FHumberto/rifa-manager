namespace RifaManager.Web.State;

public sealed class AuthState
{
    public string? AccessToken { get; private set; }
    public Guid? UserId { get; private set; }
    public string? UserName { get; private set; }
    public string? UserEmail { get; private set; }
    public string? UserProfile { get; private set; }

    public bool IsAuthenticated => !string.IsNullOrWhiteSpace(AccessToken);

    public void SignIn(string accessToken, Guid userId, string userName, string userEmail, string userProfile)
    {
        AccessToken = accessToken;
        UserId = userId;
        UserName = userName;
        UserEmail = userEmail;
        UserProfile = userProfile;
    }

    public void SignOut()
    {
        AccessToken = null;
        UserId = null;
        UserName = null;
        UserEmail = null;
        UserProfile = null;
    }
}
