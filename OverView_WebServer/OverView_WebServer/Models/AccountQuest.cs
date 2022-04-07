using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OverView_WebServer.Models
{
    public class AccountQuest
    {
        /// <summary>
        /// 客戶身分證id
        /// </summary>
        public string idno { get; set; }
        /// <summary>
        /// 登入使用者Guid
        /// </summary>
        public Guid userid { get; set; }
        /// <summary>
        /// 員工編號
        /// </summary>
        public string staffno { get; set; }
        /// <summary>
        /// 部門名稱
        /// </summary>
        public string dep { get; set; }
        /// <summary>
        /// 區域名稱
        /// </summary>
        public string gr { get; set; }
        /// <summary>
        /// 分公司名稱
        /// </summary>
        public string bh { get; set; }
        /// <summary>
        /// 使用者層級
        /// </summary>
        public int level { get; set; }
    }
}