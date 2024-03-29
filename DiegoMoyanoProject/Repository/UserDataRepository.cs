using DiegoMoyanoProject.Models;
using Microsoft.Data.Sqlite;

namespace DiegoMoyanoProject.Repository
{
    public class UserDataRepository : IUserDataRepository
    {
        private readonly string _connectionString;

        public UserDataRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string? GetImagePath(int userId, ImageType type)
        {
            string queryString = "SELECT imagePath from UserImage WHERE userId=@userId AND type =@type";
            string? path = null;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@userId", userId));
                command.Parameters.Add(new SqliteParameter("@type", type));
                connection.Open();
                var reader = command.ExecuteScalar();
                if (reader != null) path = reader.ToString();
                connection.Close();
            }
            return path;
        }

        public bool UploadImage(ImageData image)
        {
            var queryString = "INSERT INTO UserImage (userId, imagePath, type) VALUES (@userId, @imagePath, @type)";
            bool inserted = false;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@userId", image.UserId));
                command.Parameters.Add(new SqliteParameter("@imagePath", image.Path));
                command.Parameters.Add(new SqliteParameter("@type", image.ImageType));
                connection.Open();
                inserted = command.ExecuteNonQuery() > 0;
                connection.Close();
            }
            if (!inserted) throw (new NotImplementedException("Error cargando los datos"));
            return inserted;
        }
        public ImageData? GetImage(int id, ImageType type)
        {
            string queryString = "SELECT imagePath from UserImage WHERE userId=@userId AND type =@type";
            ImageData? img = null;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@userId", id));
                command.Parameters.Add(new SqliteParameter("@type", type));
                connection.Open();
                var reader = command.ExecuteScalar();
                if (reader != null) img = new ImageData(id, reader.ToString(), type);
                connection.Close();
            }
            return img;
        }

        public List<ImageData> GetUserImages(int userId)
        {
            string queryString = "SELECT * from UserImage WHERE userId=@userId";
            List<ImageData> images = new List<ImageData>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@userId", userId));
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["userId"]);
                        string path = reader["imagePath"].ToString();
                        ImageType type = (ImageType)Convert.ToInt32(reader["type"]);
                        ImageData img = new ImageData(id, path, type);
                        images.Add(img);
                    }
                }
                connection.Close();
            }
            return images;
        }

        public bool ExistsImage(int userId, ImageType type)
        {
            string queryString = "SELECT id from UserImage WHERE userId=@userId AND type =@type";
            var exists = false;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@userId", userId));
                command.Parameters.Add(new SqliteParameter("@type", type));
                connection.Open();
                var reader = command.ExecuteScalar();
                exists = reader != null;
                connection.Close();
            }
            return exists;
        }
        public bool DeleteImage(int userId, ImageType type)
        {
            string queryString = "DELETE from UserImage WHERE userId=@userId AND type =@type";
            var deleted = false;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@userId", userId));
                command.Parameters.Add(new SqliteParameter("@type", type));
                connection.Open();
                deleted = command.ExecuteNonQuery()>0;
                connection.Close();
            }
            return deleted;
        }

    }

}

