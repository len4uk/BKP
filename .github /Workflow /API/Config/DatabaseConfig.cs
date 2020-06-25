using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Config
{
    /// <summary>
    /// Конфигурация Базы Данных
    /// </summary>
    public class DatabaseConfig
    {
        /// <summary>
        /// Адрес (Хост)
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Пользователь
        /// </summary>
        public string User { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Имя базы данных
        /// </summary>
        public string DatabaseName { get; set; }
        /// <summary>
        /// Порт для подключения
        /// </summary>
        public int Port { get; set; }
    }
}
