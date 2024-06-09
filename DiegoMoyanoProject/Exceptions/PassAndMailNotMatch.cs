namespace DiegoMoyanoProject.Exceptions
{
    public class PassAndMailNotMatch : Exception
    {
        public PassAndMailNotMatch() : base() { }
        public PassAndMailNotMatch(string message) : base(message) { }
    }
    public class noDateException : Exception
    {
        public noDateException() : base() { }
        public noDateException(string message) : base(message) { }
    }
}
