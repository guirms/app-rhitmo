using Application.Interfaces;
using Application.Objects.Responses.Location;
using AutoMapper;
using Domain.External.Interfaces.Services;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Application.AppServices
{
    public class LocationAppService : ILocationAppService
    {
        private readonly IViacepExternalService _viacepExternalService;
        private readonly IMapper _mapper;

        public LocationAppService(IViacepExternalService viacepExternalService, IMapper mapper)
        {
            _viacepExternalService = viacepExternalService;
            _mapper = mapper;
        }

        public async Task<LocationByCepResponse> GetLocationByCep(string cep)
        {
            if (!IsValidCep(cep))
                throw new ValidationException("CEP inválido");

            return _mapper.Map<LocationByCepResponse>(await _viacepExternalService.GetLocationByCep(cep));
        }

        private static bool IsValidCep(string cep)
        {
            var cleanCEP = Regex.Replace(cep, @"[^\d]", "");

            if (cleanCEP.Length != cep.Length)
                return false;

            if (cleanCEP.Length != 8)
                return false;

            if (cleanCEP.All(c => c == cleanCEP[0]))
                return false;

            return true;
        }
    }
}
