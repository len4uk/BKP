using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Constants
{
    public static class FolderName
    {
        /// <summary>Название папок логов для всех приложений</summary>
        public const string Logs = "Logs";

        public static class Server
        {
            /// <summary>Название папки, в которой хранятся локальные для объекта настройки сервера</summary>
            public static string LocalConfig = "IBS";
            /// <summary>Название корневой папки, в которой хранятся данные сервера</summary>
            public const string Root = "Server";
        }
    }
}
