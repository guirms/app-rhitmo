using Domain.Models;

namespace Infra.Data.Interfaces;

public interface ICustomerRepository: IBaseRepository<Customer>
{
    Task<bool> GetCustomerWithSameCredentials(string cpf, string email);
}