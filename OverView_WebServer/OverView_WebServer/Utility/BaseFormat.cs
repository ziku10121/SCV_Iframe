using FubonQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OverView_WebServer.Utility
{
    public class BaseFormat
    {
        public enum UserLevel
        {
            /// <summary>Admin</summary>
            Admin = 99,
            /// <summary>總經理</summary>
            AllCompany = 1,
            /// <summary>部長</summary>
            Minister = 2,
            /// <summary>區長</summary>
            Mayor = 3,
            /// <summary>經理人</summary>
            Manager = 4,
            /// <summary>副經理人</summary>
            DepManager = 5,
            /// <summary>組長</summary>
            Leader = 6,
            /// <summary>營業員</summary>
            Salesperson = 7
        }
    }

}