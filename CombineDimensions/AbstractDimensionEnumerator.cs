namespace CombineDimensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    
    internal class AbstractDimensionEnumerator : IEnumerator<List<object>>
    {
        private readonly List<object> allValues;
        private readonly AbstractDimension nextDimension;

        private AbstractDimensionEnumerator nextDimensionEnumerator;

        private int currentValue;

        /// <summary>
        /// State machine implemented according to https://msdn.microsoft.com/en-us/library/78dfe2yb(v=vs.110).aspx.
        /// </summary>
        /// <param name="dimension">The dimension that orchestrates the enumerator.</param>
        public AbstractDimensionEnumerator(AbstractDimension dimension)
        {
            this.allValues = dimension.AllValues().ToList();

            if (this.allValues.Count() < 1)
            {
                throw new NotSupportedException("Dimensions must have at least 1 value.");
            }

            this.nextDimension = dimension.NextDimension;

            this.Reset();
        }

        public List<object> Current
        {
            get
            {
                var current = this.allValues.ElementAt(this.currentValue);
                var currentList = new List<object> { current };

                if (this.nextDimensionEnumerator == null)
                {
                    return currentList;
                }
                else
                {
                    List<object> nextDimensionCurrent = (List<object>)nextDimensionEnumerator.Current;
                    return currentList.Concat(nextDimensionCurrent).ToList();
                }
            }
        }

        public void Dispose() { }

        public bool MoveNext()
        {
            // This is a very verbose implementation, but I found it easy to reason through while writing it.
            // I'm sure some if/else statements can be removed.

            if (this.nextDimension == null)
            {
                if (this.IsOnLastElement())
                {
                    return false;
                }

                this.currentValue += 1;
                return true;
            }
            else
            {
                if (this.IsOnLastElement())
                {
                    // I need to check if next dimension is also on last element.
                    if (!this.nextDimensionEnumerator.IsOnLastElement())
                    {
                        // Because next dimension is not on the last, we move it to its next.
                        this.nextDimensionEnumerator.MoveNext();
                        return true;
                    }
                    else
                    {
                        // Next dimension and I are both on the last one.
                        return false;
                    }
                }
                else
                {
                    if (!this.nextDimensionEnumerator.IsOnLastElement())
                    {
                        // Because next dimension is not on the last, we move it to its next.
                        this.nextDimensionEnumerator.MoveNext();

                        if (this.currentValue == -1)
                        {
                            // If I haven't started myself, I also have to move next.
                            this.currentValue += 1;
                        }

                        return true;
                    }
                    else
                    {
                        // Next dimension is on last one, but I'm not.
                        // We create a new iterator for next dimension and move it to its first element.
                        // I move to my next position.
                        this.nextDimensionEnumerator = new AbstractDimensionEnumerator(this.nextDimension);
                        this.nextDimensionEnumerator.MoveNext();

                        this.currentValue += 1;

                        return true;
                    }
                }
            }
        }

        public void Reset()
        {
            this.currentValue = -1;
            if (this.nextDimension != null)
            {
                this.nextDimensionEnumerator = new AbstractDimensionEnumerator(this.nextDimension);
            }
        }

        public bool IsOnLastElement()
        {
            return this.currentValue >= this.allValues.Count - 1;
        }

        object IEnumerator.Current
        {
            get { return this.Current; }
        }
    }
}
