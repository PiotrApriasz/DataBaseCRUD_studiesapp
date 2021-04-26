using System;
using MySql.Data.MySqlClient;
using Spectre.Console;

namespace DataBaseManager
{
    /// <summary>
    /// Base class for queries
    /// </summary>
    public abstract class Query
    {
        #region Properties

        /// <summary>
        /// Text where start text of query is stored. You can set this in inheriting class
        /// </summary>
        protected string StartText { get; init; }
        
        /// <summary>
        /// Stores all query
        /// </summary>
        protected string QueryText { get; set; }

        /// <summary>
        /// Stores number of table which user choosen 
        /// </summary>
        protected int Table { get; set; }
        
        /// <summary>
        /// Instance of Helper class
        /// </summary>
        protected Helper Helper { get; }

        /// <summary>
        /// Instance of DBConnector class. Uses to connect or disconnect data base
        /// </summary>
        protected DBConnector Connector { get; }

        #endregion

        #region Constructor

        protected Query(string username, string password)
        {
            Connector = new DBConnector(username, password);
            Helper = new Helper(Connector);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Abstract function. You can override it for every queries with query creator code
        /// </summary>
        protected abstract void CreatorActions();

        /// <summary>
        /// Function where you can choose if you want to enter your query or use creator
        /// Performs this actions
        /// </summary>
        public void ChooseModeMenu()
        {
            var choose = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("---Do you want to enter your own query or use creator?---")
                    .PageSize(3)
                    .AddChoices(new[]
                    {
                        "Own Query", "Creator"
                    }));
            
            switch (choose)
            {
                case "Own Query":
                    Console.Write($"Enter your {StartText} query \n({StartText} is already in query, start with columns names)");
                    Console.Write($"\n-> {StartText} ");
                    QueryText += $"{StartText} ";
                    QueryText += Console.ReadLine();
                    break;
                case "Creator":
                    CreatorActions();
                    break;
            }
        }

        /// <summary>
        /// Function which executes earlier builded query
        /// Error handling for MySQL erorrs 
        /// </summary>
        public virtual void ExecuteQuery()
        {
            if (Connector.OpenConnection())
            {
                try
                {
                    var cmd = new MySqlCommand(QueryText, Connector.Connection);
                    int numberOfRows = cmd.ExecuteNonQuery();

                    Console.WriteLine();
                    Console.WriteLine(numberOfRows + " row(s) affected");
                    Console.WriteLine("Press any key");
                    Console.ReadKey();
                }
                catch (MySqlException e)
                {
                    switch (e.Number)
                    {
                        case 1064:
                            Console.WriteLine("\nThere is a syntax error in your query!");
                            Console.WriteLine("Press any key");
                            Console.ReadKey();
                            break;
                        case 1054:
                            Console.WriteLine("\nUnknown column!");
                            Console.WriteLine("Press any key");
                            Console.ReadKey();
                            break;
                        case 1146:
                            Console.WriteLine("\nTable you entered doesn't exists");
                            Console.WriteLine("Press any key");
                            Console.ReadKey();
                            break;
                        default:
                            Console.WriteLine("\nSomething is wrong with your query!");
                            Console.WriteLine("Press any key");
                            Console.ReadKey();
                            break;
                    }
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("ERROR: You performed operation by query from creator on table created by you!");
                    Console.WriteLine("Press any key");
                    Console.ReadKey();
                }
                finally
                {
                    Connector.CloseConnection();
                }
            }
            else
            {
                Console.WriteLine("\nSomething went wrong!");
                Console.WriteLine("Press any key");
                Console.ReadKey();
            }
        }

        #endregion
    }
}