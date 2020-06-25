using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.Interfaces
{
    public interface IFileLogger : ILogger
    {
        /// <summary> Конфигурация логгера </summary>
        FileLoggerConfig Config { get; }
        /// <summary> Задать конфигурацию </summary>
        void SetConfig(FileLoggerConfig logConfig);
    }
}
