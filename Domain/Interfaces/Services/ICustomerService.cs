using Application.Objects.Requests.Usuario;

namespace Application.Interfaces;

public interface ICustomerService
{
    Task SaveCustomer(SaveCustomerRequest saveCustomerRequest);
}