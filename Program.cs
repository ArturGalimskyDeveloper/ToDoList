using System;
using System.Collections.Generic;
using CommandLine;

namespace todo_list
{
    class Program
    {
        private static List<string> TaskList = new List<string>()
        {
            "Task1", "task2", "Task3"
        };

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<AddOptions, ListOptions>(args)
                .WithParsed<AddOptions>(RunAddOptions)
                .WithParsed<ListOptions>(RunListOptions)
                .WithNotParsed(HandleParseError);
        }

        private static void HandleParseError(IEnumerable<Error> errs)
        {
            if (errs.IsVersion())
            {                
                return;
            }

            if (errs.IsHelp())
            {                
                return;
            }
            Console.WriteLine("Parser Fail");
        }

        private static void RunAddOptions(AddOptions opts)
        {
            System.Console.WriteLine("Add task is: " + opts.Text);
            System.Console.WriteLine("Date of task is: " + opts.Date);       
        }

        private static void RunListOptions(ListOptions opts)
        {               
            foreach(var task in TaskList)
            {
                System.Console.WriteLine("task is : " + task);
            }
        }
    }
}
