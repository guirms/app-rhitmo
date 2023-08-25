using Application.Interfaces;
using Application.Objects.Requests.Usuario;
using FluentValidation;

namespace Application.AppServices
{
    public class CustomerAppService : ICustomerAppService
    {
        private readonly ICustomerService _customerService;
        private readonly IValidator<SaveCustomerRequest> _usuarioLoginRequestValidator;

        public CustomerAppService(IValidator<SaveCustomerRequest> usuarioLoginRequestValidator, ICustomerService customerService)
        {
            _usuarioLoginRequestValidator = usuarioLoginRequestValidator;
            _customerService = customerService;
        }

        public async Task SaveCustomer(SaveCustomerRequest saveCustomerRequest)
        {
            var usuarioCadastroRequestValidateResult = _usuarioLoginRequestValidator.Validate(saveCustomerRequest);

            if (!usuarioCadastroRequestValidateResult.IsValid)
                throw new ValidationException(usuarioCadastroRequestValidateResult.Errors.FirstOrDefault()?.ToString());

            await _customerService.SaveCustomer(saveCustomerRequest);
        }
    }
}
