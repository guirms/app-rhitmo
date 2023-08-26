using Domain.Models;
using Domain.Objects.Responses;

namespace Infra.Data.Interfaces;

public interface ICustomerRepository : IBaseRepository<Customer>
{
    Task<bool> GetCustomerWithSameCredentials(string cpf, string email, int? userExceptId = null);
    Task<List<CustomersToGridResponse>> GetCustomersToGrid();
}