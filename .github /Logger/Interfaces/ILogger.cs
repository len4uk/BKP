using System;
using System.Runtime.CompilerServices;


namespace Logger.Interfaces
{
    public interface ILogger
    {
        /// <summary>
        /// Добавить сообщение в файл лога c указанием класса, файла и номера строки
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        /// <param name="memberName">Имя члена класса, в котором вызван данный метод</param>
        /// <param name="sourceFilePath">Название файла исходного кода</param>
        /// <param name="sourceLineNumber">Номер строки в файле исходного кода</param>
        void AppendMessage(string message,
            [CallerMemberName]string memberName = "",
            [CallerFilePath]string sourceFilePath = "",
            [CallerLineNumber]int sourceLineNumber = 0);
        /// <summary>
        /// Добавить короткое сообщение
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="logLevel">Уровень</param>
        void AppendShortMessage(string message, LogLevel logLevel);

        /// <summary>
        /// Добавить исключение
        /// </summary>
        /// <param name="ex">Исключение</param>
        /// <param name="message">Сообщение</param>
        /// <param name="logLevel">Уровень</param>
        void AppendException(Exception ex, string message = "", LogLevel logLevel = LogLevel.Error);
    }
}
