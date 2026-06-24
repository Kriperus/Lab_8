using System;
using System.Collections.Generic;

namespace TaskScheduler
{
    // Класс для отображения меню и работы с пользователем
    public class TaskMenu
    {
        private List<Task> tasks;

        public TaskMenu(List<Task> tasks)
        {
            this.tasks = tasks;
        }

        public void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║           ПЛАНИРОВЩИК ЗАДАЧ - ГЛАВНОЕ МЕНЮ             ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════════╣");
            Console.WriteLine("║  1. Просмотр всех задач                                 ║");
            Console.WriteLine("║  2. Добавить новую задачу                               ║");
            Console.WriteLine("║  3. Удалить задачу по ID                                ║");
            Console.WriteLine("║  4. [Запрос] Задачи с высоким приоритетом (срок ≤ 3 дн.)║");
            Console.WriteLine("║  5. [Запрос] Выполненные задачи (по убыванию срока)     ║");
            Console.WriteLine("║  6. [Запрос] Количество задач с низким приоритетом      ║");
            Console.WriteLine("║  7. [Запрос] Средний приоритет всех задач               ║");
            Console.WriteLine("║  8. [Доп.] Просроченные задачи                          ║");
            Console.WriteLine("║  9. [Доп.] Статистика по приоритетам                    ║");
            Console.WriteLine("║  0. Выход и сохранение                                  ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
        }

        public int GetUserChoice()
        {
            return InputValidator.GetValidInt("Ваш выбор: ", 0, 9);
        }

        public void ProcessChoice(int choice)
        {
            switch (choice)
            {
                case 1:
                    ViewAllTasks();
                    break;
                case 2:
                    AddNewTask();
                    break;
                case 3:
                    DeleteTask();
                    break;
                case 4:
                    ShowHighPriorityUrgent();
                    break;
                case 5:
                    ShowCompletedTasks();
                    break;
                case 6:
                    ShowLowPriorityCount();
                    break;
                case 7:
                    ShowAveragePriority();
                    break;
                case 8:
                    ShowOverdueTasks();
                    break;
                case 9:
                    ShowPriorityStatistics();
                    break;
            }
        }

        private void ViewAllTasks()
        {
            Console.Clear();
            TaskService.ViewAllTasks(tasks);
        }

        private void AddNewTask()
        {
            Console.Clear();
            Console.WriteLine("=== ДОБАВЛЕНИЕ НОВОЙ ЗАДАЧИ ===");

            string title = InputValidator.GetValidString("Введите название задачи: ");
            string description = InputValidator.GetValidString("Введите описание задачи: ");
            DateTime dueDate = InputValidator.GetValidDateTime("Введите срок выполнения (дд.мм.гггг чч:мм): ");
            int priority = InputValidator.GetValidInt("Выберите приоритет (1-низкий, 2-средний, 3-высокий): ", 1, 3);
            bool isCompleted = InputValidator.GetValidBool("Задача выполнена?");

            Task newTask = new Task(0, title, description, dueDate, priority, isCompleted);
            TaskService.AddTask(tasks, newTask);

            Console.WriteLine($"Задача успешно добавлена с ID: {newTask.Id}");
        }

        private void DeleteTask()
        {
            Console.Clear();
            Console.WriteLine("=== УДАЛЕНИЕ ЗАДАЧИ ===");

            if (tasks.Count == 0)
            {
                Console.WriteLine("База данных пуста. Нет задач для удаления.");
                return;
            }

            TaskService.ViewAllTasks(tasks);
            int id = InputValidator.GetValidInt("Введите ID задачи для удаления: ", 1);

            bool deleted = TaskService.DeleteTaskById(tasks, id);
            if (deleted)
            {
                Console.WriteLine($"Задача с ID {id} успешно удалена.");
            }
            else
            {
                Console.WriteLine($"Задача с ID {id} не найдена.");
            }
        }

        private void ShowHighPriorityUrgent()
        {
            Console.Clear();
            Console.WriteLine("=== ЗАДАЧИ С ВЫСОКИМ ПРИОРИТЕТОМ (СРОК ≤ 3 ДНЕЙ) ===");

            var urgentTasks = TaskService.GetHighPriorityUrgentTasks(tasks);

            if (urgentTasks.Count == 0)
            {
                Console.WriteLine("Нет таких задач.");
            }
            else
            {
                foreach (var task in urgentTasks)
                {
                    Console.WriteLine(task);
                }
                Console.WriteLine($"\nНайдено задач: {urgentTasks.Count}");
            }
        }

        private void ShowCompletedTasks()
        {
            Console.Clear();
            Console.WriteLine("=== ВЫПОЛНЕННЫЕ ЗАДАЧИ (ПО УБЫВАНИЮ СРОКА) ===");

            var completedTasks = TaskService.GetCompletedTasksSorted(tasks);

            if (completedTasks.Count == 0)
            {
                Console.WriteLine("Нет выполненных задач.");
            }
            else
            {
                foreach (var task in completedTasks)
                {
                    Console.WriteLine(task);
                }
                Console.WriteLine($"\nНайдено задач: {completedTasks.Count}");
            }
        }

        private void ShowLowPriorityCount()
        {
            Console.Clear();
            Console.WriteLine("=== КОЛИЧЕСТВО ЗАДАЧ С НИЗКИМ ПРИОРИТЕТОМ ===");

            int count = TaskService.GetLowPriorityTaskCount(tasks);
            Console.WriteLine($"Количество невыполненных задач с низким приоритетом: {count}");
        }

        private void ShowAveragePriority()
        {
            Console.Clear();
            Console.WriteLine("=== СРЕДНИЙ ПРИОРИТЕТ ВСЕХ ЗАДАЧ ===");

            double average = TaskService.GetAveragePriority(tasks);
            if (tasks.Count == 0)
            {
                Console.WriteLine("База данных пуста.");
            }
            else
            {
                Console.WriteLine($"Средний приоритет всех задач: {average:F2}");
                Console.WriteLine($"(1 - низкий, 2 - средний, 3 - высокий)");
            }
        }

        private void ShowOverdueTasks()
        {
            Console.Clear();
            Console.WriteLine("=== ПРОСРОЧЕННЫЕ ЗАДАЧИ ===");

            var overdueTasks = TaskService.GetOverdueTasks(tasks);

            if (overdueTasks.Count == 0)
            {
                Console.WriteLine("Нет просроченных задач.");
            }
            else
            {
                foreach (var task in overdueTasks)
                {
                    Console.WriteLine(task);
                }
                Console.WriteLine($"\nНайдено задач: {overdueTasks.Count}");
            }
        }

        private void ShowPriorityStatistics()
        {
            Console.Clear();
            Console.WriteLine("=== СТАТИСТИКА ПО ПРИОРИТЕТАМ ===");

            if (tasks.Count == 0)
            {
                Console.WriteLine("База данных пуста.");
                return;
            }

            var stats = TaskService.GetPriorityStatistics(tasks);

            Console.WriteLine($"Всего задач: {tasks.Count}");
            Console.WriteLine("\nРаспределение по приоритетам:");

            foreach (var item in stats.OrderBy(x => x.Key))
            {
                string priorityName = item.Key switch
                {
                    1 => "Низкий",
                    2 => "Средний",
                    3 => "Высокий",
                    _ => "Неизвестно"
                };
                double percentage = (double)item.Value / tasks.Count * 100;
                Console.WriteLine($"  {priorityName,-8}: {item.Value,-5} задач ({percentage:F1}%)");
            }
        }

        public void WaitForUser()
        {
            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }
}