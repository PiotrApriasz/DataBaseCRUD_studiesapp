using System;
using DataBaseManager;

namespace CLIinterface
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataManager = new DBConnect();

            while (true)
            {
                Console.WriteLine("---What you want to do?---");
                Console.WriteLine("--- 1. Insert");
                Console.WriteLine("--- 2. Update");
                Console.WriteLine("--- 3. Delete");
                Console.WriteLine("--- 4. Select");
                Console.WriteLine("--- 5. Exit");
                Console.WriteLine("--------------------------");
                Console.Write("-> ");

                var choose = Console.ReadLine();

                switch (choose)
                {
                    case "1":
                        dataManager.Insert();
                        Console.Clear();
                        break;
                    case "2":
                        dataManager.Update();
                        Console.Clear();
                        break;
                    case "3":
                        dataManager.Delete();
                        Console.Clear();
                        break;
                    case "4":
                        dataManager.Select();
                        Console.Clear();
                        break;
                    case "5":
                        Environment.Exit(0);
                        break;
                }
            }
            
        }
    }
}