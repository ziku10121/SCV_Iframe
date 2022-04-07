using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Systex.Tools;

namespace OverView_WebServer.Utility
{
    public class LogProcessor
    {
        public enum LogFIlePrefix
        {
            MainFile = 0,
            /// <summary>Common Log</summary>
            Common = 1,
            /// <summary>Query Log</summary>
            Query = 2,
            /// <summary>FDIP Log</summary>
            FDIP = 3, 
            /// <summary>Fubon Query Log</summary>
            Fubon = 4,
            /// <summary>D365 Log</summary>
            D356 = 5
        }

        public enum LogType : uint
        {
            INFO,
            DEBUG,
            ERROR,
            EXCEPTION,
            SERVER_IN,
            SERVER_OUT,
            SERVER_OUT_FAIL
        }

        public class LogCollection
        {
            public static string LOG_PATH = string.Empty;
            public static string ServerName = string.Empty;
            public static string ServiceVersion = string.Empty;
            public static string Loglevel = string.Empty;
            public static Dictionary<LogFIlePrefix, LogWritter> LogWritterTable = new Dictionary<LogFIlePrefix, LogWritter>();


            public LogCollection(string _logpath, string _serverName, string _serviceVersion, string _loglevel)
            {
                LOG_PATH = _logpath;
                ServerName = _serverName;
                ServiceVersion = _serviceVersion;
                Loglevel = _loglevel;
                Start();
            }

            public void Start()
            {
                try
                {
                    foreach (LogFIlePrefix _lfs in Enum.GetValues(typeof(LogFIlePrefix)))
                    {
                        LogWritter _logWritter = new LogWritter();
                        _logWritter.Initialize();
                        _logWritter.logFIlePrefix = _lfs.ToString();
                        _logWritter.Start();

                        LogWritterTable.Add(_lfs, _logWritter);
                    }
                }
                catch
                {}
            }
            public void Stop()
            {
                try
                {
                    foreach (KeyValuePair<LogFIlePrefix, LogWritter> _logObj in LogWritterTable)
                    {
                        LogWritter _logWritter = null;
                        if (_logObj.Value != null)
                        {
                            _logWritter = _logObj.Value;
                            _logWritter.Stop();
                        }
                    }
                }
                catch { }
                finally
                {
                    LogWritterTable = null;
                }
            }

        }
        public class LogWritter
        {

            internal string logPath = @".";
            internal string serviceName = "SCVWeb";
            internal string serviceVersion = "1.0.0.0";
            internal string logFIlePrefix = "SCVWeb";
            internal string enableMonitor = "false";
            internal string traceLevel = "9";
            internal bool enableQueue = false;

            private MessageMonitor logWriter = null;



            internal LogWritter()
            {
                logPath = @"D:\\FubonCrm\\Log\\SCVWeb\\log.txt";
                serviceName = "SCVWeb";
                serviceVersion = "1.0.0.0";
                logFIlePrefix = "SCVWeb";
                enableMonitor = "false";
                traceLevel = "9";
                enableQueue = false;
            }


            internal void Initialize()
            {

                logPath = LogCollection.LOG_PATH + "\\" + DateTime.Now.ToString("yyyyMMdd");
                logFIlePrefix = LogCollection.ServerName;
                traceLevel = LogCollection.Loglevel;
                serviceName = LogCollection.ServerName;
                serviceVersion = LogCollection.ServiceVersion;
            }

            internal void Initialize(string _plainLogPath, string _plaintracelvl)
            {

                logPath = _plainLogPath;
                logFIlePrefix = LogCollection.ServerName;
                traceLevel = _plaintracelvl;
                serviceName = LogCollection.ServerName;
                serviceVersion = LogCollection.ServiceVersion;
            }

            internal bool Start()
            {
                bool _result = false;

                try
                {
                    logWriter = new MessageMonitor(logPath, logFIlePrefix, traceLevel, 0, false);
                    _result = true;
                }
                catch
                {
                    _result = false;
                }

                return _result;
            }

            internal void Stop()
            {
                try
                {
                    if (logWriter != null) logWriter.Stop();
                }
                catch { }
            }

            public void WriteLog(string _location, LogType _type, string _code, string _msg)
            {
                MessageMonitor.Priority _priority = LogWritterUtility.ConvertToPriority(_type);
                string _log = "Type: " + _type.ToString() + "\t" + _msg;

                if (enableQueue && (_priority.Equals(MessageMonitor.Priority.EXCEPTION) || _priority.Equals(MessageMonitor.Priority.FAULT)))
                    logWriter.EnLogString(this.serviceName + "." + _location, _priority, "8001|" + this.serviceName + "|" + _log);
                else
                    logWriter.EnLogString(this.serviceName + "." + _location, _priority, _log);
            }
        }
        public static class LogWritterUtility
        {
            public static MessageMonitor.Priority ConvertToPriority(LogType _type)
            {
                MessageMonitor.Priority _priority = MessageMonitor.Priority.DEBUG;

                switch (_type)
                {
                    case LogType.DEBUG:
                        _priority = MessageMonitor.Priority.DEBUG;
                        break;
                    case LogType.INFO:
                        _priority = MessageMonitor.Priority.NORMAL;
                        break;
                    case LogType.ERROR:
                        _priority = MessageMonitor.Priority.FAULT;
                        break;
                    case LogType.EXCEPTION:
                        _priority = MessageMonitor.Priority.EXCEPTION;
                        break;
                    case LogType.SERVER_IN:
                        _priority = MessageMonitor.Priority.NORMAL;
                        break;
                    case LogType.SERVER_OUT:
                        _priority = MessageMonitor.Priority.NORMAL;
                        break;
                    case LogType.SERVER_OUT_FAIL:
                        _priority = MessageMonitor.Priority.NORMAL;
                        break;
                }

                return _priority;
            }


        }
    }
}