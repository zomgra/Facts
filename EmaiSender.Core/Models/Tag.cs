namespace EmaiSender.Core.Models
{
    public class Tag
    {
        public Guid TagId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string Name { get; set; }
    }
}