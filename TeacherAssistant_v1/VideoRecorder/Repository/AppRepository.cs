using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VideoRecorder.Model;
using System.Data.SqlClient;

namespace VideoRecorder.Repository
{
    public class AppRepository
    {
        string connectionString = @"Server=DESKTOP-LS8TDQK\SQLEXPRESS; Database=TeacherAssistant_v1; Integrated Security=True";

        public bool LoginValidation(LoginModel loginModel)
        {
            bool isExist = false;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                string command = @"Select * from LoginTable Where userID='" + loginModel.UserID + "' and password='" + loginModel.Password + "'";
                SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                SqlDataReader dataReader;
                sqlConnection.Open();
                dataReader = sqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    isExist = true;
                }
                sqlConnection.Close();
            }
            catch
            {
                sqlConnection.Close();
            }
            return isExist;
        }
    }
}
