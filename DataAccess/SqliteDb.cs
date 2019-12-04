using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Models;
using Windows.Storage;

namespace Weather.DataAccess
{
    class SqliteDb
    {
        public const string DB_FILENAME = "weather.db";

        public async static void InitializeDatabase()
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync(DB_FILENAME, CreationCollisionOption.OpenIfExists);
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, DB_FILENAME);
            using (SqliteConnection conn = new SqliteConnection($"Filename={dbpath}"))
            {
                conn.Open();

                var sql = "CREATE TABLE IF NOT EXISTS User " +
                    "(Id INTEGER PRIMARY KEY" +
                    "Name NVARCHAR(100), " +
                    "Default_Zip INTEGER, " +
                    "Measurment_System INTEGER," +
                    "Font_Id INTEGER)";

                var cmd = new SqliteCommand(sql, conn);
                cmd.ExecuteReader();

                sql = "CREATE TABLE IF NOT EXISTS Locations " +
                    "(Zip INTEGER PRIMARY KEY, " +
                    "City NVARCHAR(100), " +
                    "State NVARCHAR(100)";

                cmd = new SqliteCommand(sql, conn);
                cmd.ExecuteReader();
            }
        }

        public static void AddUser()
        {
            User user = new User();
            user.DefaultZip = 72143;
            user.Name = "";
            user.MeasurmentSystem = 0;
            user.FontId = 0;

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, DB_FILENAME);
            using (SqliteConnection conn = new SqliteConnection($"Filename={dbpath}"))
            {
                conn.Open();

                SqliteCommand cmd = new SqliteCommand();
                cmd.Connection = conn;

                cmd.CommandText = "INSERT INTO User VALUES(@Id, @Name, @DefaultZip, @MeasurementSystem, @FontId)";
                //cmd.Parameters.AddWithValue("@ID", user.Id);
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@DefaultZip", user.DefaultZip);
                cmd.Parameters.AddWithValue("@MeasurementSystem", user.MeasurmentSystem);
                cmd.Parameters.AddWithValue("@FontId", user.FontId);

                cmd.ExecuteReader();

                conn.Close();
            }
        }

        public static void AddLocation(Location location)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, DB_FILENAME);
            using (SqliteConnection conn = new SqliteConnection($"Filename={dbpath}"))
            {
                conn.Open();

                SqliteCommand cmd = new SqliteCommand();
                cmd.Connection = conn;

                cmd.CommandText = "INSERT INTO Locations VALUES (@Zip, @City, @State); SELECT last_insert_rowid()";
                cmd.Parameters.AddWithValue("@Zip", location.Zip);
                cmd.Parameters.AddWithValue("@City", location.City);
                cmd.Parameters.AddWithValue("@State", location.State);

                // Get ID that was automatically assigned
                var newId = (long)cmd.ExecuteScalar();

                conn.Close();
            }
        }

        public static List<Location> GetAllLocations()
        {
            var locations = new List<Location>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, DB_FILENAME);
            using (SqliteConnection conn = new SqliteConnection($"Filename={dbpath}"))
            {
                conn.Open();

                SqliteCommand cmd = new SqliteCommand("SELECT Zip, City, State FROM Location", conn);
                SqliteDataReader query = cmd.ExecuteReader();
                while (query.Read())
                {
                    locations.Add(new Location
                    {
                        Zip = query.GetInt32(0),
                        City = query.GetString(1),
                        State = query.GetString(2)
                    });
                }
                conn.Close();
            }

            return locations;
        }

        public static void DeleteLocation(int zip)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, DB_FILENAME);
            using (SqliteConnection conn = new SqliteConnection($"Filename={dbpath}"))
            {
                conn.Open();

                SqliteCommand cmd = new SqliteCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM Location WHERE Zip = @Zip";
                cmd.Parameters.AddWithValue("@Zip", zip);
                cmd.ExecuteReader();

                conn.Close();
            }
        }

        public static void UpdateUser(User user)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, DB_FILENAME);
            using (SqliteConnection conn = new SqliteConnection($"Filename={dbpath}"))
            {
                conn.Open();

                SqliteCommand cmd = new SqliteCommand();
                cmd.Connection = conn;

                cmd.CommandText = "UPDATE User SET Name = @Name, Default_Zip = @DefaultZip, " +
                    "Measurement_System = @MeasurementSystem, Font_Id = @FontId WHERE Id = 0";
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@DefaultZip", user.DefaultZip);
                cmd.Parameters.AddWithValue("@MeasurementSystem", user.MeasurmentSystem);
                cmd.Parameters.AddWithValue("@FontId", user.FontId);

                cmd.ExecuteReader();

                conn.Close();
            }
        }
    }
}
