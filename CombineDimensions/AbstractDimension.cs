namespace CombineDimensions
{
    using System.Collections;
    using System.Collections.Generic;

    public abstract partial class AbstractDimension : IEnumerable
    {
        internal AbstractDimension NextDimension { get; set; }

        public abstract List<object> AllValues();

        public IEnumerator GetEnumerator()
        {
            return AllValues().GetEnumerator();
        }
    }
}
