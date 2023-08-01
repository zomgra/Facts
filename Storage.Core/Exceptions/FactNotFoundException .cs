namespace Storage.Core.Exceptions
{
    public class FactNotFoundException : Exception
    {
        public FactNotFoundException() : base()
        {

        }
        public FactNotFoundException(string message) :base(message)
        {

        }
        public FactNotFoundException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
