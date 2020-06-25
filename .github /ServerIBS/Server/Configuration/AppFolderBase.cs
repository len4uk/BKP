using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace ServerIBS.Server.Configuration
{
    /// <summary>
    /// Предоставляет интерфейс для доступа к папкам по ключу. Наследуюясь от данного класса 
    /// нужно проинициализировать в конструкторе класса наследника поле <c>_folders</c>.
    /// </summary>
    /// <typeparam name="TFolderNameEnum">Тип перечисления-ключа для доступа к папкам по этому ключу</typeparam>
    public abstract class AppFolderBase<TFolderNameEnum> where TFolderNameEnum : struct
    {
        private Dictionary<TFolderNameEnum, string> _folders;

        protected AppFolderBase()
        {
            _folders = new Dictionary<TFolderNameEnum, string>();
        }
        /// <summary>
        /// Получить значения всех свойств типа <see cref="string"/>, помеченных атрибутом <see cref="PathAttribute"/>.
        /// Чтобы получать значения свойств при помощи этого метода, нужно помечать свойства типа  <see cref="string"/>
        /// атрибутом  <see cref="PathAttribute"/>.
        /// </summary>
        /// <returns>Значения свойств-путей к папкам</returns>
        protected string[] GetPathesFromPropertiesMarkedWithPathAttribute()
        {
            Type type = GetType();
            PropertyInfo[] properties = type.GetProperties();
            List<string> pathes = new List<string>(properties.Length);
            for (int i = 0; i < properties.Length; i++)
            {
                PropertyInfo propInfo = properties[i];
                foreach (CustomAttributeData attribute in propInfo.CustomAttributes)
                {
                    if (attribute.AttributeType == typeof(PathAttribute) &&
                        propInfo.PropertyType == typeof(string))
                    {
                        pathes.Add((string)propInfo.GetValue(this));
                    }
                }
            }
            return pathes.ToArray();
        }

        /// <summary>
        /// Получить путь к папке
        /// </summary>
        /// <param name="folder">Ключ, по которому будет получен путь к папке</param>
        /// <returns>Путь к папке</returns>
        public string GetPath(TFolderNameEnum folder)
        {
            return _folders[folder];
        }
        /// <summary>
        /// Задать путь к папке
        /// </summary>
        /// <param name="folder">Ключ, по которому будет получен путь к папке</param>
        /// <param name="path">Путь к папке</param>
        public void SetPath(TFolderNameEnum folder, string path)
        {
            if (_folders.ContainsKey(folder))
                _folders[folder] = path;
            else
                _folders.Add(folder, path);
        }

        /// <summary>
        /// Проверить папки на существование и создать, если не существует.
        /// </summary>
        public void CheckFolders()
        {
            foreach (KeyValuePair<TFolderNameEnum, string> pair in _folders)
            {
                if (!Directory.Exists(pair.Value))
                {
                    Directory.CreateDirectory(pair.Value);
                }
            }
        }

        /// <summary>
        /// Проверить папки на существование и создать, если не существует.
        /// </summary>
        public Task CheckFoldersAsync()
        {
            return Task.Run(() =>
            {
                foreach (KeyValuePair<TFolderNameEnum, string> pair in _folders)
                {
                    if (!Directory.Exists(pair.Value))
                    {
                        Directory.CreateDirectory(pair.Value);
                    }
                }
            });
        }
    }
}
