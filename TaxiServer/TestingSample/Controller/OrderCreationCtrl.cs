using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TestingSample.Model;
using TestingSample.Base;

namespace TestingSample.Controller
{
    /// <summary>
    /// Controller for order creation routine.
    /// </summary>
    static class OrderCreationCtrl
    {
        public static string INVALID_NAME = null;
        public static string INVALID_PHONE = null;
        public static string INVALID_COORD = null;
        public static List<int> INVALID_OPTIONS = null;

        public static string ValidateName(string name)
        {
            if(name == null || name.Equals(""))
                return INVALID_NAME;

            //Basically name may contain only alphabetical characters, whitespaces and an apostrophe
            foreach(char ch in name)
            {
                if (ch != ' ' &&
                    ch != '\'' &&
                    Char.IsLetter(ch) == false)
                    return INVALID_NAME;
            }
            return name;
        }

        public static string ValidatePhoneNumber(string phone)
        {
            if (phone == null || phone.Equals(""))
                return INVALID_PHONE;

            phone = Regex.Replace(phone, @"\s+", ""); //kill whitespaces

            if (phone[0] == '+')
            {
                if (phone.Substring(1).Length != 11)
                    return INVALID_PHONE;
                foreach (char ch in phone.Substring(1))
                    if (Char.IsDigit(ch) == false)
                        return INVALID_PHONE;
            }
            else // 8 (xxxx) xxxxxx format
            {
                if (phone.Length != 11)
                    return INVALID_PHONE;
                foreach (char ch in phone)
                    if (Char.IsDigit(ch) == false)
                        return INVALID_PHONE;
            }
            return phone;            
        }

        public static string ValidateCoordinateFormat(string coord)
        {
            if (coord == null)
                return INVALID_COORD;

            System.Console.Out.WriteLine("Dummy coordinate parser is in use.");
            return coord;
        }

        public static List<int> ValidateExtraOptions(List<int> options)
        {
            if (options == null)
                return INVALID_OPTIONS;

            foreach (int id in options)
                if (id > CONST.OPT_FEMALE_DRIVER || id < 1)
                    return INVALID_OPTIONS;

            return options;
        }

        public static int CreateOrder(string name, string phone, string lonA, string latA, string lonZ, string latZ,
            List<int> options)
        {
            OrderInstanceModel order = new OrderInstanceModel(name, phone, lonA, latA, lonZ, latZ, options);
            order.SetStatus(CONST.ORDER_STATUS_PENDING);
            order.ForceSetOrderID(OrderPoolModel.GetInstance().CreateOrder(order)); //order stored into DB
            return order.orderID;
        }
    }
}
