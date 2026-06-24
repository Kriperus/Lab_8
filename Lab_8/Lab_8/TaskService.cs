using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskScheduler
{
    // Вспомогательный класс для работы с базой данных задач
    public static class TaskService
    {
        // Просмотр базы данных
        public static void ViewAllTasks(List<Task> tasks)
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("База данных пуста.");
                return;
            }

            Console.WriteLine("\n=== СПИСОК ВСЕХ ЗАДАЧ ===");
            foreach (var task in tasks)
            {
                Console.WriteLine(task);
            }
            Console.WriteLine($"\nВсего задач: {tasks.Count}");
        }

        // Удаление элементов (по ключу - ID)
        public static bool DeleteTaskById(List<Task> tasks, int id)
        {
            var taskToRemove = tasks.FirstOrDefault(t => t.Id == id);
            if (taskToRemove == null)
            {
                return false;
            }

            tasks.Remove(taskToRemove);
            return true;
        }

        // Добавление элементов
        public static void AddTask(List<Task> tasks, Task newTask)
        {
            // Автоматическая генерация ID
            int maxId = tasks.Count > 0 ? tasks.Max(t => t.Id) : 0;
            newTask.Id = maxId + 1;
            tasks.Add(newTask);
        }

        // Поиск задачи по ID
        public static Task FindTaskById(List<Task> tasks, int id)
        {
            return tasks.FirstOrDefault(t => t.Id == id);
        }

        // Проверка существования задачи с указанным ID
        public static bool TaskExists(List<Task> tasks, int id)
        {
            return tasks.Any(t => t.Id == id);
        }

        // Получение следующего ID для новой задачи
        public static int GetNextId(List<Task> tasks)
        {
            return tasks.Count > 0 ? tasks.Max(t => t.Id) + 1 : 1;
        }

        // Запрос 1: Возвращает перечень задач с высоким приоритетом, срок которых истекает в ближайшие 3 дня
        public static List<Task> GetHighPriorityUrgentTasks(List<Task> tasks)
        {
            DateTime now = DateTime.Now;
            DateTime threeDaysLater = now.AddDays(3);

            return tasks
                .Where(t => t.Priority == 3 && t.DueDate >= now && t.DueDate <= threeDaysLater && !t.IsCompleted)
                .OrderBy(t => t.DueDate)
                .ToList();
        }

        // Запрос 2: Возвращает перечень выполненных задач, отсортированных по убыванию даты выполнения (сроку)
        public static List<Task> GetCompletedTasksSorted(List<Task> tasks)
        {
            return tasks
                .Where(t => t.IsCompleted)
                .OrderByDescending(t => t.DueDate)
                .ToList();
        }

        // Запрос 3: Возвращает одно значение - количество задач с низким приоритетом
        public static int GetLowPriorityTaskCount(List<Task> tasks)
        {
            return tasks.Count(t => t.Priority == 1 && !t.IsCompleted);
        }

        // Запрос 4: Возвращает одно значение - средний приоритет всех задач (округлённый до целого)
        public static double GetAveragePriority(List<Task> tasks)
        {
            if (tasks.Count == 0)
            {
                return 0;
            }

            return tasks.Average(t => t.Priority);
        }

        // Дополнительный запрос: задачи с просроченным сроком
        public static List<Task> GetOverdueTasks(List<Task> tasks)
        {
            return tasks
                .Where(t => t.DueDate < DateTime.Now && !t.IsCompleted)
                .OrderBy(t => t.DueDate)
                .ToList();
        }

        // Дополнительный запрос: статистика по приоритетам
        public static Dictionary<int, int> GetPriorityStatistics(List<Task> tasks)
        {
            return tasks
                .GroupBy(t => t.Priority)
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}