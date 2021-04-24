using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using CommonHR;
using Org.BouncyCastle.Asn1.Cms;

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
            var table = ChooseTable();

            query += InsertHR.TableSwitcher(table);
            query += InsertHR.ValuesGetter(table);

            if (OpenConnection())
            {
                var cmd = new MySqlCommand(query, _connection);
                int numberOfRows = cmd.ExecuteNonQuery();
                
                Console.WriteLine();
                Console.WriteLine(numberOfRows + " row(s) affected");
                Console.ReadKey();

                CloseConnection();
            }
        }

        /// <summary>
        /// Update statement
        /// </summary>
        public void Update()
        {
        }

        /// <summary>
        /// Delete statement
        /// </summary>
        public void Delete()
        {
        }

        /// <summary>
        /// Select statement
        /// </summary>
        /// <returns></returns>
        public List <string> [] Select()
        {
            return new List<string>[1];
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

        #endregion
    }
}