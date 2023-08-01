namespace Storage.Core.ViewModels
{
    public class FactViewModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public List<TagViewModel> Tags { get; set; }
    }
}
