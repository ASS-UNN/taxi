using System;
using System.Collections.Generic;
using Hik.Communication.ScsServices.Service;

namespace TaxiDesktopClient
{
    [ScsService]
    public interface ITaxiService
    {
        string ShowTest(int N);
        int CreateOrder(string Name, string Phone, string From, string To, List<string> Extra);
        int CreateOrder(string Name, string Phone, float FromX, float FromY, string To, List<string> Extra);
        int CreateOrder(string Name, string Phone, string From, float ToX, float ToY, List<string> Extra);
        int CreateOrder(string Name, string Phone, float FromX, float FromY, float ToX, float ToY, List<string> Extra);
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
