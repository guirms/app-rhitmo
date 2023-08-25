namespace Domain.Models.Base
{
    public class BaseModel
    {
        public required DateTime InsertedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
