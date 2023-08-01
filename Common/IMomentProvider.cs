
namespace Common
{
    public interface IMomentProvider
    {
        DateTimeOffset Now { get;}
    }
    public class MomentProvider : IMomentProvider
    {
        public DateTimeOffset Now { get =>
                 DateTimeOffset.Now; }
    }
}
