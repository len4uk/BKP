using DataBases.Database;
using System;
using System.Collections.Generic;
using System.IO;

namespace ServerIBS.ConnectedPhones
{
    public class ConnectPhone
    {
        public string Regs(List<string> regs)
        {
            if (regs.Count == 7)
                if (Database.Instance.AddUser(regs[1], regs[5], regs[0], regs[3], regs[2], regs[4]))
                    return "OK";
            return "ERROR";
        }

        public string Login(List<string> login)
        {
            if (login.Count == 2)
                if (Database.Instance.CheckUser(login[0], login[1]))
                    return "OK";
            return "ERROR";
        }

        internal List<string> City(List<string> city)
        {
            if (city.Count == 1)
            {
                List<string> res = new List<string>();
                var result = Database.Instance.CheckRouteCity(city[0]);
              
                for(int i = 0; i < 100; i++)
                    foreach (var r in result)
                        res.Add(r.GetTcp());


                if (res.Count != 0)
                    return res;

            }
            return new List<string>() { "Error"};
        }

        internal List<string> Route(List<string> route)
        {
            if (route.Count == 3)
            {
                List<string> res = new List<string>();
                var result = Database.Instance.CheckRouteNumber(route[0], route[1], route[2]);
                
                foreach (var r in result)
                    res.Add(r.GetTcp());

                if (res.Count != 0)
                    return res;
            }
            return new List<string>() { "Error" };
        }
    }
}
