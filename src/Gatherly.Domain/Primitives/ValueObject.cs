using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Domain.Primitives
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        public abstract IEnumerable<object> GetAtomicValues();
        public override bool Equals(object? obj)
        {
            return obj is ValueObject other && ValuesAreEqual(other);
        }

        private bool ValuesAreEqual(ValueObject other)
        {
            return GetAtomicValues().SequenceEqual(other.GetAtomicValues());
        }

        public override int GetHashCode()
        {
            return GetAtomicValues()
                .Aggregate(default(int),
                HashCode.Combine);
        }

        public bool Equals(ValueObject? other)
        {
            return other is not null && ValuesAreEqual(other);
        }
    }
}
