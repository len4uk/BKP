using API.Constants;
using System;
using System.IO;

namespace ServerIBS.Server.Configuration
{
    public class ServerFolder : BaseAppFolder
    {
        public ServerFolder()
          : base() // Этот параметр для сервера не важен, сервер использует свои расположения, отличные от расположений остальных клиентских приложений
                    //Он тут нужен для совместимости по архитектуре с BaseConfiguration, CommonFiles итп
        {
            string root = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                FolderName.Server.Root);
            string main = Path.Combine(root, FolderName.Server.LocalConfig);
          
            string logs = Path.Combine(main, FolderName.Logs);

            SetPath(Folder.Root, root);
            SetPath(Folder.Logs, logs);
        }


        [Path]
        public string Logs
        {
            get { return GetPath(Folder.Logs); }
        }

        [Path]
        public string Root
        {
            get { return GetPath(Folder.Root); }
        }
    }
}
