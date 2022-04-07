using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OverView_WebServer.Assets.Models
{
    public class TwStock
    {
        /// <summary>
        /// 現股
        /// </summary>
        public Decimal stockp;
        /// <summary>
        /// 融資
        /// </summary>
        public Decimal marginp;
        ///<summary>
        /// 融券
        /// </summary>
        public Decimal ssellp;
        ///<summary>
        /// 出借
        /// </summary>
        public Decimal loanp;
        ///<summary>
        /// 不限用途
        /// </summary>
        public Decimal nrecp;
        /// <summary>
        /// 境內衍生性金融
        /// </summary>
        public Decimal dfinp;
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