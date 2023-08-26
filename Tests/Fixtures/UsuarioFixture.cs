using Application.Interfaces;
using Application.Objects.Requests.Usuario;
using Application.Services;
using FluentValidation;
using Infra.Data.Interfaces;
using Moq;
using Tests.Setup;
using Xunit;

namespace Tests.Collections;

[CollectionDefinition(nameof(UsuarioFixture))]
public class UsuarioFixture : TestsSetup
{
    protected CustomerService _usuarioService;
    protected readonly Mock<ICustomerRepository> UsuarioRepositoryMock;
    protected readonly Mock<IAuthService> AutenticacaoServiceMock;
    protected readonly Mock<IValidator<AddCustomerRequest>> UsuarioLoginValidatorMock = new();

    public UsuarioFixture()
    {
        UsuarioRepositoryMock = CriarInstancia<ICustomerRepository>();
        AutenticacaoServiceMock = CriarInstancia<IAuthService>();

        //_usuarioService = new UsuarioService(AutoMapperMock,
        //    UsuarioRepositoryMock.Object,
        //    AutenticacaoServiceMock.Object,
        //    UsuarioCadastroValidatorMock.Object,
        //    UsuarioLoginValidatorMock.Object);
    }

}