using Realtea.Core.Enums;
using Realtea.Core.Exceptions;

namespace Realtea.Core.ValueObjects
{
    public record Money
    {
        private const decimal Zero = decimal.Zero;
        
        private Money() { }

        private Money(decimal value)
        {
            Value = value;
        }
        public static Money Create(decimal value)
        {
            if (value < Zero)
                throw new ApiException(nameof(value), FailureType.InvalidData);

            return new Money(value);
        }

        public decimal Value { get; }
    }
}
