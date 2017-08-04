using System;
using System.Collections.Generic;
using System.Linq;

namespace CombineDimensions
{
    public abstract class AbstractDimension<TSource, TNext> : where TNext : IDimension<object>
    {
        private IList<TSource> allValues;
        private int currentValue;
        private TNext nextDimension;

        public AbstractDimension(TNext nextDimension)
        {
            this.nextDimension = nextDimension;
            
            this.allValues = AllValues() ?? new List<TSource>();
            this.currentValue = 0;
        }

        public abstract IList<TSource> AllValues();

        public static IEnumerable<TSource> Merge<TSource>(
            this IEnumerable<TSource> first,
            IEnumerable<TSource> second)
        {

        }

        /// <summary>
        /// Returns possible values
        /// </summary>
        /// <remarks>
        /// An IndexOutOfRangeException will be thrown to indicate that the end of
        /// possible values has been reached.
        /// </remarks>
        /// <returns></returns>
        public IEnumerable<object> NextMerge()
        {
            if (this.nextDimension == null)
            {
                if (this.currentValue >= this.allValues.Count - 1)
                {
                    this.currentValue = 0;
                    throw new IndexOutOfRangeException();
                }

                this.currentValue += 1;
                return new List<object> { this.allValues.ElementAt(this.currentValue) };
            }

            try
            {
                IEnumerable<object> nextDimensionMerge = this.nextDimension.NextMerge();
                return new List<object> { this.allValues.ElementAt(this.currentValue) }.Concat(nextDimensionMerge);
            }
            catch(IndexOutOfRangeException)
            {
                if (this.currentValue >= this.allValues.Count - 1)
                {
                    // I've already reached the end of the list. I should stop iterating.
                    // Only if the next dimension doesn't throw an IndexOutOfRangeException
                    // should I return a list. If the next dimension has reached its last value,
                    // reset mine too.

                    this.currentValue = 0;
                    throw;
                }
                else
                {
                    // I'm still iterating. If the next dimension throws an IndexOutOfRangeException
                    // I should move on to the next value and ask for my own next iteration.

                    this.currentValue += 1;
                    return this.NextMerge();
                }
            }
        }
    }
}
