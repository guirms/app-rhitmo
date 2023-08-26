using Application.Interfaces;
using Application.Objects.Requests.Usuario;
using FluentValidation;

namespace Application.AppServices
{
    public class CustomerAppService : ICustomerAppService
    {
        private readonly ICustomerService _customerService;
        private readonly IValidator<AddCustomerRequest> _usuarioLoginRequestValidator;

        public CustomerAppService(IValidator<AddCustomerRequest> usuarioLoginRequestValidator, ICustomerService customerService)
        {
            _usuarioLoginRequestValidator = usuarioLoginRequestValidator;
            _customerService = customerService;
        }

        public async Task SaveCustomer(AddCustomerRequest saveCustomerRequest)
        {
            var usuarioCadastroRequestValidateResult = _usuarioLoginRequestValidator.Validate(saveCustomerRequest);

            if (!usuarioCadastroRequestValidateResult.IsValid)
                throw new ValidationException(usuarioCadastroRequestValidateResult.Errors.FirstOrDefault()?.ToString());

            await _customerService.SaveCustomer(saveCustomerRequest);
        }
    }
}
