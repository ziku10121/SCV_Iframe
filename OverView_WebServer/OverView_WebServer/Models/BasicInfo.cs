using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OverView_WebServer.Models
{
    public class BasicInfo
    {
        public string actno;
        public string idno;
        public string idnomsk;
        public string name;
        public string cusgrptype;
        public int gender;
        public int age;
        public DateTime bday;
        public string mate;
    }

    public class Status
    {
        /// <summary>
        /// 登入情況狀態碼
        /// </summary>
        public enum StatusEnum
        {
            //未定義
            UNDIFINE = 0,
            //成功抓取資料
            SUCCESS = 1,
            //SQL連線失敗
            DB_FAIL = 2,
            //以上錯誤
            FAIL = 2
        }
    }
}