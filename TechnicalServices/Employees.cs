using System.Data;
using BAIS3150FinalProject.Domain;
using Microsoft.Data.SqlClient;

namespace BAIS3150FinalProject.TechnicalServices
{
    public class Employees
    {
        private string? _connectionString;

        public Employees()
        {
            ConfigurationBuilder DatabaseUserBuilder = new();
            DatabaseUserBuilder.SetBasePath(Directory.GetCurrentDirectory());
            DatabaseUserBuilder.AddJsonFile("appsettings.json");
            IConfiguration DatabaseUserConfiguration = DatabaseUserBuilder.Build();
            _connectionString = DatabaseUserConfiguration.GetConnectionString("BAIS3150");
        }

        public List<Employee> GetEmployees()
        {
            List<Employee> EmployeeList = new();

            SqlConnection apandit1 = new();
            apandit1.ConnectionString = _connectionString;
            apandit1.Open();

            SqlCommand GetEmployeesCommand = new()
            {
                Connection = apandit1,
                CommandType = CommandType.StoredProcedure,
                CommandText = "BAIS3150FinalProject.GetEmployees"
            };

            GetEmployeesCommand.ExecuteNonQuery();

            SqlDataReader GetEmployeesDataReader = GetEmployeesCommand.ExecuteReader();

            if (GetEmployeesDataReader.HasRows)
            {
                while (GetEmployeesDataReader.Read())
                {
                    Employee employee = new()
                    {
                        EmployeeID = GetEmployeesDataReader.GetInt32(0),
                        EmployeeName = GetEmployeesDataReader.GetString(1)
                    };
                    EmployeeList.Add(employee);
                }

            }

            GetEmployeesDataReader.Close();
            apandit1.Close();

            return EmployeeList;
        }

        public Employee GetEmployee(int employeeID)
        {
            Employee ActiveEmployee = new();

            SqlConnection apandit1 = new();
            apandit1.ConnectionString = _connectionString;
            apandit1.Open();

            SqlCommand GetEmployeeCommand = new()
            {
                Connection = apandit1,
                CommandType = CommandType.StoredProcedure,
                CommandText = "BAIS3150FinalProject.GetEmployee"
            };

            SqlParameter GetEmployeeCommandParameter = new()
            {
                ParameterName = "@EmployeeID",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                SqlValue = employeeID
            };
            GetEmployeeCommand.Parameters.Add(GetEmployeeCommandParameter);

            GetEmployeeCommand.ExecuteNonQuery();

            SqlDataReader GetEmployeeCommandReader = GetEmployeeCommand.ExecuteReader();

            if (GetEmployeeCommandReader.HasRows)
            {
                if (GetEmployeeCommandReader.Read())
                {
                    ActiveEmployee.EmployeeID = GetEmployeeCommandReader.GetInt32(0);
                    ActiveEmployee.EmployeeName = GetEmployeeCommandReader.GetString(1);
                }
            }

            GetEmployeeCommandReader.Close();
            apandit1.Close();

            return ActiveEmployee;
        }
    }
}
