using System;
using System.Collections.Generic;
using Hik.Communication.ScsServices.Service;

namespace TestingSample
{
    [ScsService]
    public interface ITaxiService
    {
        int CreateOrder(string Name, string Phone, string StartAddress, string StartGeographicalLatitude, string StartGeographicalLongitude, string FinishAddress, string FinishGeographicalLatitude, string FinishGeographicalLongitude, List<string> Extra);
        decimal GetPrice(int OrderID);
        string GetOperatorPhone();
        int GetOrderStatus(int OrderID);
        string GetDriverPhone(int OrderID);
        string GetDriverName(int OrderID);
        Tuple<float, float> GetDriverPosition(int OrderID);
        void AbortOrder(int OrderID);
    }
}
