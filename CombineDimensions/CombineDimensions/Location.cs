//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CombineDimensions
//{
//    public class Location<TNext> : AbstractDimension<string, TNext> where TNext : IDimension<object>
//    {
//        public const string East = "East";

//        public const string West = "West";

//        public Location(TNext nextDimension) : base(nextDimension)
//        {
//        }
        
//        public override IList<string> AllValues()
//        {
//            return new List<string> { East, West };
//        }
//    }
//}
