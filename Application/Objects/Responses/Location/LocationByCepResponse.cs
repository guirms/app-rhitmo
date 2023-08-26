namespace Application.Objects.Responses.Location
{
    public record LocationByCepResponse
    {
        public required string Estado { get; set; }
        public required string Cidade { get; set; }
    }
}
