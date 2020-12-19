using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RestaurantApp.Logger
{
    public static class LogWriter
    {
        private static void Write(this Log log)
        {
            log.Date = DateTime.Now;

            StreamWriter sw = new StreamWriter(LogConfiguration.LogDirectory + DateTime.Today.ToString("yyyy.MM.dd") + ".txt", true);
            StringBuilder sb = new StringBuilder();
            sb.Append(log.Date + " >>> ");
            sb.Append("[" + log.Type + "] ");
            sb.Append(log.CustomMessage);
            if (!String.IsNullOrEmpty(log.SourceMessage))
                sb.Append(" >>> " + log.SourceMessage);
            sw.WriteLine(sb.ToString());
            sw.Close();
        }

        public static void LogInfo(string customMessage)
        {
            Log log = new Log();
            log.Type = LogType.INFO;
            log.CustomMessage = customMessage;
            log.Write();
        }

        public static void LogCustomMessage(LogType type, string customMessage, string sourceMessage)
        {
            Log log = new Log();
            log.Type = type;
            log.CustomMessage = customMessage;
            log.SourceMessage = sourceMessage;
            log.Write();
        }

        public static void LogException(Exception exception)
        {
            Log log = new Log();
            log.Type = LogType.ERR;
            log.CustomMessage = exception.ToString();
            log.Write();
        }
    }
}
