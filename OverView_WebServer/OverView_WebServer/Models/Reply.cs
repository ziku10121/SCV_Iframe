using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OverView_WebServer.Models
{
    public class Reply
    {
        public Status.StatusEnum status = Status.StatusEnum.UNDIFINE;
        public string errorMsg;
        public Array AllCompany;
        public bool isFirm;
        public List<AccountReply> actData;
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