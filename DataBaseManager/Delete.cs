using System;

namespace DataBaseManager
{
    public class Delete : Query
    {
        public Delete(string username, string password) : base(username, password)
        {
            StartText = $"DELETE FROM ";
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