using System;
using System.Collections.Generic;
using Hik.Communication.ScsServices.Service;

namespace TaxiDesktopClient
{
    [ScsService]
    public interface ITaxiService
    {
        int CreateOrder(string Name, string Phone, string StartGeographicalLatitude, string StartGeographicalLongitude, string FinishGeographicalLatitude, string FinishGeographicalLongitude, List<int> Extra);
        decimal GetPrice();
        decimal GetPriceByCoords(string lonA, string latA, string lonZ, string latZ);
        string GetOperatorPhone();
        int GetOrderStatus();
        string GetDriverPhone();
        string GetDriverName();
        Tuple<double,double> GetDriverPosition();
        void AbortOrder();
    }
}
