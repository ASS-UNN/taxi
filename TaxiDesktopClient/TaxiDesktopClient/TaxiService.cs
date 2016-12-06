using System;
using System.Collections.Generic;
using Hik.Communication.ScsServices.Service;

namespace TaxiDesktopClient
{
    [ScsService]
    public interface ITaxiService
    {
        int CreateOrder(string Name, string Phone, string StartAddress, string StartGeographicalLatitude, string StartGeographicalLongitude, string FinishAddress, string FinishGeographicalLatitude, string FinishGeographicalLongitude, List<string> Extra);
        float GetPrice(int OrderID);
        string GetOperatorPhone();
        int GetOrderStatus(int OrderID);
        string GetDriverPhone(int OrderID);
        string GetDriverName(int OrderID);
        Coord GetDriverPosition(int OrderID);
        void AbortOrder(int OrderID);
    }

    public struct Coord
    {
        public float x;
        public float y;
    };
}
