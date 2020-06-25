using Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Config
{
    /// <summary>
    /// Конфигурация сервера
    /// </summary>
    public class ServerConfig
    {
        /// <summary>
        /// Настройки подключенния к БД
        /// </summary>
        public DatabaseConfig Database { get; set; }

        /// <summary>
        /// Настройки логгера
        /// </summary>
        public FileLoggerConfig Logger { get; set; }

        /// <summary>
        /// Настройки сервера
        /// </summary>
        public ServerConnectionConf ServerConnectionConf { get; set; }
    }
}
