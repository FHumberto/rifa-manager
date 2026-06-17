namespace RifaManager.Web.Constants;

public static class ApiRoutes
{
    public const string HealthCheck = "/";

    public static class Auth
    {
        public const string Login = "api/v1/Auth/login";
    }

    public static class Rifas
    {
        public const string Base = "api/v1/Rifas";
    }

    public static class Participantes
    {
        public const string Base = "api/v1/Participantes";
    }

    public static class Bilhetes
    {
        public const string Base = "api/v1/Bilhetes";
    }

    public static class Usuario
    {
        public const string Base = "api/v1/Usuario";

        public static string ById(Guid id) => $"{Base}/{id}";
    }
}
