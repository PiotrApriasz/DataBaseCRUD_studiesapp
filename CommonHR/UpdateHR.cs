namespace CommonHR
{
    public class UpdateHR
    {
        /// <summary>
        /// Returns first part of Update statement with table choosen
        /// </summary>
        /// <param name="tableNumber"></param>
        /// <returns></returns>
        public static string TableSwitcher(int tableNumber)
        {
            string query;
            
            switch (tableNumber)
            {
                case 1:
                    query = "UPDATE countries SET ";
                    break;
                case 2:
                    query = "UPDATE departments SET ";
                    break;
                case 3:
                    query = "UPDATE employees SET ";
                    break;
                case 4:
                    query = "UPDATE job_history SET ";
                    break;
                case 5:
                    query = "UPDATE jobs SET ";
                    break;
                case 6:
                    query = "UPDATE locations SET ";
                    break;
                case 7:
                    query = "UPDATE regions SET ";
                    break;
                default:
                    query = null;
                    break;
            }

            return query;
        }
    }
}