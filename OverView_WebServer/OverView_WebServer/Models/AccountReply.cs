using FDIPDefinition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OverView_WebServer.Models
{
    public class AccountReply
    {
        public Status.StatusEnum status = Status.StatusEnum.UNDIFINE;
        public string errorMsg;
        public bool isFirm;
        public List<Firm> AllCompany;
        public List<AccountFormat> actData;
    }
}