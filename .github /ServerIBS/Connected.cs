using ServerIBS.ConnectedPhones;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServerIBS
{
    public class Connected
    {
        public string Result { get; set; }
        public List<string> ListResult { get; set; }

        public Connected(string text)
        {
            List<string> massTextClient = text.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            if (massTextClient[0] == "1")
            {
                massTextClient.ToList().RemoveAt(0);
                ControllerConnected(massTextClient);
            }
               

            if (massTextClient[0] == "2")
            {
               massTextClient.RemoveAt(0);
                PhoneConnected(massTextClient);
            }
               

        }

        private void PhoneConnected(List<string> massTextClient)
        {
            ConnectPhone phone = new ConnectPhone();

            string switcher = massTextClient[0];
            massTextClient.RemoveAt(0);

            Result = null;
            ListResult = null;

            switch (switcher)
            {
                case "regs":  Result = phone.Regs(massTextClient); break;
                case "login": Result = phone.Login(massTextClient); break;
                case "city": ListResult = phone.City(massTextClient);  break;
                case "route": ListResult = phone.Route(massTextClient); break;
            }

            if (ListResult != null)
                ListResult[ListResult.Count - 1] += "&&&";
            else if (Result != null)
                Result += "&&&";

        }

        private void ControllerConnected(List<string> massTextClient)
        {
          
        }
    }
}
