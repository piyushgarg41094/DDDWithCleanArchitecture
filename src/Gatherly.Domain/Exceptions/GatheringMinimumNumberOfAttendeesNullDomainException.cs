using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Domain.Exceptions
{
    public sealed class GatheringMinimumNumberOfAttendeesNullDomainException : DomainException
    {
        public GatheringMinimumNumberOfAttendeesNullDomainException(string message) : base(message)
        {
        }
    }
}
