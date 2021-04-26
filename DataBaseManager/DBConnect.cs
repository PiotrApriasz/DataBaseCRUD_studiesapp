using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using MySql.Data.MySqlClient;
using CommonHR;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace DataBaseManager
{
    public class DBConnect
    {
        #region Variables

        /// <summary>
        /// Will be used to open a connection to the database
        /// </summary>
        private MySqlConnection _connection;

        /// <summary>
        /// Indicates where our server is hosted. Here it is localhost
        /// </summary>
        private string _server;

        /// <summary>
        /// Will be used to store database name to use in program
        /// </summary>
        private string _database;

        /// <summary>
        /// MySQL username
        /// </summary>
        private string _uid;

        /// <summary>
        /// MySQL password
        /// </summary>
        private string _password;

        #endregion

        #region Constructor

        public DBConnect()
        {
            _server = "localhost";
            _database = "hr";
            _uid = "piotrek";
            _password = "HasloDoBazy";
            string connectionString;
            connectionString = "SERVER=" + _server + ";" + "DATABASE=" + 
                               _database + ";" + "UID=" + _uid + ";" + "PASSWORD=" + _password + ";";
            
            _connection = new MySqlConnection(connectionString);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Open connection to database
        /// </summary>
        /// <returns></returns>
        private bool OpenConnection()
        {
            try
            {
                _connection.Open();
                return true;
            }
            catch (MySqlException e)
            {
                switch (e.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server!");
                        break;
                    case 1045:
                        Console.WriteLine("Invalid username or password, please try again");
                        break;
                }

                return false;
            }
        }

        /// <summary>
        /// Close connection
        /// </summary>
        /// <returns></returns>
        private bool CloseConnection()
        {
            try
            {
                _connection.Close();
                return true;
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        /// <summary>
        /// Insert statement
        /// </summary>
        public void Insert()
        {
            Console.Clear();
            string query = "";

            Console.WriteLine("---Do you want to enter your own query or use creator?---");
            Console.WriteLine("--- 1. Own query");
            Console.WriteLine("--- 2. Creator");
            Console.WriteLine("----------------------------------------------------------------");
            Console.Write("-> ");

            var choose = Console.ReadLine();

            switch (choose)
            {
                case "1":
                    Console.Write("Enter your INSERT query \n(insert into is already in query, start with columns names)");
                    Console.Write("\n-> INSERT INTO ");
                    query += "insert into ";
                    query += Console.ReadLine();
                    break;
                case "2":
                    var table = ChooseTable();
                    query += InsertHR.TableSwitcher(table);
                    query += InsertHR.ValuesGetter(table);
                    break;
            }

            ExecuteQueryIUDC(choose, query);
        }

        /// <summary>
        /// Update statement
        /// </summary>
        public void Update()
        {
            Console.Clear();
            string query = "";

            Console.WriteLine("---Do you want to enter your own query or use creator?---");
            Console.WriteLine("--- 1. Own query");
            Console.WriteLine("--- 2. Creator");
            Console.WriteLine("----------------------------------------------------------------");
            Console.Write("-> ");

            var choose = Console.ReadLine();

            switch (choose)
            {
                case "1":
                    Console.Write("Enter your UPDATE query \n(update is already in query, start with table name)");
                    Console.Write("\n-> UPDATE ");
                    query += "update ";
                    query += Console.ReadLine();
                    break;
                case "2":
                    //TODO implement query creator 
                    Console.WriteLine("\nFunction will be added in future!");
                    Console.WriteLine("Press any key");
                    Console.ReadKey();
                    break;
            }
            
            if (choose == "1") ExecuteQueryIUDC(choose, query);
        }

        /// <summary>
        /// Delete statement
        /// </summary>
        public void Delete()
        {
            Console.Clear();
            string query = "";

            Console.WriteLine("---Do you want to enter your own query or use creator?---");
            Console.WriteLine("--- 1. Own query");
            Console.WriteLine("--- 2. Creator");
            Console.WriteLine("----------------------------------------------------------------");
            Console.Write("-> ");

            var choose = Console.ReadLine();

            switch (choose)
            {
                case "1":
                    Console.Write("Enter your DELETE query \n(delete from is already in query, start with table name)");
                    Console.Write("\n-> DELETE FROM ");
                    query += "delete from ";
                    query += Console.ReadLine();
                    break;
                case "2":
                    //TODO implement query creator 
                    Console.WriteLine("\nFunction will be added in future!");
                    Console.WriteLine("Press any key");
                    Console.ReadKey();
                    break;
            }
            
            if (choose == "1") ExecuteQueryIUDC(choose, query);
        }

        /// <summary>
        /// Select statement
        /// </summary>
        /// <returns></returns>
        public void Select()
        {
            Console.Clear();
            string query = "";

            Console.WriteLine("---Do you want to enter your own query or use creator?---");
            Console.WriteLine("--- 1. Own query");
            Console.WriteLine("--- 2. Creator");
            Console.WriteLine("----------------------------------------------------------------");
            Console.Write("-> ");

            var choose = Console.ReadLine();

            switch (choose)
            {
                case "1":
                    Console.Write("Enter your SELECT query \n(select is already in query, start with columns)");
                    Console.Write("\n-> SELECT ");
                    query += "select ";
                    query += Console.ReadLine();
                    break;
                case "2":
                    var table = ChooseTable();
                    query += SelectHR.TableSwitcher(table);
                    break;
            }
            
            if (OpenConnection() && (choose == "1" || choose == "2"))
            {
                try
                {
                    var cmd = new MySqlCommand(query, _connection);
                    var dataReader = cmd.ExecuteReader();
                    var schemaTable = dataReader.GetSchemaTable();

                    var table = new Table();
                    
                    foreach (DataRow column in schemaTable.Rows)
                    {
                        table.AddColumn(new TableColumn(
                            column.Field<String>("ColumnName")).Centered());
                    }
                    
                    while (dataReader.Read())
                    {
                        var entriesList = new List<string>();

                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            entriesList.Add(dataReader[i].ToString());
                        }

                        string[] entries = entriesList.ToArray();
                        
                        table.AddRow(entries);
                    }
                    
                    AnsiConsole.Render(table);

                    dataReader.Close();
                    
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
                finally
                {
                    CloseConnection();
                }
            }
            else
            {
                Console.WriteLine("\nSomething went wrong!");
                Console.WriteLine("Press any key");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Count statement
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return 1;
        }

        /// <summary>
        /// Write all tables from data base. User can choose one by number
        /// </summary>
        private int ChooseTable()
        {
            string query = "SHOW TABLES;";
            int counter = 1;
            int table;

            Console.WriteLine("Choose table which you want to edit");
            Console.WriteLine();

            if (OpenConnection())
            {
                var cmd = new MySqlCommand(query, _connection);
                var dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Console.Write(counter + ". ");
                    Console.WriteLine(dataReader.GetString(0));
                    counter++;
                }
                
                dataReader.Close();
                this.CloseConnection();
            }

            Console.WriteLine();
            Console.Write("-> ");
            int.TryParse(Console.ReadLine(), out table);

            return table;

        }

        /// <summary>
        /// Backup
        /// </summary>
        public void Backup()
        {
        }

        /// <summary>
        /// Restore
        /// </summary>
        public void Restore()
        {
        }
        
        private void ExecuteQueryIUDC(string choose, string query)
        {
            if (OpenConnection() && (choose == "1" || choose == "2"))
            {
                try
                {
                    var cmd = new MySqlCommand(query, _connection);
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
                finally
                {
                    CloseConnection();
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