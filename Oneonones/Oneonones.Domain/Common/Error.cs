using System.Collections.Generic;

namespace Oneonones.Domain.Common
{
    public class Error
    {
        public IList<string> Errors { get; private set; }

        public Error(string error) => Errors = new[] { error };

        public Error(IList<string> errors) => Errors = errors;
    }
}