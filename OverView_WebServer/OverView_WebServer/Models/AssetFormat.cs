using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OverView_WebServer.Assets.Models;

namespace OverView_WebServer.Models
{
    public class AssetFormat
    {
        public string firmno;
        public TwStock twstock;
        public SubBrokerage sbrokerage;
        public OSU osu;
        public Future future;
        public Trust trust;
        public CMA cma;
        /// <summary>
        /// 總資產
        /// </summary>
        public Decimal assetp;
    }
}