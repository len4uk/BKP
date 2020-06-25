using ServerIBS.Server.Configuration;

namespace ServerIBS.Server
{
    public class CommonConfiguration<T> : BaseConfiguration where T : BaseAppFolder
    {
        public CommonConfiguration(T classFolder)
           : base(classFolder)
        {
          
        }
    }
}
