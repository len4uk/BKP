using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace DataBases.Database
{
    partial class Database
    {
        /// <summary>
        /// Добавление пользователя
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <param name="fio">ФИО</param>
        /// <param name="phone">Телефон</param>
        /// <param name="email">Почта</param>
        /// <param name="numberCard">Номер карты</param>
        /// <returns></returns>
        public bool AddUser(string login, string password, string fio, string phone = "", string email = "", string numberCard = "")
        {
            var command = new MySqlCommand();
            command.Parameters.Add(new MySqlParameter("@Login", MySqlDbType.String) { Value = login });
            command.Parameters.Add(new MySqlParameter("@Password", MySqlDbType.String) { Value = password });
            command.Parameters.Add(new MySqlParameter("@Fio", MySqlDbType.String) { Value = fio });
            command.Parameters.Add(new MySqlParameter("@Phone", MySqlDbType.String) { Value =string.IsNullOrEmpty(phone) ? null : phone });
            command.Parameters.Add(new MySqlParameter("@Email", MySqlDbType.String) { Value = string.IsNullOrEmpty(email) ? null : email });
            command.Parameters.Add(new MySqlParameter("@Card", MySqlDbType.String) { Value = string.IsNullOrEmpty(numberCard) ? null : numberCard });

            command.CommandText =
                "INSERT INTO users (login, password, fio, phone, email, number_card)  VALUES (@Login, @Password, @Fio, @Phone, @Email, @Card);";

            if (ChangeData(command) <= 0)
            {  
                Log("AddUser. Не удалось добавить пользователя", command);
                return false;
            }
            return true;

        }

        public bool CheckUser(string login, string password)
        {
            var command = new MySqlCommand();
            command.Parameters.Add(new MySqlParameter("@Login", MySqlDbType.String) { Value = login });
            command.Parameters.Add(new MySqlParameter("@Password", MySqlDbType.String) { Value = password });
            command.CommandText =
                "SELECT u.id FROM users u WHERE u.login = @Login AND u.password = @Password";

            IList<DbDataRecord> records = GetData(command);
            if (records.Count != 1)
                return false;

            return true;
        }
    }
}
