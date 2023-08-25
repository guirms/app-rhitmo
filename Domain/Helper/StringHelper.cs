namespace Domain.Helper;

public static class StringHelper
{
    public static string GetSafeValue(this string? request)
    {
        return request == null ? "" : request;
    }
}