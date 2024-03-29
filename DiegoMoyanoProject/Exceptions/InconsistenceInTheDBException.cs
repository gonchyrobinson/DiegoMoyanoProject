namespace DiegoMoyanoProject.Exceptions
{
    public class InconsistenceInTheDBException : Exception
    {
        public InconsistenceInTheDBException() : base() { }
        public InconsistenceInTheDBException(string message) : base(message) { }
    }
}
