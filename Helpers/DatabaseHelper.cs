using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
namespace VehicleRentalSystem.Helpers;

public static class DatabaseHelper
{
    private const string ConnectionString =
        @"Server=.\SQLEXPRESS;Database=VehicleRentalDB;" +
        @"Trusted_Connection=True;TrustServerCertificate=True;";

    public static SqlConnection CreateConnection()
    {
        return new SqlConnection(ConnectionString);
    }
}