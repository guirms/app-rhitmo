using Application.Interfaces;
using Application.Objects.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController, AllowAnonymous]
[Route("Location")]
public class LocationController : ControllerBase
{
    private readonly ILocationAppService _locationAppService;

    public LocationController(ILocationAppService locationAppService)
    {
        _locationAppService = locationAppService;
    }

    [HttpGet("GetLocationByCep")]
    public async Task<JsonResult> GetLocationByCep(string cep)
    {
        try
        {
            var gridReponse = await _locationAppService.GetLocationByCep(cep);

            return ResponseBase.DefaultResponse(true, objectData: gridReponse);
        }
        catch (Exception ex)
        {
            return ResponseBase.DefaultResponse(false, ex.Message);
        }
    }
}