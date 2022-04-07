using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OverView_WebServer.Models
{
    public class AccountFormat
    {
        //[Attribute]
        public BasicInfo BasicInfo = new BasicInfo();
        public string firmno;
        public string acttype;
        public DateTime actopndate;
        public string email;
        public string mobile;           //手機號碼
        public string phone;            //電話號碼
        public string hraddr;           //戶籍地址
        public string caddr;            //聯絡地址
        public string contact;
        public string agent;
        public int cussrctyp;           //客戶來源
        public Decimal translmt;        //買賣額度
        public int evalidflag;          //Y,N電子戶註記
        public string banknm;
        public Decimal emgstklmt;       //興櫃額度
        public int crvalidflag;          //Y,N信用戶註記
        public Decimal mgntralmt;       //融資額度
        public Decimal sselllmt;        //融券額度
        public int dtrdflag;            //Y,N
        public int piflag;              //Y,N
        public DateTime actclsdate;
        public Decimal dtrdlmt;         //當沖額度
        public DateTime eopndate;
    }
}