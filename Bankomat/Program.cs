using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bankomat.DAL.Model;
using Bankomat.DAL;
using GeneratorName;

namespace Bankomat
{

    class Program
    {
        static Random random = new Random();
        public static User RandomUser()
        {
            User user = new User();
            user.UserId = random.Next(10, 999);
            user.Login = "Sergio";
            user.Password = random.Next(1000, 9999).ToString();
            user.DoB = new DateTime(1900 + random.Next(45, 98), 01 + random.Next(0, 11), 01 + random.Next(0, 27));
            user.IIN = (user.DoB.Year - 1900).ToString() + (user.DoB.Month).ToString() + (user.DoB.Day).ToString() + random.Next(100, 999).ToString() + random.Next(100, 999).ToString();
            Generator generator = new Generator();
            string nameTime = generator.GenerateDefault(GeneratorName.Gender.man);
            nameTime = nameTime.Replace("<center><b><font size=7>", "")
                .Replace("</font></b></center>", "")
                .Replace("\n", "");
            string[] str = nameTime.Split(' ');
            user.FirstName = str[0];
            user.LastName = str[1];
            return user;
        }

        public static Bank RandomBank()
        {
            int sum = random.Next(1000, 9999);
            Bank bank = new Bank(sum);
            return bank;
        }

        static ServiceUser serviceUser = new ServiceUser();

        ///////////////////////////////////////////////////////////////////////////

        static void Main(string[] args)
        {
            string message;

            #region  Create Users
            User user = new User();
            user.UserId = 1;
            user.Login = "admin";
            user.Password = "admin";
            user.DoB = new DateTime(1900, 01, 5);
            user.IIN = "110890302518";
            user.LastName = "Admin";
            user.FirstName = "Admin";

            serviceUser.CreateUser(user, out message);

            MainMenu();

        }

        ///////////////////////////////////////////////////////////////////////////
        // Главное меню
        public static void MainMenu()
        {
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("\t\tБАНК \"SOMEBANK\"");
            Console.WriteLine(new string('-', 50));
            Console.ForegroundColor = color;
            ConsoleColor color1 = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("\t\tВыберите пункт меню:");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("1.Регистрация\n2.Вход\n0.Выход");
            Console.ForegroundColor = color1;
            int ch = 0;
            Int32.TryParse(Console.ReadLine(), out ch);
            switch (ch)
            {
                case 1:
                    {
                        string message;
                        for (int i = 0; i < 10; i++)
                        {
                            try
                            {
                                serviceUser.CreateUser(RandomUser(), out message);
                            }
                            catch (Exception ex) { }

                        }
                        #endregion

                        for (int i = 0; i < 10; i++)
                        {
                            try
                            {
                                serviceUser.CreateUser(RandomUser(), out message);
                            }
                            catch (Exception) { }

                        }
                        foreach (var item in serviceUser.users)
                        {
                            item.PrintInfo();

                        }
                        Menu();
                    }
                    break;
                case 2:
                    {
                        int count = 3;
                        bool flag = true;
                        Console.Write("Введите логин: ");
                        string Login = Console.ReadLine();

                        while (flag)
                        {
                            Console.Write("Введите пароль: ");
                            string Pass = Console.ReadLine();
                            User user = serviceUser.LogOn(Login, Pass);

                            if (user == null)
                            {
                                ConsoleColor color2 = Console.ForegroundColor;
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.WriteLine("Login Error");
                                count--;

                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Осталось {0} попыток: ", count);
                                if (count == 0)
                                {
                                    Console.WriteLine("Вы исчерпали количество допустимых попыток");
                                    break;
                                }
                                Console.ForegroundColor = color;
                            }
                            else
                            {
                                Console.WriteLine("Hello {0} {1}.",
                                    user.LastName, user.FirstName[0]);
                                Console.Clear();

                                Menu();
                                flag = false;
                            }

                        }

                    }
                    break;
                case 0:
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Спасибо за выбор нашего банка!");
                        Console.ForegroundColor = color;
                    }
                    break;
                default:
                    Console.WriteLine("Повторите ввод");
                    break;
            }
        }

        // метод Меню
        public static void Menu()
        {
            Bank bank = new Bank();
            bool inProgress = true;
            while (inProgress)
            {
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(new string('-', 50));
                Console.WriteLine("\t\tВыберите пункт меню:");
                Console.WriteLine(new string('-', 50));
                Console.WriteLine("1. Открыть счет");
                Console.WriteLine("2. Показать баланс счета");
                Console.WriteLine("3. Пополнить баланс");
                Console.WriteLine("4. Снять деньги со счета");
                Console.WriteLine("5. Выход");

                Console.ForegroundColor = color;
                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        {
                            Console.WriteLine(new string('-', 50));
                            Console.WriteLine("\t\t1. Открыть счет");
                            Console.WriteLine(new string('-', 50));
                            bank.OpenAccount();
                            Console.WriteLine(new string('-', 50));
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine(new string('-', 50));
                            Console.WriteLine("\t\t2. Показать баланс счета");
                            Console.WriteLine(new string('-', 50));
                            bool flag = true;
                            int id;
                            decimal sum = 0;
                            while (flag)
                            {
                                Console.Write("Введите ID счета:> ");
                                id = Convert.ToInt32(Console.ReadLine());
                                if (bank.FindAccount(id) != null)
                                {
                                    sum = bank.FindAccount(id).CurrentSum;
                                    flag = false;
                                }
                                else
                                {
                                    Console.WriteLine("Счета с ID {0} не существует", id);
                                }
                            }
                            Console.WriteLine("Текущий баланс: ${0}", sum);
                            Console.WriteLine(new string('-', 50));
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine(new string('-', 50));
                            Console.WriteLine("\t\t3. Пополнить баланс");
                            Console.WriteLine(new string('-', 50));

                            bool flag = true;
                            int id = 0;

                            while (flag)
                            {
                                Console.Write("Введите ID счета:> ");
                                id = Convert.ToInt32(Console.ReadLine());
                                if (bank.FindAccount(id) != null)
                                {
                                    flag = false;
                                }
                                else
                                {
                                    Console.WriteLine("Счета с ID {0} не существует", id);
                                }
                            }

                            Console.Write("Введите сумму:>$ ");
                            int sum = Convert.ToInt32(Console.ReadLine());
                            bank.PutAccount(sum, id);
                            Console.WriteLine(new string('-', 50));
                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine(new string('-', 50));
                            Console.WriteLine("\t\t4. Снять деньги со счета");
                            Console.WriteLine(new string('-', 50));
                            bool flag = true;
                            int id = 0;

                            while (flag)
                            {
                                Console.Write("Введите ID счета:> ");
                                id = Convert.ToInt32(Console.ReadLine());
                                if (bank.FindAccount(id) != null)
                                {
                                    flag = false;
                                }
                                else
                                {
                                    Console.WriteLine("Счета с ID {0} не существует", id);
                                }
                            }

                            Console.Write("Введите сумму:>$ ");
                            int sum = Convert.ToInt32(Console.ReadLine());
                            bank.WithdrawAccount(sum, id);
                            Console.WriteLine(new string('-', 50));
                            break;
                        }
                    case 5:
                        {
                            Console.WriteLine(new string('-', 50));
                            Console.WriteLine("\t\t5. Выход");
                            Console.WriteLine(new string('-', 50));
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("Спасибо за выбор нашего банка!");
                            Console.ForegroundColor = color;
                            inProgress = false;
                            Console.WriteLine(new string('-', 50));
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Выберите пункт меню");
                        }
                        break;
                }
            }
        }
    }
}



