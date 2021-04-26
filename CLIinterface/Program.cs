using System;
using DataBaseManager;
using Spectre.Console;

namespace CLIinterface
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            string username = args[0];
            string password = args[1];

            while (true)
            {
                AnsiConsole.MarkupLine("[red]WARNING: Note that in current verions if you add new table you can't use" +
                                   " query qreator to perform operation on this table[/]");
                AnsiConsole.MarkupLine("[red]Use Own query option instead[/]");
                
                var rule = new Rule();
                AnsiConsole.Render(rule);
                
                var mainMenu = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("---What you want to do?---")
                        .PageSize(7)
                        .AddChoices(new[]
                        {
                            "Insert", "Update", "Delete", "Select", "Create", "Exit"
                        }));

                switch (mainMenu)
                {
                    case "Insert":
                        var insert = new Insert(username, password);
                        insert.ChooseModeMenu();
                        insert.ExecuteQuery();
                        Console.Clear();
                        break;
                    case "Update":
                        var update = new Update(username, password);
                        update.ChooseModeMenu();
                        update.ExecuteQuery();
                        Console.Clear();
                        break;
                    case "Delete":
                        var delete = new Delete(username, password);
                        delete.ChooseModeMenu();
                        delete.ExecuteQuery();
                        Console.Clear();
                        break;
                    case "Select":
                        var select = new Select(username, password);
                        select.ChooseModeMenu();
                        select.ExecuteQuery();
                        Console.Clear();
                        break;
                    case "Create":
                        var create = new Create(username, password);
                        create.ChooseModeMenu();
                        create.ExecuteQuery();
                        Console.Clear();
                        break;
                    case "Exit":
                        Environment.Exit(0);
                        Console.Clear();
                        break;
                }
            }
            
        }
    }
}