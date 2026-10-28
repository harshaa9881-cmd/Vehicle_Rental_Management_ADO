using VehicleRentalSystem.Models;
using VehicleRentalSystem.Repositories;
using VehicleRentalSystem.Services;

namespace VehicleRentalSystem;

class Program
{
    static async Task Main(string[] args)
    {
        CustomerRepository customerRepository = new CustomerRepository();
        VehicleRepository vehicleRepository = new VehicleRepository();
        RentalRepository rentalRepository = new RentalRepository();
        PaymentRepository paymentRepository = new PaymentRepository();

        CustomerService customerService = new CustomerService(customerRepository);
        VehicleService vehicleService = new VehicleService(vehicleRepository);
        RentalService rentalService = new RentalService(rentalRepository);
        PaymentService paymentService = new PaymentService(paymentRepository);

        bool running = true;

        while (running)
        {
            Console.WriteLine("\n===== VEHICLE RENTAL SYSTEM =====");
            Console.WriteLine("1. Manage Customers");
            Console.WriteLine("2. Manage Vehicles");
            Console.WriteLine("3. Manage Rentals");
            Console.WriteLine("4. Manage Payments");
            Console.WriteLine("0. Exit");
            Console.Write("Enter your choice: ");

            string? choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        await CustomerMenu(customerService);
                        break;

                    case "2":
                        await VehicleMenu(vehicleService);
                        break;

                    case "3":
                        await RentalMenu(rentalService);
                        break;

                    case "4":
                        await PaymentMenu(paymentService);
                        break;

                    case "0":
                        running = false;
                        Console.WriteLine("Exiting Vehicle Rental System...");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    static async Task CustomerMenu(CustomerService service)
    {
        Console.WriteLine("\n--- CUSTOMER MENU ---");
        Console.WriteLine("1. Add Customer");
        Console.WriteLine("2. View All Customers");
        Console.WriteLine("3. View Customer by ID");
        Console.WriteLine("4. Update Customer");
        Console.WriteLine("5. Delete Customer");
        Console.Write("Enter your choice: ");

        string? choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Customer customer = new Customer();

                Console.Write("Enter customer name: ");
                customer.FullName = Console.ReadLine() ?? "";

                Console.Write("Enter email: ");
                customer.Email = Console.ReadLine() ?? "";

                Console.Write("Enter phone number: ");
                customer.PhoneNumber = Console.ReadLine() ?? "";

                int customerId = await service.AddAsync(customer);
                Console.WriteLine($"Customer added successfully. ID: {customerId}");
                break;

            case "2":
                List<Customer> customers = await service.GetAllAsync();

                foreach (Customer item in customers)
                {
                    Console.WriteLine(
                        $"ID: {item.CustomerId}, Name: {item.FullName}, " +
                        $"Email: {item.Email}, Phone: {item.PhoneNumber}");
                }
                break;

            case "3":
                Console.Write("Enter customer ID: ");
                int searchCustomerId = int.Parse(Console.ReadLine() ?? "0");

                Customer? foundCustomer =
                    await service.GetByIdAsync(searchCustomerId);

                if (foundCustomer == null)
                    Console.WriteLine("Customer not found.");
                else
                    Console.WriteLine(
                        $"ID: {foundCustomer.CustomerId}, " +
                        $"Name: {foundCustomer.FullName}, " +
                        $"Email: {foundCustomer.Email}, " +
                        $"Phone: {foundCustomer.PhoneNumber}");
                break;

            case "4":
                Console.Write("Enter customer ID: ");
                int updateCustomerId = int.Parse(Console.ReadLine() ?? "0");

                Customer? updateCustomer =
                    await service.GetByIdAsync(updateCustomerId);

                if (updateCustomer == null)
                {
                    Console.WriteLine("Customer not found.");
                    break;
                }

                Console.Write("Enter new name: ");
                updateCustomer.FullName = Console.ReadLine() ?? "";

                Console.Write("Enter new email: ");
                updateCustomer.Email = Console.ReadLine() ?? "";

                Console.Write("Enter new phone number: ");
                updateCustomer.PhoneNumber = Console.ReadLine() ?? "";

                bool customerUpdated = await service.UpdateAsync(updateCustomer);
                Console.WriteLine(customerUpdated
                    ? "Customer updated successfully."
                    : "Customer update failed.");
                break;

            case "5":
                Console.Write("Enter customer ID: ");
                int deleteCustomerId = int.Parse(Console.ReadLine() ?? "0");

                bool customerDeleted = await service.DeleteAsync(deleteCustomerId);
                Console.WriteLine(customerDeleted
                    ? "Customer deleted successfully."
                    : "Customer not found.");
                break;

            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }

    static async Task VehicleMenu(VehicleService service)
    {
        Console.WriteLine("\n--- VEHICLE MENU ---");
        Console.WriteLine("1. Add Vehicle");
        Console.WriteLine("2. View All Vehicles");
        Console.WriteLine("3. View Vehicle by ID");
        Console.WriteLine("4. Update Vehicle");
        Console.WriteLine("5. Delete Vehicle");
        Console.Write("Enter your choice: ");

        string? choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Vehicle vehicle = new Vehicle();

                Console.Write("Enter registration number: ");
                vehicle.RegistrationNumber = Console.ReadLine() ?? "";

                Console.Write("Enter make: ");
                vehicle.Make = Console.ReadLine() ?? "";

                Console.Write("Enter model: ");
                vehicle.Model = Console.ReadLine() ?? "";

                Console.Write("Enter vehicle type: ");
                vehicle.VehicleType = Console.ReadLine() ?? "";

                Console.Write("Enter daily rate: ");
                vehicle.DailyRate = decimal.Parse(Console.ReadLine() ?? "0");

                vehicle.IsAvailable = true;
                vehicle.IsActive = true;

                int vehicleId = await service.AddAsync(vehicle);
                Console.WriteLine($"Vehicle added successfully. ID: {vehicleId}");
                break;

            case "2":
                List<Vehicle> vehicles = await service.GetAllAsync();

                foreach (Vehicle item in vehicles)
                {
                    Console.WriteLine(
                        $"ID: {item.VehicleId}, Registration: {item.RegistrationNumber}, " +
                        $"Make: {item.Make}, Model: {item.Model}, " +
                        $"Type: {item.VehicleType}, Daily Rate: {item.DailyRate}, " +
                        $"Available: {item.IsAvailable}, Active: {item.IsActive}");
                }
                break;

            case "3":
                Console.Write("Enter vehicle ID: ");
                int searchVehicleId = int.Parse(Console.ReadLine() ?? "0");

                Vehicle? foundVehicle = await service.GetByIdAsync(searchVehicleId);

                if (foundVehicle == null)
                    Console.WriteLine("Vehicle not found.");
                else
                    Console.WriteLine(
                        $"ID: {foundVehicle.VehicleId}, " +
                        $"Registration: {foundVehicle.RegistrationNumber}, " +
                        $"Make: {foundVehicle.Make}, Model: {foundVehicle.Model}, " +
                        $"Type: {foundVehicle.VehicleType}, " +
                        $"Daily Rate: {foundVehicle.DailyRate}");
                break;

            case "4":
                Console.Write("Enter vehicle ID: ");
                int updateVehicleId = int.Parse(Console.ReadLine() ?? "0");

                Vehicle? updateVehicle = await service.GetByIdAsync(updateVehicleId);

                if (updateVehicle == null)
                {
                    Console.WriteLine("Vehicle not found.");
                    break;
                }

                Console.Write("Enter registration number: ");
                updateVehicle.RegistrationNumber = Console.ReadLine() ?? "";

                Console.Write("Enter make: ");
                updateVehicle.Make = Console.ReadLine() ?? "";

                Console.Write("Enter model: ");
                updateVehicle.Model = Console.ReadLine() ?? "";

                Console.Write("Enter vehicle type: ");
                updateVehicle.VehicleType = Console.ReadLine() ?? "";

                Console.Write("Enter daily rate: ");
                updateVehicle.DailyRate = decimal.Parse(Console.ReadLine() ?? "0");

                Console.Write("Is available? (true/false): ");
                updateVehicle.IsAvailable =
                    bool.Parse(Console.ReadLine() ?? "false");

                Console.Write("Is active? (true/false): ");
                updateVehicle.IsActive =
                    bool.Parse(Console.ReadLine() ?? "false");

                bool vehicleUpdated = await service.UpdateAsync(updateVehicle);
                Console.WriteLine(vehicleUpdated
                    ? "Vehicle updated successfully."
                    : "Vehicle update failed.");
                break;

            case "5":
                Console.Write("Enter vehicle ID: ");
                int deleteVehicleId = int.Parse(Console.ReadLine() ?? "0");

                bool vehicleDeleted = await service.DeleteAsync(deleteVehicleId);
                Console.WriteLine(vehicleDeleted
                    ? "Vehicle deleted successfully."
                    : "Vehicle not found.");
                break;

            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }

    static async Task RentalMenu(RentalService service)
    {
        Console.WriteLine("\n--- RENTAL MENU ---");
        Console.WriteLine("1. Add Rental");
        Console.WriteLine("2. View All Rentals");
        Console.WriteLine("3. View Rental by ID");
        Console.WriteLine("4. Update Rental");
        Console.WriteLine("5. Delete Rental");
        Console.WriteLine("6. Return Vehicle");
        Console.Write("Enter your choice: ");

        string? choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Rental rental = new Rental();

                Console.Write("Enter customer ID: ");
                rental.CustomerId = int.Parse(Console.ReadLine() ?? "0");

                Console.Write("Enter vehicle ID: ");
                rental.VehicleId = int.Parse(Console.ReadLine() ?? "0");

                rental.RentalDate = DateTime.Today;

                Console.Write("Enter expected return date (yyyy-MM-dd): ");
                rental.ExpectedReturnDate =
                    DateTime.Parse(Console.ReadLine() ?? "");

                Console.Write("Enter total amount: ");
                rental.TotalAmount = decimal.Parse(Console.ReadLine() ?? "0");

                int rentalId = await service.AddAsync(rental);
                Console.WriteLine($"Rental added successfully. ID: {rentalId}");
                break;

            case "2":
                List<Rental> rentals = await service.GetAllAsync();

                foreach (Rental item in rentals)
                {
                    Console.WriteLine(
                        $"Rental ID: {item.RentalId}, Customer ID: {item.CustomerId}, " +
                        $"Vehicle ID: {item.VehicleId}, Rental Date: {item.RentalDate:yyyy-MM-dd}, " +
                        $"Expected Return: {item.ExpectedReturnDate:yyyy-MM-dd}, " +
                        $"Actual Return: {item.ActualReturnDate?.ToString("yyyy-MM-dd") ?? "Not returned"}, " +
                        $"Amount: {item.TotalAmount}, Status: {item.Status}");
                }
                break;

            case "3":
                Console.Write("Enter rental ID: ");
                int searchRentalId = int.Parse(Console.ReadLine() ?? "0");

                Rental? foundRental = await service.GetByIdAsync(searchRentalId);

                if (foundRental == null)
                    Console.WriteLine("Rental not found.");
                else
                    Console.WriteLine(
                        $"Rental ID: {foundRental.RentalId}, " +
                        $"Customer ID: {foundRental.CustomerId}, " +
                        $"Vehicle ID: {foundRental.VehicleId}, " +
                        $"Status: {foundRental.Status}, " +
                        $"Amount: {foundRental.TotalAmount}");
                break;

            case "4":
                Console.Write("Enter rental ID: ");
                int updateRentalId = int.Parse(Console.ReadLine() ?? "0");

                Rental? updateRental = await service.GetByIdAsync(updateRentalId);

                if (updateRental == null)
                {
                    Console.WriteLine("Rental not found.");
                    break;
                }

                Console.Write("Enter customer ID: ");
                updateRental.CustomerId = int.Parse(Console.ReadLine() ?? "0");

                Console.Write("Enter vehicle ID: ");
                updateRental.VehicleId = int.Parse(Console.ReadLine() ?? "0");

                Console.Write("Enter rental date (yyyy-MM-dd): ");
                updateRental.RentalDate =
                    DateTime.Parse(Console.ReadLine() ?? "");

                Console.Write("Enter expected return date (yyyy-MM-dd): ");
                updateRental.ExpectedReturnDate =
                    DateTime.Parse(Console.ReadLine() ?? "");

                Console.Write("Enter total amount: ");
                updateRental.TotalAmount =
                    decimal.Parse(Console.ReadLine() ?? "0");

                Console.Write("Enter status: ");
                updateRental.Status = Console.ReadLine() ?? "Active";

                bool rentalUpdated = await service.UpdateAsync(updateRental);
                Console.WriteLine(rentalUpdated
                    ? "Rental updated successfully."
                    : "Rental update failed.");
                break;

            case "5":
                Console.Write("Enter rental ID: ");
                int deleteRentalId = int.Parse(Console.ReadLine() ?? "0");

                bool rentalDeleted = await service.DeleteAsync(deleteRentalId);
                Console.WriteLine(rentalDeleted
                    ? "Rental deleted successfully."
                    : "Rental not found.");
                break;

            case "6":
                Console.Write("Enter rental ID to return: ");
                int returnRentalId = int.Parse(Console.ReadLine() ?? "0");

                bool returned = await service.ReturnVehicleAsync(returnRentalId);
                Console.WriteLine(returned
                    ? "Vehicle return recorded successfully."
                    : "Rental not found.");
                break;

            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }

    static async Task PaymentMenu(PaymentService service)
    {
        Console.WriteLine("\n--- PAYMENT MENU ---");
        Console.WriteLine("1. Add Payment");
        Console.WriteLine("2. View All Payments");
        Console.WriteLine("3. View Payment by ID");
        Console.WriteLine("4. View Payments by Rental ID");
        Console.WriteLine("5. Update Payment");
        Console.WriteLine("6. Delete Payment");
        Console.Write("Enter your choice: ");

        string? choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Payment payment = new Payment();

                Console.Write("Enter rental ID: ");
                payment.RentalId = int.Parse(Console.ReadLine() ?? "0");

                Console.Write("Enter amount: ");
                payment.Amount = decimal.Parse(Console.ReadLine() ?? "0");

                Console.Write("Enter payment method: ");
                payment.PaymentMethod = Console.ReadLine() ?? "";

                payment.PaymentDate = DateTime.Now;
                payment.Status = "Successful";

                int paymentId = await service.AddAsync(payment);
                Console.WriteLine($"Payment added successfully. ID: {paymentId}");
                break;

            case "2":
                List<Payment> payments = await service.GetAllAsync();

                foreach (Payment item in payments)
                {
                    Console.WriteLine(
                        $"Payment ID: {item.PaymentId}, Rental ID: {item.RentalId}, " +
                        $"Amount: {item.Amount}, Date: {item.PaymentDate:yyyy-MM-dd}, " +
                        $"Method: {item.PaymentMethod}, Status: {item.Status}");
                }
                break;

            case "3":
                Console.Write("Enter payment ID: ");
                int searchPaymentId = int.Parse(Console.ReadLine() ?? "0");

                Payment? foundPayment = await service.GetByIdAsync(searchPaymentId);

                if (foundPayment == null)
                    Console.WriteLine("Payment not found.");
                else
                    Console.WriteLine(
                        $"Payment ID: {foundPayment.PaymentId}, " +
                        $"Rental ID: {foundPayment.RentalId}, " +
                        $"Amount: {foundPayment.Amount}, " +
                        $"Method: {foundPayment.PaymentMethod}, " +
                        $"Status: {foundPayment.Status}");
                break;

            case "4":
                Console.Write("Enter rental ID: ");
                int searchPaymentRentalId = int.Parse(Console.ReadLine() ?? "0");

                List<Payment> rentalPayments =
                    await service.GetByRentalIdAsync(searchPaymentRentalId);

                foreach (Payment item in rentalPayments)
                {
                    Console.WriteLine(
                        $"Payment ID: {item.PaymentId}, Amount: {item.Amount}, " +
                        $"Date: {item.PaymentDate:yyyy-MM-dd}, " +
                        $"Method: {item.PaymentMethod}, Status: {item.Status}");
                }
                break;

            case "5":
                Console.Write("Enter payment ID: ");
                int updatePaymentId = int.Parse(Console.ReadLine() ?? "0");

                Payment? updatePayment =
                    await service.GetByIdAsync(updatePaymentId);

                if (updatePayment == null)
                {
                    Console.WriteLine("Payment not found.");
                    break;
                }

                Console.Write("Enter rental ID: ");
                updatePayment.RentalId = int.Parse(Console.ReadLine() ?? "0");

                Console.Write("Enter amount: ");
                updatePayment.Amount = decimal.Parse(Console.ReadLine() ?? "0");

                Console.Write("Enter payment method: ");
                updatePayment.PaymentMethod = Console.ReadLine() ?? "";

                Console.Write("Enter status (Successful/Pending/Failed): ");
                updatePayment.Status = Console.ReadLine() ?? "";

                bool paymentUpdated = await service.UpdateAsync(updatePayment);
                Console.WriteLine(paymentUpdated
                    ? "Payment updated successfully."
                    : "Payment update failed.");
                break;

            case "6":
                Console.Write("Enter payment ID: ");
                int deletePaymentId = int.Parse(Console.ReadLine() ?? "0");

                bool paymentDeleted = await service.DeleteAsync(deletePaymentId);
                Console.WriteLine(paymentDeleted
                    ? "Payment deleted successfully."
                    : "Payment not found.");
                break;

            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }
}