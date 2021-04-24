using MySql.Data.MySqlClient;

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
    }
}