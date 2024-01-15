using BAIS3150FinalProject.Domain;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace BAIS3150FinalProject.TechnicalServices
{

    public class Sales
    {

        private string? _connectionString;

        public Sales()
        {
            ConfigurationBuilder DatabaseUserBuilder = new();
            DatabaseUserBuilder.SetBasePath(Directory.GetCurrentDirectory());
            DatabaseUserBuilder.AddJsonFile("appsettings.json");
            IConfiguration DatabaseUserConfiguration = DatabaseUserBuilder.Build();
            _connectionString = DatabaseUserConfiguration.GetConnectionString("BAIS3150");
        }

        public int AddSale(Sale ABCSale)
        {
            int SaleNumber;

            SqlConnection apandit1 = new();
            apandit1.ConnectionString = _connectionString;
            apandit1.Open();

            SqlCommand AddSaleCommand = new()
            {
                Connection = apandit1,
                CommandType = CommandType.StoredProcedure,
                CommandText = "BAIS3150FinalProject.AddSale"
            };

            SqlParameter AddSaleCommandParameter;
            AddSaleCommandParameter = new()
            {
                ParameterName = "@CustomerID",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                SqlValue = ABCSale.CustomerID
            };
            AddSaleCommand.Parameters.Add(AddSaleCommandParameter);

            AddSaleCommandParameter = new()
            {
                ParameterName = "@EmployeeID",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                SqlValue = ABCSale.EmployeeID
            };
            AddSaleCommand.Parameters.Add(AddSaleCommandParameter);

            AddSaleCommandParameter = new()
            {
                ParameterName = "@SaleDate",
                SqlDbType = SqlDbType.Date,
                Direction = ParameterDirection.Input,
                SqlValue = ABCSale.SaleDate
            };
            AddSaleCommand.Parameters.Add(AddSaleCommandParameter);

            AddSaleCommandParameter = new()
            {
                ParameterName = "@SubTotal",
                SqlDbType = SqlDbType.Decimal,
                Direction = ParameterDirection.Input,
                SqlValue = ABCSale.SubTotal
            };
            AddSaleCommand.Parameters.Add(AddSaleCommandParameter);

            AddSaleCommandParameter = new()
            {
                ParameterName = "@GST",
                SqlDbType = SqlDbType.Decimal,
                Direction = ParameterDirection.Input,
                SqlValue = ABCSale.GST
            };
            AddSaleCommand.Parameters.Add(AddSaleCommandParameter);

            AddSaleCommandParameter = new()
            {
                ParameterName = "@SaleTotal",
                SqlDbType = SqlDbType.Decimal,
                Direction = ParameterDirection.Input,
                SqlValue = ABCSale.SaleTotal
            };
            AddSaleCommand.Parameters.Add(AddSaleCommandParameter);

            AddSaleCommandParameter = new()
            {
                ParameterName = "@AddSaleNumber",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            AddSaleCommand.Parameters.Add(AddSaleCommandParameter);

            AddSaleCommand.ExecuteNonQuery();

            SaleNumber = (int)AddSaleCommand.Parameters["@AddSaleNumber"].Value;

            SqlCommand AddSaleItemCommand = new()
            {
                Connection = apandit1,
                CommandType = CommandType.StoredProcedure,
                CommandText = "BAIS3150FinalProject.AddSaleItem"
            };

            foreach (SaleItem ABCSaleItem in ABCSale.SaleItemList)
            {
                AddSaleItemCommand.Parameters.Clear();

                SqlParameter AddSaleItemCommandParameter;

                AddSaleItemCommandParameter = new()
                {
                    ParameterName = "@SaleNumber",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    SqlValue = SaleNumber
                };
                AddSaleItemCommand.Parameters.Add(AddSaleItemCommandParameter);

                AddSaleItemCommandParameter = new()
                {
                    ParameterName = "@ItemCode",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    SqlValue = ABCSaleItem.ItemCode
                };
                AddSaleItemCommand.Parameters.Add(AddSaleItemCommandParameter);

                AddSaleItemCommandParameter = new()
                {
                    ParameterName = "@Quantity",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    SqlValue = ABCSaleItem.Quantity
                };
                AddSaleItemCommand.Parameters.Add(AddSaleItemCommandParameter);

                AddSaleItemCommandParameter = new()
                {
                    ParameterName = "@ItemTotal",
                    SqlDbType = SqlDbType.Decimal,
                    Direction = ParameterDirection.Input,
                    SqlValue = ABCSaleItem.ItemTotal
                };
                AddSaleItemCommand.Parameters.Add(AddSaleItemCommandParameter);

                AddSaleItemCommand.ExecuteNonQuery();
            }

            SqlCommand UpdateItemInventoryQuantityCommand = new()
            {
                Connection = apandit1,
                CommandType = CommandType.StoredProcedure,
                CommandText = "BAIS3150FinalProject.UpdateInventoryQuantity"
            };

            foreach (SaleItem ABCSaleItem in ABCSale.SaleItemList)
            {
                UpdateItemInventoryQuantityCommand.Parameters.Clear();

                SqlParameter UpdateItemInventoryQuantityCommandParameter;

                UpdateItemInventoryQuantityCommandParameter = new()
                {
                    ParameterName = "@ItemCode",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    SqlValue = ABCSaleItem.ItemCode
                };
                UpdateItemInventoryQuantityCommand.Parameters.Add(UpdateItemInventoryQuantityCommandParameter);

                UpdateItemInventoryQuantityCommandParameter = new()
                {
                    ParameterName = "@Quantity",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    SqlValue = ABCSaleItem.Quantity
                };
                UpdateItemInventoryQuantityCommand.Parameters.Add(UpdateItemInventoryQuantityCommandParameter);

                UpdateItemInventoryQuantityCommand.ExecuteNonQuery();
            }

            apandit1.Close();

            return SaleNumber;
        }
    }
}
