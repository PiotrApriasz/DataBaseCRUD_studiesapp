using System;
using MySql.Data.MySqlClient;
using Spectre.Console;

namespace DataBaseManager
{
    public class Helper
    {
        private DBConnector Connector { get; }

        public Helper(DBConnector connector)
        {
            Connector = connector;
        }
        
        /// <summary>
        /// Write all tables from data base. User can choose one by number
        /// </summary>
        public int ChooseTable()
        {
            const string query = "SHOW TABLES;";
            var counter = 1;

            Console.WriteLine("Choose table which you want to edit");
            Console.WriteLine();

            if (Connector.OpenConnection())
            {
                var cmd = new MySqlCommand(query, Connector.Connection);
                var dataReader = cmd.ExecuteReader();
                var table = new Table();
                table.AddColumn(new TableColumn("Nr.").Centered());
                table.AddColumn(new TableColumn("Table").Centered());

                while (dataReader.Read())
                {
                    table.AddRow(counter.ToString(), dataReader.GetString(0));
                    counter++;
                }
                
                dataReader.Close();
                Connector.CloseConnection();
                AnsiConsole.Render(table);
            }

            Console.WriteLine();
            var choose = AnsiConsole.Ask<int>("-> ");

            return choose;

        }
    }
}