using API.Data.ApiEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data
{
    public class Routes
    {
        /// <summary>Номер маршрута</summary>
        public int Number { get; set; }
        /// <summary>НАчальная остановка</summary>
        public string StartStop { get; set; }
        /// <summary>Конечная остановка</summary>
        public string EndStop { get; set; }
        /// <summary>Общее количество автобусов</summary>
        public int Count { get; set; }
        /// <summary>Автобусов на маршруте</summary>
        public int CountRoute { get; set; }
        /// <summary>Загруженность маршрута</summary>
        public float Load { get; set; }
        /// <summary>Вид транспорта</summary>
        public ModeTransport Type { get; set; }
        /// <summary>Направление</summary>
        public int Direction { get; set; }


        public string GetTcp()
        {
            return "&" + Number + "|" + StartStop + "|" + EndStop + "|" + CountRoute + "|" + Count + "|" + Type + "|" + Load + "|" + Direction;
        }
    }
}
