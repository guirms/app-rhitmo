using Application.Objects.Requests.Usuario;
using Domain.Objects.Responses;

namespace Application.Interfaces;

public interface ICustomerService
{
    Task<List<CustomersToGridResponse>?> GetCustomersToGrid();
    Task SaveCustomer(AddCustomerRequest saveCustomerRequest);
    Task UpdateCustomer(AddCustomerRequest updateCustomerRequest, int customerId);
    Task DeleteCustomer(int customerId);
}