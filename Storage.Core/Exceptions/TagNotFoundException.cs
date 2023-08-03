
namespace Storage.Core.Exceptions
{
    public class TagNotFoundException : Exception
    {
        public TagNotFoundException() : base()
        {

        }
        public TagNotFoundException(string message) :base(message)
        {

        }
        public TagNotFoundException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
