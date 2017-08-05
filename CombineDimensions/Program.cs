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
            var version = new MyVersion(location);

            try
            {
                while(true)
                {
                    var l = version.Merge().ToList();
                    Console.WriteLine(l.ToPrettyString());
                    Console.WriteLine(l.ToTypeString());
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("I'm done");
            }

            // Console.WriteLine(new List<object> { "aloha", 1 });
        }
    }
}
