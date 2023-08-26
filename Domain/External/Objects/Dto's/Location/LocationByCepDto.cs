namespace Application.Objects.Responses.Location
{
    public record LocationByCepDto
    {
        public required string Localidade { get; set; }
        public required string Uf { get; set; }
    }
}
