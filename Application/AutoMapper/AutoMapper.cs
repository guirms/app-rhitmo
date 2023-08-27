using Application.Objects.Requests.Usuario;
using Application.Objects.Responses.Location;
using AutoMapper;
using Domain.External.Objects.Dto_s.CreditCard;
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
            .ForMember(c => c.Cep, opts => opts.MapFrom(c => FormatCep(c.Cep)))
            .ForMember(c => c.CreditCardDto, opts => opts.MapFrom(c => c.CreditCard))
            .ForMember(c => c.InsertedAt, opts => opts.MapFrom(c => c.InsertedAt.ToString("dd/MM/yyyy")));
    }

    private void CreditCardMap()
    {
        CreateMap<AddCustomerRequest, CreditCard>()
            .ForMember(c => c.Name, opts => opts.MapFrom(a => a.CardHolderName))
            .ForMember(c => c.Number, opts => opts.MapFrom(a => a.CardNumber))
            .ForMember(c => c.ExpirationMonth, opts => opts.MapFrom(a => a.CardExpirationMonth))
            .ForMember(c => c.ExpirationYear, opts => opts.MapFrom(a => a.CardExpirationYear))
            .ForMember(c => c.SecurityCode, opts => opts.MapFrom(a => a.CardSecurityCode));


        CreateMap<CreditCard, CreditCardDto>();
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

    private static string FormatCep(string cep)
    {
        if (cep.Length != 8)
            throw new ArgumentException("CEP deve conter 8 dígitos.");

        return $"{cep[..2]}.{cep.Substring(2, 3)} - {cep.Substring(5)}";
    }
    #endregion
}
