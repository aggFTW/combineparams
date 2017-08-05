namespace CombineDimensions
{
    using System.Collections.Generic;

    public class Location : AbstractDimension
    {
        public const string East = "East";

        public const string West = "West";

        public Location(AbstractDimension nextDimension = null) : base(nextDimension)
        {
        }

        public override List<object> AllValues()
        {
            return new List<object> { East, West };
        }
    }
}

