using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TeacherAssistant_v1.BLL;
using TeacherAssistant_v1.Model;
using TeacherAssistant_v1.UI;

namespace TeacherAssistant_v1.UI
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        LoginManager loginManager = new LoginManager();
        LoginModel loginModel;


        public Login()
        {
            InitializeComponent();
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            userIDMsg.Text = "";
            passwordMsg.Text = "";
            msg.Text = "";

            if (userID.Text=="")
            {
               userIDMsg.Text = "User ID Must Not be Blanked";
            }
            if(password.Password == "")
            {
                passwordMsg.Text = "Password Must Not be Blanked";
            }
            else
            {
                loginModel = new LoginModel();
                loginModel.UserID = userID.Text;
                loginModel.Password = password.Password;

                if(loginManager.LoginVaidation(loginModel))
                {
                    msg.Foreground = Brushes.Green;
                    msg.Text = "Successfully Verified";
                    TakeAttendance takeAttendance = new TakeAttendance();
                    takeAttendance.Show();
                    this.Hide();
                }
                else
                {
                    msg.Text = "User Not Found";
                }
                
            }
        }
    }
}
