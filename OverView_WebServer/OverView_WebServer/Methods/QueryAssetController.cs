using FDIPDefinition;
using FDIPDefinition.Definition;
using FDIPDefinition.Utility;
using FubonDefinition.Definition;
using OverView_WebServer.Assets.Models;
using OverView_WebServer.Models;
using OverView_WebServer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OverView_WebServer.Methods
{
    public class QueryAssetController : BaseClass
    {
        private const string _classname = "QueryAssetController";

        TwStock twStock;
        SubBrokerage sBroke;
        Future future;
        OSU osu;
        Trust trust;
        CMA cma;

        // Post api/<controller>
        [HttpPost, ActionName("get")]
        public HttpResponseMessage Get([FromBody]List<AssetQuest> _quest)
        {
            string _funcname = _classname + "GetAssets";
            AssetReply _reply = new AssetReply();
            List<AssetFormat> assetlist = new List<AssetFormat>();
            LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.Query].WriteLog(_funcname, LogProcessor.LogType.INFO, " ", "");
            try
            {
                #region 確認FubonQuery元件是否存在
                if (!CheckFDQObject())
                {
                    LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.MainFile].WriteLog(_funcname, LogProcessor.LogType.INFO, ((int)ReturnStatus.SERVER_ERROR).ToString("d4"), "CheckFDQObject Fail.");
                }
                #endregion

                //if quest
                foreach (AssetQuest item in _quest)
                {
                    twStock = new TwStock();
                    sBroke = new SubBrokerage();
                    future = new Future();
                    osu = new OSU();
                    trust = new Trust();
                    cma = new CMA();
                    AssetFormat asset = new AssetFormat();
                    foreach (string _actno in item.account)
                    {
                        
                        QueryReply qrAssets = GetActAssets(_actno);
                        if (qrAssets.ActStat_List != null && qrAssets.ActStat_List.Count > 0)
                        {
                            //當帳戶每個資產明細都是同個帳戶類型
                            AccountStatus _qr = qrAssets.ActStat_List[0];
                            if (_qr.ActType == "001" || _qr.ActType == "007")
                            {
                                TwStockCalculate(qrAssets);
                            }
                            if (_qr.ActType == "002")
                            {
                                SbrokeCalculate(qrAssets);
                            }
                            if (_qr.ActType == "003")
                            {
                                OSUCalculate(qrAssets);
                            }
                            if (_qr.ActType == "004")
                            {
                                FutureCalculate(qrAssets);
                            }
                            if (_qr.ActType == "005")
                            {
                                CMACalculate(qrAssets);
                            }
                            if (_qr.ActType == "006")
                            {
                                TrustCalculate(qrAssets);
                            }
                        }
                    }
                    asset.firmno = item.firmno;
                    asset.twstock = twStock;
                    asset.sbrokerage = sBroke;
                    asset.osu = osu;
                    asset.future = future;
                    asset.trust = trust;
                    asset.cma = cma;
                    SumAssteCalculate(asset);
                    assetlist.Add(asset);
                }
                _reply.status = Status.StatusEnum.SUCCESS;
                _reply.assetData = assetlist;
                LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.MainFile].WriteLog(_funcname, LogProcessor.LogType.INFO, ((int)ReturnStatus.SUCCESS).ToString("d4"), "Server Send to Client JSON Response :  " + Serialization.ConvertObjectToJson(_reply));
                return Request.CreateResponse(HttpStatusCode.OK, _reply);
            }
            catch (Exception ex)
            {
                _reply.status = Status.StatusEnum.FAIL;
                _reply.errorMsg = "查詢失敗";
                LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.MainFile].WriteLog(_funcname, LogProcessor.LogType.EXCEPTION, ((int)ReturnStatus.SERVER_ERROR).ToString("d4"), ex.Message + ex.StackTrace);
                LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.MainFile].WriteLog(_funcname, LogProcessor.LogType.ERROR, ((int)ReturnStatus.SERVER_ERROR).ToString("d4"), "Server Send to Client JSON Response :" + Serialization.ConvertObjectToJson(_reply));
                return Request.CreateResponse(HttpStatusCode.BadRequest, _reply);
            }
            
        }

        /// <summary>
        /// FDIP連線設定
        /// </summary>
        private FDIPQuery.Common.FDQArgs FDQArgs()
        {
            FDIPQuery.Common.FDQArgs _args = new FDIPQuery.Common.FDQArgs();
            //_args.SQLInfo.DB_FDIP.DbConnString = Config.ConfigData.DataBase.DB_FDIP;
            //_args.SQLInfo.DB_SYSDEF.DbConnString = Config.ConfigData.DataBase.DB_SYSDEF;
            _args.SQLInfo.DB_FDIP.DbConnString = Properties.Settings.Default.DB_FDIP;
            _args.SQLInfo.DB_SYSDEF.DbConnString = Properties.Settings.Default.DB_SYSDEF;
            _args.SQLInfo.DB_FDIP.DbTimeout = 30;
            _args.SQLInfo.DB_SYSDEF.DbTimeout = 30;
            _args.IsSSM = false;

            return _args;
        }

        /// <summary>
        /// 撈取該帳戶所有資產項目
        /// </summary>
        /// <param name="_company">公司</param>
        /// <param name="_list">帳戶基本資料</param>
        /// <returns>使用者能看到該客戶帳戶的分公司</returns>
        private QueryReply GetActAssets(string _actno)
        {
            string _funcname = _classname + "GetActAssets";
            QueryReply _qrfdq = null;
            QueryRequest _qc = new QueryRequest();

            _qc.FCode = FDIPDefinition.Definition.Query.DefQueryFunc.ACT_STAT_D;
            _qc.Account = new QueryRequest.QACCOUNT();
            _qc.Account.Actno = _actno;
            LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.Fubon].WriteLog(_funcname, LogProcessor.LogType.INFO, ((int)ReturnStatus.SUCCESS).ToString("d4"), "Fdq Qc :  " + Serialization.ConvertObjectToJson(_qc));
            _qrfdq = _fdq.Query(_qc);   //找出該帳號庫存總額
            LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.Fubon].WriteLog(_funcname, LogProcessor.LogType.INFO, ((int)ReturnStatus.SUCCESS).ToString("d4"), "Fdq Result :  " + Serialization.ConvertObjectToJson(_qrfdq));

            return _qrfdq;
        }
    
        private void TwStockCalculate(QueryReply _assets)
        {
            float elsep = 0;    //001、005

            float stock = 0;
            float margin = 0;
            float ssell = 0;
            float loan = 0;
            float nrec = 0;
            float dfin = 0;
            float total;
            
            foreach (AccountStatus _qr in _assets.ActStat_List)
            {
                switch (_qr.InstrumentType)
                {
                    case "001":
                        //股票
                        elsep += Convert.ToInt32(_qr.TotalAsset);
                        break;
                    case "002":
                        //股票(先股)與股票(現股當沖)
                        stock += Convert.ToInt32(_qr.TotalAsset);
                        break;
                    case "003":
                        //股票(融資)與股票(融資當沖)
                        margin += Convert.ToInt32(_qr.TotalAsset);
                        break;
                    case "004":
                        //股票(融券)與股票(融券當沖)
                        ssell += Convert.ToInt32(_qr.TotalAsset);
                        break;
                    case "005":
                        //集保基金
                        elsep += Convert.ToInt32(_qr.TotalAsset);
                        break;
                    case "006":
                        //雙向借券
                        loan += Convert.ToInt32(_qr.TotalAsset);
                        break;
                    case "007":
                        //不限用途借貸
                        nrec += Convert.ToInt32(_qr.TotalAsset);
                        break;
                    case "009":
                        //境內衍生金融(自營)
                        dfin += Convert.ToInt32(_qr.TotalAsset);
                        break;
                    default:
                        break;
                }
            }
            total = stock + margin + ssell + loan + nrec + dfin;
            twStock.stockp = Convert.ToDecimal(stock);
            twStock.marginp = Convert.ToDecimal(margin);
            twStock.ssellp = Convert.ToDecimal(ssell);
            twStock.loanp = Convert.ToDecimal(loan);
            twStock.nrecp = Convert.ToDecimal(nrec);
            twStock.dfinp = Convert.ToDecimal(dfin);
            twStock.totalp = Convert.ToDecimal(total);
        }

        private void SbrokeCalculate(QueryReply _assets)
        {
            float elsep = 0;    //001、00F、00G

            float ostock = 0;
            float ocredit = 0;
            float odebt = 0;
            float ofund = 0;
            float truct = 0;
            float total;

            foreach (AccountStatus _qr in _assets.ActStat_List)
            {
                switch (_qr.InstrumentType)
                {
                    case "001":
                        //海外股票
                        elsep += Convert.ToInt32(_qr.TotalAsset);
                        break;
                    case "002":
                        //海外股票(先股)與海外股票(現股當沖)
                        ostock += Convert.ToInt32(_qr.TotalAsset);
                        break;
                    case "003":
                        //海外股票(融資)與海外股票(融資當沖)
                        ocredit += Convert.ToInt32(_qr.TotalAsset);
                        break;
                    case "004":
                        //海外股票(融券)與海外股票(融券當沖)
                        ocredit += Convert.ToInt32(_qr.TotalAsset);
                        break;
                    case "005":
                        //境外基金
                        ofund += Convert.ToInt32(_qr.TotalAsset);
                        break;
                    case "008":
                        //海外債
                        odebt += Convert.ToInt32(_qr.TotalAsset);
                        break;
                    case "009":
                        //境外結構型商品
                        truct += Convert.ToInt32(_qr.TotalAsset);
                        break;
                    case "00F":
                        //海外債(現金)
                        elsep += Convert.ToInt32(_qr.TotalAsset);
                        break;
                    case "00G":
                        //海外債(融資)
                        elsep += Convert.ToInt32(_qr.TotalAsset);
                        break;
                    default:
                        break;
                }
            }
            total = ostock +  ocredit + odebt + ofund + truct;
            sBroke.ostockp = Convert.ToDecimal(ostock);
            sBroke.ocreditp = Convert.ToDecimal(ocredit);
            sBroke.odebtp = Convert.ToDecimal(odebt);
            sBroke.ofundp = Convert.ToDecimal(ofund);
            sBroke.structp = Convert.ToDecimal(truct);
            sBroke.totalp = Convert.ToDecimal(total);
        }

        private void OSUCalculate(QueryReply _assets)
        {
            float elsep = 0; //001、003、004、00F、00G

            float ostock = 0;
            float odebt = 0;
            float ofund = 0;
            float truct = 0;
            float total;

            foreach (AccountStatus _qr in _assets.ActStat_List)
            {
                switch (_qr.InstrumentType)
                {
                    case "001":
                        //海外股票
                        elsep += Convert.ToInt32(_qr.TotalAsset);
                        break;
                    case "002":
                        //海外股票(先股)與海外股票(現股當沖)
                        ostock += Convert.ToInt32(_qr.TotalAsset);
                        break;
                    case "003":
                        //海外股票(融資)與海外股票(融資當沖)
                        elsep += Convert.ToInt32(_qr.TotalAsset);
                        break;
                    case "004":
                        //海外股票(融券)與海外股票(融券當沖)
                        elsep += Convert.ToInt32(_qr.TotalAsset);
                        break;
                    case "005":
                        //境外基金
                        ofund += Convert.ToInt32(_qr.TotalAsset);
                        break;
                    case "008":
                        //海外債
                        odebt += Convert.ToInt32(_qr.TotalAsset);
                        break;
                    case "009":
                        //境外結構型商品
                        truct += Convert.ToInt32(_qr.TotalAsset);
                        break;
                    case "00F":
                        //海外債(現金)
                        elsep += Convert.ToInt32(_qr.TotalAsset);
                        break;
                    case "00G":
                        //海外債(融資)
                        elsep += Convert.ToInt32(_qr.TotalAsset);
                        break;
                    default:
                        break;
                }
            }
            total = ostock + odebt + ofund + truct;
            osu.ostockp = Convert.ToDecimal(ostock);
            osu.odebtp = Convert.ToDecimal(odebt);
            osu.ofundp = Convert.ToDecimal(ofund);
            osu.structp = Convert.ToDecimal(truct);
            osu.totalp = Convert.ToDecimal(total);
        }

        private void FutureCalculate(QueryReply _assets)
        {
            float elsep = 0; //00J、00K、00L、00M

            float dfut = 0;
            float ofut = 0;
            float total;

            foreach (AccountStatus _qr in _assets.ActStat_List)
            {
                switch (_qr.InstrumentType)
                {
                    case "00A":
                        //國內期權
                        dfut += Convert.ToInt32(_qr.TotalAsset);
                        break;
                    case "00J":
                        //國內期貨
                        elsep += Convert.ToInt32(_qr.TotalAsset);
                        break;
                    case "00K":
                        //國內選擇權
                        elsep += Convert.ToInt32(_qr.TotalAsset);
                        break;
                    case "00I":
                        //國外期權
                        ofut += Convert.ToInt32(_qr.TotalAsset);
                        break;
                    case "00L":
                        //國外期貨
                        elsep += Convert.ToInt32(_qr.TotalAsset);
                        break;
                    case "00M":
                        //國外選擇權
                        elsep += Convert.ToInt32(_qr.TotalAsset);
                        break;
                    default:
                        break;
                }
            }
            total = dfut + ofut;
            future.dfutp = Convert.ToDecimal(dfut);
            future.ofutp = Convert.ToDecimal(ofut);
            future.totalp = Convert.ToDecimal(total);
        }

        private void TrustCalculate(QueryReply _assets)
        {
            float elsep = 0; //00D、00E

            float sptrust = 0;
            float trustloan = 0;
            float total;

            foreach (AccountStatus _qr in _assets.ActStat_List)
            {
                switch (_qr.InstrumentType)
                {
                    case "005":
                        //信託基金
                        sptrust += Convert.ToInt32(_qr.TotalAsset);
                        break;
                    case "006":
                        //信託借券
                        trustloan += Convert.ToInt32(_qr.TotalAsset);
                        break;
                    case "00D":
                        //信託基金(台幣)
                        elsep += Convert.ToInt32(_qr.TotalAsset);
                        break;
                    case "00E":
                        //信託基金(外幣)
                        elsep += Convert.ToInt32(_qr.TotalAsset);
                        break;
                    default:
                        break;
                }
            }
            total = sptrust + trustloan;
            trust.sptrustp = Convert.ToDecimal(sptrust);
            trust.trustloanp = Convert.ToDecimal(trustloan);
            trust.totalp = Convert.ToDecimal(total);
        }

        private void CMACalculate(QueryReply _assets)
        {
            float act = 0;
            float total;

            foreach (AccountStatus _qr in _assets.ActStat_List)
            {

                switch (_qr.InstrumentType)
                {
                    case "00H":
                        //資金管理帳戶
                        act += Convert.ToInt32(_qr.TotalAsset);
                        break;
                    default:
                        break;
                }
            }
            total = act;
            cma.totalp = Convert.ToDecimal(total);
        }

        private void SumAssteCalculate(AssetFormat asset)
        {
            //算出該公司不同帳戶總資產與各帳戶比例
            asset.assetp = twStock.totalp + sBroke.totalp + future.totalp + osu.totalp + trust.totalp + cma.totalp;
            if (asset.assetp == 0) return;
            twStock.pr = Convert.ToInt32(Math.Round(100 / (asset.assetp) * twStock.totalp));
            sBroke.pr = Convert.ToInt32(Math.Round(100 / (asset.assetp) * sBroke.totalp));
            future.pr = Convert.ToInt32(Math.Round(100 / (asset.assetp) * future.totalp));
            osu.pr = Convert.ToInt32(Math.Round(100 / (asset.assetp) * osu.totalp));
            trust.pr = Convert.ToInt32(Math.Round(100 / (asset.assetp) * trust.totalp));
            cma.pr = Convert.ToInt32(Math.Round(100 / (asset.assetp) * cma.totalp));
        }
    }
}