using Logger.Interfaces;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Logger
{
    public class FileLogger : IFileLogger
    {
        private static readonly object _appendExceptionLocker = new object();
        private static readonly object _lockerObject = new object();
        private const string _separator = "=========================";

        /// <summary>
        /// Конфигурация
        /// LogPath - Путь до корневой папки логов
        /// LogLevel - Минимальный уровень логов
        /// </summary>
        public FileLoggerConfig Config { get; private set; }
        /// <summary>
        /// Задать конфигурацию
        /// </summary>
        /// <param name="logConfig">Конфигурация</param>
        /// <exception cref="ArgumentException">Имя файла не определено</exception>
        public void SetConfig(FileLoggerConfig logConfig)
        {
            lock (_lockerObject)
            {
                if (string.IsNullOrWhiteSpace(logConfig.LogName))
                    throw new ArgumentException("fileName");

                Config = logConfig;
                if (!Directory.Exists(Config.LogPath))
                    Directory.CreateDirectory(Config.LogPath);

                FullPathToLastWritedLogFile = Path.Combine(Config.LogPath,
                    string.Format("{0} - {1}{2}", Config.LogName, DateTime.Now.ToShortDateString(), ".txt"));
            }

            FileOperations.CompressAllFiles(Directory.GetFiles(Config.LogPath, $"{Config.LogName} - *.txt"));
        }

        public FileLogger(FileLoggerConfig config)
        {
            try
            {
                SetConfig(config);
            }
            catch (ArgumentException)
            {
                FileLoggerConfig conf = new FileLoggerConfig()
                {
                    LogPath = config.LogPath,
                    LogName = "notInitNameLog",
                    LogLevel = config.LogLevel,
                };
                SetConfig(conf);
            }
        }

        /// <summary>Полный путь к файлу конфигурации, который был открыт для записи последний раз.
        /// Каждую новую запись это свойство обновляется</summary>
        public string FullPathToLastWritedLogFile { get; private set; }

        public void AppendMessage(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            lock (_lockerObject)
            {
                DateTime now = DateTime.Now;
                string resultText = string.Format("{0}: {2}Метод: {4}. Строка: {6}.{2}Файл: {5}{2}Сообщение: {1}{2}{3}{2}{2}",
                    now.TimeOfDay,
                    message,
                    Environment.NewLine,
                    _separator,
                    memberName,
                    sourceFilePath,
                    sourceLineNumber);

                FullPathToLastWritedLogFile = Path.Combine(Config.LogPath,
                    string.Format("{0} - {1}{2}", Config.LogName, now.ToShortDateString(), ".txt"));
                File.AppendAllText(FullPathToLastWritedLogFile, resultText);
            }
        }

        public void AppendShortMessage(string message, LogLevel logLevel)
        {
            Task.Run(() =>
            {
                lock (_lockerObject)
                {
                    DateTime now = DateTime.Now;
                    string resultText = string.Format("[{1}][{0}]: {2}{3}",
                        logLevel,
                        now.TimeOfDay,
                        message,
                        Environment.NewLine);

                    FullPathToLastWritedLogFile = Path.Combine(Config.LogPath,
                        string.Format("{0} - {1}{2}", Config.LogName, now.ToShortDateString(), ".txt"));
                    File.AppendAllText(FullPathToLastWritedLogFile, resultText);
                }

            });
        }

        public void AppendException(Exception ex, string message = "", LogLevel logLevel = LogLevel.Error)
        {
            lock (_appendExceptionLocker)
            {
                string mess;
                if (message == "")
                {
                    mess = string.Format(
                        "Message: {1};{0}" +
                        "Source: {2};{0}" +
                        "TargetSite: {3}{0}" +
                        "InnerException: {4};{0}" +
                        "StackTrace: {0}{5};"
                        , Environment.NewLine, ex.Message, ex.Source, ex.TargetSite, ex.InnerException, ex.StackTrace);
                }
                else
                {
                    mess = string.Format(
                        "DevelopMessage: {1}; {0}" +
                        "Message: {2};{0}" +
                        "Source: {3};{0}" +
                        "TargetSite: {4}{0}" +
                        "InnerException: {5};{0}" +
                        "StackTrace: {0}{6};"
                        , Environment.NewLine, message, ex.Message, ex.Source, ex.TargetSite, ex.InnerException,
                        ex.StackTrace);
                }

                AppendShortMessage(mess, logLevel);
            }
        }
    }
}
