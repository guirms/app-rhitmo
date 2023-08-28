namespace Application.Objects.Responses.Location
{
    public record LocationByCepResponse
    {
        public required string State { get; set; }
        public required string City { get; set; }
    }
}
