using Application.Objects.Requests.Usuario;
using Application.Objects.Responses.Location;
using AutoMapper;
using Domain.Helper;
using Domain.Models;
using Domain.Objects.Responses;

namespace Application.AutoMapper;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CustomerMap();
        CreditCardMap();
        BankSlipMap();
        LocationMap();
    }


    private void CustomerMap()
    {
        CreateMap<AddCustomerRequest, Customer>();

        CreateMap<Customer, CustomersToGridResponse>()
            .ForMember(c => c.Cpf, opts => opts.MapFrom(c => FormatCpf(c.Cpf)))
            .ForMember(c => c.InsertedAt, opts => opts.MapFrom(c => c.InsertedAt.ToString("dd/MM/yyyy")));
    }

    private void CreditCardMap()
    {
        CreateMap<AddCustomerRequest, CreditCard>()
            .ForMember(c => c.Number, opts => opts.MapFrom(a => a.CardNumber))
            .ForMember(c => c.Name, opts => opts.MapFrom(a => a.CardName));
    }

    private void BankSlipMap()
    {
        CreateMap<AddCustomerRequest, BankSlip>();
    }

    private void LocationMap()
    {
        CreateMap<LocationByCepDto, LocationByCepResponse>()
            .ForMember(l => l.Cidade, opts => opts.MapFrom(l => l.Localidade))
            .ForMember(l => l.Estado, opts => opts.MapFrom(l => l.Uf.GetState()));
    }

    #region Private methods

    private static string FormatCpf(string cpf)
    {
        if (cpf.Length != 11)
            throw new ArgumentException("CPF deve conter 11 dígitos.");

        return $"{cpf[..3]}.{cpf.Substring(3, 3)}.{cpf.Substring(6, 3)}-{cpf[9..]}";
    }

    #endregion
}
