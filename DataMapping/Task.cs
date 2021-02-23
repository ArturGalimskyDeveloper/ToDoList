namespace TodoList
{
    public class TodoTask
    {
        public int ID { get; set; }
        public string TEXT { get; set; }
        public int USER_ID { get; set; }

        public TodoTask(int id, string text, int user_id)
        {
            ID = id;
            TEXT = text;
            USER_ID = user_id;
        }
    }
}