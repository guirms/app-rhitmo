﻿using AutoMapper;
using Domain.Models;
using Domain.Objects.Responses;
using Infra.Data.Context;
using Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories;

public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    private readonly IMapper _mapper;

    public CustomerRepository(ConfigContext context, IMapper mapper) : base(context)
    {
        _mapper = mapper;
    }

    public Task<bool> GetCustomerWithSameCredentials(string cpf, string email, int? userExceptId = null)
    {
        if (userExceptId.HasValue)
            return _context.Set<Customer>()
                .AnyAsync(c => (c.Cpf == cpf || c.Email == email) && c.CustomerId != userExceptId);
        else
            return _context.Set<Customer>()
                .AnyAsync(c => c.Cpf == cpf || c.Email == email);
    }

    public async Task<List<CustomersToGridResponse>> GetCustomersToGrid()
    {
        var customer = await _context.Set<Customer>()
            .AsNoTracking()
            .ToListAsync();

        return _mapper.ProjectTo<CustomersToGridResponse>(customer.AsQueryable()).ToList();
    }

}