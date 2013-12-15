using System.Collections.Generic;

namespace CCC.TestApp.Core.Application.Usecases
{
    public abstract class ValidatingResponseModel
    {
        public readonly List<string> Errors = new List<string>();
        public bool Succeeded { get; set; }
    }
}