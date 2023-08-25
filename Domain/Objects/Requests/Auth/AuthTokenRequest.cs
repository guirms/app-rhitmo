namespace Application.Objects.Requests.Autenticacao;

public class AuthTokenRequest
{
    public AuthTokenRequest(string email, string senha)
    {
        Email = email;
        Senha = senha;
    }

    public string Email { get; set; }
    public string Senha { get; set; }
}