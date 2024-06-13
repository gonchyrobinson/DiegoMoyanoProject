using DiegoMoyanoProject.Models;
using Microsoft.Data.Sqlite;
using System.Data;

namespace DiegoMoyanoProject.Repository
{
    public class UserPdfRepository : IUserPdfRepository
    {
        private readonly string _connectionString;

        public UserPdfRepository(string connectionString)
        {
            _connectionString = connectionString;
        }


        public bool AddDate(DateTime date)
        {
            this.deleteOlderIfNeeded();
            var queryString = "INSERT INTO UserPdf (date) VALUES (@date)";
            bool inserted = false;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@date", date.ToShortDateString()));;
                connection.Open();
                inserted = command.ExecuteNonQuery() > 0;
                connection.Close();
            }
            if (!inserted) throw (new NotImplementedException("Error cargando los datos"));
            return inserted;
        }
        public bool UpdatePdf(PdfData pdf, int id)
        {
            var queryString = "UPDATE UserPdf SET pdf = @pdf WHERE  id =@id";
            bool inserted = false;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@pdf", pdf.Pdf));
                command.Parameters.Add(new SqliteParameter("@id", id));
                connection.Open();
                inserted = command.ExecuteNonQuery() > 0;
                connection.Close();
            }
            return inserted;
        }

        public PdfData? GetPdfData( int id)
        {
            string queryString = "SELECT pdf,id from UserPdf WHERE id = @id";
            PdfData? pdf = null;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@id", id));
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader != null && reader.Read() && !reader.IsDBNull("pdf")) pdf = new PdfData((byte[])reader["pdf"], Convert.ToInt32(reader["id"]));
                connection.Close();
            }
            return pdf;
        }
        public byte[]? GetPdf(int id)
        {
            string queryString = "SELECT pdf from UserPdf WHERE id = @id";
            byte[]? pdf = null;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@id", id));
                connection.Open();
                var reader = command.ExecuteScalar();
                if (reader != null && reader!=DBNull.Value) pdf = ((byte[])reader);
                connection.Close();
            }
            return pdf;
        }
        
        
        public bool DeletePdf(int id)
        {
            string queryString = "UPDATE UserPdf SET pdf = NULL WHERE  id = @id";
            var deleted = false;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@id", id));
                connection.Open();
                deleted = command.ExecuteNonQuery() > 0;
                connection.Close();
            }
            return deleted;
        }

        public List<FileDate> GetAllDates()
        {
            string queryString = "SELECT date, id from UserPdf"; ;
            List<FileDate> dates = new List<FileDate>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["id"]);
                        DateTime date = DateTime.Parse(reader["date"].ToString());
                        dates.Add(new FileDate(id, date));
                    }
                }
                connection.Close();
            }
            return dates;
        }

        public bool deleteOlderIfNeeded(int maxSupported = 3)
        {
            bool deleted = false;
            if (this.countImagesAdded() >= maxSupported)
            {
                string queryString = "DELETE FROM UserPdf WHERE id = (select min(id) from UserPdf)";
                using (var connection = new SqliteConnection(_connectionString))
                {
                    var command = new SqliteCommand(queryString, connection);
                    connection.Open();
                    deleted = command.ExecuteNonQuery() > 0;
                    connection.Close();
                }
                if (!deleted) throw new NotImplementedException("Error al insertar un nuevo registro en imagenes, error al borrar el registro mas viejo cuando tengo 4 registros o mas");
            }
            return deleted;
        }
        public int countImagesAdded()
        {
            string queryString = "SELECT count(id) FROM UserPdf";
            int total = 0;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                connection.Open();
                var reader = command.ExecuteScalar();
                if (reader != null) total = Convert.ToInt32(reader);
                connection.Close();
            }
            return total;
        }
        public int GetMaxId()
        {
            string queryString = "SELECT max(id) FROM UserPdf";
            //Not exists
            int id = -1;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                connection.Open();
                var reader = command.ExecuteScalar();
                if (reader != null) id = Convert.ToInt32(reader);
                connection.Close();
            }
            return id;
        }
        public bool DeleteRow(int id)
        {
            string queryString = "DELETE FROM UserPdf WHERE id = @id";
            bool deleted = false;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@id", id));
                connection.Open();
                deleted = command.ExecuteNonQuery() > 0;
                connection.Close();
            }
            if (!deleted) throw new NotImplementedException("Error al insertar un nuevo registro en imagenes");
            return deleted;
        }
    }
}

