using API.Data;
using System.Collections.Generic;

namespace API
{
    public interface IDatabase
    {
        bool AddUser(string login, string password, string fio, string phone = null, string email = null, string numberCard = null);

        bool CheckUser(string login, string password);

        List<Routes> CheckRouteCity(string city);

        List<RouteTransport> CheckRouteNumber(string city, string number, string mode);
    }
}
