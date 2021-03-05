using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TodoList
{
    public class TaskDataMapper
    {
        public static List<string> GetAll(int user_id)
        {
            List<string> tasks = new List<string>();

            using(var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TodoListDbConnection"].ConnectionString))
            {
                connection.Open();

                var sql = @"
                    CREATE TABLE #user_tasks
                    (
                        task_id INT NOT NULL,
                        task_text VARCHAR(30) NOT NULL,
                        indx INT NOT NULL IDENTITY(1,1)
                    );

                    INSERT INTO 
                        #user_tasks
                    SELECT
                        task_id, 
                        task_text
                    FROM
                        dbo.Tasks
                    WHERE
                        user_id = @user_id;

                    SELECT indx, task_text FROM #user_tasks;
                ";
                using(var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            tasks.Add(reader[0].ToString() + ") " + reader[1].ToString());
                        }
                    }

                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return tasks;
        }

        public static void Save(string text, int user_id)
        {
            using(var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TodoListDbConnection"].ConnectionString))
            {
                connection.Open();

                var sql = "INSERT INTO dbo.tasks(task_text, user_id) VALUES(@task_text, @user_id)";
                using(var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@task_text", SqlDbType.VarChar, 30).Value = text;
                    command.Parameters.Add("@user_id", SqlDbType.Int).Value = user_id;
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void Delete(TodoTask task)
        {
            using(var connection = new SqliteConnection(Configuration.CONNECTION_STRING))
            {
                connection.Open();

                using(var command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = "DELETE FROM [Customer] WHERE [ID] = @ID";
                    command.Parameters.AddWithValue("@ID", task.ID);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}