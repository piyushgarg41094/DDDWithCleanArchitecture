using Gatherly.Domain.Primitives;
using Gatherly.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Domain.ValueObjects
{
    public sealed class FirstName : ValueObject
    {
        public const int MaxLength = 50;
        private FirstName(string value)
        {
            //if(value.Length > MaxLength)
            //{
            //    throw new ArgumentException();
            //}
            Value = value;
        }
        public string Value { get; }
        public static Result<FirstName> Create(string firstName)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                return Result.Failure<FirstName>(new Error(
                    "FirstName.Empty",
                    "First Name is Empty"));
            }
            if (firstName.Length > MaxLength)
            {
                return Result.Failure<FirstName>(new Error(
                    "FirstName.TooLong",
                    "First Name is Too Long"));
            }
            return new FirstName(firstName);
        }
        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
