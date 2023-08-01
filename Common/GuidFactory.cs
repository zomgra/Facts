
namespace Common
{
    public class GuidFactory : IGuidFactory
    {
        public Guid CreateGuid()
            => Guid.NewGuid();
    }
}
