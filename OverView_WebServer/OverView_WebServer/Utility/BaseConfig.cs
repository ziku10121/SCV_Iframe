using FDIPConfig.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OverView_WebServer.Utility
{
    public class BaseConfig
    {
        /// <summary>
        /// 資料庫  MSSQL or MySQL
        /// </summary>
        public static DefSQLType SQLType = DefSQLType.MSSQL;
    }
}