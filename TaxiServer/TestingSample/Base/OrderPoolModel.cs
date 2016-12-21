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

        private int MaybeInt(Object obj)
        {
            if (!obj.Equals(DBNull.Value))
                return Convert.ToInt32(obj);
            else return -1;
        }
        private String MaybeString(Object obj)
        {
            if (!obj.Equals(DBNull.Value))
                return Convert.ToString(obj);
            else return "";
        }

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
                    options.Add(MaybeInt(orderOptions[CONST.OPTION_ID]));
                } while (orderOptions.Read());
            }
            catch (Exception)
            {
                options = null;
            }
            model = new OrderInstanceModel(
                MaybeString(order[CONST.CUST_NAME]),
                MaybeString(order[CONST.PHONE]),
                MaybeString(order[CONST.COORD1]),
                MaybeString(order[CONST.COORD2]),
                MaybeString(order[CONST.COORD3]),
                MaybeString(order[CONST.COORD4]),
                options);
            model.SetStatus(MaybeInt(order[CONST.STATUS]));
            model.ForceSetOrderID(MaybeInt(order[CONST.ORDER_ID]));
            return model;
        }

        public int CreateOrder(OrderInstanceModel order)
        {
            query = "INSERT INTO orders (cust_name, phone, coord1, coord2, coord3, coord4, status) VALUES " +
                "('{cust_name}', '{phone}', '{coord1}', '{coord2}', '{coord3}', '{coord4}', {status});";
            var context = order.Context();
            query = FillContext(query, context);
            return db.ExecuteInsert(query);
        }

        public DriverInstanceModel GetDriverByLogin(string username, string pwd)
        {
            query = "SELECT * FROM drivers WHERE login = '{login}' AND password = '{password}';";
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
                MaybeInt(reader[CONST.DRIVER_ID]),
                MaybeString(reader[CONST.DRIVER_NAME]), 
                username, pwd, 
                MaybeString(reader[CONST.PHONE] ),
                MaybeString(reader[CONST.COORD1]), 
                MaybeString(reader[CONST.COORD2]),
                MaybeInt(reader[CONST.STATUS]),
                MaybeInt(reader[CONST.RATING]));
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
            return MaybeInt(reader[CONST.STATUS]);
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

            query = "UPDATE orders SET status = {status} WHERE order_id = {order_id};";
            query = FillContext(query, new Dictionary<string, string>() { { CONST.ORDER_ID, orderID.ToString() }, { CONST.STATUS, CONST.ORDER_STATUS_ATTENDED.ToString() } });
            db.ExecuteUpdate(query);
            return true;
        }

        internal bool CompleteOrder(int driverID)
        {
            query = "SELECT current_order FROM drivers WHERE driver_id = {driver_id};";
            query = FillContext(query, new Dictionary<string, string>() { { CONST.DRIVER_ID, driverID.ToString() } });
            SQLiteDataReader reader = db.ExecuteSelect(query);
            String orderID = MaybeString(reader[CONST.CURRENT_ORDER]);
            
            query = "UPDATE drivers SET current_order = NULL, status = {status} WHERE driver_id = {driver_id};";
            query = FillContext(query, new Dictionary<string, string>() { { CONST.DRIVER_ID, driverID.ToString() }, { CONST.STATUS, CONST.DRIVER_STATUS_IDLE.ToString() } });
            db.ExecuteUpdate(query);

            query = "UPDATE orders SET status = {status} WHERE order_id = {order_id};";
            query = FillContext(query, new Dictionary<string, string>() { { CONST.ORDER_ID, orderID }, {CONST.STATUS, CONST.ORDER_STATUS_COMPLETED.ToString()} });
            db.ExecuteUpdate(query);
            return true;
        }

        internal void UpdateDriverPosition(int driverID, string lon, string lat)
        {
            query = "UPDATE drivers SET coord1 = '{coord1}', coord2 = '{coord2}' WHERE driver_id = {driver_id};";
            query = FillContext(query, new Dictionary<string, string>() { { CONST.DRIVER_ID, driverID.ToString() },
                {CONST.COORD1, lon }, {CONST.COORD2, lat }});
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

        internal List<Tuple<int, string, string, string, string>> GetAvailableOrders()
        {
            var orders = new List<Tuple<int, string, string, string, string>>();
            query = "SELECT order_id, coord1, coord2, coord3, coord4 FROM orders WHERE status = {status};";
            query = FillContext(query, new Dictionary<string, string>() { { CONST.STATUS, CONST.ORDER_STATUS_PENDING.ToString() } });
            SQLiteDataReader reader = db.ExecuteSelect(query);
            do
            {
                var orderData = new Tuple<int, string, string, string, string>(
                    MaybeInt(reader[CONST.ORDER_ID]),
                    MaybeString(reader[CONST.COORD1]),
                    MaybeString(reader[CONST.COORD2]),
                    MaybeString(reader[CONST.COORD3]),
                    MaybeString(reader[CONST.COORD4]));
                orders.Add(orderData);
            } while (reader.Read());
            return orders;
        }

        internal int DecreaseDriverStatus(int orderID)
        {
            Object currentRating = GetDriverDataFromOrder(CONST.RATING, orderID);
            int rating = Convert.ToInt32(currentRating);
            if (rating < 1) return -1;
            rating--;
            int driverID = Convert.ToInt32(OrderPoolModel.GetInstance().GetDriverDataFromOrder(CONST.DRIVER_ID, orderID));
            query = "UPDATE drivers SET rating = {rating} WHERE driver_id = {driver_id};";
            query = FillContext(query, new Dictionary<string, string>() { { CONST.RATING, rating.ToString() }, { CONST.DRIVER_ID, driverID.ToString() } });
            db.ExecuteUpdate(query);
            return rating;
        }
        internal int IncreaseDriverStatus(int orderID)
        {
            Object currentRating = GetDriverDataFromOrder(CONST.RATING, orderID);
            int rating = Convert.ToInt32(currentRating);
            rating++;
            int driverID = Convert.ToInt32(OrderPoolModel.GetInstance().GetDriverDataFromOrder(CONST.DRIVER_ID, orderID));
            query = "UPDATE drivers SET rating = {rating} WHERE driver_id = {driver_id};";
            query = FillContext(query, new Dictionary<string, string>() { { CONST.RATING, rating.ToString() }, { CONST.DRIVER_ID, driverID.ToString() } });
            db.ExecuteUpdate(query);
            return rating;
        }
    }
}
