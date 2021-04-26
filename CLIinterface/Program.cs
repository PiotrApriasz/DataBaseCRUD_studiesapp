using System;
using DataBaseManager;
using Spectre.Console;

namespace CLIinterface
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataManager = new DBConnect();

            while (true)
            {
                var mainMenu = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("---What you want to do?---")
                        .PageSize(6)
                        .AddChoices(new[]
                        {
                            "Insert", "Update", "Delete", "Select", "Exit"
                        }));

                switch (mainMenu)
                {
                    case "Insert":
                        dataManager.Insert();
                        Console.Clear();
                        break;
                    case "Update":
                        dataManager.Update();
                        Console.Clear();
                        break;
                    case "Delete":
                        dataManager.Delete();
                        Console.Clear();
                        break;
                    case "Select":
                        dataManager.Select();
                        Console.Clear();
                        break;
                    case "Exit":
                        Environment.Exit(0);
                        break;
                }
            }
            
        }
    }
}