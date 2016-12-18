using System;
using System.Collections.Generic;
using Hik.Communication.ScsServices.Service;

namespace TaxiDesktopClient
{
    [ScsService]
    public interface ITaxiService
    {
        int CreateOrder(string Name, string Phone, string StartGeographicalLatitude, string StartGeographicalLongitude, string FinishGeographicalLatitude, string FinishGeographicalLongitude, List<int> Extra);
        decimal GetPrice(string StartGeographicalLatitude, string StartGeographicalLongitude, string FinishGeographicalLatitude, string FinishGeographicalLongitude);
        string GetOperatorPhone();
        int GetOrderStatus(int OrderID);
        string GetDriverPhone(int OrderID);
        string GetDriverName(int OrderID);
        Tuple<double,double> GetDriverPosition(int OrderID);
        void AbortOrder(int OrderID);
    }
}
