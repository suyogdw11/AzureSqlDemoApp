using AzureSqlDemoApp.Models;
using System.Data.SqlClient;

namespace AzureSqlDemoApp.Services
{
    public class ProductService
    {
        public static string db_source = "sqldbapp.database.windows.net";
        public static string db_user = "sqladmin";
        public static string db_password = "1likePoland.com";
        public static string db_database = "sqldb";

        private SqlConnection GetConnection() 
        {
            var _builder = new SqlConnectionStringBuilder();
            _builder.DataSource = db_source;
            _builder.UserID = db_user;
            _builder.Password = db_password;
            _builder.InitialCatalog = db_database;
            return new SqlConnection(_builder.ConnectionString);
        }

        public List<Product> GetProducts()
        {
            SqlConnection conn = GetConnection();
            List<Product> products = new List<Product>();
            string statement = "Select ProductId,ProductName,Quantity from Products;";
            conn.Open();
            SqlCommand cmd = new SqlCommand(statement, conn);
            using (SqlDataReader reader = cmd.ExecuteReader()) 
            {
                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductId = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2)
                    };
                    products.Add(product);
                }
            }
            conn.Close();
            return products;
        }
    }
}
