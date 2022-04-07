using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OverView_WebServer.Assets.Models
{
    public class Trust
    {
        /// <summary>
        /// 特定金錢信託
        /// </summary>
        public Decimal sptrustp;
        /// <summary>
        /// 信託借券
        /// </summary>
        public Decimal trustloanp;
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