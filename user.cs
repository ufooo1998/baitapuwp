using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo2
{
    class user
    {
        private string _username;
        private string _email;
        private string _phone;

        public string username { get => _username; set => _username = value; }
        public string email { get => _email; set => _email = value; }
        public string phone { get => _phone; set => _phone = value; }
    }
}
