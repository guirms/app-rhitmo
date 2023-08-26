using Application.Objects.Responses.Location;

namespace Domain.External.Interfaces.Services
{
    public interface IViacepExternalService
    {
        Task<LocationByCepDto> GetLocationByCep(string cep);
    }
}
