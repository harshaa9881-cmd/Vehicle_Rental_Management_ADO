using Microsoft.Data.SqlClient;
using System.Data;
using VehicleRentalSystem.Helpers;
using VehicleRentalSystem.Models;

namespace VehicleRentalSystem.Repositories;

public class PaymentRepository
{
    public async Task<int> AddAsync(Payment payment)
    {
        const string sql = """
            INSERT INTO Payments
            (RentalId, Amount, PaymentDate, PaymentMethod, Status)
            VALUES
            (@RentalId, @Amount, @PaymentDate, @PaymentMethod, @Status);

            SELECT CAST(SCOPE_IDENTITY() AS INT);
            """;

        await using SqlConnection connection = DatabaseHelper.CreateConnection();
        await using SqlCommand command = new SqlCommand(sql, connection);

        command.Parameters.Add("@RentalId", SqlDbType.Int).Value =
            payment.RentalId;

        command.Parameters.Add("@Amount", SqlDbType.Decimal).Value =
            payment.Amount;
        command.Parameters["@Amount"].Precision = 10;
        command.Parameters["@Amount"].Scale = 2;

        command.Parameters.Add("@PaymentDate", SqlDbType.DateTime).Value =
            payment.PaymentDate;

        command.Parameters.Add("@PaymentMethod", SqlDbType.NVarChar, 50).Value =
            payment.PaymentMethod;

        command.Parameters.Add("@Status", SqlDbType.NVarChar, 20).Value =
            payment.Status;

        await connection.OpenAsync();

        object? result = await command.ExecuteScalarAsync();

        return Convert.ToInt32(result);
    }

    public async Task<List<Payment>> GetAllAsync()
    {
        const string sql = """
            SELECT PaymentId, RentalId, Amount, PaymentDate, PaymentMethod, Status
            FROM Payments
            ORDER BY PaymentId;
            """;

        List<Payment> payments = new List<Payment>();

        await using SqlConnection connection = DatabaseHelper.CreateConnection();
        await using SqlCommand command = new SqlCommand(sql, connection);

        await connection.OpenAsync();

        await using SqlDataReader reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            Payment payment = new Payment
            {
                PaymentId = reader.GetInt32(0),
                RentalId = reader.GetInt32(1),
                Amount = reader.GetDecimal(2),
                PaymentDate = reader.GetDateTime(3),
                PaymentMethod = reader.GetString(4),
                Status = reader.GetString(5)
            };

            payments.Add(payment);
        }

        return payments;
    }

    public async Task<Payment?> GetByIdAsync(int paymentId)
    {
        const string sql = """
            SELECT PaymentId, RentalId, Amount, PaymentDate, PaymentMethod, Status
            FROM Payments
            WHERE PaymentId = @PaymentId;
            """;

        await using SqlConnection connection = DatabaseHelper.CreateConnection();
        await using SqlCommand command = new SqlCommand(sql, connection);

        command.Parameters.Add("@PaymentId", SqlDbType.Int).Value = paymentId;

        await connection.OpenAsync();

        await using SqlDataReader reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            Payment payment = new Payment
            {
                PaymentId = reader.GetInt32(0),
                RentalId = reader.GetInt32(1),
                Amount = reader.GetDecimal(2),
                PaymentDate = reader.GetDateTime(3),
                PaymentMethod = reader.GetString(4),
                Status = reader.GetString(5)
            };

            return payment;
        }

        return null;
    }

    public async Task<List<Payment>> GetByRentalIdAsync(int rentalId)
    {
        const string sql = """
            SELECT PaymentId, RentalId, Amount, PaymentDate, PaymentMethod, Status
            FROM Payments
            WHERE RentalId = @RentalId
            ORDER BY PaymentId;
            """;

        List<Payment> payments = new List<Payment>();

        await using SqlConnection connection = DatabaseHelper.CreateConnection();
        await using SqlCommand command = new SqlCommand(sql, connection);

        command.Parameters.Add("@RentalId", SqlDbType.Int).Value = rentalId;

        await connection.OpenAsync();

        await using SqlDataReader reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            Payment payment = new Payment
            {
                PaymentId = reader.GetInt32(0),
                RentalId = reader.GetInt32(1),
                Amount = reader.GetDecimal(2),
                PaymentDate = reader.GetDateTime(3),
                PaymentMethod = reader.GetString(4),
                Status = reader.GetString(5)
            };

            payments.Add(payment);
        }

        return payments;
    }
}