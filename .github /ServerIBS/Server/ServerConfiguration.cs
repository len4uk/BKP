using API.Config;
using API.Constants;
using ServerIBS.Server.Configuration;
using Logger;
using ServerIBS.Server;

namespace ServerIBS.Server
{
    public class ServerConfiguration : CommonConfiguration<ServerFolder>
    {
        private const string PASS = "P@SSw0rd";
        public static readonly ServerConfiguration Instance;

        static ServerConfiguration()
        {
            Instance = new ServerConfiguration();
        }

        public ServerConfiguration() : base(new ServerFolder())
        {
            Files = new ServerFiles(Folders);
        }

        public ServerFolder Folders => (ServerFolder)_baseAppFolder;

        public ServerFiles Files { get; protected set; }

            
        public ServerConfig ServerConfig { get; protected set; }
             

        public string NameObject => FolderName.Server.LocalConfig;

        public void LoadAllSettings()
        {           
            ServerConfig = LoadObjectDecrypted<ServerConfig>(Files.ServerConfigFile, Resources.Config, PASS);
            //Применение настроек к Logger. 
            ServerConfig.Logger.LogPath = Folders.Logs;
            LogManager.SetConfigurationFileLogger(ServerConfig.Logger);
            
        }

        public void SaveServerConfig(ServerConfig newConfig = null)
        {
            SaveObjectEncrypted(newConfig ?? ServerConfig, Files.ServerConfigFile, PASS);
        }


    }
}
