using Application.Objects.Responses.Location;
using Domain.External.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Infra.External.Services
{
    public class ViacepExternalService : IViacepExternalService
    {
        private readonly IConfiguration _configuration;

        public ViacepExternalService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<LocationByCepDto> GetLocationByCep(string cep)
        {
            try
            {
                var externalUrl = _configuration["External:ViaCepBaseUrl"] + cep + "/json";
                using var client = new HttpClient();
                using var response = await client.GetAsync(externalUrl);
                using var content = response.Content;

                var responseContent = await content.ReadAsStringAsync()
                    ?? throw new InvalidOperationException();

                var locationByCepDto = JsonConvert.DeserializeObject<LocationByCepDto>(responseContent)
                    ?? throw new InvalidOperationException();

                return locationByCepDto;
            }
            catch
            {
                throw new InvalidOperationException("Erro ao consultar por cep, digite manualmente");
            }
        }
    }
}
