using Tests.Collections;

namespace Tests.Tests.Usuario.Validator;

public class UsuarioValidateTest : UsuarioFixture
{
    //[Fact(DisplayName = "Email inválido")]
    //[Trait("UsuarioValidator", "UsuarioCadastroValidator")]
    //public void CadastrarUsuario_EnviandoEmailInvalido_DeveRetornarEmailInvalido()
    //{
    //    var usuarioTeste = new UsuarioCadastroRequest("emailinvalido", "teste123", "teste123");

    //    var chamadaMetodo = UsuarioCadastroInstantiedValidator.TestValidate(usuarioTeste);
    //    chamadaMetodo.ShouldHaveValidationErrorFor(usuario => usuario.Email);

    //    Assert.Equal("Email em formato inválido", chamadaMetodo.Errors.FirstOrDefault()?.ToString());
    //}

    //[Fact(DisplayName = "Email nulo")]
    //[Trait("UsuarioValidator", "UsuarioCadastroValidator")]
    //public void CadastrarUsuario_EnviandoEmailNulo_DeveRetornarEmailInvalido()
    //{
    //    var usuarioTeste = new UsuarioCadastroRequest("", "teste", "teste");

    //    var chamadaMetodo = UsuarioCadastroInstantiedValidator.TestValidate(usuarioTeste);
    //    chamadaMetodo.ShouldHaveValidationErrorFor(usuario => usuario.Email);

    //    Assert.Equal("Email em formato inválido", chamadaMetodo.Errors.FirstOrDefault()?.ToString());
    //}

    //[Fact(DisplayName = "Senha nula")]
    //[Trait("UsuarioValidator", "UsuarioCadastroValidator")]
    //public void CadastrarUsuario_EnviandoSenhaNula_DeveRetornarSenhaNula()
    //{
    //    var usuarioTeste = new UsuarioCadastroRequest("testeunitario@teste.com", "", "");

    //    var chamadaMetodo = UsuarioCadastroInstantiedValidator.TestValidate(usuarioTeste);
    //    chamadaMetodo.ShouldHaveValidationErrorFor(usuario => usuario.Senha);

    //    Assert.Equal("Senha nula é inválida", chamadaMetodo.Errors.FirstOrDefault()?.ToString());
    //}

    //[Fact(DisplayName = "Email inválido")]
    //[Trait("UsuarioValidator", "UsuarioLoginValidator")]
    //public void RealizarLogin_EnviandoEmailInvalido_DeveRetornarEmailInvalido()
    //{
    //    var usuarioTeste = new SaveCustomerRequest("emailinvalido", "teste123");

    //    var chamadaMetodo = UsuarioLoginInstantiedValidator.TestValidate(usuarioTeste);
    //    chamadaMetodo.ShouldHaveValidationErrorFor(usuario => usuario.Email);

    //    Assert.Equal("Email em formato inválido", chamadaMetodo.Errors.FirstOrDefault()?.ToString());
    //}

    //[Fact(DisplayName = "Email nulo")]
    //[Trait("UsuarioValidator", "UsuarioLoginValidator")]
    //public void RealizarLogin_EnviandoEmailNulo_DeveRetornarEmailInvalido()
    //{
    //    var usuarioTeste = new SaveCustomerRequest("", "teste123");

    //    var chamadaMetodo = UsuarioLoginInstantiedValidator.TestValidate(usuarioTeste);
    //    chamadaMetodo.ShouldHaveValidationErrorFor(usuario => usuario.Email);

    //    Assert.Equal("Email em formato inválido", chamadaMetodo.Errors.FirstOrDefault()?.ToString());
    //}

}
