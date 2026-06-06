using RifaManager.Domain.Entities;

namespace RifaManager.Domain.Security.Tokens;

public interface IAccessTokenGenerator
{
    string Generate(Usuario usuario);
}
