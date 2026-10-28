
using Microsoft.Data.SqlClient;
using System.Data;
using VehicleRentalSystem.Helpers;
using VehicleRentalSystem.Models;

namespace VehicleRentalSystem.Repositories;

public class VehicleRepository
{
    public async Task<int> AddAsync(Vehicle vehicle)
    {
        const string sql = """
            INSERT INTO Vehicles
            (RegistrationNumber, Make, Model, VehicleType, DailyRate, IsAvailable, IsActive)
            VALUES
            (@RegistrationNumber, @Make, @Model, @VehicleType, @DailyRate, @IsAvailable, @IsActive);

            SELECT CAST(SCOPE_IDENTITY() AS INT);
            """;

        await using SqlConnection connection = DatabaseHelper.CreateConnection();
        await using SqlCommand command = new SqlCommand(sql, connection);

        command.Parameters.Add("@RegistrationNumber", SqlDbType.NVarChar, 20).Value =
            vehicle.RegistrationNumber;
        command.Parameters.Add("@Make", SqlDbType.NVarChar, 50).Value =
            vehicle.Make;
        command.Parameters.Add("@Model", SqlDbType.NVarChar, 50).Value =
            vehicle.Model;
        command.Parameters.Add("@VehicleType", SqlDbType.NVarChar, 50).Value =
            vehicle.VehicleType;
        command.Parameters.Add("@DailyRate", SqlDbType.Decimal).Value =
            vehicle.DailyRate;
        command.Parameters.Add("@IsAvailable", SqlDbType.Bit).Value =
            vehicle.IsAvailable;
        command.Parameters.Add("@IsActive", SqlDbType.Bit).Value =
            vehicle.IsActive;

        command.Parameters["@DailyRate"].Precision = 10;
        command.Parameters["@DailyRate"].Scale = 2;

        await connection.OpenAsync();

        object? result = await command.ExecuteScalarAsync();

        return Convert.ToInt32(result);
    }

    public async Task<List<Vehicle>> GetAllAsync()
    {
        const string sql = """
        SELECT VehicleId, RegistrationNumber, Make, Model,
               VehicleType, DailyRate, IsAvailable, IsActive
        FROM Vehicles
        ORDER BY VehicleId;
        """;

        List<Vehicle> vehicles = new List<Vehicle>();

        await using SqlConnection connection = DatabaseHelper.CreateConnection();
        await using SqlCommand command = new SqlCommand(sql, connection);

        await connection.OpenAsync();

        await using SqlDataReader reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            Vehicle vehicle = new Vehicle
            {
                VehicleId = reader.GetInt32(0),
                RegistrationNumber = reader.GetString(1),
                Make = reader.GetString(2),
                Model = reader.GetString(3),
                VehicleType = reader.GetString(4),
                DailyRate = reader.GetDecimal(5),
                IsAvailable = reader.GetBoolean(6),
                IsActive = reader.GetBoolean(7)
            };

            vehicles.Add(vehicle);
        }

        return vehicles;
    }

    public async Task<Vehicle?> GetByIdAsync(int vehicleId)
    {
        const string sql = """
        SELECT VehicleId, RegistrationNumber, Make, Model,
               VehicleType, DailyRate, IsAvailable, IsActive
        FROM Vehicles
        WHERE VehicleId = @VehicleId;
        """;

        await using SqlConnection connection = DatabaseHelper.CreateConnection();
        await using SqlCommand command = new SqlCommand(sql, connection);

        command.Parameters.Add("@VehicleId", SqlDbType.Int).Value = vehicleId;

        await connection.OpenAsync();

        await using SqlDataReader reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            Vehicle vehicle = new Vehicle
            {
                VehicleId = reader.GetInt32(0),
                RegistrationNumber = reader.GetString(1),
                Make = reader.GetString(2),
                Model = reader.GetString(3),
                VehicleType = reader.GetString(4),
                DailyRate = reader.GetDecimal(5),
                IsAvailable = reader.GetBoolean(6),
                IsActive = reader.GetBoolean(7)
            };

            return vehicle;
        }

        return null;
    }


}
