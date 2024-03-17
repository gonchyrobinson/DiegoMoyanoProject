using DiegoMoyanoProject.Exceptions;
using DiegoMoyanoProject.Models;
using Microsoft.Data.Sqlite;

namespace DiegoMoyanoProject.Repository
{
    public class LoginRepository:ILoginRepository
    {
        private readonly string _conectionString;

        public LoginRepository(string conectionString)
        {
            _conectionString = conectionString;
        }

        public User? Log(string mail, string pass)
        {
            var queryString = "SELECT * FROM user WHERE mail = @mail AND pass = @pass";
            User? logedUser = null;
            using (var connection = new SqliteConnection(_conectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@mail", mail));
                command.Parameters.Add(new SqliteParameter("@pass", pass));
                connection.Open();
                using(var reader  = command.ExecuteReader()) 
                { 
                    if(reader.Read())
                    {
                        int userId = Convert.ToInt32(reader["id"]);
                        string userMail = reader["mail"].ToString();
                        string userPass = reader["pass"].ToString();
                        string username = reader["username"].ToString();
                        Role userRole = (Role)Convert.ToInt32(reader["role"]);
                        logedUser = new User(userId, userMail, userPass, username,userRole);
                    }
                }
                connection.Close();
            }
            if (logedUser == null) throw (new PassAndMailNotMatch($"user-mail {mail} and pass {pass} doesn´t match"));
            return logedUser;
        }
    }
}

