using System;
using System.IO;
using System.IO.Compression;

namespace Logger
{
    /// <summary>
    ///     Операции над файлами
    /// </summary>
    internal class FileOperations
    {
        /// <summary>
        ///     Удаление файлов старше месяца из папки
        /// </summary>
        /// <param name="dirPath">папка</param>
        /// <param name="ext"></param>
        public static void DeleteOldFilesInDir(string dirPath, string ext)
        {
            string[] files = Directory.GetFiles(dirPath);

            foreach (string file in files)
            {
                var fi = new FileInfo(file);
                if (fi.LastWriteTime < DateTime.Now.AddMonths(-1) && fi.Extension.Contains(ext))
                    fi.Delete();
            }
        }

        /// <summary>
        ///     Сжатие файла в gz
        /// </summary>
        /// <param name="filePath"></param>
        public static void CompressFile(string filePath)
        {
            var fi = new FileInfo(filePath);

            using (FileStream inFile = fi.OpenRead())
            {
                if ((File.GetAttributes(fi.FullName) & FileAttributes.Hidden) != FileAttributes.Hidden &
                    fi.Extension != ".gz")
                {
                    using (FileStream outFile = File.Create(fi.FullName + ".gz"))
                    {
                        using (var compress = new GZipStream(outFile, CompressionMode.Compress))
                        {
                            inFile.CopyTo(compress);
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     Сжатие всех файлов в папке кроме последнего
        /// </summary>
        /// <param name="dirPath">папка</param>
        public static void CompressAllFiles(string[] files)
        {
            foreach (string file in files)
            {
                var fi = new FileInfo(file);
                if (fi.CreationTime < DateTime.Now.AddDays(-1) && !fi.Extension.Contains("gz"))
                {
                    CompressFile(file);
                    fi.Delete();
                }
            }
        }
    }
}
