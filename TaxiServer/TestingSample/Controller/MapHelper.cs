using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestingSample.Controller
{
    class MapHelper
    {
        public static Tuple<double, double, double, double> ConvertTextAddressToCoords(string locA, string locZ)
        {
            Random rnd = new Random();
            return new Tuple<double, double, double, double>((double)rnd.NextDouble(), (double)rnd.NextDouble(),
                (double)rnd.NextDouble(), (double)rnd.NextDouble());
        }
    }
}
