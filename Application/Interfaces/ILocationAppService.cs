using Application.Objects.Responses.Location;
namespace Application.Interfaces;

public interface ILocationAppService
{
    Task<LocationByCepResponse> GetLocationByCep(string cep);
}