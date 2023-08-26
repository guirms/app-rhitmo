using Domain.Models.Base;
using Domain.Models.Enums;

namespace Domain.Models;

public class Customer : BaseModel
{
    public required int CustomerId { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Cpf { get; set; }
    public required string Address { get; set; }
    public required string State { get; set; }
    public required string City { get; set; }
    public required string Cep { get; set; }
    public EPaymentMethod PaymentMethod { get; set; }

    public virtual CreditCard? CreditCard { get; set; }
    public virtual BankSlip? BankSlip { get; set; }
}