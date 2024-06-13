using DiegoMoyanoProject.Exceptions;
using DiegoMoyanoProject.Models;
using Microsoft.Data.Sqlite;
using SQLitePCL;
using System.Globalization;
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
            string queryString = "INSERT INTO Images(dateUploaded) VALUES(@date)";
            bool inserted = false;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@date", date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)));
                connection.Open();
                inserted = command.ExecuteNonQuery() > 0;
                connection.Close();
            }
            if (!inserted) throw new NotImplementedException("Error al insertar un nuevo registro en imagenes");
            return inserted;
        }

        public bool Delete(DateTime date, ImageType type, int id)
        {
            string queryString = "UPDATE Images SET " + type.ToString() + " = null WHERE dateUploaded = @date and id = @id";
            bool updated = false;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@date", date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)));
                command.Parameters.Add(new SqliteParameter("id", id));
                connection.Open();
                updated = command.ExecuteNonQuery() > 0;
                connection.Close();
            }
            return updated;
        }

        public ImageFile? getImage(DateTime date, ImageType type, int id)
        {
            string queryString = "SELECT " + type.ToString() + " FROM Images WHERE dateUploaded like @date and id = @id";
            ImageFile? image = null;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@date", date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)));
                command.Parameters.Add(new SqliteParameter("@id", id));
                connection.Open();
                var reader = command.ExecuteScalar();
                if (reader != null) image = new ImageFile(reader.ToString(), type, id);
                connection.Close();
            }
            return image;
        }

        public bool Update(ImageFile img, DateTime date, int id)
        {
            string queryString = "UPDATE Images SET " + img.ImageType.ToString() + " = @newImg WHERE dateUploaded = @date and id = @id";
            bool updated = false;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@newImg", img.Path));
                command.Parameters.Add(new SqliteParameter("@date", date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)));
                command.Parameters.Add(new SqliteParameter("@id", id));
                connection.Open();
                updated = command.ExecuteNonQuery() > 0;
                connection.Close();
            }
            return updated;
        }
        public bool deleteOlderIfNeeded(int maxSupported = 3)
        {
            bool deleted = false;
            if (this.countImagesAdded() >= maxSupported)
            {
                string queryString = "DELETE FROM Images WHERE dateUploaded = (SELECT min(dateUploaded) from Images) and id = (select min(id) from Images)";
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
        public List<FileDate> GetAllDatesAndId()
        {
            string queryString = "SELECT dateUploaded, id from Images"; ;
            List<FileDate> dates = new List<FileDate>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DateTime date = DateTime.Parse(reader["dateUploaded"].ToString());
                        int id = Convert.ToInt32(reader["id"]);
                        dates.Add(new FileDate(id, date));
                    }
                }
                connection.Close();
            }
            return dates;
        }
        public int? GetMaxId()
        {
            string queryString = "SELECT max(id) FROM Images";
            int? maxId = null;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                connection.Open();
                var reader = command.ExecuteScalar();
                if (reader != null && reader != "") maxId = Convert.ToInt32(reader);
                connection.Close();
            }
            return maxId;
        }
        public bool DeleteRow(int id)
        {
            string queryString = "DELETE FROM Images WHERE id = @id";
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
