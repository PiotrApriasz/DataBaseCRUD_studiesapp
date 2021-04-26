using System;

namespace DataBaseManager
{
    public class Create : Query
    {
        public Create(string username, string password) : base(username, password)
        {
            StartText = $"CREATE TABLE ";
        }

        protected override void CreatorActions()
        {
            //TODO implement query creator 
            QueryText = "select null;";
            Console.WriteLine("\nFunction will be added in future!");
            Console.WriteLine("Press any key");
            Console.ReadKey();
        }
    }
}