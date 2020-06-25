using API.Constants;
using System;
using System.IO;

namespace ServerIBS.Server.Configuration
{
    public class BaseAppFolder : AppFolderBase<Folder>
    {
        public BaseAppFolder()
        {          
            string root = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            string log = Path.Combine(root, FolderName.Logs);

            SetPath(Folder.Root, root);
            SetPath(Folder.Logs, log);
        }
    }
}
