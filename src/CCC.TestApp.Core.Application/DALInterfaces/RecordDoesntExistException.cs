using System;

namespace CCC.TestApp.Core.Application.DALInterfaces
{
    public class RecordDoesntExistException : Exception
    {
        public RecordDoesntExistException(string message) : base(message) {}

        public RecordDoesntExistException() {}
    }
}