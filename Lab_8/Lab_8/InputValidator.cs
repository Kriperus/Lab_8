using System;

namespace TaskScheduler
{
    // Класс для валидации вводимых данных
    public static class InputValidator
    {
        public static int GetValidInt(string prompt, int min = int.MinValue, int max = int.MaxValue)
        {
            int result = 0; // Инициализируем значением по умолчанию
            bool isValid;

            do
            {
                Console.Write(prompt);
                string input = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Ошибка: значение не может быть пустым.");
                    isValid = false;
                    continue;
                }

                if (!int.TryParse(input, out result))
                {
                    Console.WriteLine("Ошибка: введите целое число.");
                    isValid = false;
                    continue;
                }

                if (result < min || result > max)
                {
                    Console.WriteLine($"Ошибка: число должно быть в диапазоне от {min} до {max}.");
                    isValid = false;
                    continue;
                }

                isValid = true;
            } while (!isValid);

            return result;
        }

        public static string GetValidString(string prompt, bool allowEmpty = false)
        {
            string result = string.Empty; // Инициализируем значением по умолчанию

            do
            {
                Console.Write(prompt);
                result = Console.ReadLine()?.Trim() ?? string.Empty;

                if (!allowEmpty && string.IsNullOrWhiteSpace(result))
                {
                    Console.WriteLine("Ошибка: поле не может быть пустым.");
                    continue;
                }

                break;
            } while (true);

            return result;
        }

        public static DateTime GetValidDateTime(string prompt)
        {
            DateTime result = DateTime.Now; // Инициализируем значением по умолчанию
            bool isValid;

            do
            {
                Console.Write(prompt);
                string input = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Ошибка: значение не может быть пустым.");
                    isValid = false;
                    continue;
                }

                if (!DateTime.TryParse(input, out result))
                {
                    Console.WriteLine("Ошибка: введите корректную дату и время (например, 25.12.2026 15:30).");
                    isValid = false;
                    continue;
                }

                if (result < DateTime.Now)
                {
                    Console.WriteLine("Ошибка: дата не может быть в прошлом.");
                    isValid = false;
                    continue;
                }

                isValid = true;
            } while (!isValid);

            return result;
        }

        public static bool GetValidBool(string prompt)
        {
            int result = GetValidInt(prompt + " (0-нет, 1-да): ", 0, 1);
            return result == 1;
        }
    }
}