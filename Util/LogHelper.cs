
namespace JNyuluSoft.Util
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;
    using log4net;

    public class LogHelper
    {
        private static readonly string _loggerFile = "jnyulu.logger";
        private static readonly log4net.ILog _log = LogManager.GetLogger(Assembly.GetCallingAssembly(), _loggerFile);


        public static void Info(String message)
        {
            _log.Info(message);
        }

        public static void Error(String message)
        {
            _log.Error(message);
        } 
    }
}
