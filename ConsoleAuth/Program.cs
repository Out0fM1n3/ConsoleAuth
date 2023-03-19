using System;
using System.Linq;
using System.Threading;

namespace ConsoleAuth
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Объявление двумерного массива для хранения данных пользователей
            string[,] Data = new string[100, 2];
            // Объявление переменной для отслеживания количества попыток входа
            int Count = 3;

            // Начало бесконечного цикла
            while (true)
            {
                // Вывод меню на экран
                Console.WriteLine("Выберите действие: ");
                Console.WriteLine("1. Регистрация");
                Console.WriteLine("2. Вход");
                Console.WriteLine("3. Выход");

                // Считывание выбора пользователя
                string choice = Console.ReadLine();
                int numChoice;

                // Проверка корректности ввода пользователя с помощью TryParse
                if (int.TryParse(choice, out numChoice))
                {
                    // Если пользователь выбрал "1", вызываем метод Register и передаем ему массив Data в качестве аргумента
                    if (numChoice == 1)
                    {
                        //Вызов метода ConsoleClear
                        ConsoleClear();
                        Register(Data);
                    }
                    // Если пользователь выбрал "2", вызываем метод Auth и передаем ему массив Data и переменную Count в качестве аргументов
                    else if (numChoice == 2)
                    {
                        //Вызов метода ConsoleClear
                        ConsoleClear();
                        Auth(Data, ref Count);
                    }
                    // Если пользователь выбрал "3", прерываем цикл с помощью оператора break и завершаем выполнение программы
                    else if (numChoice == 3)
                    {
                        break;
                    }
                }
                else
                {
                    // Если пользователь ввел некорректное значение, выводим сообщение об ошибке и продолжаем выполнение цикла с начала
                    Console.WriteLine("Неверный ввод. Пожалуйста, введите число от 1 до 3.\n");
                }
            }
        }

        static void Register(string[,] Data)
        {
            // Начало бесконечного цикла
            while (true)
            {
                // Вывод сообщения о переходе к регистрации
                Console.WriteLine("\nРегистрация:");

                // Вывод сообщения и считывание имени пользователя
                Console.Write("\nВведите имя пользователя или 'Назад' для возврата: ");
                string login = Console.ReadLine();

                // Если пользователь ввел "Назад", прерываем цикл с помощью оператора break и завершаем выполнение метода
                if (login == "Назад")
                {
                    break;
                }

                // Объявление переменной для отслеживания наличия похожего логина в массиве Data
                bool similarLoginExists = false;

                // Перебор всех элементов первого измерения массива Data
                for (int i = 0; i < Data.GetLength(0); i++)
                {
                    // Если текущий элемент не равен null и содержит введенный пользователем логин
                    if (Data[i, 0] != null && Data[i, 0].Contains(login))
                    {
                        // Устанавливаем значение переменной similarLoginExists в true и прерываем цикл for с помощью оператора break
                        similarLoginExists = true;
                        break;
                    }
                }

                // Если значение переменной similarLoginExists равно true
                if (similarLoginExists)
                {
                    // Выводим сообщение об ошибке и продолжаем выполнение цикла while с начала с помощью оператора continue
                    Console.WriteLine("Ошибка регистрации. Похожий логин уже существует.");
                    continue;
                }

                // Вывод сообщения и считывание пароля пользователя
                Console.Write("Введите пароль: ");
                string password = Console.ReadLine();

                // Перебор всех элементов первого измерения массива Data
                for (int i = 0; i < Data.GetLength(0); i++)
                {
                    // Если текущий элемент равен null
                    if (Data[i, 0] == null)
                    {
                        // Сохраняем данные пользователя в массиве Data и выводим сообщение об успешной регистрации 
                        Data[i, 0] = login;
                        Data[i, 1] = password;
                        Console.WriteLine("Регистрация прошла успешно!");
                        Console.WriteLine($"\nВаш логин: {Data[0, 0]} Ваш пароль: {Data[0, 1]}\n");
                        return;
                    }
                }
                // Если все места в массиве заняты, выводим сообщение об ошибке 
                Console.WriteLine("Ошибка регистрации. Нет свободных мест.");
            }
        }

        static void Auth(string[,] Data, ref int Count)
        {
            // Начало бесконечного цикла
            while (true)
            {
                // Вывод сообщения о переходе к авторизации
                Console.WriteLine("\nАвторизация:");

                // Вывод сообщения и считывание имени пользователя
                Console.Write("\nВведите имя пользователя или 'Назад' для возврата: ");
                string login = Console.ReadLine();

                // Если пользователь ввел "Назад", прерываем цикл с помощью оператора break и завершаем выполнение метода
                if (login == "Назад")
                {
                    break;
                }

                // Вывод сообщения и считывание пароля пользователя
                Console.Write("\nВведите пароль: ");
                string password = Console.ReadLine();

                // Перебор всех элементов первого измерения массива Data
                for (var i = 0; i < Data.GetLength(0); i++)
                {
                    // Если логин и пароль пользователя соответствуют данным в массиве Data
                    if (login == Data[i, 0] && password == Data[i, 1])
                    {
                        // Выводим сообщение об успешной авторизации и завершаем выполнение метода с помощью оператора return
                        Console.WriteLine("Авторизация прошла успешно!");
                        return;
                    }
                }

                // Уменьшаем значение переменной Count на единицу 
                Count--;
                // Если значение переменной Count не равно нулю 
                if (Count != 0)
                {
                    //Вызов метода ConsoleClear
                    ConsoleClear();

                    // Выводим сообщение об ошибке и количестве оставшихся попыток 
                    Console.Write($"\nВы ввели неверные данные попробуйте ещё раз! Кол-во оставшихся попыток {Count}");
                }
                else
                {
                    // Если значение переменной Count равно нулю, выводим сообщение о достижении максимального количества попыток и вызываем метод DelayCaptha 
                    Console.WriteLine("Вы достигли максимального кол-ва попыток пожалуйста подождите 15 секунд, после чего разгадайте капчу.");
                    DelayCaptha();
                }
            }
        }

        static int DelayCaptha()
        {
            // Объявление и инициализация массива символов алфавита
            char[] alphabet = new char[]
            {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I',
            'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R',
            'S', 'T', 'U','V','W','X','Y','Z',
            'a','b','c','d','e','f','g','h','i',
            'm', 'n', 'o','p', 'q', 'r', 's', 't',
            'u', 'v', 'w', 'x', 'y', 'z'
            };

            // Объявление массива символов для хранения капчи
            char[] Captha = new char[5];

            // Создание объекта класса Random для генерации случайных чисел 
            Random rnd = new Random();

            // Ожидание 15 секунд 
            Thread.Sleep(15000);

            //Вызов метода ConsoleClear
            ConsoleClear();

            // Генерация капчи из 5 символов алфавита 
            for (var i = 0; i < 5; i++)
            {
                Captha[i] = alphabet[rnd.Next(alphabet.Length)];
            }

            // Вывод сообщения и капчи 
            Console.Write("\nРешите капчу: ");

            foreach (var element in Captha)
            {
                Console.Write($"{element}");
            }

            // Считывание ответа пользователя 
            Console.Write("\nОтвет: ");
            string Answ = Console.ReadLine();

            // Если ответ пользователя соответствует сгенерированной капче 
            if (Answ.SequenceEqual(Captha))
            {
                // Выводим сообщение об успешном решении капчи 
                Console.WriteLine("Всё верно, можете снова войти в систему!");
            }
            else
            {
                // Если ответ неверный, выводим сообщение об ошибке и вызываем метод DelayCaptha повторно 
                Console.WriteLine("Неверно! Подождите ещё 15 секунд.");
                DelayCaptha();
            }
            //Вызов метода ConsoleClear
            ConsoleClear();

            return 1;
        }
        
        static void ConsoleClear()
        {
            Console.Clear();
        }
    }
}
