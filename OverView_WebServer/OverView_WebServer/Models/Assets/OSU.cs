using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OverView_WebServer.Assets.Models
{
    public class OSU
    {
        /// <summary>
        /// 海外股票
        /// </summary>
        public Decimal ostockp;
        /// <summary>
        /// 海外債
        /// </summary>
        public Decimal odebtp;
        /// <summary>
        /// 境外基金
        /// </summary>
        public Decimal ofundp;
        /// <summary>
        /// 結構型商品
        /// </summary>
        public Decimal structp;
        /// <summary>
        /// 總額
        /// </summary>
        public Decimal totalp;
        /// <summary>
        /// 佔比
        /// </summary>
        public int pr;
    }
}