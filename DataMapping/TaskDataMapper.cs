using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TodoList
{
    public class TaskDataMapper
    {
        private static string CreateUserTasksTableQuery = @"
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
        ";

        public static List<string> GetAll(int user_id)
        {
            List<string> tasks = new List<string>();

            using(var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TodoListDbConnection"].ConnectionString))
            {
                connection.Open();

                var sql = CreateUserTasksTableQuery + "SELECT indx, task_text, task_id FROM #user_tasks;";
                using(var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            tasks.Add(reader[0].ToString() + ") " + reader[1].ToString() + " " + reader[2].ToString());
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

        public static bool Delete(int user_id, int taskIndex)
        {
            bool result = false;
            using(var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TodoListDbConnection"].ConnectionString))
            {
                connection.Open();

                var sql = CreateUserTasksTableQuery + @"
                    DELETE 
                        t1
                    FROM 
                        dbo.Tasks t1
                    INNER JOIN 
                        #user_tasks t2 ON t1.task_id=t2.task_id
                    WHERE
                        t2.indx = @indx;
                ";
                using(var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@indx", taskIndex);
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                    result = command.ExecuteNonQuery() == 1 ? true : false;
                }
                connection.Close();
            }
            return result;
        }
    }
}