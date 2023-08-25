namespace Application.Interfaces;

public interface IAuthService
{
    string GenerateSessionToken(string email);
}