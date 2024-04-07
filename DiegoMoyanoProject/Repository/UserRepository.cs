using DiegoMoyanoProject.Models;
using Microsoft.Data.Sqlite;

namespace DiegoMoyanoProject.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly string conectionString;
        public UserRepository(string ConectionString)
        {
            conectionString = ConectionString;
        }
        public List<User> ListUsers()
        {
            string query = $"SELECT * FROM User";
            var list = new List<User>();
            using (var conection = new SqliteConnection(conectionString))
            {
                conection.Open();
                var command = new SqliteCommand(query, conection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var Usu = new User();
                        Usu.Id = Convert.ToInt32(reader["id"]);
                        Usu.Username = reader["username"].ToString();
                        Usu.Role = (Role)Convert.ToInt32(reader["role"]);
                        Usu.Pass = reader["pass"].ToString();
                        Usu.Mail = reader["mail"].ToString();
                        if (!reader.IsDBNull(5))
                        {
                            Usu.CapitalInvested = Convert.ToDecimal(reader["capitalInvested"]);
                        }
                        if (!reader.IsDBNull(6))
                        {
                            Usu.Rentability = Convert.ToDecimal(reader["rentability"]);
                        }
                        list.Add(Usu);
                    }
                }
                conection.Close();
            }
            return list;
        }
        public List<User> ListOperativeUsers()
        {
            string query = $"SELECT * FROM User WHERE role = @operative";
            var list = new List<User>();
            using (var conection = new SqliteConnection(conectionString))
            {
                conection.Open();
                var command = new SqliteCommand(query, conection);
                command.Parameters.Add(new SqliteParameter("@operative", Role.Operative));
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var Usu = new User();
                        Usu.Id = Convert.ToInt32(reader["id"]);
                        Usu.Username = reader["username"].ToString();
                        Usu.Role = (Role)Convert.ToInt32(reader["role"]);
                        Usu.Pass = reader["pass"].ToString();
                        Usu.Mail = reader["mail"].ToString();
                        if (!reader.IsDBNull(5))
                        {
                            Usu.CapitalInvested = Convert.ToDecimal(reader["capitalInvested"]);
                        }
                        if (!reader.IsDBNull(6))
                        {
                            Usu.Rentability = Convert.ToDecimal(reader["rentability"]);
                        }
                        list.Add(Usu);
                    }
                }
                conection.Close();
            }
            return list;
        }
        public User? GetUserById(int id)
        {
            var query = $"SELECT * FROM User where id=@id";
            User? Usu = null;
            using (var conection = new SqliteConnection(conectionString))
            {
                var command = new SqliteCommand(query, conection);
                command.Parameters.Add(new SqliteParameter("@id", id));
                conection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Usu = new User();
                        Usu.Id = Convert.ToInt32(reader["id"]);
                        Usu.Username = reader["username"].ToString();
                        Usu.Role = (Role)Convert.ToInt32(reader["role"]);
                        Usu.Pass = reader["pass"].ToString();
                        Usu.Mail = reader["mail"].ToString();
                        if (!reader.IsDBNull(5))
                        {
                            Usu.CapitalInvested = Convert.ToDecimal(reader["capitalInvested"]);
                        }
                        if (!reader.IsDBNull(6))
                        {
                            Usu.Rentability = Convert.ToDecimal(reader["rentability"]);
                        }

                    }
                }
                conection.Close();
            }
            if (Usu == null)
            {
                throw (new Exception("No existe el usuario"));
            }
            return Usu;
        }
        public bool CreateUser(User usu)
        {
            var query = $"INSERT INTO User(username, role, pass, mail, rentability, capitalInvested) VALUES (@username, @role, @pass, @mail,0,0)";
            using (var conection = new SqliteConnection(conectionString))
            {
                var command = new SqliteCommand(query, conection);
                command.Parameters.Add(new SqliteParameter("@username", usu.Username));
                command.Parameters.Add(new SqliteParameter("@role", usu.Role));
                command.Parameters.Add(new SqliteParameter("@pass", usu.Pass));
                command.Parameters.Add(new SqliteParameter("@mail", usu.Mail));
                conection.Open();
                var anda = command.ExecuteNonQuery();
                conection.Close();
                if (anda > 0)
                {
                    return true;
                }
                else
                {
                    throw new Exception("Error al crear Usuario");
                }
            }
        }
        public bool UpdateUser(int id, User usu)
        {
            var query = $"update User SET username=@username, role=@role, pass=@pass, mail=@mail WHERE id=@id";
            using (var conection = new SqliteConnection(conectionString))
            {
                var command = new SqliteCommand(query, conection);
                command.Parameters.Add(new SqliteParameter("@id", id));
                command.Parameters.Add(new SqliteParameter("@username", usu.Username));
                command.Parameters.Add(new SqliteParameter("@role", usu.Role));
                command.Parameters.Add(new SqliteParameter("@pass", usu.Pass));
                command.Parameters.Add(new SqliteParameter("@mail", usu.Mail));

                conection.Open();
                var anda = command.ExecuteNonQuery();
                conection.Close();
                if (anda > 0)
                {
                    return true;
                }
                else
                {
                    throw new Exception("Error al modificar Usuario");
                }

            }
        }
        public bool DeleteUser(int id)
        {
            var query = $"DELETE FROM User WHERE id=@id";
            using (var conection = new SqliteConnection(conectionString))
            {
                var command = new SqliteCommand(query, conection);
                command.Parameters.Add(new SqliteParameter("@id", id));
                conection.Open();
                var anda = command.ExecuteNonQuery();
                conection.Close();
                if (anda > 0)
                {
                    return true;
                }
                else
                {
                    throw new Exception("Error al eliminar");
                }
            }
        }
        public string? GetMail(int? id)
        {
            var query = $"SELECT mail FROM User WHERE id=@id";
            using (var conection = new SqliteConnection(conectionString))
            {
                var command = new SqliteCommand(query, conection);
                command.Parameters.Add(new SqliteParameter("@id", id));
                conection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var Mail = reader["mail"].ToString();
                        return Mail;
                    }
                    else
                    {

                        return null;
                    }
                }
            }
        }
        public bool AddRentabilityandCapitalInvested(int id, User usu)
        {
            var query = $"update User SET capitalInvested=@capitalInvested, rentability = @rentability WHERE id=@id";
            using (var conection = new SqliteConnection(conectionString))
            {
                var command = new SqliteCommand(query, conection);
                command.Parameters.Add(new SqliteParameter("@id", id));
                command.Parameters.Add(new SqliteParameter("@capitalInvested", usu.CapitalInvested));
                command.Parameters.Add(new SqliteParameter("@rentability", usu.Rentability));
                conection.Open();
                var anda = command.ExecuteNonQuery();
                conection.Close();
                if (anda > 0)
                {
                    return true;
                }
                else
                {
                    throw new Exception("Error al modificar Usuario");
                }

            }
        }

    }
}

