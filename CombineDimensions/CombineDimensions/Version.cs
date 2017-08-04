//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CombineDimensions
//{
//    public class MyVersion<TNext> : AbstractDimension<double, TNext> where TNext : IDimension<object>
//    {
//        public const double One = 1.0;

//        public const double Two = 2.0;

//        public MyVersion(TNext nextDimension) : base(nextDimension)
//        {
//        }

//        public override IList<double> AllValues()
//        {
//            return new List<double> { One, Two};
//        }
//    }
//}
