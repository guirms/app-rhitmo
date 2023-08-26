namespace Domain.Helper;

public static class StringHelper
{
    private static readonly Dictionary<string, string> states = new()
    {
            { "AC", "Acre" },
            { "AL", "Alagoas" },
            { "AP", "Amapá" },
            { "AM", "Amazonas" },
            { "BA", "Bahia" },
            { "CE", "Ceará" },
            { "DF", "Distrito Federal" },
            { "ES", "Espírito Santo" },
            { "GO", "Goiás" },
            { "MA", "Maranhão" },
            { "MT", "Mato Grosso" },
            { "MS", "Mato Grosso do Sul" },
            { "MG", "Minas Gerais" },
            { "PA", "Pará" },
            { "PB", "Paraíba" },
            { "PR", "Paraná" },
            { "PE", "Pernambuco" },
            { "PI", "Piauí" },
            { "RJ", "Rio de Janeiro" },
            { "RN", "Rio Grande do Norte" },
            { "RS", "Rio Grande do Sul" },
            { "RO", "Rondônia" },
            { "RR", "Roraima" },
            { "SC", "Santa Catarina" },
            { "SP", "São Paulo" },
            { "SE", "Sergipe" },
            { "TO", "Tocantins" }
        };

    public static string GetSafeValue(this string? request)
    {
        return request == null ? "" : request;
    }

    public static string GetState(this string request)
    {
        return states[request] ?? string.Empty;
    }
}