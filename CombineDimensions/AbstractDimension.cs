using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CombineDimensions
{
    public abstract class AbstractDimension : IEnumerable
    {
        public AbstractDimension(AbstractDimension nextDimension = null)
        {
            this.NextDimension = nextDimension;
        }

        public AbstractDimension NextDimension { get; private set; }

        public abstract List<object> AllValues();

        public IEnumerator GetEnumerator()
        {
            return new AbstractDimensionEnumerator(this, this.NextDimension);
        }

        private class AbstractDimensionEnumerator : IEnumerator<List<object>>
        {
            private readonly List<object> allValues;
            private readonly AbstractDimension nextDimension;

            private AbstractDimensionEnumerator nextDimensionEnumerator;

            private int currentValue;

            public AbstractDimensionEnumerator(AbstractDimension dimension, AbstractDimension nextDimension)
            {
                this.allValues = dimension.AllValues().ToList();
                this.nextDimension = nextDimension;

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
                            this.nextDimensionEnumerator = new AbstractDimensionEnumerator(this.nextDimension, this.nextDimension.NextDimension);
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
                    this.nextDimensionEnumerator = new AbstractDimensionEnumerator(this.nextDimension, this.nextDimension.NextDimension);
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
}
