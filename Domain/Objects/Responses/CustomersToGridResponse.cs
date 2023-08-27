using Domain.Models.Enums;
using Domain.Models;
using Domain.External.Objects.Dto_s.CreditCard;

namespace Domain.Objects.Responses
{
    public class CustomersToGridResponse
    {
        public required string CustomerId { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Cpf { get; set; }
        public required string Address { get; set; }
        public required string State { get; set; }
        public required string City { get; set; }
        public required string Cep { get; set; }
        public EPaymentMethod PaymentMethod { get; set; }
        public required string InsertedAt { get; set; }
        public CreditCardDto? CreditCardDto { get; set; }
    }
}
