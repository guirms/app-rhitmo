using Application.Objects.Requests.Usuario;
using AutoMapper;
using Domain.Models;

namespace Application.AutoMapper;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CustomerMap();
    }

    #region Usuario

    private void CustomerMap()
    {
        CreateMap<SaveCustomerRequest, Customer>()
            .ForMember(c => c.InsertedAt, opts => opts.MapFrom(s => DateTime.Now));
    }

    #endregion

}
