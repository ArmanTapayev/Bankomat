using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bankomat.DAL.Model;

namespace Bankomat.DAL
{
    public class ServiceUser
    {
        public static Random randomBank = new Random();

        public List<User> users = new List<User>();

        public bool CreateUser(User user, out string message)
        {
            if (user != null)
            {
                users.Add(user);
                message = "User and Accout are created";
                return true;
            }
            else
            {
                message = "Error";
                throw new Exception("404");
            }
        }

        public User LogOn(string Log, string Pass)
        {
            return users.FirstOrDefault(f => f.Password == Pass && f.Login == Log);
        }
    }
}

