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

            return ResponseBase.DefaultResponse(true, "Token gerado com sucesso", token);
        }
        catch (Exception ex)
        {
            return ResponseBase.DefaultResponse(false, $"Erro ao gerar token: {ex.Message}");
        }
    }
}