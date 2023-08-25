using Application.Objects.Requests.Usuario;

namespace Application.Interfaces;

public interface ICustomerAppService
{
    Task SaveCustomer(SaveCustomerRequest saveCustomerRequest);
}