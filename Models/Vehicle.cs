using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleRentalSystem.Models;

public class Vehicle
{
    public int VehicleId { get; set; }
    public string RegistrationNumber { get; set; } = string.Empty;
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string VehicleType { get; set; } = string.Empty;
    public decimal DailyRate { get; set; }
    public bool IsAvailable { get; set; }
    public bool IsActive { get; set; }
}