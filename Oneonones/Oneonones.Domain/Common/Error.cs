using System.Collections.Generic;

namespace Oneonones.Domain.Common
{
    public class Error
    {
        public IList<string> Errors { get; private set; }

        public Error(IList<string> errors) => Errors = errors;
        public Error(string error) : this(new[] { error }) { }
    }
}