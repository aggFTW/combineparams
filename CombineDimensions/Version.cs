namespace CombineDimensions
{
    using System.Collections.Generic;

    public class MyVersion : AbstractDimension
    {
        public const double One = 1.0;

        public const double Two = 2.0;

        public MyVersion(AbstractDimension nextDimension = null) : base(nextDimension)
        {
        }

        public override IEnumerable<object> AllValues()
        {
            return new List<object> { One, Two };
        }
    }
}
