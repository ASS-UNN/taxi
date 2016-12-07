using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestingSample.Model
{
    class OrderPoolModel
    {
        //Singleton implementation stuff
        private static OrderPoolModel instance = null;
        private OrderPoolModel() { }
        public static OrderPoolModel GetInstance()
        {
            if (instance == null)
                return instance = new OrderPoolModel();
            else return instance;
        }
        
        //class body
        private Object guardLock = new Object(); //thread safety object
        private Dictionary<int, OrderInstanceModel> orders = new Dictionary<int, OrderInstanceModel>();
        private int primaryKey = -1;

        public int GenerateOrderID(OrderInstanceModel newModel)
        {
            lock (guardLock)
            {
                orders.Add(++primaryKey, newModel);
                return primaryKey;
            }
        }
        public void RemoveOrder(OrderInstanceModel model)
        {
            lock (guardLock)
            {
                orders.Remove(model.orderID);
            }
        }
        public OrderInstanceModel GetOrderByID(int id)
        {
            OrderInstanceModel model = null;
            if (orders.TryGetValue(id, out model))
                return model;
            else return null;
        }
    }
}
