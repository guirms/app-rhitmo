using Application.Interfaces;
using Application.Objects.Requests.Usuario;
using AutoMapper;
using Domain.Helper;
using Domain.Models;
using Domain.Models.Enums;
using Domain.Objects.Responses;
using Domain.Utils;
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

    public async Task<List<CustomersToGridResponse>?> GetCustomersToGrid()
    {
        var customersToGrid = await _customerRepository.GetCustomersToGrid();

        if (customersToGrid == null)
            return customersToGrid;

        foreach (var customer in customersToGrid)
        {
            if (customer.PaymentMethod == EPaymentMethod.CreditCard && customer.CreditCardDto != null)
                customer.CreditCardDto.Number = 
                    FormatCreditCardNumber(StringEncryptionService.DecryptString(_configuration["SecretKey"].GetSafeValue(), customer.CreditCardDto.Number));
        }

        return customersToGrid;
    }

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
        var customer = await _customerRepository.GetCustomerById(customerId)
            ?? throw new InvalidOperationException("Não existe nenhum usuário com os dados informados");

        var hasCustomerWithSameCredentials = await _customerRepository
            .GetCustomerWithSameCredentials(updateCustomerRequest.Cpf, updateCustomerRequest.Email, customerId);

        if (hasCustomerWithSameCredentials)
            throw new InvalidOperationException("Já existe um usuário com esse CPF ou E-mail");

        var oldInsertedAt = customer.InsertedAt;

        customer = _mapper.Map(updateCustomerRequest, customer);

        var currentDateTime = DateTime.Now;

        if (updateCustomerRequest.PaymentMethod == EPaymentMethod.CreditCard)
        {
            customer.CreditCard = _mapper.Map(updateCustomerRequest, customer.CreditCard);

            if (customer.CreditCard != null)
            {
                customer.CreditCard.InsertedAt = oldInsertedAt;
                customer.CreditCard.UpdatedAt = currentDateTime;
            }
        }
        else
        {
            customer.BankSlip = _mapper.Map(updateCustomerRequest, customer.BankSlip);

            if (customer.BankSlip != null)
            {
                customer.BankSlip.InsertedAt = oldInsertedAt;
                customer.BankSlip.UpdatedAt = currentDateTime;
            }
        }

        customer.InsertedAt = oldInsertedAt;
        customer.UpdatedAt = currentDateTime;

        await _customerRepository.UpdateAsync(customer);
    }

    public async Task DeleteCustomer(int customerId) => await _customerRepository.DeleteAsync(customerId);

    private static string FormatCreditCardNumber(string creditCardNumber)
    {
        if (creditCardNumber.Length != 16)
            throw new ArgumentException("Número de cartão de crédito deve conter 16 dígitos.");

        return $"{creditCardNumber.Substring(0, 4)} {creditCardNumber.Substring(4, 4)} {creditCardNumber.Substring(8, 4)} {creditCardNumber.Substring(12)}";
    }
}