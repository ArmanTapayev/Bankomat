using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bankomat.DAL.Model;

namespace Bankomat.DAL
{
    public class Bank : Account
    {
        Account[] accounts;

        public Bank()
        {

        }
        public Bank(decimal sum) : base(sum) { }

        public void OpenAccount()
        {
            Console.Write("Укажите сумму для создания счета:>$ ");
            decimal sum = Convert.ToDecimal(Console.ReadLine());
            if (accounts == null)
            {
                accounts = new Account[] { new Account(sum) };
            }
            else
            {
                Account[] tempAccount = new Account[accounts.Length + 1];
                for (int i = 0; i < accounts.Length; i++)
                {
                    tempAccount[i] = accounts[i];
                }
                tempAccount[tempAccount.Length - 1] = new Account(sum);
                accounts = tempAccount;
            }

        }
        public void PutAccount(decimal sum, int id)
        {
            Account account = FindAccount(id);
            if (account == null)
                throw new Exception("Счет не найден");
            account.Add(sum);

        }

        public void WithdrawAccount(decimal sum, int id)
        {
            Account account = FindAccount(id);
            if (account == null)
                throw new Exception("Счет не найден");
            account.Withdraw(sum);
        }

        public Account FindAccount(int id)
        {
            for (int i = 0; i < accounts.Length; i++)
            {
                if (accounts[i].Id == id)
                {
                    return accounts[i];
                }
            }
            return null;
        }
    }
}
