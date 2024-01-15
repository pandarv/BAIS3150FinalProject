using BAIS3150FinalProject.Domain;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace BAIS3150FinalProject.TechnicalServices
{
    public class Items
    {
        private string? _connectionString;
        
        public Items()
        {
            ConfigurationBuilder DatabaseUserBuilder = new();
            DatabaseUserBuilder.SetBasePath(Directory.GetCurrentDirectory());
            DatabaseUserBuilder.AddJsonFile("appsettings.json");
            IConfiguration DatabaseUserConfiguration = DatabaseUserBuilder.Build();
            _connectionString = DatabaseUserConfiguration.GetConnectionString("BAIS3150");
        }

        public bool AddItem(Item item)
        {
            bool Success = false;

            SqlConnection apandit1 = new();
            apandit1.ConnectionString = _connectionString;
            apandit1.Open();

            SqlCommand AddItemCommand = new()
            {
                Connection = apandit1,
                CommandType = CommandType.StoredProcedure,
                CommandText = "BAIS3150FinalProject.AddInventoryItem"
            };

            SqlParameter AddItemCommandParameter;

            AddItemCommandParameter = new()
            {
                ParameterName = "@ItemCode",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = item.ItemCode
            };
            AddItemCommand.Parameters.Add(AddItemCommandParameter);
            
            AddItemCommandParameter = new()
            {
                ParameterName = "@Description",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = item.Description
            };
            AddItemCommand.Parameters.Add(AddItemCommandParameter);

            AddItemCommandParameter = new()
            {
                ParameterName = "@UnitPrice",
                SqlDbType = SqlDbType.Decimal,
                Direction = ParameterDirection.Input,
                SqlValue = item.UnitPrice
            };
            AddItemCommand.Parameters.Add(AddItemCommandParameter);

            AddItemCommandParameter = new()
            {
                ParameterName = "@StockQuantity",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                SqlValue = item.StockQuantity
            };
            AddItemCommand.Parameters.Add(AddItemCommandParameter);

            AddItemCommandParameter = new()
            {
                ParameterName = "@IsDeleted",
                SqlDbType = SqlDbType.Bit,
                Direction = ParameterDirection.Input,
                SqlValue = item.IsDeleted
            };
            AddItemCommand.Parameters.Add(AddItemCommandParameter);

            AddItemCommand.ExecuteNonQuery();

            apandit1.Close();
            Success = true;

            return Success;
        }

        public bool UpdateItem(Item existedItem)
        {
            bool Success = false;

            SqlConnection apandit1 = new();
            apandit1.ConnectionString = _connectionString;
            apandit1.Open();

            SqlCommand UpdateItemCommand = new()
            {
                Connection = apandit1,
                CommandType = CommandType.StoredProcedure,
                CommandText = "BAIS3150FinalProject.UpdateInventoryItem"
            };

            SqlParameter UpdateItemCommandParameter;

            UpdateItemCommandParameter = new()
            {
                ParameterName = "@ItemCode",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = existedItem.ItemCode
            };
            UpdateItemCommand.Parameters.Add(UpdateItemCommandParameter);

            UpdateItemCommandParameter = new()
            {
                ParameterName = "@Description",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = existedItem.Description
            };
            UpdateItemCommand.Parameters.Add(UpdateItemCommandParameter);

            UpdateItemCommandParameter = new()
            {
                ParameterName = "@UnitPrice",
                SqlDbType = SqlDbType.Decimal,
                Direction = ParameterDirection.Input,
                SqlValue = existedItem.UnitPrice
            };
            UpdateItemCommand.Parameters.Add(UpdateItemCommandParameter);

            UpdateItemCommandParameter = new()
            {
                ParameterName = "@StockQuantity",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                SqlValue = existedItem.StockQuantity
            };
            UpdateItemCommand.Parameters.Add(UpdateItemCommandParameter);

            UpdateItemCommandParameter = new()
            {
                ParameterName = "@IsDeleted",
                SqlDbType = SqlDbType.Bit,
                Direction = ParameterDirection.Input,
                SqlValue = existedItem.IsDeleted
            };
            UpdateItemCommand.Parameters.Add(UpdateItemCommandParameter);

            UpdateItemCommand.ExecuteNonQuery();

            apandit1.Close();
            Success = true;


            return Success;
        }

        public bool DeleteItem(string itemCode)
        {
            bool Success = false;

            SqlConnection apandit1 = new();
            apandit1.ConnectionString = _connectionString;
            apandit1.Open();

            SqlCommand DeleteItemCommand = new()
            {
                Connection = apandit1,
                CommandType = CommandType.StoredProcedure,
                CommandText = "BAIS3150FinalProject.DeleteInventoryItem"
            };

            SqlParameter DeleteItemCommandParameter = new()
            {
                ParameterName = "@ItemCode",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = itemCode
            };
            DeleteItemCommand.Parameters.Add(DeleteItemCommandParameter);

            DeleteItemCommand.ExecuteNonQuery();

            apandit1.Close();
            Success = true;


            return Success;
        }

        public Item GetItem(string itemCode)
        {
            Item ActiveItem = new();

            SqlConnection apandit1 = new();
            apandit1.ConnectionString = _connectionString;
            apandit1.Open();

            SqlCommand GetItemCommand = new()
            {
                Connection = apandit1,
                CommandType = CommandType.StoredProcedure,
                CommandText = "BAIS3150FinalProject.GetInventoryItem"
            };

            SqlParameter GetItemCommandParameter = new()
            {
                ParameterName = "@ItemCode",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = itemCode
            };
            GetItemCommand.Parameters.Add(GetItemCommandParameter);

            GetItemCommand.ExecuteNonQuery();

            SqlDataReader GetItemDataReader = GetItemCommand.ExecuteReader();

            if(GetItemDataReader.HasRows)
            {
                if(GetItemDataReader.Read())
                {
                    ActiveItem.ItemCode = (string)GetItemDataReader.GetValue(0);
                    ActiveItem.Description = (string)GetItemDataReader.GetValue(1);
                    ActiveItem.UnitPrice = (decimal)GetItemDataReader.GetValue(2);
                    ActiveItem.StockQuantity = (int)GetItemDataReader.GetValue(3);
                    ActiveItem.IsDeleted = (bool)GetItemDataReader.GetValue(4);
                }
            }

            GetItemDataReader.Close();
            apandit1.Close();

            return ActiveItem;
        }
        
        public Item GetActiveItem(string itemCode)
        {
            Item ActiveItem = new();

            SqlConnection apandit1 = new();
            apandit1.ConnectionString = _connectionString;
            apandit1.Open();

            SqlCommand GetItemCommand = new()
            {
                Connection = apandit1,
                CommandType = CommandType.StoredProcedure,
                CommandText = "BAIS3150FinalProject.GetSingleNonDeletedInventoryItem"
            };

            SqlParameter GetItemCommandParameter = new()
            {
                ParameterName = "@ItemCode",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = itemCode
            };
            GetItemCommand.Parameters.Add(GetItemCommandParameter);

            GetItemCommand.ExecuteNonQuery();

            SqlDataReader GetItemDataReader = GetItemCommand.ExecuteReader();

            if(GetItemDataReader.HasRows)
            {
                if(GetItemDataReader.Read())
                {
                    ActiveItem.ItemCode = (string)GetItemDataReader.GetValue(0);
                    ActiveItem.Description = (string)GetItemDataReader.GetValue(1);
                    ActiveItem.UnitPrice = (decimal)GetItemDataReader.GetValue(2);
                    ActiveItem.StockQuantity = (int)GetItemDataReader.GetValue(3);
                    ActiveItem.IsDeleted = (bool)GetItemDataReader.GetValue(4);
                }
            }

            GetItemDataReader.Close();
            apandit1.Close();

            return ActiveItem;
        }
    }
}
