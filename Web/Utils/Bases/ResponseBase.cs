using Microsoft.AspNetCore.Mvc;

namespace Application.Objects.Bases;

public class ResponseBase
{
    public static JsonResult DefaultResponse(bool success, string? message = null, object? objectData = null)
    {
        return BigJson(new
        {
            Success = success,
            Message = message,
            Data = objectData,
        });
    }

    public static JsonResult BigJson(object data)
    {
        return new JsonResult(data);
    }
}