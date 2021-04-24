using System;

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

        /// <summary>
        /// Gets data which user wants to add to specific table in hr data base
        /// </summary>
        /// <param name="tableNumber">Number of table user choosen</param>
        /// <returns></returns>
        public static string ValuesGetter(int tableNumber)
        {
            string query;
            
            switch (tableNumber)
            {
                case 1:
                    query = GetCountriesData();
                    break;
                case 2:
                    query = GetDepartmentsData();
                    break;
                case 3:
                    query = GetEmployeeData();
                    break;
                case 4:
                    query = GetJobHistoryData();
                    break;
                case 5:
                    query = GetJobsData();
                    break;
                case 6:
                    query = GetLocationsData();
                    break;
                case 7:
                    query = GetRegionsData();
                    break;
                default:
                    query = null;
                    break;
            }

            return query;
        }

        private static string GetRegionsData()
        {
            string query;
            Console.WriteLine("Enter region id");
            int.TryParse(Console.ReadLine(), out var region_id);

            Console.WriteLine("Enter region name");
            string region_name = Console.ReadLine();

            query = $"VALUES({region_id}, '{region_name}');";
            return query;
        }

        private static string GetLocationsData()
        {
            string query;
            Console.WriteLine("Enter location id");
            int.TryParse(Console.ReadLine(), out var location_id);

            Console.WriteLine("Enter street address");
            string street_address = Console.ReadLine();

            Console.WriteLine("Enter postal code");
            string postal_code = Console.ReadLine();

            Console.WriteLine("Enter city");
            string city = Console.ReadLine();

            Console.WriteLine("Enter state province");
            string state_province = Console.ReadLine();

            Console.WriteLine("Enter country id");
            string country_id = Console.ReadLine();

            query = $"VALUES({location_id}, '{street_address}', '{postal_code}', '{city}', '{state_province}', '{country_id}');";
            return query;
        }

        private static string GetJobsData()
        {
            string query;
            Console.WriteLine("Enter job id");
            string job_id = Console.ReadLine();

            Console.WriteLine("Enter job title");
            string job_title = Console.ReadLine();

            Console.WriteLine("Enter minimal salary");
            decimal.TryParse(Console.ReadLine(), out var min_salary);

            Console.WriteLine("Enter maximal salary");
            decimal.TryParse(Console.ReadLine(), out var max_salary);

            query = $"VALUES('{job_id}', '{job_title}', {min_salary}, {max_salary});";
            return query;
        }

        private static string GetJobHistoryData()
        {
            string query;
            Console.WriteLine("Enter employee id");
            int.TryParse(Console.ReadLine(), out var employee_id);

            Console.WriteLine("Enter start date in format YEAR-MONTH-DAY");
            string start_date = Console.ReadLine();

            Console.WriteLine("Enter end date in format YEAR-MONTH-DAY");
            string end_date = Console.ReadLine();

            Console.WriteLine("Enter job id");
            string job_id = Console.ReadLine();

            Console.WriteLine("Enter department id");
            int.TryParse(Console.ReadLine(), out var department_id);

            query = $"VALUES({employee_id}, '{start_date}', '{end_date}', '{job_id}', {department_id});";
            return query;
        }

        private static string GetEmployeeData()
        {
            string query;
            Console.WriteLine("Enter employee id");
            int.TryParse(Console.ReadLine(), out var employee_id);

            Console.WriteLine("Enter first name");
            string first_name = Console.ReadLine();

            Console.WriteLine("Enter last name");
            string last_name = Console.ReadLine();

            Console.WriteLine("Enter email");
            string email = Console.ReadLine();

            Console.WriteLine("Enter phone number");
            string phone_number = Console.ReadLine();

            Console.WriteLine("Enter hire daate in format YEAR-MONTH-DAY");
            string hire_date = Console.ReadLine();

            Console.WriteLine("Enter job id");
            string job_id = Console.ReadLine();

            Console.WriteLine("Enter salary");
            decimal.TryParse(Console.ReadLine(), out var salary);

            Console.WriteLine("Enter commission pct");
            decimal.TryParse(Console.ReadLine(), out var commision_pct);

            Console.WriteLine("Enter manager id");
            int.TryParse(Console.ReadLine(), out var manager_id);

            Console.WriteLine("Enter department id");
            int.TryParse(Console.ReadLine(), out var department_id);

            query = $"VALUES({employee_id}, '{first_name}', '{last_name}', '{email}', '{phone_number}', ";
            query += $"'{hire_date}', '{job_id}', {salary}, {commision_pct}, {manager_id}, {department_id});";
            return query;
        }

        private static string GetDepartmentsData()
        {
            string query;
            Console.WriteLine("Enter department id");
            int.TryParse(Console.ReadLine(), out var department_id);

            Console.WriteLine("Enter department_name");
            string department_name = Console.ReadLine();

            Console.WriteLine("Enter manager id");
            int.TryParse(Console.ReadLine(), out var manager_id);

            Console.WriteLine("Enter location id");
            int.TryParse(Console.ReadLine(), out var location_id);

            query = $"VALUES({department_id}, '{department_name}', {manager_id}, {location_id});";
            return query;
        }

        private static string GetCountriesData()
        {
            string query;
            Console.WriteLine("Enter country id");
            string country_id = Console.ReadLine();

            Console.WriteLine("Enter country name");
            string country_name = Console.ReadLine();

            Console.WriteLine("Enter region id");
            int.TryParse(Console.ReadLine(), out var region_id);

            query = $"VALUES('{country_id}', '{country_name}', {region_id});";
            return query;
        }
    }
}