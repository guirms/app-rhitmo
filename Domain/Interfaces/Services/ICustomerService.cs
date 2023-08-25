using Application.Objects.Requests.Usuario;
using Application.Objects.Responses.Usuario;

namespace Application.Interfaces;

public interface ICustomerService
{
    Task SaveCustomer(SaveCustomerRequest saveCustomerRequest);
}