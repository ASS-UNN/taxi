using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using TestingSample.Base;

namespace TestingSample.Model
{
    class OrderPoolModel
    {
        //Singleton implementation stuff
        private static OrderPoolModel instance = null;
        private OrderPoolModel() {}
        public static OrderPoolModel GetInstance()
        {
            if (instance == null)
                return instance = new OrderPoolModel();
            else return instance;
        }
        
        //class body
        DBLoader db = DBLoader.GetInstance();
        String query = null;

        private String FillContext(String contextString, Dictionary<String, String> contextVars)
        {
            foreach (KeyValuePair<String, String> contextVar in contextVars)
                contextString = contextString.Replace("{" + contextVar.Key + "}", contextVar.Value);
            return contextString;
        }

        public void UpdateOrderStatus(int orderID, int withStatus = CONST.ORDER_STATUS_INACTIVE)
        {
            String id = orderID.ToString();
            String status = withStatus.ToString();
            query = "UPDATE orders SET status = {status} WHERE order_id = {id};";
            query = FillContext(query, new Dictionary<string, string>() { { "id", id }, {"status", status }});
            db.ExecuteUpdate(query);
        }
        public OrderInstanceModel GetOrderByID(int id)
        {
            OrderInstanceModel model = null;
            query = "SELECT * FROM orders WHERE order_id = {id};";
            query = FillContext(query, new Dictionary<string, string>() { { "id", id.ToString() } });
            SQLiteDataReader order = db.ExecuteSelect(query);
            query = "SELECT option_id FROM order_options WHERE order_id = {id};";
            query = FillContext(query, new Dictionary<string, string>() { { "id", id.ToString() } });
            SQLiteDataReader orderOptions;
            List<int> options = new List<int>();
            try
            {
                orderOptions = db.ExecuteSelect(query);
                do
                {
                    options.Add((int)orderOptions[CONST.OPTION_ID]);
                } while (orderOptions.Read());
            }
            catch (Exception)
            {
                options = null;
            }
            model = new OrderInstanceModel(
                (String)order[CONST.CUST_NAME],
                (String)order[CONST.PHONE],
                (double)order[CONST.COORD1],
                (double)order[CONST.COORD2],
                (double)order[CONST.COORD3],
                (double)order[CONST.COORD4],
                options);
            model.SetStatus((int)order[CONST.STATUS]);
            return model;
        }

        public int CreateOrder(OrderInstanceModel order)
        {
            query = "INSERT INTO orders (cust_name, phone, coord1, coord2, coord3, coord4, status) VALUES " +
                "({cust_name}, {phone}, {coord1}, {coord2}, {coord3}, {coord4}, {status});";
            var context = order.Context();
            query = FillContext(query, context);
            return db.ExecuteInsert(query);
        }

        public DriverInstanceModel GetDriverByLogin(string username, string pwd)
        {
            query = "SELECT * FROM drivers WHERE login = {login} AND password = {password};";
            query = FillContext(query, new Dictionary<string, string>() { { CONST.LOGIN, username }, {CONST.PASSWORD, pwd} });
            SQLiteDataReader reader = null;
            try
            {
                reader = db.ExecuteSelect(query);
            }
            catch (Exception)
            {
                return null;
            }

            DriverInstanceModel driver = new DriverInstanceModel(
                (int)reader[CONST.DRIVER_ID],
                (String)reader[CONST.DRIVER_NAME], 
                username, pwd, 
                (String)reader[CONST.PHONE],
                (double)reader[CONST.COORD1], 
                (double)reader[CONST.COORD2], 
                (int)reader[CONST.STATUS], 
                (int)reader[CONST.RATING]);
            return driver;
        }

        public int GetOrderStatus(int orderID)
        {
            query = "SELECT status FROM orders WHERE order_id = {order_id};";
            query = FillContext(query, new Dictionary<string, string>() { { CONST.ORDER_ID, orderID.ToString() } });
            SQLiteDataReader reader = null;
            try
            {
                reader = db.ExecuteSelect(query);
            }
            catch (Exception)
            {                
                return -1;
            }
            return (int)reader[CONST.STATUS];
        }

        internal bool TieDriverAndOrder(int driverID, int orderID)
        {
            int status = GetOrderStatus(orderID);
            if (status != CONST.ORDER_STATUS_PENDING)
                return false;

            query = "UPDATE drivers SET current_order = {current_order}, status = {status} WHERE driver_id = {driver_id};";
            query = FillContext(query, new Dictionary<string, string>() 
                { { CONST.DRIVER_ID, driverID.ToString()}, {CONST.CURRENT_ORDER, orderID.ToString()}, {CONST.STATUS, CONST.DRIVER_STATUS_ATTENDING.ToString()} });
            db.ExecuteUpdate(query);
            return true;
        }

        internal bool CompleteOrder(int driverID)
        {
            query = "SELECT current_order FROM drivers WHERE driver_id = {driver_id};";
            query = FillContext(query, new Dictionary<string, string>() { { CONST.DRIVER_ID, driverID.ToString() } });
            SQLiteDataReader reader = db.ExecuteSelect(query);
            String orderID = (String)reader[CONST.CURRENT_ORDER];
            
            query = "UPDATE drivers SET current_order = NULL, status = {status} WHERE driver_id = {driver_id};";
            query = FillContext(query, new Dictionary<string, string>() { { CONST.DRIVER_ID, driverID.ToString() }, { CONST.STATUS, CONST.DRIVER_STATUS_IDLE.ToString() } });
            db.ExecuteUpdate(query);

            query = "UPDATE orders SET status = {status} WHERE order_id = {order_id};";
            query = FillContext(query, new Dictionary<string, string>() { { CONST.ORDER_ID, orderID }, {CONST.STATUS, CONST.ORDER_STATUS_COMPLETED.ToString()} });
            db.ExecuteUpdate(query);
            return true;
        }

        internal void UpdateDriverPosition(int driverID, double lon, double lat)
        {
            query = "UPDATE drivers SET coord1 = {coord1}, coord2 = {coord2} WHERE driver_id = {driver_id};";
            query = FillContext(query, new Dictionary<string, string>() { { CONST.DRIVER_ID, driverID.ToString() },
                {CONST.COORD1, Convert.ToString(lon) }, {CONST.COORD2, Convert.ToString(lat) }});
            db.ExecuteUpdate(query);
        }

        public Object GetDriverDataFromOrder(String datatype, int orderID)
        {
            query = "SELECT {datatype} FROM drivers WHERE current_order = {current_order};";
            var context = new Dictionary<String, String>() { {"datatype", datatype }, {CONST.CURRENT_ORDER, orderID.ToString()} };
            query = FillContext(query, context);
            SQLiteDataReader reader = db.ExecuteSelect(query);
            return reader[datatype];
        }

        internal List<Tuple<int, double, double, double, double>> GetAvailableOrders()
        {
            var orders = new List<Tuple<int, double, double, double, double>>();
            query = "SELECT order_id, coord1, coord2, coord3, coord4 FROM orders WHERE status = {status};";
            query = FillContext(query, new Dictionary<string, string>() { { CONST.STATUS, CONST.ORDER_STATUS_PENDING.ToString() } });
            SQLiteDataReader reader = db.ExecuteSelect(query);
            do
            {
                var orderData = new Tuple<int, double, double, double, double>(
                    (int)reader[CONST.ORDER_ID],
                    (double)reader[CONST.COORD1],
                    (double)reader[CONST.COORD2],
                    (double)reader[CONST.COORD3],
                    (double)reader[CONST.COORD4]);
                orders.Add(orderData);
            } while (reader.Read());
            return orders;
        }
    }
}
