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
}
