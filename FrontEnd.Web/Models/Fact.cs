namespace FrontEnd.Web.Models
{
    public class Fact
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}
