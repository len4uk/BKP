using API.Constants;
using ServerIBS.Server.Configuration;
using System.IO;

namespace ServerIBS.Server
{
    public class ServerFiles
    {
        public ServerFiles(ServerFolder folder)
        {
            ServerConfigFile = Path.Combine(FileName.Server.Config);         
        }

        /// <summary>Конфигурационный файл сервера</summary>
        public string ServerConfigFile { get; private set; }
     
    }
}
