using System;

namespace CCC.TestApp.Core.Application.DALInterfaces
{
    public class RecordDoesntExistException : Exception
    {
        protected RecordDoesntExistException(string message) : base(message) {}

        protected RecordDoesntExistException() {}
    }
}