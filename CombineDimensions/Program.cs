using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombineDimensions
{
    class Program
    {
        static void Main(string[] args)
        {
            var location = new Location();
            var version = new MyVersion();
            var workload = new Workload();

            foreach (var comb in location)
            {
                var l = comb;
                Console.WriteLine(l);
            }

            foreach (var comb in DimensionCombiner.Combine(version, location))
            {
                var l = (List<object>)comb;
                Console.WriteLine(l.ToPrettyString());
            }

            foreach (var comb in DimensionCombiner.Combine(version, location, workload))
            {
                var l = (List<object>)comb;
                Console.WriteLine(l.ToPrettyString());
            }
        }
    }
}
