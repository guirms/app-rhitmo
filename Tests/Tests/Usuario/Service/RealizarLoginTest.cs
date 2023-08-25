using Application.Objects.Requests.Usuario;
using FluentValidation.Results;
using Moq;
using Tests.Collections;
using Xunit;

namespace Tests.Tests.Usuario.Service;

public class RealizarLoginTest : UsuarioFixture
{
    [Fact(DisplayName = "Id de usuário nulo")]
    [Trait("UsuarioAppService", "AlterarSenha")]
    public void RealizarLogin_EnviandoLoginInvalido_EmailOuSenhaInvalidos()
    {
        var usuarioTeste = new SaveCustomerRequest("teste@teste.com", "teste123");

        UsuarioLoginValidatorMock.Setup(u => u.Validate(usuarioTeste)).Returns(new ValidationResult(new ValidationFailure[] { }));
        UsuarioRepositoryMock.Setup(u => u.ConsultarUsuarioIdPorEmailESenha(usuarioTeste.Email, usuarioTeste.Senha)).Returns(0);

        var chamadaMetodo = Assert.Throws<NullReferenceException>(() => _usuarioService.RealizarLogin(usuarioTeste));

        Assert.Equal("Usuário ou senha inválidos", chamadaMetodo.Message);
    }

    [Fact(DisplayName = "Id de usuário nulo")]
    [Trait("UsuarioAppService", "AlterarSenha")]
    public void RealizarLogin_GerandoTokenNulo_ErroAoGerarToken()
    {
        var usuarioTeste = new SaveCustomerRequest("teste@teste.com", "teste123");

        UsuarioLoginValidatorMock.Setup(u => u.Validate(usuarioTeste)).Returns(new ValidationResult(new ValidationFailure[] { }));
        AutenticacaoServiceMock.Setup(a => a.GerarSenhaHashMd5(usuarioTeste.Senha)).Returns("teste");
        UsuarioRepositoryMock.Setup(u => u.ConsultarUsuarioIdPorEmailESenha(usuarioTeste.Email, "teste")).Returns(10);
        AutenticacaoServiceMock.Setup(u => u.GerarTokenSessao(usuarioTeste.Email, It.IsAny<string>())).Returns(string.Empty);

        var chamadaMetodo = Assert.Throws<Exception>(() => _usuarioService.RealizarLogin(usuarioTeste));

        Assert.Equal("Erro ao gerar token de sessão", chamadaMetodo.Message);
    }
}