using System;

namespace CCC.TestApp.Core.Application.Usecases
{
    public class RecordDoesntExistException : Exception
    {
        protected RecordDoesntExistException(string message) : base(message) {}

        protected RecordDoesntExistException() {}
    }
}