using API.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace DataBases.Database
{
    partial class Database
    {
        public Transport GetTransport(int id, string city, string mode)
        {
            var command = new MySqlCommand();
            command.CommandText =
                $"SELECT * FROM transport_{city}_{mode}";

            List<DbDataRecord> records =  GetData(command);

            return new Transport()
            {
                Direction = Convert.ToInt32(records[0]["direction"]),
                Load = Convert.ToInt32(records[0]["load"])
            };
        }

      
    }
}
