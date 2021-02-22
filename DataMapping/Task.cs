namespace TodoList
{
    public class TodoTask
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public string Date { get; set; }

        public TodoTask(int id, string text, string date)
        {
            ID = id;
            Text = text;
            Date = date;
        }

    }
}