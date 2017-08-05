using System;
using System.Collections.Generic;
using System.Linq;

namespace CombineDimensions
{
    public abstract class AbstractDimension
    {
        private readonly List<object> allValues;
        private readonly AbstractDimension nextDimension;

        private int currentValue;
        
        public AbstractDimension(AbstractDimension nextDimension = null)
        {
            this.nextDimension = nextDimension;
            this.allValues = AllValues().ToList();
            this.currentValue = 0;
        }

        public abstract IEnumerable<object> AllValues();

        /// <summary>
        /// Returns possible values
        /// </summary>
        /// <remarks>
        /// An IndexOutOfRangeException will be thrown to indicate that the end of
        /// possible values has been reached.
        /// </remarks>
        /// <returns></returns>
        public IEnumerable<object> Merge()
        {
            if (this.nextDimension == null)
            {
                if (this.currentValue > this.allValues.Count - 1)
                {
                    this.currentValue = 0;
                    throw new IndexOutOfRangeException();
                }

                var current = this.allValues.ElementAt(this.currentValue);
                this.currentValue += 1;
                yield return new List<object> { current };
            }

            try
            {
                IEnumerable<object> nextDimensionMerge = this.nextDimension.Merge();
                return new List<object> { this.allValues.ElementAt(this.currentValue) }.Concat(nextDimensionMerge);
            }
            catch(IndexOutOfRangeException)
            {
                if (this.currentValue >= this.allValues.Count - 1)
                {
                    // I've already reached the end of the list. I should stop iterating.
                    // Because the next dimension has reached its last value,
                    // reset mine too and throw to let parent know that I'm done.

                    this.currentValue = 0;
                    throw;
                }
                else
                {
                    // I'm still iterating. I should move on to the next value and ask 
                    // for my own next iteration.

                    this.currentValue += 1;
                    return this.Merge();
                }
            }
        }
    }
}
