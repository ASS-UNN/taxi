using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TestingSample.Base;

namespace TestingSample.Test
{
    class CustomerBehaviourTest
    {
        TaxiService service = new TaxiService();

        public CustomerBehaviourTest()
        {
            Debug.Assert(-1 == GetStatus(), "Invalid status of non-existent order");
            Creation1();
            Debug.Assert(1 == GetStatus(), "Status of just-created order should be PENDING (=1)");
            Abort();
            Debug.Assert(-1 == GetStatus(), "Invalid status of aborted order");
            int orderID = Creation2();
            Debug.Assert(1 == GetStatus(), "Status of just-created order should be PENDING (=1) (2)");
            CheckParams_NoDriver();

            Console.WriteLine("Manual intervention required: random driver assigned for order #" + orderID);
            Console.WriteLine("\nPlease press Return...");
            DBLoader.GetInstance().ExecuteUpdate("UPDATE orders SET status = 2 WHERE order_id = " + orderID + ";");
            DBLoader.GetInstance().ExecuteUpdate("UPDATE drivers SET status = 2, current_order = " + orderID + " WHERE login = 'lex';");
            Console.ReadLine();

            Debug.Assert(2 == GetStatus(), "Status of driver-taken order should be ATTENDING (=2)");
            CheckParams_DriverExists();

            Console.WriteLine("Manual intervention required. Marked as completed order #" + orderID);
            Console.WriteLine("\nPlease press Return...");
            DBLoader.GetInstance().ExecuteUpdate("UPDATE orders SET status = 3 WHERE order_id = " + orderID + ";");
            DBLoader.GetInstance().ExecuteUpdate("UPDATE drivers SET status = 1, current_order = NULL WHERE login = 'lex';");
            Console.ReadLine();

            Debug.Assert(3 == GetStatus(), "Status of finished order should be COMPLETED (=3)");
        }

        void CheckParams_NoDriver()
        {
            Debug.Assert(service.GetPrice() > 0, "Negative service cost or system inaccessibility");
            string phone = service.GetOperatorPhone();
            Debug.Assert(phone != null && !phone.Equals(""), "Invalid operator phone acquired");
        }

        void CheckParams_DriverExists()
        {
            string name = service.GetDriverName();
            string phone = service.GetDriverPhone();
            Debug.Assert(name != null && !name.Equals(""), "Invalid driver name acquired");
            Debug.Assert(phone != null && !phone.Equals(""), "Invalid driver phone acquired");
            Tuple<double, double> coords = service.GetDriverPosition();
            Console.WriteLine(coords.Item1);
            Console.WriteLine(coords.Item2);
            DBLoader.GetInstance().ExecuteUpdate("UPDATE drivers SET coord1 = '42.000000', coord2 = '18.000123' WHERE login = 'lex';");
            coords = service.GetDriverPosition();
            Console.WriteLine(coords.Item1);
            Console.WriteLine(coords.Item2);
        }

        void Creation1()
        {
            string name = "Zeus the Great";
            string phoneB = "4242";
            string phoneG = "+79114947124";
            List<int> optionsB = new List<int>() { 142, 11, 1 };
            List<int> optionsG1 = new List<int>() { 3, 4 };
            int orderID = service.CreateOrder(name, phoneB, "0","0","0","0", optionsG1);
            Debug.Assert(orderID == -1, "Invalid return ID from intentionally erroneous order creation (bad phone)");
            orderID = service.CreateOrder(name, phoneG, "0","0","0","0", optionsB);
            Debug.Assert(orderID == -1, "Invalid return ID from intentionally erroneous order creation (bad options)");
            orderID = service.CreateOrder(name, phoneG, "0", "0", "0", "0", optionsG1);
            Debug.Assert(orderID > 0, "Invalid return ID from correct order creation");
        }

        int Creation2()
        {
            String name = "Zeus Ultimating";
            String phoneG = "+79114947125";
            List<int> optionsG2 = new List<int>();
            int orderID = service.CreateOrder(name, phoneG, "0", "0", "0", "0", optionsG2);
            Debug.Assert(orderID > 0, "Invalid return ID from correct order creation (2)");
            return orderID;
        }

        void Abort()
        {
            service.AbortOrder();
        }

        int GetStatus()
        {
            return service.GetOrderStatus();
        }

    }
}
