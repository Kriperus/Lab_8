using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TaskScheduler
{
    // Класс для работы с бинарным файлом
    public static class TaskFileManager
    {
        private const string DataFile = "tasks.dat";

        // Чтение базы данных из бинарного файла
#pragma warning disable SYSLIB0011 // Отключаем предупреждение об устаревании BinaryFormatter
        public static List<Task> LoadTasks()
        {
            try
            {
                if (!File.Exists(DataFile))
                {
                    return new List<Task>();
                }

                using (var stream = new FileStream(DataFile, FileMode.Open, FileAccess.Read))
                {
                    var formatter = new BinaryFormatter();
                    return (List<Task>)formatter.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
                return new List<Task>();
            }
        }

        // Сохранение базы данных в бинарный файл
        public static void SaveTasks(List<Task> tasks)
        {
            try
            {
                using (var stream = new FileStream(DataFile, FileMode.Create, FileAccess.Write))
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, tasks);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении файла: {ex.Message}");
            }
        }
#pragma warning restore SYSLIB0011 // Восстанавливаем предупреждение

        // Проверка существования файла
        public static bool FileExists()
        {
            return File.Exists(DataFile);
        }

        // Получение пути к файлу
        public static string GetFilePath()
        {
            return Path.GetFullPath(DataFile);
        }

        // Получение размера файла в байтах
        public static long GetFileSize()
        {
            if (!FileExists())
            {
                return 0;
            }

            return new FileInfo(DataFile).Length;
        }
    }
}