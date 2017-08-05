using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombineDimensions
{
    public class Workload : AbstractDimension
    {
        public const string Hadoop = "Hadoop";
        public const string Spark = "Spark";

        public override List<object> AllValues()
        {
            return new List<object> { Hadoop, Spark };
        }
    }
}
