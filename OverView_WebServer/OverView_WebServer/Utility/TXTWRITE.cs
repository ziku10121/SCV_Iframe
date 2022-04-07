using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace OverView_WebServer.Utility
{
    public class TXTWRITE
    {
        public static void write(string log)
        {
            string path = @"D:\\FubonCrm\\Log\\SCVWeb\\log.txt";
            try
            {
                StreamReader streamReader = new StreamReader(path);
                string text = streamReader.ReadToEnd();
                streamReader.Close();
                StreamWriter sw = new StreamWriter(path);
                sw.WriteLine(text + log);
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}