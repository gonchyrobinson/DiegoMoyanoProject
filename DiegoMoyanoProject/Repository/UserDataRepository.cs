using DiegoMoyanoProject.Models;
using Microsoft.Data.Sqlite;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DiegoMoyanoProject.Repository
{
    public class UserDataRepository : IUserDataRepository
    {
        private readonly string _connectionString;

        public UserDataRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string? GetImagePath(ImageType type)
        {
            string queryString = "SELECT imagePath from UserImage WHERE type =@type";
            string? path = null;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
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
            var queryString = "INSERT INTO UserImage (imagePath, type, date, `order`) VALUES (@imagePath, @type, @date, @order)";
            bool inserted = false;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@imagePath", image.Path));
                command.Parameters.Add(new SqliteParameter("@type", image.ImageType));
                command.Parameters.Add(new SqliteParameter("@date", DateTime.Today.ToString("yyyy-MM-dd")));
                command.Parameters.Add(new SqliteParameter("@order", image.Order));
                connection.Open();
                inserted = command.ExecuteNonQuery() > 0;
                connection.Close();
            }
            if (!inserted) throw (new NotImplementedException("Error cargando los datos"));
            return inserted;
        }
        public bool UpdateImage(ImageData image, int order)
        {
            var queryString = "UPDATE UserImage SET imagePath = @imagePath WHERE type = @type AND `order` = @order";
            bool inserted = false;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@order", order));
                command.Parameters.Add(new SqliteParameter("@imagePath", image.Path));
                command.Parameters.Add(new SqliteParameter("@type", image.ImageType));
                connection.Open();
                inserted = command.ExecuteNonQuery() > 0;
                connection.Close();
            }
            if (!inserted) throw (new NotImplementedException("Error cargando los datos"));
            return inserted;
        }

        public ImageData? GetImage(ImageType type, int order)
        {
            string queryString = "SELECT imagePath from UserImage WHERE type =@type AND `order` = @order";
            ImageData? img = null;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@type", type));
                command.Parameters.Add(new SqliteParameter("@order", order));
                connection.Open();
                var reader = command.ExecuteScalar();
                if (reader != null) img = new ImageData(reader.ToString(), type,order);
                connection.Close();
            }
            return img;
        }

        public List<ImageData> GetUserImages()
        {
            string queryString = "SELECT * from UserImage WHERE `order` = (SELECT MIN(`order`) FROM UserImage)";
            List<ImageData> images = new List<ImageData>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string path = reader["imagePath"].ToString();
                        ImageType type = (ImageType)Convert.ToInt32(reader["type"]);
                        int order = Convert.ToInt32(reader["order"]);
                        ImageData img = new ImageData(path, type,order);
                        images.Add(img);
                    }
                }
                connection.Close();
            }
            return images;
        }
        public List<ImageData> GetUserImages(DateTime date)
        {
            string queryString = "SELECT * from UserImage WHERE date = @date";
            List<ImageData> images = new List<ImageData>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@date", date.ToString("yyyy-MM-dd")));
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string path = reader["imagePath"].ToString();
                        ImageType type = (ImageType)Convert.ToInt32(reader["type"]);
                        int order = Convert.ToInt32(reader["order"]);
                        ImageData img = new ImageData(path, type, order);
                        images.Add(img);
                    }
                }
                connection.Close();
            }
            return images;
        }

        public bool ExistsImage( ImageType type, int order)
        {
            string queryString = "SELECT id from UserImage WHERE type =@type and `order` = @order";
            var exists = false;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@type", type));
                command.Parameters.Add(new SqliteParameter("@order", order));
                connection.Open();
                var reader = command.ExecuteScalar();
                exists = reader != null;
                connection.Close();
            }
            return exists;
        }
        public bool DeleteImage(ImageType type, DateTime date)
        {
            string queryString = "DELETE from UserImage WHERE type =@type AND date = @date";
            var deleted = false;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@type", type));
                command.Parameters.Add(new SqliteParameter("@date", date));
                connection.Open();
                deleted = command.ExecuteNonQuery() > 0;
                connection.Close();
            }
            return deleted;
        }

        public int CountImages()
        {
            string queryString = "SELECT MAX(`order`) FROM UserImage";
            int? order = null;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                connection.Open();
                var orderDB = command.ExecuteScalar();
                if (orderDB != null)
                {
                    order = Convert.ToInt32(orderDB);
                }
                connection.Close();
            }
            if (order == null) order = 0;
            return (int)order;
        }
        public List<DateTime> GetAllDates()
        {
            string queryString = "SELECT DISTINCT date from UserImage"; ;
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
        public int? MaxOrder(ImageType type)
        {
            string queryString = "SELECT MAX(`order`) FROM UserImage WHERE type = @type";
            int? order = null;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@type", type));
                connection.Open();
                var orderDB = command.ExecuteScalar();
                if (orderDB != null)
                {
                    order = Convert.ToInt32(orderDB);
                }
                connection.Close();
            }
      
            return order;
        }
        public bool AddOrder(ImageType type)
        {
            var queryString = "UPDATE UserImage SET `order` = `order`+1 WHERE type = @type AND `order` < 3";
            bool updated = false;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@type", type));
                connection.Open();
                updated = command.ExecuteNonQuery() > 0;
                connection.Close();
            }
            return updated;
        }
        public bool DeleteImage(ImageType type, int order)
        {
            string queryString = "DELETE from UserImage WHERE type =@type AND `order` = @order";
            var deleted = false;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@type", type));
                command.Parameters.Add(new SqliteParameter("@order", order));
                connection.Open();
                deleted = command.ExecuteNonQuery() > 0;
                connection.Close();
            }
            return deleted;
        }
    }
}

