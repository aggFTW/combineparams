using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombineDimensions
{
    public static class ListExtensions
    {
        public static string ToPrettyString(this IList<object> list)
        {
            return string.Format("[{0}]", string.Join(", ", list));
        }

        public static string ToTypeString(this IList<object> list)
        {
            return string.Format("[{0}]", string.Join(", ", list.Select(x => x.GetType().ToString())));
        }
    }
}
