namespace CombineDimensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
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
                // TODO Check that dimensions is not null.

                var seenDimensionObjects = new HashSet<AbstractDimension>();
                if (dimensions.Any(dim => !seenDimensionObjects.Add(dim)))
                {
                    throw new NotSupportedException("There is at least 1 dimension object passed more than once as a parameter");
                }

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

                currentDimension.NextDimension = null;

                return new AbstractDimensionEnumerator(initialDimension);
            }
        }
    }
}
