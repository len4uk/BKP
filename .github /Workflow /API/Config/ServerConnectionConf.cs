using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Config
{ /// <summary>
  /// Серверная конфигурация подключения
  /// </summary>
    public class ServerConnectionConf
    {
        /// <summary>
        /// Порт сервера
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// Максимальное кол-во клиентов
        /// </summary>
        public int MaxConnections { get; set; }
    }
}
