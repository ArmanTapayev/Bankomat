using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Bankomat.DAL;


namespace Bankomat.DAL.Model
{
    public enum Gender { man, girl };
    public class User
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime DoB { get; set; }
        public string IIN { get; set; }
        public Gender Sex { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        List<Account> Accounts = new List<Account>();

        List<Bank> banks = new List<Bank>();

        public int GetAge()
        {
            return (int)(DateTime.Now - DoB).Days / 365;
        }

        private int age;
        public int Age
        {
            get { return age; }
            set
            {
                age = (int)(DateTime.Now.Year - DoB.Date.Year);
            }
        }
        public void PrintInfo()
        {
            Console.WriteLine(new string('-', 50));
            string str = string.Format("ID:{0} ФИО: {1} {2}., Возраст: {3}\n" +
                "Login: {4}, Password: {5}\n" +
                "Дата рождения: {6:dd/MM/yyyy}\nIIN: {7}, пол: {8} ",
                UserId, LastName, FirstName[0], GetAge(), Login, Password, DoB, IIN, Sex);
            Console.WriteLine(str);
            Console.WriteLine(new string('-', 50));
        }
    }
}

