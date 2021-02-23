namespace TodoList
{
    public class TodoTask
    {
        public int ID { get; set; }
        public string TEXT { get; set; }

        public TodoTask(int id, string text)
        {
            ID = id;
            TEXT = text;
        }

    }
}