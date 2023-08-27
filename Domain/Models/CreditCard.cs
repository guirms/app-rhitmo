using Domain.Models.Base;

namespace Domain.Models;

public class CreditCard : BaseModel
{
    public required int CreditCardId { get; set; }
    public required string Name { get; set; }
    public required string Number { get; set; }
    public required string ExpirationMonth { get; set; }
    public required string ExpirationYear { get; set; }
    public required string SecurityCode { get; set; }
    public int CustomerId { get; set; }

    public required virtual Customer Customer { get; set; }
}