using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using System.Data.SqlClient;
using System.Data;

namespace I_fucking_hate_this_class
{
    internal class DataBaseHandler
    {
        private static string connectionString = @"Server=TIREMACHINE\SQLEXPRESS;Database=InventoryDB;Trusted_Connection=True;";


        public static void Initialize()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                EnsureTableExists(conn, "Products", @"
                CREATE TABLE Products (
                    ProductID INT PRIMARY KEY IDENTITY,
                    Name NVARCHAR(100) NOT NULL,
                    Description NVARCHAR(255),
                    Price DECIMAL(18,2) NOT NULL,
                    QuantityInStock INT NOT NULL,
                    Category NVARCHAR(50)
                )");

                EnsureTableExists(conn, "Customers", @"
                CREATE TABLE Customers (
                    CustomerID INT PRIMARY KEY IDENTITY,
                    Name NVARCHAR(100) NOT NULL,
                    Email NVARCHAR(100),
                    Phone NVARCHAR(20)
                )");

                EnsureTableExists(conn, "Sales", @"
                CREATE TABLE Sales (
                    SaleID INT PRIMARY KEY IDENTITY,
                    CustomerID INT FOREIGN KEY REFERENCES Customers(CustomerID),
                    SaleDate DATETIME NOT NULL,
                    TotalAmount DECIMAL(18,2) NOT NULL
                )");

                EnsureTableExists(conn, "SaleItems", @"
                CREATE TABLE SaleItems (
                    SaleItemID INT PRIMARY KEY IDENTITY,
                    SaleID INT FOREIGN KEY REFERENCES Sales(SaleID),
                    ProductID INT FOREIGN KEY REFERENCES Products(ProductID),
                    Quantity INT NOT NULL,
                    UnitPrice DECIMAL(18,2) NOT NULL
                )");
            }
        }

        private static void EnsureTableExists(SqlConnection conn, string tableName, string createSql)
        {
            string checkSql = $@"
            IF NOT EXISTS (
                SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}'
            )
            BEGIN
                {createSql}
            END";

            using (SqlCommand cmd = new SqlCommand(checkSql, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
}
