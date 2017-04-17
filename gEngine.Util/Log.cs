using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Util
{
    public class Log
    {
        private StreamWriter writer;
        private FileStream fileStream;
        private static Log single=null;
        protected Log()
        {
            string logfile = "logfile.txt";
            logfile = Directory.GetCurrentDirectory() +"\\"+ logfile;
            FileInfo fileinfo = new FileInfo(logfile);
            fileStream = fileinfo.Create();
            writer = new StreamWriter(fileStream);
        }

        protected void WriteLog(string log)
        {
            writer.WriteLine(log);
            writer.Flush();
            Console.Write(log+"\n");
        }

        public static Log Single()
        {
            if (single != null)
                return single;
            single = new Log();
            return single;
        }

        public static void LogError(string err)
        {
            Single().WriteLog("Error: " + DateTime.Now + "\t" + err);
        }

        public static void LogWarning(string warning)
        {
            Single().WriteLog("Warning: "+DateTime.Now + "\t" + warning);
        }

        public static void LogInfo(string info)
        {
            Single().WriteLog("Info: "+DateTime.Now + "\t" + info);
        }
    }
}
