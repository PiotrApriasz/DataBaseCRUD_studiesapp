namespace CommonHR
{
    public static class InsertHR
    {
        /// <summary>
        /// Returns first part of INSERT statement with tables and columns choosen by user
        /// </summary>
        /// <param name="tableNumber">Number of table user choosen</param>
        /// <returns></returns>
        public static string TableSwitcher(int tableNumber)
        {
            string query;
            
            switch (tableNumber)
            {
                case 1:
                    query = "INSERT INTO countries (country_id, country_name, region_id) ";
                    break;
                case 2:
                    query = "INSERT INTO departments (department_id, department_name, manager_id, location_id) ";
                    break;
                case 3:
                    query = "INSERT INTO employees (employee_id, first_name, last_name, email, phone_number, ";
                    query += "hire_date, job_id, salary, commision_pct, manager_id, department_id) ";
                    break;
                case 4:
                    query = "INSERT INTO job_history (employee_id, start_date, end_date, job_id, department_id) ";
                    break;
                case 5:
                    query = "INSERT INTO jobs (job_id, job_title, min_salary, max_salary) ";
                    break;
                case 6:
                    query = "INSERT INTO locations (location_id, street_address, postal_code, city, state_province, country_id) ";
                    break;
                case 7:
                    query = "INSERT INTO regions (region_id, region_name) ";
                    break;
                default:
                    query = null;
                    break;
            }

            return query;
        }
    }
}