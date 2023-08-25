using Microsoft.AspNetCore.Mvc;

namespace Application.Objects.Bases;

public class ResponseBase
{
    public static JsonResult ResponderController(bool sucessoOperacao, string msg, object? dataOperacao = null)
    {
        return BigJson(new
        {
            Sucesso = sucessoOperacao,
            Mensagem = msg,
            Data = dataOperacao,
        });
    }
    
    public static JsonResult BigJson(object data)
    {
        return new JsonResult(data);
    }
}