namespace CCC.TestApp.Core.Application.Usecases.Users
{
    public class UserDoesntExistException : RecordDoesntExistException
    {
        public UserDoesntExistException() {}
        public UserDoesntExistException(string message) : base(message) {}
    }
}