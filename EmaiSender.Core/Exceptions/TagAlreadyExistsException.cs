namespace EmaiSender.Core.Exceptions
{
    public class TagAlreadyExistsException : Exception
    {
        public TagAlreadyExistsException() : base()
        {

        }
        public TagAlreadyExistsException(string message) : base(message)
        {

        }
        public TagAlreadyExistsException(string message, Exception ex) : base(message, ex)
        {

        }
    }
}
