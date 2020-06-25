
using API;
using Logger;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace DataBases.Database
{
    public partial class Database : IDatabase
    {
        private static Database _instance;
        private static readonly object _sync = new object();
        public string ConnectionString { get; protected set; }

        public Database()
        {

        }

        /// <summary>
        /// Создаёт единственный экземпляр объекта в памяти. Свойство потокобезопасно.
        /// </summary>
        public static Database Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_sync)
                    {
                        if (_instance == null)
                        {
                            _instance = new Database();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Установить строку подключения для базы данных
        /// </summary>
        /// <param name="user">Имя пользователя БД</param>
        /// <param name="password">Пароль рользователя БД</param>
        /// <param name="database">Имя БД</param>
        /// <param name="server">Адрес сервера БД</param>
        /// <param name="port">Порт сервера БД</param>
        public void SetConnectionString(string user, string password, string database, string server, uint port = 3306)
        {

            var mySqlConnectionStringBuilder = new MySqlConnectionStringBuilder();
            mySqlConnectionStringBuilder.UserID = user;
            mySqlConnectionStringBuilder.Password = password;
            mySqlConnectionStringBuilder.Server = server;
            mySqlConnectionStringBuilder.Database = database;
            mySqlConnectionStringBuilder.Port = port;
            mySqlConnectionStringBuilder.MaximumPoolSize = 50;
            mySqlConnectionStringBuilder.MinimumPoolSize = 2;
            mySqlConnectionStringBuilder.Pooling = true;

            ConnectionString = mySqlConnectionStringBuilder.ToString();
        }

        /// <summary>
        /// Проверить соединение с базой данных.
        /// </summary>
        /// <returns></returns>
        public bool CheckConnection()
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (Exception ex)
                {
                    LogManager.Write("CheckConnection. При подключении к БД было сгенерировано исключение: " + ex.Message,
                        LogLevel.Error);
                    return false;
                }
            }
        }

        /// <summary>
        /// Логирование аварии
        /// </summary>
        /// <param name="exMessage"></param>
        /// <param name="command"></param>
        private static void Log(string exMessage, MySqlCommand command)
        {
            LogManager.Write(exMessage, LogLevel.Error);
            LogManager.Write("Запрос: " + MySqlCommandToStringConverter(command), LogLevel.Debug);
        }

        private static string MySqlCommandToStringConverter(MySqlCommand cmd)
        {
            string query = cmd.CommandText;
            foreach (MySqlParameter prm in cmd.Parameters)
            {
                switch (prm.MySqlDbType)
                {
                    case MySqlDbType.Bit:
                        query = query.Replace(prm.ParameterName, string.Format("{0}", (bool)prm.Value ? 1 : 0));
                        break;
                    case MySqlDbType.Int32:
                        query = query.Replace(prm.ParameterName, string.Format("{0}", prm.Value));
                        break;
                    default:
                        query = query.Replace(prm.ParameterName, string.Format("'{0}'", prm.Value));
                        break;
                }
            }
            return query;
        }

        #region Общие методы для всех по работе с базой данных (редактирование, получение каких-то общих данных)

        /// <summary>
        /// Получить кол-во записей из другого запроса, вызывает внутри себя: 
        /// command.CommandText = string.Format("SELECT Count(*) FROM ( {0} ) c;", from);
        /// </summary>
        /// <param name="from">Запрос, из результата которого нужно получить кол-во записей в нём.</param>
        /// <param name="parameters">Параметры необходимые для выполнения переданного запроса.</param>
        /// <returns></returns>
        protected int GetCount(string from, MySqlParameter[] parameters)
        {
            var command = new MySqlCommand();
            command.Parameters.AddRange(parameters);
            command.CommandText = String.Format("SELECT Count(*) FROM ( {0} ) c;", from);

            using (command)
            using (var connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    return Int32.Parse(command.ExecuteScalar() + String.Empty);
                }
                catch (Exception ex)
                {
                    LogManager.Write("GetData. При получении данных из БД было сгенерировано исключение: " + ex.Message,
                        LogLevel.Error);
                    return 0;
                }
            }
        }

        /// <summary>
        /// Получить данные из базы данных в виде списка записей DbDataRecord. Для запросов SELECT. Вызывает внутри себя command.ExecuteReader()
        /// </summary>
        /// <param name="command">Команда</param>
        /// <returns></returns>
        protected List<DbDataRecord> GetData(MySqlCommand command)
        {
            var dataList = new List<DbDataRecord>();
            using (command)
            using (var connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandTimeout = 600;
                    using (MySqlDataReader dataReader = command.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            foreach (DbDataRecord record in dataReader)
                            {
                                dataList.Add(record);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogManager.Write("GetData. При получении данных из БД было сгенерировано исключение: " + ex.Message,
                        LogLevel.Error);
                }
            }
            return dataList;
        }

        protected object ExecuteQuery(MySqlCommand command)
        {
            using (command)
            using (var connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    return command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    LogManager.Write("ExecuteQuery. При выполнении команды было сгенерировано исключение: " + ex.Message,
                        LogLevel.Error);
                    return null;
                }
            }
        }


        /// <summary>
        /// Позволяет изменить данные в базе данных, например, запросы INSERT INTO, UPDATE, DELETE.
        /// </summary>
        /// <param name="command">Команда</param>
        /// <returns>Количество изменных строк</returns>
        protected int ChangeData(MySqlCommand command)
        {
            int changedRowsCount = 0;
            using (var connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    changedRowsCount = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    LogManager.Write("ChangeData. Изменении данных в БД: " + ex.Message, LogLevel.Error);
                }
            }
            return changedRowsCount;
        }

        protected bool ChangeData(MySqlCommand[] commands)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                var transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);

                foreach (var command in commands)
                {
                    command.Connection = connection;
                    command.Transaction = transaction;
                }

                try
                {

                    foreach (var command in commands)
                        command.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    try
                    {
                        transaction.Rollback();
                        LogManager.Write("ChangeData. Rollback. Изменении данных в БД: " + ex.Message, LogLevel.Error);
                    }
                    catch (Exception exR)
                    {
                        LogManager.Write("ChangeData. Rollback exeption! Изменении данных в БД: " + exR.Message, LogLevel.Error);
                    }
                    return false;
                }
            }

            return true;
        }

        protected bool ChangeData(MySqlCommand[] commands, ref int[] lastDbIds)
        {
            lastDbIds = new int[commands.Length];

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                var transaction = connection.BeginTransaction();

                foreach (var command in commands)
                {
                    command.Connection = connection;
                    command.Transaction = transaction;
                }

                try
                {
                    for (var i = 0; i < commands.Length; i++)
                    {
                        var cmd = commands[i];
                        cmd.ExecuteNonQuery();
                        lastDbIds[i] = (int)cmd.LastInsertedId;
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    try
                    {
                        transaction.Rollback();
                        LogManager.Write("ChangeData. Rollback. Изменении данных в БД: " + ex.Message, LogLevel.Error);
                    }
                    catch (Exception exR)
                    {
                        LogManager.Write("ChangeData. Rollback exeption! Изменении данных в БД: " + exR.Message, LogLevel.Error);
                    }
                    return false;
                }
            }

            return true;
        }
        #endregion
    }
}
