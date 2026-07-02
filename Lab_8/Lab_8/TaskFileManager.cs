using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TaskScheduler
{
    // Класс для работы с бинарным файлом (ручная сериализация)
    public static class TaskFileManager
    {
        private const string DataFile = "tasks.dat";

        // Сохранение базы данных в бинарный файл
        public static void SaveTasks(List<Task> tasks)
        {
            try
            {
                using (var stream = new FileStream(DataFile, FileMode.Create, FileAccess.Write))
                using (var writer = new BinaryWriter(stream, Encoding.UTF8))
                {
                    // Записываем количество задач
                    writer.Write(tasks.Count);

                    // Записываем каждую задачу
                    foreach (var task in tasks)
                    {
                        writer.Write(task.Id);
                        writer.Write(task.Title ?? string.Empty);
                        writer.Write(task.Description ?? string.Empty);
                        writer.Write(task.DueDate.ToBinary());
                        writer.Write(task.Priority);
                        writer.Write(task.IsCompleted);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении файла: {ex.Message}");
                throw;
            }
        }

        // Чтение базы данных из бинарного файла
        public static List<Task> LoadTasks()
        {
            try
            {
                if (!File.Exists(DataFile))
                {
                    return new List<Task>();
                }

                var tasks = new List<Task>();

                using (var stream = new FileStream(DataFile, FileMode.Open, FileAccess.Read))
                using (var reader = new BinaryReader(stream, Encoding.UTF8))
                {
                    // Читаем количество задач
                    int count = reader.ReadInt32();

                    for (int i = 0; i < count; i++)
                    {
                        var task = new Task
                        {
                            Id = reader.ReadInt32(),
                            Title = reader.ReadString(),
                            Description = reader.ReadString(),
                            DueDate = DateTime.FromBinary(reader.ReadInt64()),
                            Priority = reader.ReadInt32(),
                            IsCompleted = reader.ReadBoolean()
                        };
                        tasks.Add(task);
                    }
                }

                return tasks;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
                return new List<Task>();
            }
        }

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
