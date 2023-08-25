using Domain.Models;
using Infra.Data.Context;
using Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories;

public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(ConfigContext contexto) : base(contexto)
    {
    }

    public Task<bool> GetCustomerWithSameCredentials(string cpf, string email)
    {
        return _context.Set<Customer>()
            .AnyAsync(c => c.Cpf == cpf || c.Email == email);
    }
}