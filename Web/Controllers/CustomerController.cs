using Application.Interfaces;
using Application.Objects.Bases;
using Application.Objects.Requests.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("Usuario")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _usuarioService;

    public CustomerController(ICustomerService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost("SaveCustomer")]
    public JsonResult SaveCustomer([FromBody] SaveCustomerRequest usuarioLoginRequest)
    {
        try
        {
            _usuarioService.SaveCustomer(usuarioLoginRequest);

            return ResponseBase.ResponderController(true, "Customer entered successfully");
        }
        catch (Exception e)
        {
            return ResponseBase.ResponderController(false, "Error inserting customer: ", e.Message);
        }
    }
}