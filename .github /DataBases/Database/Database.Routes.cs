using API.Data;
using API.Data.ApiEnum;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace DataBases.Database
{
    partial class Database
    {
        public List<Routes> CheckRouteCity(string city)
        {
            List<Routes> routes = new List<Routes>();

            var command = new MySqlCommand();          
            command.CommandText =
                $"SELECT * FROM route_{city}";

            IList<DbDataRecord> records = GetData(command);
           
            foreach(var record in records)
            {
                Routes route = new Routes();
                route.Number = Convert.ToInt32(record["number"]);
                route.Type = (ModeTransport)Enum.Parse(typeof(ModeTransport),Convert.ToString(record["type"]));
                route.Direction = Convert.ToInt32(record["direction"]);
                
                if(route.Direction == 1)
                {
                    route.StartStop = GetStop(city, route.Number, Convert.ToString(record["type"]), false);
                    route.EndStop = GetStop(city, route.Number, Convert.ToString(record["type"]), true);
                }
                else
                {
                    route.StartStop = GetStop(city, route.Number, Convert.ToString(record["type"]), true);
                    route.EndStop = GetStop(city, route.Number, Convert.ToString(record["type"]), false);
                }

                if (String.IsNullOrEmpty(route.StartStop) || String.IsNullOrEmpty(route.EndStop))
                    break;

                int[] idTransport = ParsingTransport(Convert.ToString(record["list_bus"]));
                route.Count = idTransport.Length;
                route.CountRoute = GetTransportRoute(idTransport, city, Convert.ToString(record["type"]));
                route.Load = Convert.ToInt32(record["average_workload"]);
                routes.Add(route);
            }

            return routes;
        }

        private int GetTransportRoute(int[] idTransport, string city, string type)
        {
            city = city.ToLower();
            type = type.ToLower();
            string id = "";

            for (int i = 0; i < idTransport.Length; i++)
                id += idTransport[i].ToString() + ",";

            id = id.Remove(id.Length - 1);

            var command = new MySqlCommand();
            command.CommandText =
                $"SELECT COUNT(id) FROM transport_{city}_{type} WHERE id IN ({id}) AND is_route = 1";
           
            IList<DbDataRecord> records = GetData(command);
            return Convert.ToInt32(records[0][0]);
        }

        /// <summary>
        /// Парсим id транспорта
        /// </summary>
        /// <param name="list_bus">строка транспорта на маршруте</param>
        /// <returns></returns>
        private int[] ParsingTransport(string list_bus)
        {
            string[] transport = list_bus.Split(new char[] { '|'});
            int[] idTransport = new int[transport.Length];

            for (int i = 0; i < idTransport.Length; i++)
                idTransport[i] = Convert.ToInt32(transport[i]);

            return idTransport;
        }
        /// <summary>
        /// Определение остановки начальной и конечной
        /// </summary>
        /// <param name="city">Город</param>
        /// <param name="number">Номер</param>
        /// <param name="type">Тип</param>
        /// <param name="max">Начальная или конечная</param>
        /// <returns></returns>
        private string GetStop(string city, int number, string type, bool max)
        {
            city = city.ToLower();
            type = type.ToLower();
            string str = "";
            string title = $"route_{city}_{number}_{type}_stop";
            if(max)
            {
                str = $"SELECT MAX(stop.id) FROM {title} stop";
            }
            else
            {
                str = $"SELECT MIN(stop.id) FROM {title} stop";
            }

            var command = new MySqlCommand();
            try
            {
                command.CommandText =
                   $"SELECT city_stop.name FROM stop_{city} city_stop WHERE  city_stop.id = (SELECT stop.id_stop FROM {title} stop WHERE stop.id = ({str}))";

                IList<DbDataRecord> records = GetData(command);
                return Convert.ToString(records[0][0]);

            }
            catch
            {
                Log("Не найдена таблица", command);
                return "";
            }
        }

        public List<RouteTransport> CheckRouteNumber(string city, string number, string mode)
        { 
            var command = new MySqlCommand();
            command.CommandText =
                $"SELECT * FROM route_{city}_{number}_{mode}_stop";

            return ConverterRouteTransport(GetData(command), city, mode);
        }

        private List<RouteTransport> ConverterRouteTransport(IList<DbDataRecord> records, string city, string mode)
        {
            List<RouteTransport> transports = new List<RouteTransport>();

            foreach (var record in records)
            {
                RouteTransport routeTransport = new RouteTransport()
                {
                    IdTransport = Convert.ToInt32(record["id_transport"]),
                    Stop = StopName(Convert.ToInt32(record["id_stop"]), city)
                };

                Transport transport = GetTransport(routeTransport.IdTransport, city, mode);

                routeTransport.Time = transport.Direction == 0 ? Convert.ToInt32(record["minutes_1"]) : Convert.ToInt32(record["minutes_2"]);
                routeTransport.Load = transport.Load;

                transports.Add(routeTransport);
            }

            return transports;
        }

        private string StopName(int id, string city)
        {
            var command = new MySqlCommand();

            command.Parameters.Add(new MySqlParameter("@Id", MySqlDbType.Int32) { Value = id });

            command.CommandText =
                $"SELECT name FROM stop_{city} WHERE id = @Id";

            var result = GetData(command);

            return result[0][0].ToString();
        }
    }
}
