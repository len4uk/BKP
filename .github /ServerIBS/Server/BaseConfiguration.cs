using ServerIBS.Server.Configuration;
using ServerIBS.Server.File;
using Logger;
using System;
using System.IO;

namespace ServerIBS.Server
{
    public abstract class BaseConfiguration
    {
        protected BaseAppFolder _baseAppFolder;

        protected BaseConfiguration(BaseAppFolder baseAppFolder)
        {
            _baseAppFolder = baseAppFolder;
        }

        /// <summary>
        /// Сохраняет настройки в файл
        /// </summary>
        /// <typeparam name="T">Класс для сереализации</typeparam>
        /// <param name="serializableObject">Объект сереализации</param>
        /// <param name="fileName">Путь к файлу</param>
        protected void SaveObject<T>(T serializableObject, string fileName)
        {
            try
            {
                DataSerializer<T>.SaveObject(serializableObject, fileName);
            }
            catch (DirectoryNotFoundException)
            {
                _baseAppFolder.CheckFolders();
                try
                {
                    DataSerializer<T>.SaveObject(serializableObject, fileName);
                }
                catch (Exception ex)
                {
                    LogManager.Write($"BaseConfiguration. Сбой при сохранении настроек: {ex.Message} - {ex.StackTrace}", LogLevel.Error);
                }
            }
            catch (Exception ex)
            {
                LogManager.Write($"BaseConfiguration. Сбой при сохранении настроек: {ex.Message} - {ex.StackTrace}", LogLevel.Error);
            }
        }
        /// <summary>
        /// Загружает настройки из файла
        /// </summary>
        /// <typeparam name="T">Класс для десереализации</typeparam>
        /// <param name="fileName">Путь к файлу</param>
        /// <param name="defaultSettings">Стандартные настройки</param>
        /// <returns>Десереализованный класс. если файл не найден, сохраняет и возвращает стандартные настройки</returns>
        protected T LoadObject<T>(string fileName, string defaultSettings) where T : class
        {
            T serilizableObj = null;

            if (!System.IO.File.Exists(fileName))
                SaveObject(DataSerializer<T>.LoadFromXml(defaultSettings), fileName);

            try
            {
                serilizableObj = DataSerializer<T>.LoadObject(fileName);
            }
            catch (Exception)
            {
                System.IO.File.Delete(fileName);
                try
                {
                    serilizableObj = DataSerializer<T>.LoadObject(fileName);
                }
                catch (Exception ex)
                {
                    LogManager.Write($"BaseConfiguration. Сбой при загрузке настроек {nameof(T)}: {ex.Message} - {ex.StackTrace}", LogLevel.Error);
                    serilizableObj = DataSerializer<T>.LoadFromXml(defaultSettings);
                    LogManager.Write("Загружены стандартные", LogLevel.Error);
                }
            }
            return serilizableObj;
        }

        protected void SaveObjectEncrypted<T>(T serializableObject, string fileName, string pass)
        {
            try
            {
                DataSerializer<T>.SaveObjectEncrypted(serializableObject, fileName, pass);
            }
            catch (DirectoryNotFoundException)
            {
                _baseAppFolder.CheckFolders();
                try
                {
                    DataSerializer<T>.SaveObjectEncrypted(serializableObject, fileName, pass);
                }
                catch (Exception ex)
                {
                    LogManager.Write($"BaseConfiguration. SaveObjectEncrypted. Сбой при сохранении настроек: {ex.Message} - {ex.StackTrace}", LogLevel.Error);
                }
            }
            catch (Exception ex)
            {
                LogManager.Write($"BaseConfiguration. SaveObjectEncrypted. Сбой при сохранении настроек: {ex.Message} - {ex.StackTrace}", LogLevel.Error);
            }
        }

        protected T LoadObjectDecrypted<T>(string fileName, string defaultSettings, string pass) where T : class
        {
            T serilizableObj = null;

            if (!System.IO.File.Exists(fileName))
                SaveObjectEncrypted(DataSerializer<T>.LoadFromXml(defaultSettings), fileName, pass);

            try
            {
                serilizableObj = DataSerializer<T>.LoadObjectDecrypted(fileName, pass);
            }
            catch (Exception)
            {
                System.IO.File.Delete(fileName);
                try
                {
                    serilizableObj = DataSerializer<T>.LoadObjectDecrypted(fileName, pass);
                }
                catch (Exception ex)
                {
                    LogManager.Write($"BaseConfiguration. LoadObjectDecrypted. Сбой при загрузке настроек {nameof(T)}: {ex.Message} - {ex.StackTrace}", LogLevel.Error);
                    serilizableObj = DataSerializer<T>.LoadFromXml(defaultSettings);
                    LogManager.Write("Загружены стандартные", LogLevel.Error);
                }
            }
            return serilizableObj;
        }
    }
}
