using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OverView_WebServer.Assets.Models
{
    public class Future
    {
        /// <summary>
        /// 國內期貨
        /// </summary>
        public Decimal dfutp;
        /// <summary>
        /// 國外期貨
        /// </summary>
        public Decimal ofutp;
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