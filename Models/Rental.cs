using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleRentalSystem.Models;

public class Rental
{
    public int RentalId { get; set; }
    public int CustomerId { get; set; }
    public int VehicleId { get; set; }
    public DateTime RentalDate { get; set; }
    public DateTime ExpectedReturnDate { get; set; }
    public DateTime? ActualReturnDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "Active";
}
