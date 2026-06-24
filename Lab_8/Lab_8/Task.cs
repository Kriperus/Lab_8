using System;
using System.Runtime.Serialization;

namespace TaskScheduler
{
    [Serializable] // Важно для бинарной сериализации
    public class Task
    {
        // Свойства
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int Priority { get; set; } // 1 - низкий, 2 - средний, 3 - высокий
        public bool IsCompleted { get; set; }

        // Конструкторы
        public Task()
        {
            Id = 0;
            Title = string.Empty;
            Description = string.Empty;
            DueDate = DateTime.Now;
            Priority = 1;
            IsCompleted = false;
        }

        public Task(int id, string title, string description, DateTime dueDate, int priority, bool isCompleted)
        {
            Id = id;
            Title = title;
            Description = description;
            DueDate = dueDate;
            Priority = priority;
            IsCompleted = isCompleted;
        }

        // Перегруженный метод ToString()
        public override string ToString()
        {
            string priorityText = Priority switch
            {
                1 => "Низкий",
                2 => "Средний",
                3 => "Высокий",
                _ => "Неизвестно"
            };

            string status = IsCompleted ? "Выполнена" : "Не выполнена";

            return $"ID: {Id,-5} | Название: {Title,-20} | Описание: {Description,-30} | " +
                   $"Срок: {DueDate:dd.MM.yyyy HH:mm,-20} | Приоритет: {priorityText,-8} | Статус: {status}";
        }
    }
}