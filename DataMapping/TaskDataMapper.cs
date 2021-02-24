using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace TodoList
{
    public class TaskDataMapper
    {
        public static TodoTask GetAllByUserId(int userid)
        {
            // using(var connection = new SqliteConnection(Configuration.CONNECTION_STRING))
            // {
            //     connection.Open();

            //     var command = connection.CreateCommand();
            //     command.CommandText = @" SELECT * FROM tasks WHERE user_id=$userid";
            //     command.Parameters.AddWithValue("$user_id", userid);

            //     using(var reader = command.ExecuteReader())
            //     {
            //         if(reader.HasRows)
            //         {
            //             reader.Read();


            //             string text = (string)reader["Text"];
            //             return new TodoTask(0, text, userid);
            //         }
            //     }
            // }

            return null;
        }

        public static List<string> GetAll(int user_id)
        {
            List<string> tasks = new List<string>();

            using(var connection = new SqliteConnection(Configuration.CONNECTION_STRING))
            {
                connection.Open();            

                using(var command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = @"
                    CREATE TEMPORARY TABLE user_tasks
                    (
                        task_id INTEGER NOT NULL,
                        text TEXT not NULL,
                        indx INTEGER PRIMARY KEY AUTOINCREMENT
                    );

                    INSERT INTO user_tasks (task_id, text)
                    SELECT task_id, TEXT
                    FROM   tasks
                    WHERE user_id = @user_id;

                    SELECT * FROM user_tasks;";  

                    command.Parameters.AddWithValue("@user_id", user_id);

                    using(SqliteDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            var sb = new System.Text.StringBuilder();
                            for(int i=0; i<reader.FieldCount; i++)
                            {
                                sb.Append(reader.GetString(i) + " ");
                            }
                            tasks.Add(sb.ToString());
                        }
                    }
                }
            }
            return tasks;
        }

        public static void Save(string text, int user_id)
        {
            using(var connection = new SqliteConnection(Configuration.CONNECTION_STRING))
            {
                connection.Open();

                using(var command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = "INSERT INTO [tasks] (text, user_id)" 
                                           + "VALUES (@text, @user_id)";
                    command.Parameters.AddWithValue("@text",text);
                    command.Parameters.AddWithValue("@user_id", user_id);

                    command.ExecuteNonQuery();
                }
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