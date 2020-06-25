using IBS_Server.ConnectedPhones;
using System;
using System.Collections.Generic;
using System.Text;

namespace IBS_Server
{
    public class Connected
    {
        public string Result { get; set; }

        public Connected(string text)
        {
            string[] massTextClient = text.Split(new char[] { '|' });

            if (massTextClient[0] == "2")
                ControllerConnected(massTextClient);

            if (massTextClient[0] == "2")
                PhoneConnected(massTextClient);

        }

        private void PhoneConnected(string[] massTextClient)
        {
            ConnectPhone phone = new ConnectPhone();

            string[] switcher = massTextClient[1].Split(new char[] { ':' });

            switch (switcher[0])
            {
                case "login": Result = phone.Login(massTextClient); break;
            }
        }

        private void ControllerConnected(string[] massTextClient)
        {
          
        }
    }
}
