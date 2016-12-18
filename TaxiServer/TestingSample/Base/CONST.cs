using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestingSample.Base
{
    static class CONST
    {
        //db column names
        public const String DB_NAME =      "db.sqlite";
        public const String CUST_NAME =    "cust_name";
        public const String STATUS =       "status";
        public const String COORD1 =       "coord1";
        public const String COORD2 =       "coord2";
        public const String COORD3 =       "coord3";
        public const String COORD4 =       "coord4";
        public const String ORDER_ID =     "order_id";
        public const String OPTION_ID =    "option_id";
        public const String DRIVER_ID =    "driver_id";
        public const String DRIVER_NAME =  "name";
        public const String LOGIN =        "login";
        public const String PASSWORD =     "password";
        public const String PHONE =        "phone";
        public const String CURRENT_ORDER ="currentorder";
        public const String RATING =       "rating";
        public const String TEXT =         "text";

        //values
        public const int OPT_CHILD_SIT = 1;
        public const int OPT_LIFTER = 2;
        public const int OPT_PET_TRANSFER = 3;
        public const int OPT_LUGGAGE = 4;
        public const int OPT_BODY_WAGON = 5;
        public const int OPT_SILENT_DRIVER = 6;
        public const int OPT_FEMALE_DRIVER = 7;

        public const int ORDER_STATUS_INACTIVE = 0;
        public const int ORDER_STATUS_PENDING = 1;
        public const int ORDER_STATUS_ATTENDED = 2;
        public const int ORDER_STATUS_COMPLETED = 3;
        public const int ORDER_STATUS_ABORTED = 4;

        public const int DRIVER_STATUS_UNAVAILABLE = 0;
        public const int DRIVER_STATUS_IDLE = 1;
        public const int DRIVER_STATUS_ATTENDING = 2;

        public const double POSITION_EPSILON = 1E-10;
    }
}
