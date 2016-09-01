using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IBatis.Model
{
    public class LoginInfo
    {
        public string username { get; set; }

        public string password { get; set; }

        public string cbox { get; set; }
        public bool Errormsg { get; set; }
    }
}
