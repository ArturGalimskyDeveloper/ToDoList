using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TodoList
{
    public class UserDataMapper
    {
        public static bool Save(User user)
        {
            bool result = false;
            using(var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TodoListDbConnection"].ConnectionString))
            {
                connection.Open();

                var sql = @"INSERT INTO dbo.users(user_id, user_name) 
                            SELECT @param1, @param2 WHERE NOT EXISTS(SELECT * FROM dbo.users WHERE user_id=@param1)";
                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@param1", SqlDbType.Int).Value = user.ID;
                    command.Parameters.Add("@param2", SqlDbType.VarChar, 30).Value = user.NAME;
                    command.CommandType = CommandType.Text;
                    result = command.ExecuteNonQuery() == 1 ? true : false;
                }
                connection.Close();
            }
            return result;
        }

        public static bool Delete(int id)
        {
            bool result = false;
            using(var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TodoListDbConnection"].ConnectionString))
            {
                connection.Open();

                var sql = "DELETE FROM dbo.Users WHERE user_id=@ID";
                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.CommandType = CommandType.Text;

                    result = command.ExecuteNonQuery() == 1 ? true : false;
                }
                connection.Close();
            }
            return result;
        }

        public static User GetById(int id)
        {
            return null;
        }
    }
}