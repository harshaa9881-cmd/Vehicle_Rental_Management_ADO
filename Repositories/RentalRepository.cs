
using Microsoft.Data.SqlClient;
using System.Data;
using VehicleRentalSystem.Helpers;
using VehicleRentalSystem.Models;

namespace VehicleRentalSystem.Repositories;

public class RentalRepository
{
    public async Task<int> AddAsync(Rental rental)
    {
        const string sql = """
            INSERT INTO Rentals
            (CustomerId, VehicleId, RentalDate, ExpectedReturnDate,
             ActualReturnDate, TotalAmount, Status)
            VALUES
            (@CustomerId, @VehicleId, @RentalDate, @ExpectedReturnDate,
             @ActualReturnDate, @TotalAmount, @Status);

            SELECT CAST(SCOPE_IDENTITY() AS INT);
            """;

        await using SqlConnection connection = DatabaseHelper.CreateConnection();
        await using SqlCommand command = new SqlCommand(sql, connection);

        command.Parameters.Add("@CustomerId", SqlDbType.Int).Value =
            rental.CustomerId;
        command.Parameters.Add("@VehicleId", SqlDbType.Int).Value =
            rental.VehicleId;
        command.Parameters.Add("@RentalDate", SqlDbType.DateTime).Value =
            rental.RentalDate;
        command.Parameters.Add("@ExpectedReturnDate", SqlDbType.DateTime).Value =
            rental.ExpectedReturnDate;
        command.Parameters.Add("@ActualReturnDate", SqlDbType.DateTime).Value =
            rental.ActualReturnDate.HasValue
                ? rental.ActualReturnDate.Value
                : DBNull.Value;

        command.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value =
            rental.TotalAmount;
        command.Parameters["@TotalAmount"].Precision = 10;
        command.Parameters["@TotalAmount"].Scale = 2;

        command.Parameters.Add("@Status", SqlDbType.NVarChar, 20).Value =
            rental.Status;

        await connection.OpenAsync();

        object? result = await command.ExecuteScalarAsync();

        return Convert.ToInt32(result);
    }

    public async Task<List<Rental>> GetAllAsync()
    {
        const string sql = """
            SELECT RentalId, CustomerId, VehicleId, RentalDate,
                   ExpectedReturnDate, ActualReturnDate, TotalAmount, Status
            FROM Rentals
            ORDER BY RentalId;
            """;

        List<Rental> rentals = new List<Rental>();

        await using SqlConnection connection = DatabaseHelper.CreateConnection();
        await using SqlCommand command = new SqlCommand(sql, connection);

        await connection.OpenAsync();

        await using SqlDataReader reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            Rental rental = new Rental
            {
                RentalId = reader.GetInt32(0),
                CustomerId = reader.GetInt32(1),
                VehicleId = reader.GetInt32(2),
                RentalDate = reader.GetDateTime(3),
                ExpectedReturnDate = reader.GetDateTime(4),
                ActualReturnDate = reader.IsDBNull(5)
                    ? null
                    : reader.GetDateTime(5),
                TotalAmount = reader.GetDecimal(6),
                Status = reader.GetString(7)
            };

            rentals.Add(rental);
        }

        return rentals;
    }

    public async Task<Rental?> GetByIdAsync(int rentalId)
    {
        const string sql = """
            SELECT RentalId, CustomerId, VehicleId, RentalDate,
                   ExpectedReturnDate, ActualReturnDate, TotalAmount, Status
            FROM Rentals
            WHERE RentalId = @RentalId;
            """;

        await using SqlConnection connection = DatabaseHelper.CreateConnection();
        await using SqlCommand command = new SqlCommand(sql, connection);

        command.Parameters.Add("@RentalId", SqlDbType.Int).Value = rentalId;

        await connection.OpenAsync();

        await using SqlDataReader reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            Rental rental = new Rental
            {
                RentalId = reader.GetInt32(0),
                CustomerId = reader.GetInt32(1),
                VehicleId = reader.GetInt32(2),
                RentalDate = reader.GetDateTime(3),
                ExpectedReturnDate = reader.GetDateTime(4),
                ActualReturnDate = reader.IsDBNull(5)
                    ? null
                    : reader.GetDateTime(5),
                TotalAmount = reader.GetDecimal(6),
                Status = reader.GetString(7)
            };

            return rental;
        }

        return null;
    }

    public async Task<bool> UpdateAsync(Rental rental)
    {
        const string sql = """
            UPDATE Rentals
            SET CustomerId = @CustomerId,
                VehicleId = @VehicleId,
                RentalDate = @RentalDate,
                ExpectedReturnDate = @ExpectedReturnDate,
                ActualReturnDate = @ActualReturnDate,
                TotalAmount = @TotalAmount,
                Status = @Status
            WHERE RentalId = @RentalId;
            """;

        await using SqlConnection connection = DatabaseHelper.CreateConnection();
        await using SqlCommand command = new SqlCommand(sql, connection);

        command.Parameters.Add("@RentalId", SqlDbType.Int).Value =
            rental.RentalId;
        command.Parameters.Add("@CustomerId", SqlDbType.Int).Value =
            rental.CustomerId;
        command.Parameters.Add("@VehicleId", SqlDbType.Int).Value =
            rental.VehicleId;
        command.Parameters.Add("@RentalDate", SqlDbType.DateTime).Value =
            rental.RentalDate;
        command.Parameters.Add("@ExpectedReturnDate", SqlDbType.DateTime).Value =
            rental.ExpectedReturnDate;
        command.Parameters.Add("@ActualReturnDate", SqlDbType.DateTime).Value =
            rental.ActualReturnDate.HasValue
                ? rental.ActualReturnDate.Value
                : DBNull.Value;

        command.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value =
            rental.TotalAmount;
        command.Parameters["@TotalAmount"].Precision = 10;
        command.Parameters["@TotalAmount"].Scale = 2;

        command.Parameters.Add("@Status", SqlDbType.NVarChar, 20).Value =
            rental.Status;

        await connection.OpenAsync();

        int rowsAffected = await command.ExecuteNonQueryAsync();

        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(int rentalId)
    {
        const string sql = """
            DELETE FROM Rentals
            WHERE RentalId = @RentalId;
            """;

        await using SqlConnection connection = DatabaseHelper.CreateConnection();
        await using SqlCommand command = new SqlCommand(sql, connection);

        command.Parameters.Add("@RentalId", SqlDbType.Int).Value = rentalId;

        await connection.OpenAsync();

        int rowsAffected = await command.ExecuteNonQueryAsync();

        return rowsAffected > 0;
    }
}
