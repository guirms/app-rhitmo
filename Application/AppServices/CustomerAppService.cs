using Application.Interfaces;
using Application.Objects.Requests.Usuario;
using Domain.Helper;
using Domain.Models.Enums;
using Domain.Utils;
using FluentValidation;
using Microsoft.Extensions.Configuration;

namespace Application.AppServices
{
    public class CustomerAppService : ICustomerAppService
    {
        private readonly ICustomerService _customerService;
        private readonly IValidator<AddCustomerRequest> _usuarioLoginRequestValidator;
        private readonly IConfiguration _configuration;

        public CustomerAppService(IValidator<AddCustomerRequest> usuarioLoginRequestValidator, ICustomerService customerService, IConfiguration configuration)
        {
            _usuarioLoginRequestValidator = usuarioLoginRequestValidator;
            _customerService = customerService;
            _configuration = configuration;
        }

        public async Task SaveCustomer(AddCustomerRequest saveCustomerRequest)
        {
            var usuarioCadastroRequestValidateResult = _usuarioLoginRequestValidator.Validate(saveCustomerRequest);

            if (!usuarioCadastroRequestValidateResult.IsValid)
                throw new ValidationException(usuarioCadastroRequestValidateResult.Errors.FirstOrDefault()?.ToString());

            if (saveCustomerRequest.PaymentMethod == EPaymentMethod.CreditCard && saveCustomerRequest.CardNumber != null)
                saveCustomerRequest.CardNumber = StringEncryptionService.EncryptString(_configuration["SecretKey"].GetSafeValue(), saveCustomerRequest.CardNumber);

            await _customerService.SaveCustomer(saveCustomerRequest);
        }

        public async Task UpdateCustomer(AddCustomerRequest updateCustomerRequest, int customerId)
        {
            if (updateCustomerRequest.PaymentMethod == EPaymentMethod.CreditCard && updateCustomerRequest.CardNumber != null)
                updateCustomerRequest.CardNumber = StringEncryptionService.EncryptString(_configuration["SecretKey"].GetSafeValue(), updateCustomerRequest.CardNumber);

            await _customerService.UpdateCustomer(updateCustomerRequest, customerId);
        }
    }
}
