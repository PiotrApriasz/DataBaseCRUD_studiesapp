namespace CommonHR
{
    public static class SelectHR
    {
        /// <summary>
        /// Creats query which selects all columns from selected table
        /// </summary>
        /// <param name="tableNumber"></param>
        /// <returns></returns>
        public static string TableSwitcher(int tableNumber)
        {
            string query;
            
            switch (tableNumber)
            {
                case 1:
                    query = "select * from countries;";
                    break;
                case 2:
                    query = "select * from departments;";
                    break;
                case 3:
                    query = "select * from  employees; ";
                    break;
                case 4:
                    query = "select * from  job_history;";
                    break;
                case 5:
                    query = "select * from jobs;";
                    break;
                case 6:
                    query = "select * from locations;";
                    break;
                case 7:
                    query = "select * from regions;";
                    break;
                default:
                    query = null;
                    break;
            }

            return query;
        }
    }
}