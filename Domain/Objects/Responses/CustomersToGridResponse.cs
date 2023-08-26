namespace Domain.Objects.Responses
{
    public class CustomersToGridResponse
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Cpf { get; set; }
        public required string InsertedAt { get; set; }
    }
}
