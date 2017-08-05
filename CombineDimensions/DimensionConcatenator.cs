namespace CombineDimensions
{
    using System.Collections;
    using System.Linq;

    public class DimensionCombiner
    {
        public static DimensionEnumerable Combine(params AbstractDimension[] dimensions)
        {
            return new DimensionEnumerable(dimensions);
        }

        public class DimensionEnumerable : IEnumerable
        {
            private readonly AbstractDimension[] dimensions;

            public DimensionEnumerable(params AbstractDimension[] dimensions)
            {
                this.dimensions = dimensions;
            }

            public IEnumerator GetEnumerator()
            {
                var currentDimension = this.dimensions.ElementAt(0);
                var initialDimension = currentDimension;

                for (int i = 1; i < this.dimensions.Count(); i++)
                {
                    var nextDimension = this.dimensions.ElementAt(i);
                    currentDimension.NextDimension = nextDimension;

                    currentDimension = nextDimension;
                }

                return new AbstractDimensionEnumerator(initialDimension);
            }
        }
    }
}
