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
    private readonly ICustomerService _customerService;
    private readonly ICustomerAppService _customerAppService;

    public CustomerController(ICustomerService customerService, ICustomerAppService customerAppService)
    {
        _customerService = customerService;
        _customerAppService = customerAppService;
    }

    [HttpPost("GetCustomersToGrid")]
    public async Task<JsonResult> GetCustomers()
    {
        try
        {
            var gridReponse = await _customerService.GetCustomersToGrid();

            return ResponseBase.DefaultResponse(true, objectData: gridReponse);
        }
        catch (Exception ex)
        {
            return ResponseBase.DefaultResponse(false, $"Erro ao resgatar clientes: {ex.Message}");
        }
    }


    [HttpPost("SaveCustomer")]
    public async Task<JsonResult> SaveCustomer([FromBody] AddCustomerRequest saveCustomerRequest)
    {
        try
        {
            await _customerAppService.SaveCustomer(saveCustomerRequest);

            return ResponseBase.DefaultResponse(true, "Cliente inserido com sucesso");
        }
        catch (Exception ex)
        {
            return ResponseBase.DefaultResponse(false, $"Erro ao inserir cliente: {ex.Message}");
        }
    }

    [HttpPut("UpdateCustomer")]
    public async Task<JsonResult> UpdateCustomer([FromBody] AddCustomerRequest updateCustomerRequest, int customerId)
    {
        try
        {
            await _customerAppService.UpdateCustomer(updateCustomerRequest, customerId);

            return ResponseBase.DefaultResponse(true, "Cliente atualizado com sucesso");
        }
        catch (Exception ex)
        {
            return ResponseBase.DefaultResponse(false, $"Erro ao atualizar cliente: {ex.Message}");
        }
    }

    [HttpDelete("DeleteCustomer")]
    public async Task<JsonResult> UpdateCustomer(int customerId)
    {
        try
        {
            await _customerService.DeleteCustomer(customerId);

            return ResponseBase.DefaultResponse(true, "Cliente deletado com sucesso");
        }
        catch (Exception ex)
        {
            return ResponseBase.DefaultResponse(false, $"Erro ao deletar cliente: {ex.Message}");
        }
    }
}