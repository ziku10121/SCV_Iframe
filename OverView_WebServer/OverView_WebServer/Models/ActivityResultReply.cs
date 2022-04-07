using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OverView_WebServer.Models
{
    public class ActivityResultReply
    {
        public Status.StatusEnum status = Status.StatusEnum.UNDIFINE;
        public string errorMsg;
    }
}