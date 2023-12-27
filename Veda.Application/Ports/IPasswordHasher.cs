namespace Veda.Application.Ports;

public interface IPasswordHasher
{
    public string HashPassword(string password);

    public bool VerifyPassword(string providedPassword, string savedPasswordHash);
}