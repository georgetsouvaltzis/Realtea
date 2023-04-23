using Realtea.Core.Enums;
using Realtea.Core.Exceptions;

namespace Realtea.Core.ValueObjects
{
    public record Sq2
    {
        private const decimal Zero = decimal.Zero;
        private const decimal MaximumAllowedSquareMeter = 1500;
        
        private Sq2() { }

        private Sq2(decimal value)
        {
            Value = value;
        }


        public static Sq2 Create(decimal value)
        {
            if (value <= Zero || value > MaximumAllowedSquareMeter)
                throw new ApiException(nameof(value), FailureType.InvalidData);

            return new Sq2(value);
        }

        public decimal Value { get; }
    }
}
