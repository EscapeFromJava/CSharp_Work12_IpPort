using System;
using System.IO;

namespace CSharp_Work12_IpPort
{
    class Program
    {
        static void Main()
        {
            Console.Write("Выберите метод ввода данных, где [1] - из файла, [2] - из консоли: ");
            
            try
            {
                int temp = Convert.ToInt32(Console.ReadLine());
                if (temp == 1 || temp == 2)
                {
                    try
                    {
                        MySettings s = null;

                        if (temp == 1)
                            s = new MySettings("file.txt");

                        if (temp == 2)
                        {
                            Console.WriteLine("Введите ip и port через \":\"");
                            s = new MySettings(Console.ReadLine());
                        }

                        Console.WriteLine("ip = {0}, port = {1}", s.Ip, s.Port);
                        string result = "ip = " + s.Ip + "\nport = " + s.Port;
                        File.WriteAllText(@"result.txt", result);
                        Console.WriteLine("Информация сохранена в файл успешно!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Некорретный ввод! Значение должно быть '1' или '2'");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Исключение: " + e.Message);
            }

            Console.ReadKey();
        }
    }

    class MySettings
    {
        private readonly string _ip;
        private readonly int _port;

        public string Ip => _ip;
        public int Port => _port;

        public MySettings(string filename)
        {
            string s;
            try
            {
                if (filename == "file.txt")
                    s = File.ReadAllText(filename);
                else
                    s = filename;
            }
            catch (Exception e)
            {
                throw new MemberAccessException("Ошибка чтения файла! " + e.Message);
            }

            string[] ar = s.Split(':');
            if (ar.Length != 2)
                throw new MeSettingsException("Неверный формат!");
            _ip = ar[0];
            if (Convert.ToInt32(ar[1]) < 0 || Convert.ToInt32(ar[1]) > 65535)
                throw new MeSettingsException("Некорректное значение порта! (диапазон от 0 до 65535)");

            string[] ar2 = ar[0].Split('.');
                if (Convert.ToInt32(ar2[0]) < 1)
                throw new MeSettingsException(ar2[0] + " не корректное значение (диапазон от 1 до 255)");
                else if (ar2.Length != 4)
                        throw new MeSettingsException("Введено некорректное значение ip! Должно быть 4 числа!");
            foreach (var el in ar2)
            {
                if (!int.TryParse(el, out _))
                    throw new MeSettingsException(el + " не является числом!");
                else if (Convert.ToInt32(el) < 0 || Convert.ToInt32(el) > 255)
                    throw new MeSettingsException(el + " не корректное значение (диапазон от 1 до 255)");
            }

            try
            {
                if (Convert.ToInt32(ar[1]) != 0)
                    _port = int.Parse(ar[1]);
                else
                    throw new MeSettingsException("Порт не может 0!");
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
                throw new MeSettingsException("Неверный формат ввода!");
            }
        }
    }

    class MeSettingsException : Exception
    {
        public MeSettingsException(string message)
            : base(message)
        {

        }

        MeSettingsException(string message, Exception exception)
            : base(message, exception)
        {

        }
    }
}