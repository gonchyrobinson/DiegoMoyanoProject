using DiegoMoyanoProject.Exceptions;
using DiegoMoyanoProject.Models;
using Microsoft.Data.Sqlite;
namespace DiegoMoyanoProject.Repository
{
    public class ImagesRepository : IImagesRepository
    {
        private readonly string _connectionString;

        public ImagesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public bool AddDate(DateTime date)
        {
            this.deleteOlderIfNeeded();
            string queryString = "INSERT INTO Images(dateUploaded) VALUES(@date)";
            bool inserted = false;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@date", date.ToShortDateString()));
                connection.Open();
                inserted = command.ExecuteNonQuery() > 0;
                connection.Close();
            }
            if (!inserted) throw new NotImplementedException("Error al insertar un nuevo registro en imagenes");
            return inserted;
        }

        public bool Delete(DateTime date, ImageType type)
        {
            string queryString = "UPDATE Images SET "+type.ToString()+" = null WHERE dateUploaded = @date";
            bool updated = false;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@date", date.ToShortDateString()));
                connection.Open();
                updated = command.ExecuteNonQuery() > 0;
                connection.Close();
            }
            return updated;
        }

        public ImageFile? getImage(DateTime date, ImageType type)
        {
            string queryString = "SELECT " + type.ToString() + " FROM Images WHERE dateUploaded = @date";
            ImageFile? image = null;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@date", date.ToShortDateString()));
                connection.Open();
                var reader = command.ExecuteScalar();
                if (reader != null) image = new ImageFile(reader.ToString(), type);
                connection.Close();
            }
            return image;
        }

        public bool Update(ImageFile img, DateTime date)
        {
            string queryString = "UPDATE Images SET " + img.ImageType.ToString() + " = @newImg WHERE dateUploaded = @date";
            bool updated = false;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@newImg", img.Blob64img));
                command.Parameters.Add(new SqliteParameter("@date", date.ToShortDateString()));
                connection.Open();
                updated = command.ExecuteNonQuery() > 0;
                connection.Close();
            }
            return updated;
        }
        public bool deleteOlderIfNeeded(int maxSupported = 3)
        {
            bool deleted= false;
            if (this.countImagesAdded() >= maxSupported)
            {
                string queryString = "DELETE FROM Images WHERE dateUploaded = (SELECT min(dateUploaded) FROM Images)";
                using (var connection = new SqliteConnection(_connectionString))
                {
                    var command = new SqliteCommand(queryString, connection);
                    connection.Open();
                    deleted = command.ExecuteNonQuery() > 0;
                    connection.Close();
                }
                if (!deleted) throw new NotImplementedException("Error al insertar un nuevo registro en imagenes");
            }
            return deleted;
        }
        public int countImagesAdded()
        {
            string queryString = "SELECT count(id) FROM Images";
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
        public List<DateTime> GetAllDates()
        {
            string queryString = "SELECT DISTINCT dateUploaded from Images"; ;
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


    }
}
