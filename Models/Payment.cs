using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleRentalSystem.Models;

public class Payment
{
    public int PaymentId { get; set; }
    public int RentalId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public string Status { get; set; } = "Successful";
}
