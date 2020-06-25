using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data
{
    public class RouteTransport
    {
        /// <summary>Id транспорта</summary>
        public int IdTransport { get; set; }
        /// <summary>Время прибытия</summary>
        public int Time { get; set; }
        /// <summary>Загруженность</summary>
        public int Load { get; set; }
        /// <summary>Название остановки</summary>
        public string Stop { get; set; }
        /// <summary>Направление</summary>
        public int Direction { get; set; }

        public string GetTcp()
        {
            return "&" + IdTransport + "|" + Time + "|" + Load + "|" + Stop + "|" + Direction;
        }
    }
}
