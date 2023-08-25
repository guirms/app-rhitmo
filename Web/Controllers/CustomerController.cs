using Application.Interfaces;
using Application.Objects.Bases;
using Application.Objects.Requests.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController, AllowAnonymous]
[Route("Customer")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerAppService _customerAppService;

    public CustomerController(ICustomerAppService customerAppService)
    {
        _customerAppService = customerAppService;
    }

    [HttpPost("SaveCustomer")]
    public async Task<JsonResult> SaveCustomer([FromBody] SaveCustomerRequest usuarioLoginRequest)
    {
        try
        {
            await _customerAppService.SaveCustomer(usuarioLoginRequest);

            return ResponseBase.ResponderController(true, "Cliente inserido com sucesso");
        }
        catch (Exception e)
        {
            return ResponseBase.ResponderController(false, $"Error ao inserir cliente: {e.Message}");
        }
    }
}