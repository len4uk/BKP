using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Constants
{
    public static class FileName
    {
        /// <summary>Название файла базы данных</summary>
        public static readonly string ContentDatabase = "Local.sdf";
               
        public static class Server
        {
            /// <summary>Конфигурационный файл базы данных, уровень лога и т.д.</summary>
            public const string Config = "config.xml";          
        }
    }
}
