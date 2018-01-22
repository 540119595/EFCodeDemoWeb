using log4net;
using log4net.Config;
using log4net.Repository;
using System;
using System.IO;

namespace Common.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class Log4NetHelper
    {
        // 使用前先添加log4net的程序包（在NuGet里添加）
        private static ILoggerRepository _repository;

        /// <summary>
        /// 读取配置文件，并使其生效。如果未找到配置文件，则抛出异常
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="configFilePath">配置文件全路径</param>
        public static void SetConfig(ILoggerRepository repository, string configFilePath)
        {
            _repository = repository;
            var fileInfo = new FileInfo(configFilePath);
            if (!fileInfo.Exists)
            {
                throw new Exception("未找到配置文件" + configFilePath);
            }
            XmlConfigurator.ConfigureAndWatch(_repository, fileInfo);
        }

        /// <summary>
        /// 输出错误日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ex"></param>
        public static void WriteError(Type t, Exception ex)
        {
            var log = LogManager.GetLogger(_repository.Name, t);
            log.Error(ex);
        }

        /// <summary>
        /// 记录消息日志
        /// </summary>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        public static void WriteInfo(Type t, string msg)
        {
            var log = LogManager.GetLogger(_repository.Name, t);
            log.Info(msg);
        }

        /// <summary>
        /// 记录警告信息
        /// </summary>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        public static void WriteWarn(Type t, string msg)
        {
            var log = LogManager.GetLogger(_repository.Name, t);
            log.Warn(msg);
        }
    }
}
