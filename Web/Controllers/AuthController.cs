using Application.Interfaces;
using Application.Objects.Bases;
using Application.Objects.Requests.Autenticacao;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("Token")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("GerarTokenSessao")]
    public JsonResult GenerateSessionToken(AuthTokenRequest authTokenRequest)
    {
        try
        {
            var token = _authService.GenerateSessionToken(authTokenRequest.Email);

            return ResponseBase.ResponderController(true, "Token gerado com sucesso", token);
        }
        catch (Exception e)
        {
            return ResponseBase.ResponderController(false, $"Erro ao gerar token: {e.Message}");
        }
    }
}