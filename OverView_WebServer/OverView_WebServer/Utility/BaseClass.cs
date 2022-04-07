using FDIPConfig;
using FDIPDefinition.Definition;
using FubonQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Caching;
using System.Web.Http;

namespace OverView_WebServer.Utility
{
    public class BaseClass : ApiController
    {
        string ClassName = "BaseClass";
        public string ThisSeverName = "SCV WebServer";

        #region 快取 變數
        private Cache _cache = HttpRuntime.Cache;
        private Object lock_obj = new object();
        #endregion

        #region FDIP 元件變數
        public FDIPQuery.Common.FDQArgs _args = null;
        public FubonQProc _fdq = null;
        #endregion

        #region LOG 元件變數
        public static LogProcessor.LogCollection logWriter = null;
        #endregion

        #region configdata 元件
        public ConfigData ConfigDT = null;
        #endregion

        #region 連線字串資料庫連線字串
        public static string FDIPDBConnStr = null;
        public static string SYSDEFDBConnStr = null;
        #endregion

        public BaseClass()
        {
            //TODO
        }

        /// <summary>
        /// 確認config
        /// </summary>
        /// <param name="_servername"></param>
        /// <returns></returns>
        public bool CheckConfigData(string _servername)
        {
            string _funcName = ClassName + "CheckConfigData";
            try
            {
                #region FDIPConfig撈資料庫資料
                //if (ConfigDT == null)
                //{
                //    //if (cache["ConfigData"] == null)                
                //    if ((_cache["ConfigData"] as ConfigData) == null)
                //    {
                //        TXTWRITE.write(DateTime.Now + "cache Config not null");
                //        ConfigDT = new ConfigData(_servername);
                //        bool _isgetConfigResult = ConfigDT.GetConfigData();

                //        lock (lock_obj)
                //        {
                //            _cache.Insert("ConfigData", ConfigDT, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration);
                //        }

                //        if (_isgetConfigResult)
                //        {
                //            FDIPDBConnStr = Config.ConfigData.DataBase.DB_FDIP;
                //            SYSDEFDBConnStr = Config.ConfigData.DataBase.DB_SYSDEF;
                //            TXTWRITE.write(DateTime.Now + "GetConfigSuccessful");
                //        }
                //        if (!_isgetConfigResult)
                //        {
                //            TXTWRITE.write(DateTime.Now + "GetConfigFail");
                //            return false;
                //        }
                //    }
                //    else
                //    {
                //        TXTWRITE.write(DateTime.Now + "cache Config not null");
                //        FDIPDBConnStr = Config.ConfigData.DataBase.DB_FDIP;
                //        SYSDEFDBConnStr = Config.ConfigData.DataBase.DB_SYSDEF;

                //        ConfigDT = _cache["ConfigData"] as ConfigData;
                //    }
                //}
                //else
                //{
                //    TXTWRITE.write(DateTime.Now + "ConfigData Config not null");
                //    ConfigDT = _cache["ConfigData"] as ConfigData;
                //}
                #endregion

                if (logWriter == null)
                {
                    if (_cache["log_writter"] == null)
                    {
                        string _logpath = @"D:\\FubonCrm\\Log\\SCVWeb\\log.txt"; ;
                        string _serviceVersion = "1.0.0.0";
                        string _loglevel = "9";

                        logWriter = new LogProcessor.LogCollection(_logpath, _servername, _serviceVersion, _loglevel);
                    }
                    else
                    {
                        lock (lock_obj)
                        {
                            _cache.Insert("log_writter", logWriter, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration);
                        }
                    }
                }
                LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.Common].WriteLog(_funcName, LogProcessor.LogType.DEBUG, ((int)ReturnStatus.SUCCESS).ToString("d4"), "logWriter create success");

                return true;
            }
            catch (Exception ex)
            {
                TXTWRITE.write(DateTime.Now + "Exception" + ex.StackTrace + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 確認FDQ是否還在
        /// </summary>     
        public bool CheckFDQObject()
        {
            string _funcName = ClassName + "CheckFDQbject";

            if (BaseConfig.SQLType == FDIPConfig.Definition.DefSQLType.MSSQL)
            {
                try
                {
                    if (_fdq == null)
                    {
                        if (_cache["FDQ"] == null)
                        {
                            LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.FDIP].WriteLog(_funcName, LogProcessor.LogType.DEBUG, ((int)ReturnStatus.SUCCESS).ToString("d4"), "Verify: Application[FDQ] is null.");

                            if (_args == null)
                            {

                                if (_cache["FDQ_ARGS"] == null)
                                {
                                    LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.FDIP].WriteLog(_funcName, LogProcessor.LogType.DEBUG, ((int)ReturnStatus.SUCCESS).ToString("d4"), "Verify: Application[FDQ_ARGS] is null.");
                                    LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.FDIP].WriteLog(_funcName, LogProcessor.LogType.DEBUG, ((int)ReturnStatus.SUCCESS).ToString("d4"), "DB_Sysdef" + Config.ConfigData.DataBase.DB_FDIP);

                                    #region FDQ 參數設定
                                    //目前只向DB查
                                    _args = new FDIPQuery.Common.FDQArgs();
                                    //_args.SQLInfo.DB_FDIP.DbConnString = Config.ConfigData.DataBase.DB_FDIP;
                                    //_args.SQLInfo.DB_FDIP.DbTimeout = Convert.ToInt16(Config.ConfigData.DataBase.DB_FDIP_Timeout);
                                    //_args.SQLInfo.DB_SYSDEF.DbConnString = Config.ConfigData.DataBase.DB_SYSDEF;
                                    //_args.SQLInfo.DB_SYSDEF.DbTimeout = Convert.ToInt16(Config.ConfigData.DataBase.DB_SYSDEF_Timeout);
                                    //_args.IsSSM = Config.ConfigData.Common.Is_System_Monitor;
                                    //_args.SSMProcName = this.ThisSeverName;
                                    //_args.SSMQList = Config.ConfigData.MsmqGroup.SSM_List;
                                    _args.SQLInfo.DB_FDIP.DbConnString = Properties.Settings.Default.DB_FDIP;
                                    _args.SQLInfo.DB_FDIP.DbTimeout = 30;
                                    _args.SQLInfo.DB_SYSDEF.DbConnString = Properties.Settings.Default.DB_SYSDEF;
                                    _args.SQLInfo.DB_SYSDEF.DbTimeout = 30;
                                    _args.SSMProcName = this.ThisSeverName;
                                    _args.IsSSM = false;
                                    _args.SSMQList = Config.ConfigData.MsmqGroup.SSM_List;
                                    #endregion
                                    Console.WriteLine(_args);
                                    lock (lock_obj)
                                    {
                                        _cache.Insert("FDQ_ARGS", _args, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration);
                                    }

                                    LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.FDIP].WriteLog(_funcName, LogProcessor.LogType.DEBUG, ((int)ReturnStatus.SUCCESS).ToString("d4"), "FDQ ARGS Success.");
                                }
                                else
                                {
                                    _args = _cache["FDQ_ARGS"] as FDIPQuery.Common.FDQArgs;
                                    LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.FDIP].WriteLog(_funcName, LogProcessor.LogType.DEBUG, ((int)ReturnStatus.SUCCESS).ToString("d4"), "FDQ ARGS Success.");
                                }
                            }
                            _fdq = new FubonQProc(_args);
                            _fdq.OnFDQOThrowLog += new FDIPEventHandler.OnPassingMessage<string>(WritingFDQLog);
                            _fdq.Start();
                            lock (lock_obj)
                            {
                                _cache.Insert("FDQ", _fdq, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration);
                            }
                            LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.FDIP].WriteLog(_funcName, LogProcessor.LogType.DEBUG, ((int)ReturnStatus.SUCCESS).ToString("d4"), "FDQ Initialize Success.");
                        }
                        else
                        {
                            _fdq = _cache["FDQ"] as FubonQProc;
                            LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.FDIP].WriteLog(_funcName, LogProcessor.LogType.EXCEPTION, ((int)ReturnStatus.SUCCESS).ToString("d4"), "Application[FDQ]: " + FDIPDefinition.Utility.Serialization.ConvertObjectToJson(_fdq));
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.FDIP].WriteLog(_funcName, LogProcessor.LogType.EXCEPTION, ((int)ReturnStatus.SUCCESS).ToString("d4"), "Application[FDQ]: " + ex.ToString() + "\r\n" + ex.StackTrace + "\r\n" + ex.Message);
                    return false;
                }
            }
            else
            {
                LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.FDIP].WriteLog(_funcName, LogProcessor.LogType.DEBUG, ((int)ReturnStatus.SUCCESS).ToString("d4"), "SQL Type is MySQL.");
            }
            return true;
        }

        /// <summary>
        /// 釋放FQO和拋出FQO錯誤訊息
        /// </summary>
        public void ReleaseFDQObject()
        {
            string _funcName = ClassName + "WritingFDQLog";
            try
            {
                //發生錯誤將FQO釋放，以免影響其他查詢
                _fdq.OnFDQOThrowLog -= new FDIPDefinition.Definition.FDIPEventHandler.OnPassingMessage<string>(WritingFDQLog);
                if (!_fdq.Stop())
                {
                    LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.FDIP].WriteLog(_funcName, LogProcessor.LogType.ERROR, ((int)ReturnStatus.SUCCESS).ToString("d4"), "QueryProcessor Stop Fail.");

                }

                lock (lock_obj)
                {
                    _cache.Remove("FDQ");
                }
                lock (lock_obj)
                {
                    _cache.Remove("FDQ_ARGS");
                }
                LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.FDIP].WriteLog(_funcName, LogProcessor.LogType.DEBUG, ((int)ReturnStatus.SUCCESS).ToString("d4"), "Stop FQO Successd.");

                logWriter.Stop();

                lock (lock_obj)
                {
                    _cache.Remove("log_writter");
                }
                _args = null;
                logWriter = null;
                _fdq = null;
            }
            catch { }
        }

        /// <summary>
        /// FDQ所拋出來的訊息
        /// </summary>
        /// <param name="_FDQlog"></param>
        private void WritingFDQLog(string _FDQlog)
        {
            string _funcName = ClassName + "WritingFDQLog";
            try
            {
                LogProcessor.LogCollection.LogWritterTable[LogProcessor.LogFIlePrefix.FDIP].WriteLog(_funcName, LogProcessor.LogType.DEBUG, ((int)ReturnStatus.SUCCESS).ToString("d4"), _FDQlog);
            }
            catch { }
        }

        //回報
        public HttpResponseMessage SendResponse(string Json)
        {
            try
            {
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                httpResponseMessage.Content = new StringContent(Json, System.Text.Encoding.UTF8, "application/json");
                return httpResponseMessage;
            }
            catch
            {
                return null;
            }
        }
    }

    public class ConfigData
    {
        private const string ClassName = "ConfigData.";
        private string LastError = string.Empty;
        public string GetLastError()
        {
            string _tmp = string.Empty;
            _tmp = LastError;
            LastError = string.Empty;
            return _tmp;
        }

        public ConfigData(string AP_Name)
        {
            try
            {
                Config.SetConfig(AP_Name);

                Config.OnLogReceived += new OnLogMessage(FIDPConfigLog);

            }
            catch (Exception ex)
            {
                throw new Exception(ClassName + ex.Message + ex.StackTrace);
            }
        }

        public bool GetConfigData()
        {
            string _funcName = ClassName + "GetConfigData";
            bool _result = true;

            try
            {
                if (BaseConfig.SQLType == FDIPConfig.Definition.DefSQLType.MSSQL)
                {
                    if (!Config.GetConfig())
                    {
                        _result = false;
                        LastError += ("\r\n" + _funcName + "=> FDIPConfig Get Failed.");
                    }
                }
                else if (BaseConfig.SQLType == FDIPConfig.Definition.DefSQLType.MYSQL)
                {
                    if (!Config.GetConfig(FDIPConfig.Definition.DefSQLType.MYSQL))
                    {
                        _result = false;
                        LastError += ("\r\n" + _funcName + "=> FDIPConfig Get Failed.");
                    }
                }
            }
            catch (Exception ex)
            {
                _result = false;
                throw new Exception(_funcName + ex.Message + ex.StackTrace);
            }

            return _result;
        }

        //FIDPConfig拋出的訊息
        private void FIDPConfigLog(LogMessage _lm)
        {

            string _funcName = ClassName + "FIDPConfigLog";
            LastError += ("\r\n" + _funcName + "=> MsgType: " + _lm.messagetype + ", MsgText: " + _lm.messagecontext);
            //System.Diagnostics.EventLog.WriteEntry("Service1", "Config Fail" + LastError, EventLogEntryType.Information);
            TXTWRITE.write(DateTime.Now + LastError);
        }
    }
}