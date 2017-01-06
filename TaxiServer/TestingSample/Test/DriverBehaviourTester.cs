using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace TestingSample
{
    class DriverBehaviourTester
    {
        TaxiService service = new TaxiService();

        public DriverBehaviourTester()
        {
            int myID = CheckLogon();
            int orderID = ObtainOrder();
            TakeOrder(orderID);
            UpdateCorrdinates("1.0", "0.0");
            UpdateCorrdinates("-7.204", "16.535");
            CompleteOrder();
            int newOrderID;
            Debug.Assert(orderID != (newOrderID = ObtainOrder()), "Driver got the same order twice");
        }

        int CheckLogon()
        {
            string loginG = "lex";
            string loginB = "badlogin";
            string passwordG = "1234";
            string passwordB = "badpassword";

            int myID = service.LogDriverIn(loginB, passwordB);
            Debug.Assert(-1 == myID, "Got invalid driver ID on intentionally wrong input");
            myID = service.LogDriverIn(loginG, passwordB);
            Debug.Assert(-1 == myID, "Got invalid driver ID on intentionally wrong input");
            myID = service.LogDriverIn(loginB, passwordG);
            Debug.Assert(-1 == myID, "Got invalid driver ID on intentionally wrong input");
            myID = service.LogDriverIn(loginG, passwordG);
            Debug.Assert(myID > 0, "Invalid driver ID on correct input");
            return myID;
        }

        int ObtainOrder()
        {
            var orders = service.GetAvailableOrders();
            Debug.Assert(orders != null, "Null returned instead of order list");
            Debug.Assert(orders.Count > 0, "Got no available orders.");
            var order = orders[orders.Count - 1];
            Debug.Assert(order.Item1 > 0, "Invalid order ID while taking from the list of orders");
            return order.Item1;
        }

        void TakeOrder(int orderID)
        {
            Debug.Assert(!service.TakeOrder(-1), "Order with invalid ID taken");
            Debug.Assert(service.TakeOrder(orderID), "Couldn't take order with valid ID");
        }

        void UpdateCorrdinates(string lon, string lat)
        {
            service.UpdateDriverPosition(lon, lat);
            Console.WriteLine("Driver position should be updated to (" + lon + ", " + lat + "). Please manually check the database");
            Console.ReadLine();
        }

        void CompleteOrder()
        {
            Debug.Assert(service.MarkOrderComplete(), "Couldn't finish order with valid ID");
        } 
    }
}
