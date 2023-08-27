namespace Domain.External.Objects.Dto_s.CreditCard
{
    public class CreditCardDto
    {
        public required string Name { get; set; }
        public required string Number { get; set; }
        public required string ExpirationMonth { get; set; }
        public required string ExpirationYear { get; set; }
        public required string SecurityCode { get; set; }
    }
}
