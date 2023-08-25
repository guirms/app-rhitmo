using Domain.Models.Base;

namespace Domain.Models
{
    public class BankSlip : BaseModel
    {
        public required int BankSlipId { get; set; }
        public int CustomerId { get; set; }

        public required virtual Customer Customer { get; set; }
    }
}
