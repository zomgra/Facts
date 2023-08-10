using Newtonsoft.Json;

namespace EmaiSender.Core.Models
{
    public class Tag
    {
        [JsonProperty("Id")]
        public Guid TagId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string Name { get; set; }
        //public ICollection<User> Users { get; set; } = new List<User>();
    }
}