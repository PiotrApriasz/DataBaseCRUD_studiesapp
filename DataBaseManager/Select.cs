using System;
using System.Collections.Generic;
using System.Data;
using CommonHR;
using MySql.Data.MySqlClient;
using Spectre.Console;

namespace DataBaseManager
{
    public class Select : Query
    {
        public Select(string username, string password) : base(username, password)
        {
            StartText = "SELECT ";
        }

        protected override void CreatorActions()
        {
            Table = Helper.ChooseTable();
            QueryText += SelectHR.TableSwitcher(Table);
        }

        public override void ExecuteQuery()
        {
            if (Connector.OpenConnection())
            {
                try
                {
                    var cmd = new MySqlCommand(QueryText, Connector.Connection);
                    var dataReader = cmd.ExecuteReader();
                    var schemaTable = dataReader.GetSchemaTable();

                    var table = new Table();

                    if (schemaTable != null)
                        foreach (DataRow column in schemaTable.Rows)
                        {
                            table.AddColumn(new TableColumn(
                                column.Field<string>("ColumnName") ?? string.Empty).Centered());
                        }
                    else
                    {
                        throw new NullReferenceException();
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
    }
}