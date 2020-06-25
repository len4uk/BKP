namespace Logger
{
    public class FileLoggerConfig
    {
        /// <summary> Путь до файла лога </summary>
        public string LogPath { get; set; }
        /// <summary> Название лога </summary>
        public string LogName { get; set; }
        /// <summary> Уровень пропускаемых сообщений лога </summary>
        public LogLevel LogLevel { get; set; }
    }
}
