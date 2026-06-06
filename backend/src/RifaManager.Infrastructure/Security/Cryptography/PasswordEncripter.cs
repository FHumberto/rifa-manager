using Microsoft.AspNetCore.Identity;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Security.Cryptography;

namespace RifaManager.Infrastructure.Security.Cryptography;

internal sealed class PasswordEncripter : IPasswordEncripter
{
    private readonly PasswordHasher<Usuario> _passwordHasher = new();

    public string Encrypt(string password)
    {
        return _passwordHasher.HashPassword(null!, password);
    }

    public bool IsValid(string password, string passwordHash)
    {
        PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword
        (
            user: null!,
            hashedPassword: passwordHash,
            providedPassword: password
        );

        return result is PasswordVerificationResult.Success
                      or PasswordVerificationResult.SuccessRehashNeeded;
    }
}
