using FrontEnd.Web.Models;

namespace FrontEnd.Web.ViewModels
{
    public class TagViewModel
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public List<Fact> Facts { get; set; } = new();
    }
}
