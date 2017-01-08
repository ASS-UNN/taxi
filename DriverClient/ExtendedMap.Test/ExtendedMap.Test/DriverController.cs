using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedMap.Test
{
    class DriverController
    {
        public Driver driver;
        public DriverController()
        {
            driver = new Driver();
        }
    }
    public class Driver {
        public List<Tuple<int, double, double, double, double>> listOfOrder = new List<Tuple<int, double, double, double, double>> {
            Tuple.Create<int, double, double, double, double>(1, 56.295305, 43.943791, 56.299691, 43.982526),
            Tuple.Create<int, double, double, double, double>(2, 56.295305, 43.943791, 56.311364, 43.938693)
        };

        public Tuple <double, double> positionDriver = new Tuple<double, double>(56.298789, 43.962556);

        public bool canLog(string login, string password) {
            return false;
        }
        public void updateList() {
            listOfOrder = null;
        }
        public string takeOrder()
        {
            return "OK!";
        }

    };
}
