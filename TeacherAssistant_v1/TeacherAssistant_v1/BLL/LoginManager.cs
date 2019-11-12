using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherAssistant_v1.Repository;
using TeacherAssistant_v1.Model;

namespace TeacherAssistant_v1.BLL
{
    public class LoginManager
    {
        AppRepository repository = new AppRepository(); 

        public bool LoginVaidation(LoginModel loginModel)
        {
            return repository.LoginValidation(loginModel);
        }

    }
}
