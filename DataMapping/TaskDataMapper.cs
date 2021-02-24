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

        public static List<string> GetAll()
        {
            List<string> tasks = new List<string>();

            using(var connection = new SqliteConnection(Configuration.CONNECTION_STRING))
            {
                connection.Open();
                string query = "SELECT task_id, text, date FROM tasks";

                using(SqliteCommand comand = new SqliteCommand(query, connection))
                {
                    using(SqliteDataReader reader = comand.ExecuteReader())
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