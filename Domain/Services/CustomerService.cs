using Application.Interfaces;
using Application.Objects.Requests.Usuario;
using AutoMapper;
using Domain.Models;
using Infra.Data.Interfaces;

namespace Application.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task SaveCustomer(SaveCustomerRequest saveCustomerRequest)
    {
        var hasCustomerWithSameCredentials = await _customerRepository
            .GetCustomerWithSameCredentials(saveCustomerRequest.Cpf, saveCustomerRequest.Email);

        if (hasCustomerWithSameCredentials)
            throw new InvalidOperationException("Já existe um usuário com esse CPF ou E-mail");

        var customer = _mapper.Map<Customer>(saveCustomerRequest);

        await _customerRepository.SaveAsync(customer);
    }
}