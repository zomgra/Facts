using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Core
{
    public class Tag
    {
        public Guid TagId { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        [InverseProperty(nameof(Fact.Tags))]
        public ICollection<Fact> Facts { get; set; }
    }
}
