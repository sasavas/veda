using Veda.Application.Ports;

namespace Veda.Infrastructure.ServiceImplementations;

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string providedPassword, string savedPasswordHash)
    {
        return BCrypt.Net.BCrypt.Verify(providedPassword, savedPasswordHash);
    }
}