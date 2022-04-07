using FDIPConfig;
using FDIPDefinition;
using FDIPDefinition.Definition;
using FDIPDefinition.Utility;
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

namespace OverView_WebServer.Methods
{
    public class AccountController : BaseClass
    {
        private const string _classname = "AccountController";

        List<Firm> company = new List<Firm>();
        List<string> staffno = new List<string>();
        List<AccountFormat> replylist = new List<AccountFormat>();
        bool isFirm;

        //快取
        private ObjectCache cache = MemoryCache.Default;
        List<Firm> _allfirm = new List<Firm>();
        private Dictionary<string, string> _cusGrpTypeData = new Dictionary<string, string>();
        
        // Post api/<controller>
        [HttpPost, ActionName("get")]
        public HttpResponseMessage Get([FromBody]AccountQuest _request)
        {
            string _funcname = _classname + "GetAccount";
            AccountReply _reply = new AccountReply();
            SqlConnection SqlConn = null;

            try
            {
                #region 確認ConfigData
                if (!CheckConfigData(this.ThisSeverName))
                {
                    QueryReply _sgtpreplylist = new QueryReply();
                    _sgtpreplylist.ErrorCode = ((int)ReturnStatus.ERROR_PARAMETER).ToString("d4");
                    _sgtpreplylist.ErrorText = "Config設定異常!";
                    LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.MainFile].WriteLog(_funcname, LogProcessor.LogType.ERROR, ((int)ReturnStatus.SERVER_ERROR).ToString("d4"), " Server Send to Client JSON Response : " + Serialization.ConvertObjectToJson(_sgtpreplylist));
                    return SendResponse(FDIPDefinition.Utility.Serialization.ConvertObjectToJson(_sgtpreplylist));
                }
                #endregion

                #region 確認FubonQuery元件是否存在
                if (!CheckFDQObject())
                {
                    LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.MainFile].WriteLog(_funcname, LogProcessor.LogType.INFO, ((int)ReturnStatus.SERVER_ERROR).ToString("d4"), "CheckFDQObject Fail.");
                }
                #endregion

                #region 確認Firm
                if (!CheckFrimData())
                {
                    LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.MainFile].WriteLog(_funcname, LogProcessor.LogType.INFO, ((int)ReturnStatus.SERVER_ERROR).ToString("d4"), "CheckFrimData Fail.");
                }
                #endregion

                #region 確認客群類別
                if (!CheckCusGrpTypeData())
                {
                    LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.MainFile].WriteLog(_funcname, LogProcessor.LogType.INFO, ((int)ReturnStatus.SERVER_ERROR).ToString("d4"), "CheckCusGrpTypeData Fail.");
                }
                #endregion

                using (SqlConn = new SqlConnection(Properties.Settings.Default.SQLConnectStr))
                {
                    SqlConn.Open();
                    List<AccountFormat> _templist = new List<AccountFormat>();
                    LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.Query].WriteLog(_funcname, LogProcessor.LogType.INFO, ((int)ReturnStatus.SERVER_ERROR).ToString("d4"), "查詢帳戶");

                    if (_request.level == Convert.ToInt32(BaseFormat.UserLevel.Admin))
                    {
                        isFirm = true;
                        //use company to search an account
                        filterFirm(_request.dep, _request.gr, _request.bh, _request.level);
                        Convertion(replylist, filterAct(_request.idno));
                    }
                    if (_request.level == Convert.ToInt32(BaseFormat.UserLevel.AllCompany))
                    {
                        isFirm = true;
                        //use company to search an account
                        filterFirm(_request.dep, _request.gr, _request.bh, _request.level);
                        Convertion(replylist, filterAct(_request.idno));
                    }
                    if (_request.level == Convert.ToInt32(BaseFormat.UserLevel.Minister))
                    {
                        isFirm = true;
                        //use company to search an account
                        filterFirm(_request.dep, _request.gr, _request.bh, _request.level);
                        
                        //Test All companys，目前部門資料庫未新增
                        Convertion(replylist, filterAct(_request.idno));

                        //正式版 companys
                        //foreach (var item in company)
                        //{
                        //    Convertion(replylist, filterAct(_request.idno, item.Firmno));
                        //}
                    }
                    if (_request.level == Convert.ToInt32(BaseFormat.UserLevel.Mayor))
                    {
                        isFirm = true;
                        //use company to search an account
                        filterFirm(_request.dep, _request.gr, _request.bh, _request.level);
                        //companys
                        foreach (var item in company)
                        {
                            Convertion(replylist, filterAct(_request.idno, item.Firmno));
                        }
                    }
                    if (_request.level == Convert.ToInt32(BaseFormat.UserLevel.Manager))
                    {
                        isFirm = false;
                        //use company to search an account
                        filterFirm(_request.dep, _request.gr, _request.bh, _request.level);
                        //company
                        foreach (var item in company)
                        {
                            Convertion(replylist, filterAct(_request.idno, item.Firmno));
                        }
                    }
                    if (_request.level == Convert.ToInt32(BaseFormat.UserLevel.DepManager))
                    {
                        //use company to search an account
                        filterFirm(_request.dep, _request.gr, _request.bh, _request.level);
                        isFirm = false;
                        foreach (var item in company)
                        {
                            Convertion(replylist, filterAct(_request.idno, item.Firmno));
                        }
                        //找出經理人的員工編號
                        GetParentStafNo(_request.userid.ToString(),SqlConn);
                        //經理人負責客戶帳戶
                        foreach (string _no in staffno)
                        {
                            Convertion(_templist, filterStaffAct(_request.idno, _no));
                        }
                        //排除經理負責客戶帳戶
                        for (int i = 0; i < _templist.Count; i++)
                        {
                            for (int x = 0; x < replylist.Count; x++)
                            {
                                if (replylist[i].BasicInfo.actno == _templist[i].BasicInfo.actno)
                                {
                                    replylist.RemoveAt(i);
                                }
                            }
                        }
                    }
                    if (_request.level == Convert.ToInt32(BaseFormat.UserLevel.Leader))
                    {
                        isFirm = false;
                        //use company to search an account
                        filterFirm(_request.dep, _request.gr, _request.bh, _request.level);
                        //找出組長管理的營業員員編
                        GetStafNo(_request.userid,SqlConn);
                        //加入自己的員編
                        staffno.Add(_request.staffno);
                        //搜尋每個員編所負責的客戶帳號
                        foreach (string _no in staffno)
                        {
                            Convertion(replylist, filterStaffAct(_request.idno, _no));
                        }
                    }
                    if (_request.level == Convert.ToInt32(BaseFormat.UserLevel.Salesperson))
                    {
                        isFirm = false;
                        //use company to search an account
                        filterFirm(_request.dep, _request.gr, _request.bh, _request.level);
                        //使用客戶id與使用者員編找出客戶帳號
                        Convertion(replylist, filterStaffAct(_request.idno, _request.staffno));
                    }
                    //找出存在帳戶的分公司
                    filterFirm(company, replylist);
                    _reply.status = Status.StatusEnum.SUCCESS;
                    _reply.AllCompany = company;
                    _reply.actData = replylist;
                    _reply.isFirm = isFirm;
                    if (company == null) { _reply.status = Status.StatusEnum.FAIL; _reply.errorMsg += "查無分公司資料!"; }
                    if (replylist.Count == 0) { _reply.status = Status.StatusEnum.FAIL; _reply.errorMsg += "查無帳戶資料!"; }
                    LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.MainFile].WriteLog(_funcname, LogProcessor.LogType.INFO, ((int)ReturnStatus.SUCCESS).ToString("d4"), "Server Send to Client JSON Response :" + Serialization.ConvertObjectToJson(_reply));
                    return Request.CreateResponse(HttpStatusCode.OK, _reply);
                }
                _reply.status = Status.StatusEnum.DB_FAIL;
                _reply.errorMsg = "無此帳戶資料";
                LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.MainFile].WriteLog(_funcname, LogProcessor.LogType.INFO, ((int)ReturnStatus.SERVER_ERROR).ToString("d4"), "Server Send to Client JSON Response :" + Serialization.ConvertObjectToJson(_reply));
                return Request.CreateResponse(HttpStatusCode.NotFound, _reply);
            }
            catch (Exception ex)
            {
                _reply.status = Status.StatusEnum.FAIL;
                _reply.errorMsg = "查詢失敗";
                ReleaseFDQObject();
                LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.MainFile].WriteLog(_funcname, LogProcessor.LogType.EXCEPTION, ((int)ReturnStatus.SERVER_ERROR).ToString("d4"), ex.Message + ex.StackTrace);
                LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.MainFile].WriteLog(_funcname, LogProcessor.LogType.ERROR, ((int)ReturnStatus.SERVER_ERROR).ToString("d4"), "Server Send to Client JSON Response : " + Serialization.ConvertObjectToJson(_reply));
                return Request.CreateResponse(HttpStatusCode.BadRequest, _reply);
            }
        }

        /// <summary>
        /// 撈出所有公司基本資料
        /// </summary>
        private QueryReply GetFirm()
        {
            string _funcname = _classname + "GetFirm";
            QueryReply _qrfdq = null;
            QueryRequest _qc = new QueryRequest();

            //FubonQuery.FubonQProc _fdq = new FubonQuery.FubonQProc(_args);
            //_fdq.Start();
            _qc.FCode = FDIPDefinition.Definition.Query.DefQueryFunc.FIRM;
            LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.Fubon].WriteLog(_funcname, LogProcessor.LogType.INFO, ((int)ReturnStatus.SUCCESS).ToString("d4"), "Fdq Qc :  " + Serialization.ConvertObjectToJson(_qc));
            _qrfdq = _fdq.Query(_qc);
            //_fdq.Stop();
            LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.Fubon].WriteLog(_funcname, LogProcessor.LogType.INFO, ((int)ReturnStatus.SUCCESS).ToString("d4"), "Fdq Result :  " + Serialization.ConvertObjectToJson(_qrfdq));

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
        /// 撈出客群類別對應表
        /// </summary>
        private QueryReply GetCusGrpType()
        {
            string _funcname = _classname + "GetCusGrpType";
            QueryReply _qrfdq = null;
            QueryRequest _qc = new QueryRequest();

            _qc.FCode = FDIPDefinition.Definition.Query.DefQueryFunc.CUS_GRP_CAT;
            LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.Fubon].WriteLog(_funcname, LogProcessor.LogType.INFO, ((int)ReturnStatus.SUCCESS).ToString("d4"), "Fdq Qc :  " + Serialization.ConvertObjectToJson(_qc));
            _qrfdq = _fdq.Query(_qc);
            LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.Fubon].WriteLog(_funcname, LogProcessor.LogType.INFO, ((int)ReturnStatus.SUCCESS).ToString("d4"), "Fdq Result :  " + Serialization.ConvertObjectToJson(_qrfdq));

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
        /// 篩選該使用者所有分公司
        /// </summary>
        /// <param name="_depnm">部</param>
        /// <param name="_areanm">區</param>
        /// <param name="_bhnm">分公司</param>
        /// <param name="_level">使用者層級</param>
        /// <returns>使用者擁有的分公司</returns>
        private void filterFirm(string _depnm, string _areanm, string _bhnm, int _level)
        {
            if (_level == (int)BaseFormat.UserLevel.Admin || _level == (int)BaseFormat.UserLevel.AllCompany)
            {
                foreach (var p in _allfirm) { company.Add(p); }
            }
            if (_level == (int)BaseFormat.UserLevel.Minister)
            {
                foreach (var p in _allfirm) { company.Add(p); }
                //foreach (var p in _allfirm.Where(p => p.DeptName == _depnm)) company.Add(p);
            }
            if (_level == (int)BaseFormat.UserLevel.Mayor)
            {
                foreach (var p in _allfirm.Where(p => p.AeraName == _areanm)) company.Add(p);
            }
            if (_level == (int)BaseFormat.UserLevel.Manager || _level == (int)BaseFormat.UserLevel.DepManager || _level == (int)BaseFormat.UserLevel.Leader || _level==(int)BaseFormat.UserLevel.Salesperson)
            {
                foreach (var p in _allfirm.Where(p => p.FirmName == _bhnm)) company.Add(p);
            }
        }

        /// <summary>
        /// 篩選使用者僅可看到的分公司
        /// </summary>
        /// <param name="_company">公司</param>
        /// <param name="_list">帳戶基本資料</param>
        /// <returns>使用者能看到該客戶帳戶的分公司</returns>
        private void filterFirm(List<Firm> _company, List<AccountFormat> _list)
        {
            List<Firm> tempCompany = new List<Firm>();

            //判斷公司為單筆就直接離開
            if (_company.Count() <= 1) return;
            //擁有帳戶的分公司
            foreach(var item in _company)
            {
                foreach(var i in _list)
                {
                    if (item.Firmno == i.firmno) tempCompany.Add(item);
                }
            }
            company = tempCompany.Distinct().ToList();
        }
        /// <summary>
        /// 取得客戶的帳戶
        /// </summary>
        /// <param name="_idno">身分證</param>
        /// <returns>帳戶</returns>
        private QueryReply filterAct(string _idno)
        {
            string _funcname = _classname + "FilterStaffAct";
            QueryReply _qrfdq = null;
            QueryRequest _qc = new QueryRequest();

            _qc.FCode = FDIPDefinition.Definition.Query.DefQueryFunc.ACT;
            _qc.Account = new QueryRequest.QACCOUNT();
            _qc.Account.Idno = _idno;
            LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.Fubon].WriteLog(_funcname, LogProcessor.LogType.INFO, ((int)ReturnStatus.SUCCESS).ToString("d4"), "Fdq Qc :  " + Serialization.ConvertObjectToJson(_qc));
            _qrfdq = _fdq.Query(_qc);   //找出firm、分公司的人
            LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.Fubon].WriteLog(_funcname, LogProcessor.LogType.INFO, ((int)ReturnStatus.SUCCESS).ToString("d4"), "Fdq Result :  " + Serialization.ConvertObjectToJson(_qrfdq));
            
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
            string _funcname = _classname + "FilterStaffAct";
            QueryReply _qrfdq = null;
            QueryRequest _qc = new QueryRequest();

            //Account進行篩選
            _qc.FCode = FDIPDefinition.Definition.Query.DefQueryFunc.ACT;
            _qc.Account = new QueryRequest.QACCOUNT();
            _qc.Account.Idno = _idno;
            _qc.Party = new QueryRequest.QPARTY();
            _qc.Party.Firm.Firmno = _firmno;
            LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.Fubon].WriteLog(_funcname, LogProcessor.LogType.INFO, ((int)ReturnStatus.SUCCESS).ToString("d4"), "Fdq Qc :  " + Serialization.ConvertObjectToJson(_qc));
            _qrfdq = _fdq.Query(_qc);   //找出firm、分公司的人
            LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.Fubon].WriteLog(_funcname, LogProcessor.LogType.INFO, ((int)ReturnStatus.SUCCESS).ToString("d4"), "Fdq Result :  " + Serialization.ConvertObjectToJson(_qrfdq));
            
            return _qrfdq;
        }

        /// <summary>
        /// 取得客戶的帳戶
        /// </summary>
        /// <param name="_idno">使用者員編</param>
        /// <returns>帳戶</returns>
        private QueryReply filterStaffAct(string _idno, string _staffno) //string firm
        {
            string _funcname = _classname + "FilterStaffAct";
            QueryReply _qrfdq = null;
            QueryRequest _qc = new QueryRequest();

            //Account進行篩選
            _qc.FCode = FDIPDefinition.Definition.Query.DefQueryFunc.ACT;
            _qc.Account = new QueryRequest.QACCOUNT();
            _qc.Account.Idno = _idno;
            _qc.Party = new QueryRequest.QPARTY();
            _qc.Party.Staffno = _staffno;
            LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.Fubon].WriteLog(_funcname, LogProcessor.LogType.INFO, ((int)ReturnStatus.SUCCESS).ToString("d4"), "Fdq Qc :  " + Serialization.ConvertObjectToJson(_qc));
            _qrfdq = _fdq.Query(_qc);
            LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.Fubon].WriteLog(_funcname, LogProcessor.LogType.INFO, ((int)ReturnStatus.SUCCESS).ToString("d4"), "Fdq Result :  " + Serialization.ConvertObjectToJson(_qrfdq));
            
            return _qrfdq;
        }

        /// <summary>
        /// 取得上司員編(D365)
        /// </summary>
        /// <param name="_guid">使用者GUID</param>
        /// <returns>員編</returns>
        private void GetParentStafNo(string _guid,SqlConnection _sqlconn)
        {
            string _funcname = _classname + "GetParentStaffNo";
            string parentGuid = null;
            LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.D356].WriteLog(_funcname, LogProcessor.LogType.INFO, ((int)ReturnStatus.SUCCESS).ToString("d4"), "D365 Qc:" + Serialization.ConvertObjectToJson(_guid));
            //連接D365
            string GuidStr = string.Format(@"SELECT ParentSystemUserId FROM dbo.SystemUserBase WHERE SystemUserId = '{0}'", _guid);
            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand(GuidStr, _sqlconn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    parentGuid = reader["ParentSystemUserId"].ToString();
                    reader.Close();
                }

                string StaffStr = string.Format(@"SELECT new_staffno FROM dbo.SystemUserBase WHERE SystemUserId = '{0}'", parentGuid);
                using (cmd = new SqlCommand(StaffStr, _sqlconn))
                {
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    staffno.Clear();
                    staffno.Add(reader["new_staffno"].ToString());
                    reader.Close();
                }

                LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.D356].WriteLog(_funcname, LogProcessor.LogType.INFO, ((int)ReturnStatus.SUCCESS).ToString("d4"), "D365 Result:" + Serialization.ConvertObjectToJson(staffno));
            }
            catch (Exception ex)
            {
                LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.D356].WriteLog(_funcname, LogProcessor.LogType.EXCEPTION, ((int)ReturnStatus.SERVER_ERROR).ToString("d4"), ex.Message + ex.StackTrace);
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// 取得底層員工編號(D365)
        /// </summary>
        /// <param name="_guid">使用者GUID</param>
        /// <returns>員工編號</returns>
        private void GetStafNo(Guid _guid,SqlConnection _sqlconn)
        {
            string _funcname = _classname + "GetStaffNo";
            //連接D365
            string StaffStr = string.Format(@"SELECT new_staffno FROM dbo.SystemUserBase WHERE ParentSystemUserId = '{0}'", _guid);
            LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.D356].WriteLog(_funcname, LogProcessor.LogType.INFO, ((int)ReturnStatus.SUCCESS).ToString("d4"), "D365 Qc:" + Serialization.ConvertObjectToJson(_guid));
            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand(StaffStr, _sqlconn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        staffno.Clear();
                        while (reader.Read())
                        {
                            staffno.Add(reader["new_staffno"].ToString());
                        }
                    }
                }
                LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.D356].WriteLog(_funcname, LogProcessor.LogType.INFO, ((int)ReturnStatus.SUCCESS).ToString("d4"), "D365 Result:"+Serialization.ConvertObjectToJson(staffno));
            }
            catch (Exception ex)
            {
                LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.D356].WriteLog(_funcname, LogProcessor.LogType.EXCEPTION, ((int)ReturnStatus.SERVER_ERROR).ToString("d4"), ex.Message + ex.StackTrace);
                Console.WriteLine(ex.Message);
            }

        }

        /// <summary>
        /// 轉換格式
        /// </summary>
        /// <param name="_list">帳戶List</param>
        /// <param name="_qr">處理的帳戶</param>
        private void Convertion(List<AccountFormat> _list, QueryReply _qr)
        {
            if (_qr.Act_List != null && _qr.Act_List.Count > 0)
            {
                foreach (Account _item in _qr.Act_List)
                {
                    AccountFormat reply = new AccountFormat();
                    reply.BasicInfo.idno = (_item.Idno.Equals(DBNull.Value)) ? "" : _item.Idno.ToString();
                    reply.BasicInfo.idnomsk = (_item.IdnoMsk.Equals(DBNull.Value)) ? "" : _item.IdnoMsk.ToString();
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
                    reply.hraddr = (_item.HRAddr.Equals(DBNull.Value)) ? "" : _item.HRAddr.ToString();
                    reply.caddr = (_item.CommAddr.Equals(DBNull.Value)) ? "" : _item.CommAddr.ToString();
                    reply.mobile = (_item.Mobile.Equals(DBNull.Value)) ? "" : _item.Mobile.ToString();
                    reply.contact = (_item.EmgcyContactName.Equals(DBNull.Value)) ? "" : _item.EmgcyContactName.ToString();
                    reply.agent = (_item.AgentName.Equals(DBNull.Value)) ? "" : _item.AgentName.ToString();
                    reply.cussrctyp = (_item.CusSrcType.Equals(DBNull.Value) || _item.CusSrcType == "未知") ? 0 : Convert.ToInt32(_item.CusSrcType);
                    reply.translmt = (_item.TransLmtAmt.Equals(DBNull.Value)) ? 0 : Convert.ToDecimal(_item.TransLmtAmt);
                    reply.evalidflag = (_item.ElectronicValidFlag.Equals(DBNull.Value)) ? 0 : Convert.ToInt32(_item.ElectronicValidFlag);
                    reply.banknm = (_item.BankName.Equals(DBNull.Value)) ? "" : _item.BankName.ToString();
                    reply.emgstklmt = (_item.EmgSTKLmtAmt.Equals(DBNull.Value)) ? 0 : Convert.ToDecimal(_item.EmgSTKLmtAmt);
                    reply.crvalidflag = (_item.CreditValidFlag.Equals(DBNull.Value)) ? 0 : Convert.ToInt32(_item.CreditValidFlag);
                    reply.mgntralmt = (_item.MgnTraLmtAmt.Equals(DBNull.Value)) ? 0 : Convert.ToDecimal(_item.MgnTraLmtAmt);
                    reply.sselllmt = (_item.SSellLmtAmt.Equals(DBNull.Value)) ? 0 : Convert.ToDecimal(_item.SSellLmtAmt);
                    reply.dtrdflag = (_item.DtQual.Equals(DBNull.Value)) ? 0 : Convert.ToInt32(_item.DtQual);
                    reply.piflag = (_item.PIQualFlag.Equals(DBNull.Value)) ? 0 : Convert.ToInt32(_item.PIQualFlag);
                    //當沖額度(未知)
                    reply.dtrdlmt = 0;
                    if (!_item.ActOpenDate.Equals(DBNull.Value)) reply.actopndate = Convert.ToDateTime(_item.ActOpenDate.ToString());
                    if (!_item.ActCloseDate.Equals(DBNull.Value)) reply.actclsdate = Convert.ToDateTime(_item.ActCloseDate.ToString());
                    if (!_item.ElectronicOpenDate.Equals(DBNull.Value)) reply.eopndate = Convert.ToDateTime(_item.ElectronicOpenDate.ToString());
                    //客群類別
                    _item.CusGrpType = _cusGrpTypeData[_item.CusGrpType];
                    reply.BasicInfo.cusgrptype = (_item.CusGrpType.Equals(DBNull.Value)) ? "" : _item.CusGrpType.ToString();
                    //帳戶類別
                    reply.acttype = (_item.ActType.Equals(DBNull.Value)) ? "" : _item.ActType.ToString();

                    _list.Add(reply);
                }
            }
        }
    }
}