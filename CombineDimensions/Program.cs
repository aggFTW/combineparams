namespace CombineDimensions
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            var location = new Location();
            var version = new MyVersion();
            var workload = new Workload();

            foreach (var comb in location)
            {
                // Prints:
                // East
                // West
                var l = comb;
                Console.WriteLine(l);
            }

            foreach (var comb in DimensionCombiner.Combine(version, location))
            {
                // Prints:
                // [1, East]
                // [1, West]
                // [2, East]
                // [2, West]
                var l = (List<object>)comb;
                Console.WriteLine(l.ToPrettyString());
            }

            foreach (var comb in DimensionCombiner.Combine(version, location, workload))
            {
                // Prints:
                // [1, East, Hadoop]
                // [1, East, Spark]
                // [1, West, Hadoop]
                // [2, East, Hadoop]
                // [2, East, Spark]
                // [2, West, Hadoop]
                var l = (List<object>)comb;
                Console.WriteLine(l.ToPrettyString());
            }

            // NOTE!
            // Notice that we need to use new location objects! ...
            // Otherwise there might be a stack overflow exception.
            foreach (var comb in DimensionCombiner.Combine(location, new Location(), workload, version))
            {
                // Prints:
                // [East, East, Hadoop, 1]
                // [East, East, Hadoop, 2]
                // [East, East, Spark, 1]
                // [East, West, Hadoop, 1]
                // [West, East, Hadoop, 1]
                // [West, East, Hadoop, 2]
                // [West, East, Spark, 1]
                // [West, West, Hadoop, 1]
                var l = (List<object>)comb;
                Console.WriteLine(l.ToPrettyString());
            }

            // DimensionCombiner checks that it´s not the same objects though.
            try
            {
                foreach (var comb in DimensionCombiner.Combine(location, location, workload, version))
                {
                    var l = (List<object>)comb;
                    Console.WriteLine(l.ToPrettyString());
                }
            }
            catch (NotSupportedException)
            {
                Console.WriteLine("We prevented a stack overflow exception!");
            }
        }
    }
}
