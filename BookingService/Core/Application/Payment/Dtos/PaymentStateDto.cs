namespace Application.Payment.Dtos
{
    public enum Status
    {
        Succes = 0,
        Failed = 1,
        Error = 2,
        Undefined = 3,
    }
    public class PaymentStateDto
    {
        public Status Status { get; set; }
        public string PaymentId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string Message {  get; set; }
    }
}
