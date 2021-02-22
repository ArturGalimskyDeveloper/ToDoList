using System.Collections.Generic;
using CommandLine;

namespace todo_list
{

    [Verb("add")]
    class AddOptions
    {
        [Option('d',Required = false, HelpText = "Date of current task")]
        public string Date { get; set; }

        [Option('t',HelpText = "Task text", Default=null)]
        public string Text {get; set;}
    }

    [Verb("list")]
    public class ListOptions
    {
        // [Option("List", HelpText = "List all tasks", Default = null)]
        // public string List {get; set;}
    }
}