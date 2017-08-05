using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CombineDimensions
{
    public abstract class AbstractDimension : IEnumerable
    {
        private readonly AbstractDimension nextDimension;

        public AbstractDimension(AbstractDimension nextDimension = null)
        {
            this.nextDimension = nextDimension;
        }

        public abstract List<object> AllValues();

        public IEnumerator GetEnumerator()
        {
            return new AbstractDimensionEnumerator(this, this.nextDimension);
        }

        private class AbstractDimensionEnumerator : IEnumerator<List<object>>
        {
            private readonly List<object> allValues;
            private readonly IEnumerator nextDimensionEnumerator;

            private int currentValue;

            public AbstractDimensionEnumerator(AbstractDimension dimension, AbstractDimension nextDimension)
            {
                this.allValues = dimension.AllValues().ToList();
                this.currentValue = -1;

                if (nextDimension != null)
                {
                    this.nextDimensionEnumerator = nextDimension.GetEnumerator();
                }
                else
                {
                    this.nextDimensionEnumerator = null;
                }
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
                        if (!this.nextDimensionEnumerator.MoveNext())
                        {
                            this.nextDimensionEnumerator.Reset();
                        }

                        List<object> nextDimensionCurrent = (List<object>)nextDimensionEnumerator.Current;
                        return currentList.Concat(nextDimensionCurrent).ToList();
                    }
                }
            }

            public void Dispose() { }

            public bool MoveNext()
            {
                this.currentValue += 1;
                return (this.currentValue < this.allValues.Count);
            }

            public void Reset()
            {
                this.currentValue = -1;
            }

            object IEnumerator.Current
            {
                get { return this.Current; }
            }
        }
    }
}
