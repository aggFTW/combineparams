namespace CombineDimensions
{
    using System.Collections.Generic;

    public interface IDimension<TSource>
    {
        IEnumerable<object> NextMerge();
    }
}
