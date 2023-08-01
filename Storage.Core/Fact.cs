using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Core
{
    public class Fact
    {
        [Key]
        public Guid FactId { get; set; }
        public string Content { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        [InverseProperty(nameof(Tag.Facts))]
        public ICollection<Tag> Tags { get; set; }
    }
}
