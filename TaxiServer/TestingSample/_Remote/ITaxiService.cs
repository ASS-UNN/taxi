using System;
using System.Collections.Generic;
using Hik.Communication.ScsServices.Service;

namespace TestingSample
{
    [ScsService]
    public interface ITaxiService
    {
        int CreateOrder(string Name, string Phone, double StartGeographicalLatitude, double StartGeographicalLongitude, double FinishGeographicalLatitude, double FinishGeographicalLongitude, List<int> Extra);
        decimal GetPrice(int OrderID);
        decimal GetPriceByCoords(double lonA, double latA, double lonZ, double latZ);
        string GetOperatorPhone();
        int GetOrderStatus(int OrderID);
        string GetDriverPhone(int OrderID);
        string GetDriverName(int OrderID);
        Tuple<double, double> GetDriverPosition(int OrderID);
        void UpdateDriverPosition(double lon, double lat);
        void AbortOrder(int OrderID);
        bool TakeOrder(int orderID);
        bool MarkOrderComplete(int orderID);
        int LogDriverIn(String login, String password);
        List<Tuple<int, double, double, double, double>> GetAvailableOrders();
    }
}
