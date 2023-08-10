namespace EmaiSender.Core.Models
{
    public class Fact
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}
