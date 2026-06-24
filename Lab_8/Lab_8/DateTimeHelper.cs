using System;

namespace TaskScheduler
{
    // Вспомогательный класс для работы с датами
    public static class DateTimeHelper
    {
        // Проверка, является ли дата сегодняшней
        public static bool IsToday(DateTime date)
        {
            return date.Date == DateTime.Today;
        }

        // Проверка, является ли дата завтрашней
        public static bool IsTomorrow(DateTime date)
        {
            return date.Date == DateTime.Today.AddDays(1);
        }

        // Получение количества дней до указанной даты
        public static int DaysUntil(DateTime date)
        {
            return (int)(date.Date - DateTime.Today).TotalDays;
        }

        // Форматирование даты для отображения
        public static string FormatDateTime(DateTime date)
        {
            return date.ToString("dd.MM.yyyy HH:mm");
        }

        // Проверка, просрочена ли дата
        public static bool IsOverdue(DateTime date)
        {
            return date < DateTime.Now;
        }

        // Получение человеко-читаемого описания срока
        public static string GetDueDateDescription(DateTime dueDate)
        {
            if (IsOverdue(dueDate))
            {
                return "ПРОСРОЧЕНА!";
            }

            int days = DaysUntil(dueDate);
            if (days == 0) return "Сегодня";
            if (days == 1) return "Завтра";
            if (days <= 7) return $"Через {days} дней";
            if (days <= 14) return "Через 2 недели";
            if (days <= 30) return "Через месяц";
            return $"Через {days} дней";
        }
    }
}