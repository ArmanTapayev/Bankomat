using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bankomat.DAL.Model
{
    public enum Currency { KZT = 398, USD = 840, RUB = 643, EUR = 978 };
    public class Account
    {
        private int id;
        static int counter = 0;
        private decimal sumAccount; // переменная для хранения суммы
        public Account()
        {

        }
        public Account(decimal sum)
        {
            CurrentSum = sum;
            Id = ++counter;
            Console.WriteLine("Открыт новый счет");
            Console.WriteLine("ID: {0}", Id);

        }
        public decimal CurrentSum
        {
            get { return sumAccount; }
            private set
            {
                if (value > 0)
                    sumAccount += value;
                else
                    throw new Exception("Введите корректную сумму");
            }
        }
        public int Id
        {
            get
            {
                return id;
            }
            set { id = value; }
        }

        public void Open()
        {
            Console.Write("Укажите сумму для создания счета:> ");
            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Account account = new Account(sum);

        }
        public decimal Withdraw(decimal sum)
        {
            decimal result = 0;
            if (sum <= sumAccount)
            {
                sumAccount -= sum;
                result = sum;
                Console.WriteLine("Со счета снята сумма: ${0}", sum);
            }
            else
            {
                Console.WriteLine("Недостаточно денег на счете");
            }
            return result;
        }
        public void Add(decimal sum)
        {
            sumAccount += sum;
            Console.WriteLine("На счет поступило: ${0}", sum);
        }

    }
}
