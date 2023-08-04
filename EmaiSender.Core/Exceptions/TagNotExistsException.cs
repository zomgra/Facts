namespace EmaiSender.Core.Exceptions
{
    public class TagNotExistsException : Exception
    {
        public TagNotExistsException() : base()
        {

        }
        public TagNotExistsException(string message) : base(message)
        {

        }
        public TagNotExistsException(string message, Exception ex) : base(message, ex)
        {

        }
    }
}
