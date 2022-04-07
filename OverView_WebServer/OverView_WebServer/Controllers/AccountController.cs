#define normal
using FDIPConfig;
using FDIPDefinition;
using FDIPDefinition.Definition;
using FDIPDefinition.Utility;
using FDIPQuery.QueryProcessor;
using OverView_WebServer.Models;
using OverView_WebServer.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Caching;
using System.Web.Http;

namespace OverView_WebServer.Controllers
{
    public class AccountController : ApiController
    {
        //private const string _classname = "CompanyController";
        private SqlConnection SqlConn = null;

        private Dictionary<string, string> _actTypeData = new Dictionary<string, string>();
        private Dictionary<string, string> _cusGrpTypeData = new Dictionary<string, string>();
        //快取
        private ObjectCache cache = MemoryCache.Default;
        List<Firm> _allfirm = new List<Firm>();
        List<string> company = new List<string>();
        List<string> staffno = new List<string>();
        List<AccountReply> replylist = new List<AccountReply>();
        bool isFirm;

        // Post api/<controller>
        [HttpPost, ActionName("get")]
        public HttpResponseMessage Get([FromBody] AccountQuest _quest)
        {
            //const string _funcname = _classname + "GetCompany";
            Reply _reply = new Reply();

            try
            {
                #region 確認FQO元件是否存在
                if (!CheckFrimData())
                {
                    //CheckFrimData Fail.
                }
                #endregion
                if (!CheckActTypeData())
                {
                    //CheckActType Fail
                }
                if (!CheckCusGrpTypeData())
                {
                    //CusGrpType Fail
                }

                using (SqlConn = new SqlConnection(Properties.Settings.Default.SQLConnectStr))
                {
                    string userStr = string.Format(@"SELECT * FROM dbo.SystemUserBase WHERE SystemUserId = '{0}'", _quest.userguid);
                    int level = 0;
                    SqlCommand cmd = null;
                    List<AccountReply> _templist = new List<AccountReply>();


                    using (cmd = new SqlCommand(userStr, SqlConn))
                    {
                        SqlConn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            level = (reader["new_level"] == null) ? 0 : Convert.ToInt32(reader["new_level"]);
                            reader.Close();

                            company.Clear();
                            replylist.Clear();
#if normal
                            #region if方法
                            if (level == Convert.ToInt32(BaseFormat.UserLevel.Admin))
                            {
                                isFirm = true;
                                Convertion(replylist, filterAct(_quest.idno));
                            }
                            if (level == Convert.ToInt32(BaseFormat.UserLevel.AllCompany)) 
                            {
                                isFirm = true;
                                Convertion(replylist, filterAct(_quest.idno)); 
                            }
                            if (level == Convert.ToInt32(BaseFormat.UserLevel.Minister))
                            {
                                isFirm = true;
                                //use company to search an account
                                filterFirm(_quest.dep, _quest.gr, _quest.bh, _quest.level);
                                //companys
                                foreach (var firmno in company)
                                {
                                    Convertion(replylist, filterAct(_quest.idno, firmno));
                                }
                            }
                            if (level == Convert.ToInt32(BaseFormat.UserLevel.Mayor))
                            {
                                isFirm = true;
                                //use company to search an account
                                filterFirm(_quest.dep, _quest.gr, _quest.bh, _quest.level);
                                //companys
                                foreach (var firmno in company)
                                {
                                    Convertion(replylist, filterAct(_quest.idno, firmno));
                                }
                            }
                            if (level == Convert.ToInt32(BaseFormat.UserLevel.Manager))
                            {
                                isFirm = false;
                                //use company to search an account
                                filterFirm(_quest.dep, _quest.gr, _quest.bh, _quest.level);
                                //company
                                foreach (var firmno in company)
                                {
                                    Convertion(replylist, filterAct(_quest.idno, firmno));
                                }
                            }
                            if (level == Convert.ToInt32(BaseFormat.UserLevel.DepManager))
                            {
                                //use company to search an account
                                filterFirm(_quest.dep, _quest.gr, _quest.bh, _quest.level);
                                isFirm = false;
                                foreach (var firmno in company)
                                {
                                    Convertion(replylist, filterAct(_quest.idno, firmno));
                                }
                                //經理人負責客戶帳戶
                                Convertion(_templist, filterStaffAct(_quest.staffno));
                                //排除經理負責客戶帳戶
                                foreach (var item in _templist)
                                {
                                    replylist.Remove(item);
                                }
                            }
                            if (level == Convert.ToInt32(BaseFormat.UserLevel.Leader))
                            {
                                isFirm = false;
                                //use company to search an account
                                filterFirm(_quest.dep, _quest.gr, _quest.bh, _quest.level);
                                //找出組長管理的營業員員編
                                GetStafNo(_quest.userguid);
                                //加入自己的員編
                                staffno.Add(_quest.staffno);
                                //搜尋每個員編所負責的客戶帳號
                                foreach (string _no in staffno)
                                {
                                    Convertion(replylist, filterStaffAct(_quest.idno, _no));
                                }
                            }
                            if (level == Convert.ToInt32(BaseFormat.UserLevel.Salesperson))
                            {
                                isFirm = false;
                                //use company to search an account
                                filterFirm(_quest.dep, _quest.gr, _quest.bh, _quest.level);
                                //使用客戶id與使用者員編找出客戶帳號
                                Convertion(replylist, filterStaffAct(_quest.idno, _quest.staffno));
                            }

                            _reply.status = Status.StatusEnum.SUCCESS;
                            _reply.AllCompany = company.ToArray();
                            _reply.actData = replylist;
                            _reply.isFirm = isFirm;
                            return Request.CreateResponse(HttpStatusCode.OK, _reply);

                            #endregion if方法
#elif   switchfun
                            #region >>> switch方法
                            switch (level)
                            {
                                case (int)BaseFormat.UserLevel.Admin:
                                    Convertion(replylist, filterAct(_quest.idno));
                                    break;
                                case (int)BaseFormat.UserLevel.AllCompany:
                                    //ALL
                                    Convertion(replylist, filterAct(_quest.idno));
                                    break;
                                case (int)BaseFormat.UserLevel.Minister:
                                    //use company to search an account
                                    filterFirm(_quest.dep, _quest.gr, _quest.bh, _quest.level);
                                    //companys
                                    foreach (var firmno in company)
                                    {
                                        Convertion(replylist, filterAct(_quest.idno, firmno));
                                    }
                                    break;
                                case (int)BaseFormat.UserLevel.Mayor:
                                    //use company to search an account
                                    filterFirm(_quest.dep, _quest.gr, _quest.bh, _quest.level);
                                    //companys
                                    foreach (var firmno in company)
                                    {
                                        Convertion(replylist, filterAct(_quest.idno, firmno));
                                    }

                                    break;
                                case (int)BaseFormat.UserLevel.Manager:
                                    //use company to search an account
                                    filterFirm(_quest.dep, _quest.gr, _quest.bh, _quest.level);
                                    //company
                                    foreach (var firmno in company)
                                    {
                                        Convertion(replylist, filterAct(_quest.idno, firmno));
                                    }

                                    break;
                                case (int)BaseFormat.UserLevel.DepManager:
                                    //use company to search an account
                                    filterFirm(_quest.dep, _quest.gr, _quest.bh, _quest.level);
                                    foreach (var firmno in company)
                                    {
                                        Convertion(replylist, filterAct(_quest.idno, firmno));
                                    }
                                    //經理人負責客戶帳戶
                                    Convertion(_templist, filterStaffAct(_quest.staffno));
                                    //排除經理負責客戶帳戶
                                    foreach (var item in _templist)
                                    {
                                        replylist.Remove(item);
                                    }
                                    break;
                                case (int)BaseFormat.UserLevel.Leader:
                                    //use company to search an account
                                    filterFirm(_quest.dep, _quest.gr, _quest.bh, _quest.level);
                                    //找出組長管理的營業員員編
                                    GetStafNo(_quest.userguid);
                                    //加入自己的員編
                                    staffno.Add(_quest.staffno);
                                    //搜尋每個員編所負責的客戶帳號
                                    foreach (string _no in staffno)
                                    {
                                        Convertion(replylist, filterStaffAct(_quest.idno, _no));
                                    }
                                    break;
                                case (int)BaseFormat.UserLevel.Salesperson:
                                    //use company to search an account
                                    filterFirm(_quest.dep, _quest.gr, _quest.bh, _quest.level);
                                    //使用客戶id與使用者員編找出客戶帳號
                                    Convertion(replylist, filterStaffAct(_quest.idno, _quest.staffno));
                                    break;
                                default:
                                    break;
                            }
                            #endregion End Switch方法
#endif
                        }
                        else
                        {
                            _reply.status = Status.StatusEnum.DB_FAIL;
                            _reply.errorMsg = "無此帳戶資料";
                            return Request.CreateResponse(HttpStatusCode.NotFound, _reply);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _reply.status = Status.StatusEnum.FAIL;
                _reply.errorMsg = e.Message;
                return Request.CreateResponse(HttpStatusCode.BadRequest, _reply);
            }
        }

        /// <summary>
        /// 撈出所有公司基本資料
        /// </summary>
        private QueryReply GetFirm()
        {
            QueryReply _qrfdq = null;
            QueryRequest _qc = new QueryRequest();

            FubonQuery.FubonQProc _fdq = new FubonQuery.FubonQProc(FDQArgs());
            _fdq.Start();
            _qc.FCode = FDIPDefinition.Definition.Query.DefQueryFunc.FIRM;
            _qrfdq = _fdq.Query(_qc);

            _fdq.Stop();
            _fdq = null;

            return _qrfdq;
        }
        private bool CheckFrimData()
        {
            try
            {
                if (cache["FirmData"] == null)
                {
                    QueryReply _qr = GetFirm();
                    if (_qr.Firm_List != null && _qr.Firm_List.Count > 0)
                    {
                        foreach (FDIPDefinition.Firm _per in _qr.Firm_List)
                        {
                            _allfirm.Add(_per);
                        }
                        cache.Add("FirmData", _allfirm, new CacheItemPolicy());
                    }
                }
                else
                {
                    _allfirm = cache["FirmData"] as List<Firm>;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 撈出帳戶類型對應表
        /// </summary>
        private QueryReply GetActType()
        {
            QueryReply _qrfdq = null;
            QueryRequest _qc = new QueryRequest();

            FubonQuery.FubonQProc _fdq = new FubonQuery.FubonQProc(FDQArgs());
            _fdq.Start();
            _qc.FCode = FDIPDefinition.Definition.Query.DefQueryFunc.ACT_CAT;
            _qrfdq = _fdq.Query(_qc);

            _fdq.Stop();
            _fdq = null;

            return _qrfdq;
        }
        private bool CheckActTypeData()
        {
            try
            {
                if (cache["ActTypeData"] == null)
                {
                    QueryReply _qr = GetActType();
                    if (_qr.ActCat_List != null && _qr.ActCat_List.Count > 0)
                    {
                        foreach (FDIPDefinition.ActCategory _per in _qr.ActCat_List)
                        {
                            _actTypeData.Add(_per.ActType, _per.ActName);
                        }
                        cache.Add("ActTypeData", _actTypeData, new CacheItemPolicy());
                    }
                }
                else
                {
                    _actTypeData = cache["ActTypeData"] as Dictionary<string, string>;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 撈出客群類別對應表
        /// </summary>
        private QueryReply GetCusGrpType()
        {
            QueryReply _qrfdq = null;
            QueryRequest _qc = new QueryRequest();

            FubonQuery.FubonQProc _fdq = new FubonQuery.FubonQProc(FDQArgs());
            _fdq.Start();
            _qc.FCode = FDIPDefinition.Definition.Query.DefQueryFunc.CUS_GRP_CAT;
            _qrfdq = _fdq.Query(_qc);

            _fdq.Stop();
            _fdq = null;

            return _qrfdq;
        }
        private bool CheckCusGrpTypeData()
        {
            try
            {
                if (cache["ActTypeData"] == null)
                {
                    QueryReply _qr = GetCusGrpType();
                    if (_qr.CusGrpCat_List != null && _qr.CusGrpCat_List.Count > 0)
                    {
                        foreach (FDIPDefinition.CusGrpCategory _per in _qr.CusGrpCat_List)
                        {
                            _cusGrpTypeData.Add(_per.CusGrpType, _per.CusGrpName);
                        }
                        cache.Add("CusGrpTypeData", _cusGrpTypeData, new CacheItemPolicy());
                    }
                }
                else
                {
                    _cusGrpTypeData = cache["CusGrpTypeData"] as Dictionary<string, string>;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// FDIP連線設定
        /// </summary>
        private FDIPQuery.Common.FDQArgs FDQArgs()
        {
            FDIPQuery.Common.FDQArgs _args = new FDIPQuery.Common.FDQArgs();
            _args.SQLInfo.DB_FDIP.DbConnString = Config.ConfigData.DataBase.DB_FDIP;
            _args.SQLInfo.DB_FDIP.DbTimeout = 30;
            _args.SQLInfo.DB_SYSDEF.DbConnString = Config.ConfigData.DataBase.DB_SYSDEF;
            _args.SQLInfo.DB_SYSDEF.DbTimeout = 30;
            _args.IsSSM = false;

            return _args;
        }

        /// <summary>
        /// 篩選分公司
        /// </summary>
        /// <param name="_depnm">部</param>
        /// <param name="_areanm">區</param>
        /// <param name="_bhnm">分公司</param>
        /// <param name="_level">使用者層級</param>
        /// /// <returns>分公司編號</returns>
        private void filterFirm(string _depnm, string _areanm, string _bhnm, int _level)
        {
            if (_level == (int)BaseFormat.UserLevel.Admin || _level == (int)BaseFormat.UserLevel.AllCompany)
            {
                foreach (var p in _allfirm) { company.Add(p.Firmno); }
            }
            if (_level == (int)BaseFormat.UserLevel.Minister)
            {
                foreach (var p in _allfirm.Where(p => p.DeptName == _depnm)) company.Add(p.Firmno);
            }
            if (_level == (int)BaseFormat.UserLevel.Mayor)
            {
                foreach (var p in _allfirm.Where(p => p.AeraName == _areanm)) company.Add(p.Firmno);
            }
            if (_level == (int)BaseFormat.UserLevel.Manager || _level == (int)BaseFormat.UserLevel.DepManager || _level == (int)BaseFormat.UserLevel.Leader)
            {
                foreach (var p in _allfirm.Where(p => p.DeptName == _depnm && p.AeraName == _areanm && p.FirmName == _bhnm)) company.Add(p.Firmno);
            }
        }

        /// <summary>
        /// 取得客戶的帳戶
        /// </summary>
        /// <param name="_idno">身分證</param>
        /// <returns>帳戶</returns>
        private QueryReply filterAct(string _idno)
        {
            QueryReply _qrfdq = null;
            QueryRequest _qc = new QueryRequest();

            FubonQuery.FubonQProc _fdq = new FubonQuery.FubonQProc(FDQArgs());
            _fdq.Start();

            _qc.FCode = FDIPDefinition.Definition.Query.DefQueryFunc.ACT;
            _qc.Account = new QueryRequest.QACCOUNT();
            _qc.Account.Idno = _idno;
            _qrfdq = _fdq.Query(_qc);   //找出firm、分公司的人

            _fdq.Stop();
            _fdq = null;

            return _qrfdq;
        }

        /// <summary>
        /// 取得客戶的帳戶
        /// </summary>
        /// <param name="_idno">身分證</param>
        /// <param name="_firmno">分公司編號</param>
        /// <returns>帳戶</returns>
        private QueryReply filterAct(string _idno, string _firmno) //string firm
        {
            QueryReply _qrfdq = null;
            QueryRequest _qc = new QueryRequest();

            FubonQuery.FubonQProc _fdq = new FubonQuery.FubonQProc(FDQArgs());
            _fdq.Start();
            //Account進行篩選
            _qc.FCode = FDIPDefinition.Definition.Query.DefQueryFunc.ACT;
            _qc.Account = new QueryRequest.QACCOUNT();
            _qc.Account.Idno = _idno;
            _qc.Firm = new QueryRequest.QFIRM();
            _qc.Party.Firm.Firmno = _firmno;

            _qrfdq = _fdq.Query(_qc);   //找出firm、分公司的人

            _fdq.Stop();
            _fdq = null;

            return _qrfdq;
        }

        /// <summary>
        /// 取得客戶的帳戶
        /// </summary>
        /// <param name="_idno">使用者員編</param>
        /// <returns>帳戶</returns>
        private QueryReply filterStaffAct(string _staffno) //string firm
        {
            QueryReply _qrfdq = null;
            QueryRequest _qc = new QueryRequest();

            FubonQuery.FubonQProc _fdq = new FubonQuery.FubonQProc(FDQArgs());
            _fdq.Start();

            //Account進行篩選
            _qc.FCode = FDIPDefinition.Definition.Query.DefQueryFunc.ACT;
            _qc.Party = new QueryRequest.QPARTY();
            _qc.Party.Staffno = _staffno;
            _qrfdq = _fdq.Query(_qc);

            _fdq.Stop();
            _fdq = null;

            return _qrfdq;
        }
        private QueryReply filterStaffAct(string _idno, string _staffno) //string firm
        {
            QueryReply _qrfdq = null;
            QueryRequest _qc = new QueryRequest();

            FubonQuery.FubonQProc _fdq = new FubonQuery.FubonQProc(FDQArgs());
            _fdq.Start();

            //Account進行篩選
            _qc.FCode = FDIPDefinition.Definition.Query.DefQueryFunc.ACT;
            _qc.Account = new QueryRequest.QACCOUNT();
            _qc.Account.Idno = _idno;
            _qc.Party = new QueryRequest.QPARTY();
            _qc.Party.Staffno = _staffno;
            _qrfdq = _fdq.Query(_qc);

            _fdq.Stop();
            _fdq = null;

            return _qrfdq;
        }

        /// <summary>
        /// 轉換格式
        /// </summary>
        private void Convertion(List<AccountReply> _list, QueryReply _qr)
        {
            if (_qr.Act_List != null && _qr.Act_List.Count > 0)
            {
                foreach (Account _item in _qr.Act_List)
                {
                    AccountReply reply = new AccountReply();
                    reply.BasicInfo.idno = (_item.Idno.Equals(DBNull.Value)) ? "" : _item.Idno.ToString();
                    reply.BasicInfo.actno = (_item.Actno.Equals(DBNull.Value)) ? "" : _item.Actno.ToString();
                    reply.BasicInfo.name = (_item.Name.Equals(DBNull.Value)) ? "" : _item.Name.ToString();
                    reply.BasicInfo.gender = (_item.Gender.Equals(DBNull.Value)) ? 0 : Convert.ToInt32(_item.Gender);
                    reply.BasicInfo.age = (_item.Age.Equals(DBNull.Value)) ? 0 : Convert.ToInt32(_item.Age);
                    if (!_item.BDay.Equals(DBNull.Value)) reply.BasicInfo.bday = Convert.ToDateTime(_item.BDay.ToString());
                    reply.BasicInfo.mate = (_item.Mate.Equals(DBNull.Value)) ? "" : _item.Mate.ToString();
                    reply.firmno = (_item.Firmno.Equals(DBNull.Value)) ? "" : _item.Firmno.ToString();
                    reply.email = (_item.EMail.Equals(DBNull.Value)) ? "" : _item.EMail.ToString();
                    reply.mobile = (_item.Mobile.Equals(DBNull.Value)) ? "" : _item.Mobile.ToString();
                    reply.phone = (_item.Phone.Equals(DBNull.Value)) ? "" : _item.Phone.ToString();
                    reply.hraddr = (_item.Mobile.Equals(DBNull.Value)) ? "" : _item.Mobile.ToString();
                    reply.mobile = (_item.Mobile.Equals(DBNull.Value)) ? "" : _item.Mobile.ToString();
                    reply.contact = (_item.EmgcyContactName.Equals(DBNull.Value)) ? "" : _item.EmgcyContactName.ToString();
                    reply.agent = (_item.AgentName.Equals(DBNull.Value)) ? "" : _item.AgentName.ToString();
                    reply.cussrctyp = (_item.CusSrcType.Equals(DBNull.Value)) ? 0 : Convert.ToInt32(_item.CusSrcType);
                    reply.translmt = (_item.TransLmtAmt.Equals(DBNull.Value)) ? 0 : Convert.ToDecimal(_item.TransLmtAmt);
                    reply.evaildflg = (_item.ActValidFlag.Equals(DBNull.Value)) ? 0 : Convert.ToInt32(_item.ActValidFlag);
                    reply.banknm = (_item.BankName.Equals(DBNull.Value)) ? "" : _item.BankName.ToString();
                    reply.emgstklmt = (_item.EmgSTKLmtAmt.Equals(DBNull.Value)) ? 0 : Convert.ToDecimal(_item.EmgSTKLmtAmt);
                    reply.crvalidflg = (_item.CreditValidFlag.Equals(DBNull.Value)) ? 0 : Convert.ToInt32(_item.CreditValidFlag);
                    reply.mgntralmt = (_item.MgnTraLmtAmt.Equals(DBNull.Value)) ? 0 : Convert.ToDecimal(_item.MgnTraLmtAmt);
                    reply.sselllmt = (_item.SSellLmtAmt.Equals(DBNull.Value)) ? 0 : Convert.ToDecimal(_item.SSellLmtAmt);
                    reply.dtrdflag = (_item.DtQual.Equals(DBNull.Value)) ? 0 : Convert.ToInt32(_item.DtQual);
                    reply.piflag = (_item.PIQualFlag.Equals(DBNull.Value)) ? 0 : Convert.ToInt32(_item.PIQualFlag);
                    //當沖額度(未知)
                    reply.dtrdlmt = 0;
                    if (!_item.ActOpenDate.Equals(DBNull.Value)) reply.actopndate = Convert.ToDateTime(_item.ActOpenDate.ToString());
                    if (!_item.ActCloseDate.Equals(DBNull.Value)) reply.actclsdate = Convert.ToDateTime(_item.ActCloseDate.ToString());
                    if (!_item.ElectronicOpenDate.Equals(DBNull.Value)) reply.eopndate = Convert.ToDateTime(_item.ElectronicOpenDate.ToString());

                    _item.CusGrpType = _cusGrpTypeData[_item.CusGrpType];
                    reply.BasicInfo.cusgrptype = (_item.CusGrpType.Equals(DBNull.Value)) ? "" : _item.CusGrpType.ToString();
                    _item.ActType = _actTypeData[_item.ActType];
                    reply.acttype = (_item.ActType.Equals(DBNull.Value)) ? "" : _item.ActType.ToString();

                    _list.Add(reply);
                }
            }
        }

        /// <summary>
        /// 取得員工編號
        /// </summary>
        /// <param name="_guid">使用者GUID</param>
        /// <returns>員工編號</returns>
        private void GetStafNo(string _guid)
        {
            staffno.Clear();
            //連接D365
            string StaffStr = string.Format(@"SELECT new_staffno FROM dbo.SystemUserBase WHERE ParentSystemUserId = '{0}'", _guid);
            SqlCommand cmd = null;
            try
            {

                using (cmd = new SqlCommand(StaffStr, SqlConn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            staffno.Add(reader["new_staffno"].ToString());
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}