using DiegoMoyanoProject.Models;
using Microsoft.Data.Sqlite;

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
        public bool UpdatePdf(PdfData pdf, DateTime date)
        {
            var queryString = "UPDATE UserPdf SET pdf = @pdf WHERE  date = @date";
            bool inserted = false;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@pdf", pdf.Pdf));
                command.Parameters.Add(new SqliteParameter("@date", date.ToShortDateString()));
                connection.Open();
                inserted = command.ExecuteNonQuery() > 0;
                connection.Close();
            }
            return inserted;
        }

        public PdfData? GetPdfData( DateTime date)
        {
            string queryString = "SELECT pdf from UserPdf WHERE date = @date Limit 1";
            PdfData? pdf = null;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@date", date.ToShortDateString()));
                connection.Open();
                var reader = command.ExecuteScalar();
                if (reader != null && reader!=DBNull.Value) pdf = new PdfData((byte[])reader);
                connection.Close();
            }
            return pdf;
        }
        public byte[]? GetPdf(DateTime date)
        {
            string queryString = "SELECT pdf from UserPdf WHERE date = @date";
            byte[]? pdf = null;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@date", date.ToShortDateString()));
                connection.Open();
                var reader = command.ExecuteScalar();
                if (reader != null && reader!=DBNull.Value) pdf = ((byte[])reader);
                connection.Close();
            }
            return pdf;
        }
        
        
        public bool DeletePdf(DateTime date)
        {
            string queryString = "UPDATE UserPdf SET pdf = NULL WHERE  date = @date";
            var deleted = false;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@date", date.ToShortDateString()));
                connection.Open();
                deleted = command.ExecuteNonQuery() > 0;
                connection.Close();
            }
            return deleted;
        }

        public List<DateTime> GetAllDates()
        {
            string queryString = "SELECT DISTINCT date from UserPdf"; ;
            List<DateTime> dates = new List<DateTime>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DateTime date = DateTime.Parse(reader[0].ToString());
                        dates.Add(date);
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
                string queryString = "DELETE FROM UserPdf WHERE date = (SELECT min(date) FROM UserPdf)";
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
    }
}

