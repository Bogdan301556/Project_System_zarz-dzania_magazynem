using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class Warehouse
{
    private string connectionString = "Server=OLEKSANDRVY\\OLEKSANDRVY;Database=WarehouseDB;Integrated Security=True;";

    public void AddProduct(int id, string name, int quantity)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Products (Name, Quantity) VALUES (@Name, @Quantity)";
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Quantity", quantity);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public void UpdateProductQuantity(int productId, int quantity)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            string query = "UPDATE Products SET Quantity = @Quantity WHERE Id = @Id";
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Quantity", quantity);
            command.Parameters.AddWithValue("@Id", productId);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public void Incoming(int productId, int quantity)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            string query = "UPDATE Products SET Quantity = Quantity + @Quantity WHERE Id = @ProductId";
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Quantity", quantity);
            command.Parameters.AddWithValue("@ProductId", productId);

            connection.Open();
            command.ExecuteNonQuery();

           
            SaveOperation(productId, OperationType.Incoming, quantity);
        }
    }

    public void Outgoing(int productId, int quantity)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            string query = "UPDATE Products SET Quantity = Quantity - @Quantity WHERE Id = @ProductId";
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Quantity", quantity);
            command.Parameters.AddWithValue("@ProductId", productId);

            connection.Open();
            command.ExecuteNonQuery();

         
            SaveOperation(productId, OperationType.Outgoing, quantity);
        }
    }

    private void SaveOperation(int productId, OperationType type, int quantity)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Operations (ProductId, OperationType, Quantity, Date) VALUES (@ProductId, @OperationType, @Quantity, @Date)";
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ProductId", productId);
            command.Parameters.AddWithValue("@OperationType", type.ToString());
            command.Parameters.AddWithValue("@Quantity", quantity);
            command.Parameters.AddWithValue("@Date", DateTime.Now);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public List<Product> GetAllProducts()
    {
        var products = new List<Product>();

        using (var connection = new SqlConnection(connectionString))
        {
            string query = "SELECT Id, Name, Quantity FROM Products";
            var command = new SqlCommand(query, connection);

            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var product = new Product(
                        (int)reader["Id"],
                        (string)reader["Name"],
                        (int)reader["Quantity"]
                    );
                    products.Add(product);
                }
            }
        }

        return products;
    }

    public List<Operation> GetAllOperations()
    {
        var operations = new List<Operation>();

        using (var connection = new SqlConnection(connectionString))
        {
            string query = "SELECT ProductId, OperationType, Quantity, Date FROM Operations";
            var command = new SqlCommand(query, connection);

            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var operation = new Operation(
                        (int)reader["ProductId"],
                        (OperationType)Enum.Parse(typeof(OperationType), (string)reader["OperationType"]),
                        (int)reader["Quantity"]
                    );
                    operations.Add(operation);
                }
            }
        }

        return operations;
    }
}
