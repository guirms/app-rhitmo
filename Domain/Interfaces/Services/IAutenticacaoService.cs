namespace Application.Interfaces;

public interface IAutenticacaoService
{
    string GerarTokenSessao(string emailUsuario, string usuarioId);
    string GerarSenhaHashMd5(string senha);
}