using System;
using DataBaseManager;

namespace CLIinterface
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataManager = new DBConnect();
            
            dataManager.Insert();
        }
    }
}