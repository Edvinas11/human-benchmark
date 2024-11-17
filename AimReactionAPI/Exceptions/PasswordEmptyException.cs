namespace AimReactionAPI.Exceptions
{
    public class PasswordEmptyException : Exception
    {
        public PasswordEmptyException(string message) : base(message) { }
    }
}
