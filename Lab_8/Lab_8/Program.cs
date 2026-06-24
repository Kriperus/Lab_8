using System;
using System.Collections.Generic;
using System.Text;

namespace TaskScheduler
{
    // Основной класс программы
    public class Program
    {
        private static List<Task> tasks;
        private static TaskMenu menu;

        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Title = "Планировщик задач";

            // Инициализация
            InitializeApplication();

            bool exit = false;
            while (!exit)
            {
                try
                {
                    menu.DisplayMainMenu();
                    int choice = menu.GetUserChoice();

                    if (choice == 0)
                    {
                        exit = true;
                        SaveAndExit();
                    }
                    else
                    {
                        menu.ProcessChoice(choice);
                        menu.WaitForUser();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Произошла ошибка: {ex.Message}");
                    menu.WaitForUser();
                }
            }
        }

        private static void InitializeApplication()
        {
            // Загрузка данных из файла
            tasks = TaskFileManager.LoadTasks();

            // Создание меню
            menu = new TaskMenu(tasks);

            // Вывод информации о загрузке
            Console.WriteLine($"Загружено задач: {tasks.Count}");
            Console.WriteLine($"Файл данных: {TaskFileManager.GetFilePath()}");
            Console.WriteLine();
        }

        private static void SaveAndExit()
        {
            TaskFileManager.SaveTasks(tasks);
            Console.WriteLine($"Сохранено задач: {tasks.Count}");
            Console.WriteLine("Данные сохранены. До свидания!");
        }
    }
}