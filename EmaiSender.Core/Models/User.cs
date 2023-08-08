namespace EmaiSender.Core.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}
