namespace DiegoMoyanoProject.Exceptions
{
    public class ModelStateInvalidException : Exception
    { 
        public ModelStateInvalidException() : base(){
        }
        public ModelStateInvalidException(string message) : base(message) { }
    }
}
