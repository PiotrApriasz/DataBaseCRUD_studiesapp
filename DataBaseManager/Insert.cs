using CommonHR;

namespace DataBaseManager
{
    public class Insert : Query
    {
        public Insert(string username, string password) : base(username, password)
        {
            StartText = $"INSERT INTO ";
        }

        protected override void CreatorActions()
        {
            Table = Helper.ChooseTable();
            QueryText += InsertHR.TableSwitcher(Table);
            QueryText += InsertHR.ValuesGetter(Table);
        }
    }
}