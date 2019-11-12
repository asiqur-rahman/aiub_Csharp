using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeacherAssistant_v1.Model
{
    public class LoginModel
    {
        public string Username { set; get; }
        public string Password { set; get; }
        public string UserID { set; get; }

        public LoginModel()
        {
            this.Username = "";
            this.Password = "";
            this.UserID = "";
        }

    }
}
