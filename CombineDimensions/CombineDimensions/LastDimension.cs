using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombineDimensions
{
    public class LastDimension : IDimension<object>
    {
        public LastDimension()
        {
        }

        public IEnumerable<object> NextMerge()
        {
            throw new NotImplementedException();
        }
    }
}
