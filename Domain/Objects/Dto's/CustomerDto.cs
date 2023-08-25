using Domain.Models.Enums;
using Domain.Models;

namespace Domain.Objects.Dto_s
{
    public class CustomerDto
    {
        public required int CustomerId { get; set; }
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
}
