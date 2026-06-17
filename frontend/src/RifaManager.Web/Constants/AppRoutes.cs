namespace RifaManager.Web.Constants;

public static class AppRoutes
{
    public const string Home = "";
    public const string Login = "login";

    public static class Errors
    {
        public const string NotFound = "not-found";
        public const string Unauthorized = "nao-autorizado";
        public const string ServerError = "erro-interno";
    }

    public static class Rifas
    {
        public const string Listar = "rifas";
        public const string Cadastrar = "rifas/cadastrar";
        public const string Editar = "rifas/editar";
        public const string Detalhes = "rifas/detalhes";
    }

    public static class Participantes
    {
        public const string Listar = "participantes";
        public const string Cadastrar = "participantes/cadastrar";
        public const string Editar = "participantes/editar";
    }

    public static class Bilhetes
    {
        public const string Listar = "bilhetes";
        public const string Pagamento = "bilhetes/pagamento";
    }
}
