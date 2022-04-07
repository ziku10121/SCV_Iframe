using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OverView_WebServer.Models
{
    public class ActivityResultQuest
    {
        /// <summary>
        /// 活動Guid
        /// </summary>
        public Guid activityguid { get; }
        /// <summary>
        /// 登入使用者Guid
        /// </summary>
        public Guid userid { get; }
        /// <summary>
        /// 員工編號
        /// </summary>
        public string staffno { get; }
        /// <summary>
        /// 部門名稱
        /// </summary>
        public string dep { get; }
        /// <summary>
        /// 區域名稱
        /// </summary>
        public string gr { get; }
        /// <summary>
        /// 分公司名稱
        /// </summary>
        public string bh { get; }
        /// <summary>
        /// 使用者層級
        /// </summary>
        public int level { get; }
    }
}