using Microsoft.Data.Sqlite;

namespace TodoList
{
    public class UserDataMapper
    {
        public static void Save(User user)
        {
            // save new user here
            using(var connection = new SqliteConnection(Configuration.CONNECTION_STRING))
            {
                connection.Open();

                using(var command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = "INSERT INTO [users] (user_id, user_name) VALUES (@ID, @name)";
                    command.Parameters.AddWithValue("@ID",user.ID);
                    command.Parameters.AddWithValue("@name", user.NAME);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void Delete(int id)
        {
            // delete new user here
            using(var connection = new SqliteConnection(Configuration.CONNECTION_STRING))
            {
                connection.Open();

                using(var command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = "DELETE FROM [users] WHERE [user_id] = @ID";
                    command.Parameters.AddWithValue("@ID", id);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static User GetById(int id)
        {
            return null;
        }
    }
}