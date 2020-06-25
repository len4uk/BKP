using Logger.Interfaces;
using System;

namespace Logger
{
    /// <summary>
    /// Логгер. Настройки по умолчанию: 
    ///     LogName = "defaultLog", 
    ///     LogPath = Environment.CurrentDirectory, 
    ///     LogLevel = LogLevel.All,
    /// </summary>
    /// <remarks>
    /// Необходимо перед использованием проинициализировать настройки <see cref="SetConfigurationFileLogger"/>
    /// объектом <see cref="FileLoggerConfig"/>. Инициализацию нужно проводить в самый ранний момент работы приложения.
    /// Хорошее место это проект с конфигурацией приложений. {App}Configuration.LoadAllSettings.
    /// </remarks>
    public static class LogManager
    {
        private static IFileLogger Logger;
        //Признак вывода на консоль
        public static bool ConsoleOutputOn = true;

        static LogManager()
        {
            Logger = new FileLogger(new FileLoggerConfig()
            {
                LogName = "DefaultLog",
                LogPath = Environment.CurrentDirectory,
                LogLevel = LogLevel.All,
            });
        }

        public static void SetConfigurationFileLogger(FileLoggerConfig _config)
        {
            Logger.SetConfig(_config);
        }


        public static void Write(string message, LogLevel logLevel)
        {
            if (Logger.Config.LogLevel <= logLevel)
            {
                Logger.AppendShortMessage(message, logLevel);

                if (ConsoleOutputOn) Console.WriteLine(message);
            }
        }

        public static void AppendText(string message)
        {
            Write(message, LogLevel.Info);
        }

        public static void AppendException(Exception ex)
        {
            Write(string.Format("{0} - {1}", ex.Message, ex.StackTrace), LogLevel.Error);
        }

        public static void AppendException(Exception ex, string mess)
        {
            Write(string.Format("{0}: {1} - {2}", mess, ex.Message, ex.StackTrace), LogLevel.Error);
        }


    }
}
