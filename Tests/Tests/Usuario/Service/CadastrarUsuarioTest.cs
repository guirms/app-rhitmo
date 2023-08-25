using Tests.Collections;

namespace Tests.Tests.Usuario.Service;

public class CadastrarUsuarioTest : UsuarioFixture
{
    //[Fact(DisplayName = "Usuario já existente")]
    //[Trait("UsuarioService", "CadastrarUsuario")]
    //public void CadastrarUsuario_EnviandoUsuarioJaExistente_DeveRetornarUsuarioExistente()
    //{
    //    var usuarioTeste = new UsuarioCadastroRequest("teste@teste.com", "teste", "teste");

    //    UsuarioCadastroValidatorMock.Setup(u => u.Validate(usuarioTeste)).Returns(new ValidationResult(new ValidationFailure[] { }));
    //    AutenticacaoServiceMock.Setup(a => a.GerarSenhaHashMd5(usuarioTeste.Senha)).Returns("teste");
    //    UsuarioRepositoryMock.Setup(u => u.ConsultarUsuarioIdPorEmailESenha(usuarioTeste.Email, usuarioTeste.Senha)).Returns(10);

    //    _usuarioService = new UsuarioService(AutoMapperMock, UsuarioRepositoryMock.Object, AutenticacaoServiceMock.Object, UsuarioCadastroValidatorMock.Object, UsuarioLoginValidatorMock.Object);

    //    var chamadaMetodo = Assert.Throws<Exception>(() => _usuarioService.CadastrarUsuario(usuarioTeste));

    //    Assert.Equal("Usuário já cadastrado no sistema", chamadaMetodo.Message);
    //}

    //[Fact(DisplayName = "Token nulo")]
    //[Trait("UsuarioService", "CadastrarUsuario")]
    //public void CadastrarUsuario_GerandoTokenNulo_ErroAoGerarToken()
    //{
    //    var usuarioTeste = new UsuarioCadastroRequest("teste@teste.com", "teste", "teste");

    //    UsuarioCadastroValidatorMock.Setup(u => u.Validate(usuarioTeste)).Returns(new ValidationResult(new ValidationFailure[] { }));
    //    UsuarioRepositoryMock.Setup(u => u.SalvarUsuario(It.IsAny<Domain.Models.Customer>())).Returns(10);
    //    AutenticacaoServiceMock.Setup(a => a.GerarSenhaHashMd5(usuarioTeste.Senha)).Returns("teste");
    //    UsuarioRepositoryMock.Setup(u => u.ConsultarUsuarioIdPorEmailESenha(usuarioTeste.Email, "teste")).Returns(0);
    //    AutenticacaoServiceMock.Setup(a => a.GerarTokenSessao(usuarioTeste.Email, usuarioTeste.Senha))
    //        .Returns(string.Empty);

    //    var chamadaMetodo = Assert.Throws<Exception>(() => _usuarioService.CadastrarUsuario(usuarioTeste));

    //    Assert.Equal("Erro ao gerar token de sessão", chamadaMetodo.Message);
    //}
}
