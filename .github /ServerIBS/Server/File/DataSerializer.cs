using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace ServerIBS.Server.File
{
    public static class DataSerializer<T>
    {
        /// <summary>
        /// Сохранить данные
        /// </summary>
        /// <param name="data">Список экземпляров заданного типа для сохранения</param>
        /// <param name="fileName">Путь к файлу, в который будут сохранены данные</param>
        public static void Save(List<T> data, string fileName)
        {
            var serializer = new XmlSerializer(typeof(List<T>));
            var fs = new FileStream(fileName, FileMode.Create);
            using (var streamWriter = new StreamWriter(fs))
            {
                serializer.Serialize(streamWriter, data);
            }
        }

        /// <summary>
        /// Сохранить данные
        /// </summary>
        /// <param name="data">Список экземпляров заданного типа для сохранения</param>
        /// <param name="fileName">Путь к файлу, в который будут сохранены данные</param>
        public static void SaveObject(T data, string fileName)
        {
            var serializer = new XmlSerializer(typeof(T));
            var fs = new FileStream(fileName, FileMode.Create);
            using (var streamWriter = new StreamWriter(fs))
            {
                serializer.Serialize(streamWriter, data);
            }
        }

        public static void SaveObjectEncrypted(T data, string fileName, string pass)
        {
            var key = new DESCryptoServiceProvider();
            var e = key.CreateEncryptor(Encoding.ASCII.GetBytes(pass), Encoding.ASCII.GetBytes("64bitPas"));
            using (var fs = System.IO.File.Open(fileName, FileMode.Create))
            using (var writer = new CryptoStream(fs, e, CryptoStreamMode.Write))
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, data);
                writer.Flush();
            }
        }

        /// <summary>
        /// Загрузить данные из файла
        /// </summary>
        /// <param name="fileName">Путь к файлу</param>
        /// <returns>Список экземпляров заданного типа</returns>
        public static T LoadObject(string fileName)
        {
            var serializer = new XmlSerializer(typeof(T));
            var fs = new FileStream(fileName, FileMode.Open);
            using (var streamReader = new StreamReader(fs))
            {
                return (T)serializer.Deserialize(streamReader);
            }
        }

        public static T LoadObjectDecrypted(string fileName, string pass)
        {
            var key = new DESCryptoServiceProvider();
            var e = key.CreateDecryptor(Encoding.ASCII.GetBytes(pass), Encoding.ASCII.GetBytes("64bitPas"));
            using (var fs = System.IO.File.Open(fileName, FileMode.Open))
            using (var stream = new CryptoStream(fs, e, CryptoStreamMode.Read))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stream);
            }
        }

        /// <summary>
        /// Загрузить данные из файла
        /// </summary>
        /// <param name="fileName">Путь к файлу</param>
        /// <returns>Список экземпляров заданного типа</returns>
        public static List<T> Load(string fileName)
        {
            var serializer = new XmlSerializer(typeof(List<T>));
            var fs = new FileStream(fileName, FileMode.Open);
            using (var streamReader = new StreamReader(fs))
            {
                return (List<T>)serializer.Deserialize(streamReader);
            }
        }

        public static T LoadFromXml(string xmlText)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (TextReader reader = new StringReader(xmlText))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}
