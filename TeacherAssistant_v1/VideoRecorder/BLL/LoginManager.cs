using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VideoRecorder.Model;
using VideoRecorder.Repository;

namespace VideoRecorder.BLL
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
