using Microsoft.Data.SqlClient;
using System.Data;
using VehicleRentalSystem.Helpers;
using VehicleRentalSystem.Models;

namespace VehicleRentalSystem.Repositories;

public class CustomerRepository
{
    public async Task<int> AddAsync(Customer customer)
    {
        const string sql = """
            INSERT INTO Customers (FullName, Email, PhoneNumber)
            VALUES (@FullName, @Email, @PhoneNumber);
            SELECT CAST(SCOPE_IDENTITY() AS INT);
            """;

        await using SqlConnection connection = DatabaseHelper.CreateConnection();
        await using SqlCommand command = new(sql, connection);

        command.Parameters.Add("@FullName", SqlDbType.NVarChar, 100).Value =
            customer.FullName;
        command.Parameters.Add("@Email", SqlDbType.NVarChar, 255).Value =
            customer.Email;
        command.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 20).Value =
            customer.PhoneNumber;

        await connection.OpenAsync();
        object? result = await command.ExecuteScalarAsync();

        return Convert.ToInt32(result);
    }
    public async Task<List<Customer>> GetAllAsync()
    {
        const string sql = """
        SELECT CustomerId, FullName, Email, PhoneNumber
        FROM Customers
        ORDER BY CustomerId;
        """;

        List<Customer> customers = new List<Customer>();

        await using SqlConnection connection = DatabaseHelper.CreateConnection();
        await using SqlCommand command = new SqlCommand(sql, connection);

        await connection.OpenAsync();

        await using SqlDataReader reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            Customer customer = new Customer
            {
                CustomerId = reader.GetInt32(0),
                FullName = reader.GetString(1),
                Email = reader.GetString(2),
                PhoneNumber = reader.GetString(3)
            };

            customers.Add(customer);
        }

        return customers;
    }
}
