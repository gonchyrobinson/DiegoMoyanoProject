namespace DiegoMoyanoProject.Exceptions
{
    public class PassAndMailNotMatch : Exception
    {
        public PassAndMailNotMatch() : base() { }
        public PassAndMailNotMatch(string message) : base(message) { }
    }
}
