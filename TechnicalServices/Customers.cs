


using BAIS3150FinalProject.Domain;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace BAIS3150FinalProject.TechnicalServices
{
    public class Customers
    {
        private string? _connectionString;

        public Customers()
        {
            ConfigurationBuilder DatabaseUserBuilder = new();
            DatabaseUserBuilder.SetBasePath(Directory.GetCurrentDirectory());
            DatabaseUserBuilder.AddJsonFile("appsettings.json");
            IConfiguration DatabaseUserConfiguration = DatabaseUserBuilder.Build();
            _connectionString = DatabaseUserConfiguration.GetConnectionString("BAIS3150");
        }


        public bool AddCustomer(Customer newCustomer)
        {
            bool Success = false;

            SqlConnection apandit1 = new();
            apandit1.ConnectionString = _connectionString;
            apandit1.Open();

            SqlCommand AddCustomerCommand = new()
            {
                Connection = apandit1,
                CommandType = CommandType.StoredProcedure,
                CommandText = "BAIS3150FinalProject.AddCustomer"
            };

            SqlParameter AddCustomerCommandParameter;

            AddCustomerCommandParameter = new()
            {
                ParameterName = "@FirstName",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = newCustomer.FirstName
            };
            AddCustomerCommand.Parameters.Add(AddCustomerCommandParameter);

            AddCustomerCommandParameter = new()
            {
                ParameterName = "@LastName",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = newCustomer.LastName
            };
            AddCustomerCommand.Parameters.Add(AddCustomerCommandParameter);

            AddCustomerCommandParameter = new()
            {
                ParameterName = "@Address",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = newCustomer.Address
            };
            AddCustomerCommand.Parameters.Add(AddCustomerCommandParameter);

            AddCustomerCommandParameter = new()
            {
                ParameterName = "@PhoneNumber",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = newCustomer.PhoneNumber
            };
            AddCustomerCommand.Parameters.Add(AddCustomerCommandParameter);

            AddCustomerCommandParameter = new()
            {
                ParameterName = "@City",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = newCustomer.City
            };
            AddCustomerCommand.Parameters.Add(AddCustomerCommandParameter);

            AddCustomerCommandParameter = new()
            {
                ParameterName = "@Province",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = newCustomer.Province
            };
            AddCustomerCommand.Parameters.Add(AddCustomerCommandParameter);

            AddCustomerCommandParameter = new()
            {
                ParameterName = "@PostalCode",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = newCustomer.PostalCode
            };
            AddCustomerCommand.Parameters.Add(AddCustomerCommandParameter);

            AddCustomerCommandParameter = new()
            {
                ParameterName = "@IsDeleted",
                SqlDbType = SqlDbType.Bit,
                Direction = ParameterDirection.Input,
                SqlValue = newCustomer.IsDeleted
            };
            AddCustomerCommand.Parameters.Add(AddCustomerCommandParameter);

            AddCustomerCommand.ExecuteNonQuery();

            apandit1.Close();
            Success = true;

            return Success;
        }


        public bool UpdateCustomer(Customer existedCustomer)
        {
            bool Success = false;

            SqlConnection apandit1 = new();
            apandit1.ConnectionString = _connectionString;
            apandit1.Open();

            SqlCommand UpdateCustomerCommand = new()
            {
                Connection = apandit1,
                CommandType = CommandType.StoredProcedure,
                CommandText = "BAIS3150FinalProject.UpdateCustomer"
            };

            SqlParameter UpdateCustomerCommandParameter;
            UpdateCustomerCommandParameter = new()
            {
                ParameterName = "@CustomerID",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                SqlValue = existedCustomer.CustomerID
            };
            UpdateCustomerCommand.Parameters.Add(UpdateCustomerCommandParameter);

            UpdateCustomerCommandParameter = new()
            {
                ParameterName = "@FirstName",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = existedCustomer.FirstName
            };
            UpdateCustomerCommand.Parameters.Add(UpdateCustomerCommandParameter);

            UpdateCustomerCommandParameter = new()
            {
                ParameterName = "@LastName",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = existedCustomer.LastName
            };
            UpdateCustomerCommand.Parameters.Add(UpdateCustomerCommandParameter);

            UpdateCustomerCommandParameter = new()
            {
                ParameterName = "@Address",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = existedCustomer.Address
            };
            UpdateCustomerCommand.Parameters.Add(UpdateCustomerCommandParameter);

            UpdateCustomerCommandParameter = new()
            {
                ParameterName = "@PhoneNumber",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = existedCustomer.PhoneNumber
            };
            UpdateCustomerCommand.Parameters.Add(UpdateCustomerCommandParameter);

            UpdateCustomerCommandParameter = new()
            {
                ParameterName = "@City",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = existedCustomer.City
            };
            UpdateCustomerCommand.Parameters.Add(UpdateCustomerCommandParameter);

            UpdateCustomerCommandParameter = new()
            {
                ParameterName = "@Province",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = existedCustomer.Province
            };
            UpdateCustomerCommand.Parameters.Add(UpdateCustomerCommandParameter);

            UpdateCustomerCommandParameter = new()
            {
                ParameterName = "@PostalCode",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = existedCustomer.PostalCode
            };
            UpdateCustomerCommand.Parameters.Add(UpdateCustomerCommandParameter);

            UpdateCustomerCommandParameter = new()
            {
                ParameterName = "@IsDeleted",
                SqlDbType = SqlDbType.Bit,
                Direction = ParameterDirection.Input,
                SqlValue = existedCustomer.IsDeleted
            };
            UpdateCustomerCommand.Parameters.Add(UpdateCustomerCommandParameter);
            UpdateCustomerCommand.ExecuteNonQuery();

            apandit1.Close();
            Success = true;

            return Success;
        }

        public bool DeleteCustomer(int customerID)
        {
            bool Success = false;

            SqlConnection apandit1 = new();
            apandit1.ConnectionString = _connectionString;
            apandit1.Open();

            SqlCommand DeleteCustomerCommand = new()
            {
                Connection = apandit1,
                CommandType = CommandType.StoredProcedure,
                CommandText = "BAIS3150FinalProject.DeleteCustomer"
            };

            SqlParameter DeleteCustomerCommandParameter = new()
            {
                ParameterName = "@CustomerID",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                SqlValue = customerID
            };
            DeleteCustomerCommand.Parameters.Add(DeleteCustomerCommandParameter);

            DeleteCustomerCommand.ExecuteNonQuery();

            apandit1.Close();
            Success = true;


            return Success;
        }

        public Customer GetAllCustomers(int customerID)
        {
            Customer ActiveCustomer = new();

            SqlConnection apandit1 = new();
            apandit1.ConnectionString = _connectionString;
            apandit1.Open();

            SqlCommand GetCustomerCommand = new()
            {
                Connection = apandit1,
                CommandType = CommandType.StoredProcedure,
                CommandText = "BAIS3150FinalProject.GetCustomer"
            };

            SqlParameter GetCustomerCommandParameter = new()
            {
                ParameterName = "@CustomerID",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                SqlValue = customerID
            };
            GetCustomerCommand.Parameters.Add(GetCustomerCommandParameter);

            GetCustomerCommand.ExecuteNonQuery();

            SqlDataReader GetCustomerDataReader = GetCustomerCommand.ExecuteReader();

            if (GetCustomerDataReader.HasRows)
            {
                if (GetCustomerDataReader.Read())
                {
                    ActiveCustomer.CustomerID = GetCustomerDataReader.GetInt32(0);
                    ActiveCustomer.FirstName = GetCustomerDataReader.GetString(1);
                    ActiveCustomer.LastName = GetCustomerDataReader.GetString(2);
                    ActiveCustomer.Address = GetCustomerDataReader.GetString(3);
                    ActiveCustomer.PhoneNumber = GetCustomerDataReader.GetString(4);
                    ActiveCustomer.City = GetCustomerDataReader.GetString(5);
                    ActiveCustomer.Province = GetCustomerDataReader.GetString(6);
                    ActiveCustomer.PostalCode = GetCustomerDataReader.GetString(7);
                    ActiveCustomer.IsDeleted = GetCustomerDataReader.GetBoolean(8);
                }
            }

            GetCustomerDataReader.Close();
            apandit1.Close();

            return ActiveCustomer;
        }
        
        public Customer GetActiveCustomer(int customerID)
        {
            Customer ActiveCustomer = new();

            SqlConnection apandit1 = new();
            apandit1.ConnectionString = _connectionString;
            apandit1.Open();

            SqlCommand GetCustomerCommand = new()
            {
                Connection = apandit1,
                CommandType = CommandType.StoredProcedure,
                CommandText = "BAIS3150FinalProject.GetNonDeletedCustomer"
            };

            SqlParameter GetCustomerCommandParameter = new()
            {
                ParameterName = "@CustomerID",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                SqlValue = customerID
            };
            GetCustomerCommand.Parameters.Add(GetCustomerCommandParameter);

            GetCustomerCommand.ExecuteNonQuery();

            SqlDataReader GetCustomerDataReader = GetCustomerCommand.ExecuteReader();

            if (GetCustomerDataReader.HasRows)
            {
                if (GetCustomerDataReader.Read())
                {
                    ActiveCustomer.CustomerID = GetCustomerDataReader.GetInt32(0);
                    ActiveCustomer.FirstName = GetCustomerDataReader.GetString(1);
                    ActiveCustomer.LastName = GetCustomerDataReader.GetString(2);
                    ActiveCustomer.Address = GetCustomerDataReader.GetString(3);
                    ActiveCustomer.PhoneNumber = GetCustomerDataReader.GetString(4);
                    ActiveCustomer.City = GetCustomerDataReader.GetString(5);
                    ActiveCustomer.Province = GetCustomerDataReader.GetString(6);
                    ActiveCustomer.PostalCode = GetCustomerDataReader.GetString(7);
                    ActiveCustomer.IsDeleted = GetCustomerDataReader.GetBoolean(8);
                }
            }

            GetCustomerDataReader.Close();
            apandit1.Close();

            return ActiveCustomer;
        }

        public Customer GetCustomerByPhoneNumber(string phoneNumber)
        {
            Customer ActiveCustomer = new();

            SqlConnection apandit1 = new();
            apandit1.ConnectionString = _connectionString;
            apandit1.Open();

            SqlCommand GetCustomerByPhoneNumberCommand = new()
            {
                Connection = apandit1,
                CommandType = CommandType.StoredProcedure,
                CommandText = "BAIS3150FinalProject.GetCustomerByPhoneNumber"
            };

            SqlParameter GetCustomerByPhoneNumberCommandParameter = new()
            {
                ParameterName = "@PhoneNumber",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = phoneNumber
            };
            GetCustomerByPhoneNumberCommand.Parameters.Add(GetCustomerByPhoneNumberCommandParameter);

            GetCustomerByPhoneNumberCommand.ExecuteNonQuery();

            SqlDataReader GetCustomerByPhoneNumberDataReader = GetCustomerByPhoneNumberCommand.ExecuteReader();

            if (GetCustomerByPhoneNumberDataReader.HasRows)
            {
                if (GetCustomerByPhoneNumberDataReader.Read())
                {
                    ActiveCustomer.CustomerID = GetCustomerByPhoneNumberDataReader.GetInt32(0);
                    ActiveCustomer.FirstName = GetCustomerByPhoneNumberDataReader.GetString(1);
                    ActiveCustomer.LastName = GetCustomerByPhoneNumberDataReader.GetString(2);
                    ActiveCustomer.Address = GetCustomerByPhoneNumberDataReader.GetString(3);
                    ActiveCustomer.PhoneNumber = GetCustomerByPhoneNumberDataReader.GetString(4);
                    ActiveCustomer.City = GetCustomerByPhoneNumberDataReader.GetString(5);
                    ActiveCustomer.Province = GetCustomerByPhoneNumberDataReader.GetString(6);
                    ActiveCustomer.PostalCode = GetCustomerByPhoneNumberDataReader.GetString(7);
                    ActiveCustomer.IsDeleted = GetCustomerByPhoneNumberDataReader.GetBoolean(8);
                }
            }

            GetCustomerByPhoneNumberDataReader.Close();
            apandit1.Close();

            return ActiveCustomer;
        }
    }
}
