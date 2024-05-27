using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace WpfAccess185_2.Models
{
    public class User
    {
        public string Username { set; get; }
        public string Password { set; get; }
        public string UserRole { set; get; }
        public User(string u, string p, string r)
        {
            Username = u;
            Password = p;
            UserRole = r;
        }
    }
}
