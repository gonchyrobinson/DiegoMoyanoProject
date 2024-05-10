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

        public byte[]? GetPdf(int order)
        {
            string queryString = "SELECT pdf from UserPdf WHERE `order` =@order";
            byte[]? path = null;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@order", order));
                connection.Open();
                var reader = command.ExecuteScalar();
                if (reader != null) path = (byte[])reader;
                connection.Close();
            }
            return path;
        } 
        public byte[]? GetPdf(DateTime date)
        {
            string queryString = "SELECT pdf from UserPdf WHERE date =@date";
            byte[]? path = null;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@date", date.ToString("yyyy-MM-dd")));
                connection.Open();
                var reader = command.ExecuteScalar();
                if (reader != null) path = (byte[])reader;
                connection.Close();
            }
            return path;
        }

        public bool UploadPdf(PdfData pdf)
        {
            var queryString = "INSERT INTO UserPdf (pdf, date, `order`) VALUES (@pdf, @date, @order)";
            bool inserted = false;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@pdf", pdf.Pdf));
                command.Parameters.Add(new SqliteParameter("@date", DateTime.Today.ToString("yyyy-MM-dd")));
                command.Parameters.Add(new SqliteParameter("@order", pdf.Order));
                connection.Open();
                inserted = command.ExecuteNonQuery() > 0;
                connection.Close();
            }
            if (!inserted) throw (new NotImplementedException("Error cargando los datos"));
            return inserted;
        }
        public bool UpdatePdf(PdfData pdf, int order)
        {
            var queryString = "UPDATE UserPdf SET pdf = @pdf WHERE  `order` = @order";
            bool inserted = false;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@pdf", pdf.Pdf));
                command.Parameters.Add(new SqliteParameter("@order", order));
                connection.Open();
                inserted = command.ExecuteNonQuery() > 0;
                connection.Close();
            }
            if (!inserted) throw (new NotImplementedException("Error cargando los datos"));
            return inserted;
        }

        public PdfData? GetPdfData( int order)
        {
            string queryString = "SELECT pdf from UserPdf WHERE `order` = @order";
            PdfData? pdf = null;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@order", order));
                connection.Open();
                var reader = command.ExecuteScalar();
                if (reader != null) pdf = new PdfData((byte[])reader, order);
                connection.Close();
            }
            return pdf;
        }

        
        
        public bool DeletePdf(DateTime date)
        {
            string queryString = "DELETE from UserPdf WHERE  date = @date";
            var deleted = false;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@date", date));
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
        public bool AddOrder()
        {
            var queryString = "UPDATE UserPdf SET `order` = `order`+1";
            bool updated = false;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                connection.Open();
                updated = command.ExecuteNonQuery() > 0;
                connection.Close();
            }
            return updated;
        } public bool ReduceOrder()
        {
            var queryString = "UPDATE UserPdf SET `order` = `order`-1 WHERE  `order` > 1";
            bool updated = false;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                connection.Open();
                updated = command.ExecuteNonQuery() > 0;
                connection.Close();
            }
            return updated;
        }
        public bool DeletePdf(int order)
        {
            string queryString = "DELETE from UserPdf WHERE `order` = @order";
            var deleted = false;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@order", order));
                connection.Open();
                deleted = command.ExecuteNonQuery() > 0;
                connection.Close();
            }
            return deleted;
        }
        public bool DeletePdfWithOrderMoreThan3(int order = 3)
        {
            string queryString = "DELETE from UserPdf WHERE `order` > @order";
            var deleted = false;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@order", order));
                connection.Open();
                deleted = command.ExecuteNonQuery() > 0;
                connection.Close();
            }
            return deleted;
        }
    }
}

