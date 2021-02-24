using System;
using System.Collections.Generic;
using CommandLine;

namespace TodoList
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<AddOptions, ListOptions, RunOptions>(args)
                .WithParsed<AddOptions>(RunAddOptions)
                .WithParsed<ListOptions>(RunListOptions)
                .WithParsed<RunOptions>(RunRunOptions)
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
            if(opts.All)
            {
                // foreach(var item in TaskDataMapper.GetAll())
                // {
                //     System.Console.WriteLine(item);
                // }
            }
            else if(opts.Today)
            {
                // filter today only tasks here...                
            }
        }
    
        private static void RunRunOptions(RunOptions opts)
        {
            System.Console.WriteLine("Running server");

            BotConnection botConnection = new BotConnection();
            System.Console.WriteLine("Stop server, readkey to exit");
            System.Console.ReadKey();
        }
    }
}
