using Application.Interfaces;
using Application.Objects.Requests.Usuario;
using AutoMapper;
using Domain.Models;
using Domain.Models.Enums;
using Domain.Objects.Responses;
using Infra.Data.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Application.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public CustomerService(ICustomerRepository customerRepository, IMapper mapper, IConfiguration configuration)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _configuration = configuration;
    }

    public Task<List<CustomersToGridResponse>> GetCustomersToGrid() => _customerRepository.GetCustomersToGrid();

    public async Task SaveCustomer(AddCustomerRequest saveCustomerRequest)
    {
        var hasCustomerWithSameCredentials = await _customerRepository
            .GetCustomerWithSameCredentials(saveCustomerRequest.Cpf, saveCustomerRequest.Email);

        if (hasCustomerWithSameCredentials)
            throw new InvalidOperationException("Já existe um usuário com esse CPF ou E-mail");

        var customer = _mapper.Map<Customer>(saveCustomerRequest);

        var currentDateTime = DateTime.Now;

        if (saveCustomerRequest.PaymentMethod == EPaymentMethod.CreditCard)
        {
            customer.CreditCard = _mapper.Map<CreditCard>(saveCustomerRequest);
            customer.CreditCard.InsertedAt = currentDateTime;
        }
        else
        {
            customer.BankSlip = _mapper.Map<BankSlip>(saveCustomerRequest);
            customer.BankSlip.InsertedAt = currentDateTime;
        }

        customer.InsertedAt = currentDateTime;

        await _customerRepository.SaveAsync(customer);
    }

    public async Task UpdateCustomer(AddCustomerRequest updateCustomerRequest, int customerId)
    {
        var customer = await _customerRepository.GetByIdAsync(customerId)
            ?? throw new InvalidOperationException("Não existe nenhum usuário com os dados informados");

        var hasCustomerWithSameCredentials = await _customerRepository
            .GetCustomerWithSameCredentials(updateCustomerRequest.Cpf, updateCustomerRequest.Email, customerId);

        if (hasCustomerWithSameCredentials)
            throw new InvalidOperationException("Já existe um usuário com esse CPF ou E-mail");

        customer = _mapper.Map(updateCustomerRequest, customer);

        var currentDateTime = DateTime.Now;

        if (updateCustomerRequest.PaymentMethod != customer.PaymentMethod)
        {
            if (updateCustomerRequest.PaymentMethod == EPaymentMethod.CreditCard)
            {
                customer.CreditCard = _mapper.Map<CreditCard>(updateCustomerRequest);
                customer.CreditCard.UpdatedAt = currentDateTime;
            }
            else
            {
                customer.BankSlip = _mapper.Map<BankSlip>(updateCustomerRequest);
                customer.BankSlip.UpdatedAt = currentDateTime;
            }
        }

        customer.UpdatedAt = currentDateTime;

        await _customerRepository.UpdateAsync(customer);
    }

    public async Task DeleteCustomer(int customerId) => await _customerRepository.DeleteAsync(customerId);
}