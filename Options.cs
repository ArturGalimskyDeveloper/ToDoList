using System.Collections.Generic;
using CommandLine;

namespace todo_list
{
    [Verb("add", HelpText = "Add task")]
    class AddOptions
    {
        [Option('d',Required = false, HelpText = "Date of current task")]
        public string Date { get; set; }

        [Option('t',HelpText = "Task text", Default=null)]
        public string Text {get; set;}
    }

    [Verb("list", HelpText = "Out tasks")]
    public class ListOptions
    {
        // [Option("List", HelpText = "List all tasks", Default = null)]
        // public string List {get; set;}
        [Option('a', HelpText = "List all tasks")]
        public bool All { get; set; }

        [Option('t', HelpText = "List day tasks")]
        public bool Today { get; set; }
    }

    
    [Verb("run", HelpText = "Run server")]
    public class RunOptions
    {

    }
}