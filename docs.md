# Vehicle Rental System — Commit-by-Commit Implementation Log

**Technology:** C# Console Application, .NET 10, ADO.NET, SQL Server Express (`.\SQLEXPRESS`)

**Models:** Customer, Vehicle, Rental, Payment

**Services:** CustomerService, VehicleService, RentalService, PaymentService

---

## Commit 1: Create the Console Project

**Commit message:** `Create Vehicle Rental System console project`

### Objective
Create the initial .NET 10 console application and verify that the project builds successfully.

### Steps
1. Create the console application.
2. Navigate into the project directory.
3. Verify the generated project structure.
4. Build the project.

### Commands
```powershell
dotnet new console -n VehicleRentalSystem -f net10.0
cd VehicleRentalSystem
dotnet build
git init
git status
git add .
git commit -m "Create Vehicle Rental System console project"
```

If the project is already inside an existing Git repository, do not run `git init` again.

### Files
- `Program.cs`
- `VehicleRentalSystem.csproj`
- Generated project files

### Expected result
- The application targets .NET 10.
- The project builds successfully.
- The initial project is committed to Git.

---

## Commit 2: Add ADO.NET Database Connectivity

**Commit message:** `Add ADO.NET database connection helper`

### Objective
Install the SQL Server data provider and create a shared helper for database connections.

### Steps
1. Install `Microsoft.Data.SqlClient`.
2. Create the `Helpers` folder.
3. Add `DatabaseHelper.cs`.
4. Configure the connection string for `VehicleRentalDB`.
5. Create a method that returns a new `SqlConnection`.

### Commands
```powershell
dotnet add package Microsoft.Data.SqlClient
dotnet build
```

### Files
- `VehicleRentalSystem.csproj`
- `Helpers/DatabaseHelper.cs`

### Expected result
- The package installs successfully.
- The connection helper compiles.
- The application can create a connection object.
- Actual database connectivity can be verified after creating the database.

---

## Commit 3: Create the SQL Server Database

**Commit message:** `Add Vehicle Rental System database schema`

### Objective
Create the database and the four tables required by the application.

### Steps
1. Create `VehicleRentalDB` in SQL Server.
2. Create the `Customers` table.
3. Create the `Vehicles` table.
4. Create the `Rentals` table.
5. Create the `Payments` table.
6. Add primary keys, foreign keys, unique constraints, default values, and basic check constraints.
7. Insert sample customers and vehicles.
8. Verify the tables and sample records in SSMS.

### Files
- `Database/VehicleRentalDB.sql`

### Expected result
- All four tables exist.
- `Rentals` references `Customers` and `Vehicles`.
- `Payments` references `Rentals` through `RentalId`.
- Duplicate customer emails and vehicle registration numbers are rejected.
- Invalid negative rates and invalid payment amounts are rejected by database constraints.

---

## Commit 4: Create the Four Model Classes

**Commit message:** `Add customer vehicle rental and payment models`

### Objective
Create the four classes that represent the application's data.

### Steps
1. Create the `Models` folder.
2. Add `Customer.cs` with customer details.
3. Add `Vehicle.cs` with vehicle details and availability.
4. Add `Rental.cs` with customer ID, vehicle ID, rental dates, total amount, and status.
5. Add `Payment.cs` with rental ID, amount, payment date, payment method, and status.
6. Build the project.

### Files
- `Models/Customer.cs`
- `Models/Vehicle.cs`
- `Models/Rental.cs`
- `Models/Payment.cs`

### Expected result
- Exactly four domain model classes are present.
- Model properties correspond to the database columns.
- The project builds successfully.
- SQL queries and business logic are not placed inside model classes.

---

## Commit 5: Implement Customer Insert and List Operations

**Commit message:** `Implement customer insert and list operations`

### Objective
Create the repository methods to insert and retrieve customers using ADO.NET.

### Steps
1. Create `CustomerRepository.cs` inside `Repositories`.
2. Implement `AddAsync(Customer customer)`.
3. Use a parameterized `INSERT` statement.
4. Retrieve the generated `CustomerId` using `SCOPE_IDENTITY()`.
5. Implement `GetAllAsync()`.
6. Use `SqlDataReader` to map database rows to `Customer` objects.

### Files
- `Repositories/CustomerRepository.cs`

### Expected result
- A customer can be inserted into SQL Server.
- The generated customer ID is returned.
- Customer records can be listed.
- User-provided values are passed through SQL parameters.

---

## Commit 6: Implement Customer Service and CRUD Operations

**Commit message:** `Add customer validation and management operations`

### Objective
Add basic customer validation and complete the required customer management operations.

### Steps
1. Create `CustomerService.cs`.
2. Inject or assign a `CustomerRepository` instance.
3. Validate that the customer's name, email, and phone number are not blank.
4. Implement customer lookup by ID.
5. Implement customer update.
6. Implement customer deactivation by setting `IsActive` to `0`.
7. Handle nonexistent customer IDs.

### Files
- `Services/CustomerService.cs`
- `Repositories/CustomerRepository.cs`

### Expected result
- Customers can be added, listed, searched, updated, and deactivated.
- Blank required fields are rejected.
- Duplicate emails are handled with a user-friendly error message.
- Existing rental history is preserved when a customer is deactivated.

---

## Commit 7: Implement Vehicle Insert and List Operations

**Commit message:** `Implement vehicle insert and list operations`

### Objective
Create repository operations for adding vehicles and retrieving vehicle records.

### Steps
1. Create `VehicleRepository.cs`.
2. Implement vehicle insertion with SQL parameters.
3. Return the generated `VehicleId`.
4. Implement listing all vehicles.
5. Implement listing available vehicles.
6. Filter available vehicles using `IsActive = 1` and `IsAvailable = 1`.

### Files
- `Repositories/VehicleRepository.cs`

### Expected result
- Vehicles can be added and listed.
- Available-vehicle results exclude inactive and unavailable vehicles.
- Duplicate registration numbers are rejected.

---

## Commit 8: Implement Vehicle Service and CRUD Operations

**Commit message:** `Add vehicle validation and management operations`

### Objective
Add basic validation and complete vehicle management operations.

### Steps
1. Create `VehicleService.cs`.
2. Validate required registration number, make, model, and vehicle type.
3. Reject negative daily rental rates.
4. Implement vehicle lookup by ID.
5. Implement vehicle update.
6. Implement vehicle deactivation.
7. Ensure deactivated vehicles are not offered for new rentals.

### Files
- `Services/VehicleService.cs`
- `Repositories/VehicleRepository.cs`

### Expected result
- Vehicles can be added, listed, searched, updated, and deactivated.
- Negative daily rates are rejected.
- Deactivated vehicles do not appear in the available-vehicle list.
- Vehicle records are retained when rental history exists.

---

## Commit 9: Implement Rental Booking with a Transaction

**Commit message:** `Implement rental booking with transaction`

### Objective
Implement the database operation that creates a rental and marks its vehicle unavailable.

### Steps
1. Create `RentalRepository.cs`.
2. Implement the rental insertion operation.
3. Open one SQL connection and begin a transaction.
4. Insert the rental record.
5. Update the corresponding vehicle to `IsAvailable = 0`.
6. Check whether the vehicle update affected a row.
7. Commit only when both operations succeed.
8. Roll back if either operation fails.

### Files
- `Repositories/RentalRepository.cs`

### Expected result
- A rental record is created for a successful booking.
- The rented vehicle becomes unavailable.
- An unavailable vehicle cannot be booked successfully.
- If the vehicle update fails, the rental insert is rolled back.
- The database does not retain a partially completed booking.

---

## Commit 10: Implement Rental Service and Rental History

**Commit message:** `Add rental validation and listing operations`

### Objective
Add the service-level checks and rental retrieval operations.

### Steps
1. Create `RentalService.cs`.
2. Verify that the customer exists and is active.
3. Verify that the vehicle exists, is active, and is available.
4. Validate the expected return date.
5. Calculate the rental amount using the vehicle's daily rate and the chosen billable-day rule.
6. Implement listing all rentals.
7. Implement rental lookup by ID.
8. Implement rental history by customer ID.

For the initial implementation, count both the pickup date and expected return date as billable days.

### Files
- `Services/RentalService.cs`
- `Repositories/RentalRepository.cs`

### Expected result
- Invalid customer and vehicle IDs are rejected.
- Inactive customers cannot create new rentals.
- Unavailable vehicles cannot be rented.
- Rental totals are calculated consistently.
- Rental history can be retrieved for a selected customer.

---

## Commit 11: Implement Rental Return

**Commit message:** `Implement rental return and vehicle availability update`

### Objective
Allow an active rental to be returned and make the associated vehicle available again.

### Steps
1. Find the rental using its ID.
2. Verify that the rental status is `Active`.
3. Set `ActualReturnDate`.
4. Change the rental status to `Returned`.
5. Set the vehicle's `IsAvailable` value to `1`.
6. Perform both database updates within one transaction.
7. Roll back both updates if either operation fails.

### Files
- `Services/RentalService.cs`
- `Repositories/RentalRepository.cs`

### Expected result
- An active rental can be returned.
- The rental status and actual return date are updated.
- The associated vehicle becomes available.
- A rental cannot be returned twice.
- Failed updates do not leave the rental and vehicle in inconsistent states.

---

## Commit 12: Implement Payment Repository

**Commit message:** `Implement payment recording and history`

### Objective
Add the database operations required to record payments and retrieve payment records.

### Steps
1. Create `PaymentRepository.cs`.
2. Implement payment insertion with SQL parameters.
3. Retrieve the generated `PaymentId`.
4. Implement listing all payments.
5. Implement payment lookup by ID.
6. Implement listing payments by `RentalId`.
7. Map the database rows to `Payment` objects.

### Files
- `Repositories/PaymentRepository.cs`

### Expected result
- A payment can be recorded for an existing rental.
- The generated payment ID is returned.
- Payment records can be listed and searched.
- Payment history can be filtered by rental ID.
- Invalid rental references and non-positive amounts are rejected by database constraints.

---

## Commit 13: Implement Payment Service

**Commit message:** `Add payment validation and service operations`

### Objective
Add basic validation and business operations for payments.

### Steps
1. Create `PaymentService.cs`.
2. Connect it to `PaymentRepository`.
3. Verify that `RentalId` is valid.
4. Verify that the rental exists.
5. Reject payment amounts that are zero or negative.
6. Require a payment method.
7. Record successful simulated payments using the `Successful` status.
8. Implement service methods for listing payments and viewing payment history.

### Files
- `Services/PaymentService.cs`
- `Repositories/PaymentRepository.cs`

### Expected result
- Valid payments can be recorded.
- Invalid rental IDs are rejected.
- Non-positive amounts and blank payment methods are rejected.
- Payments can be viewed by rental.
- No real payment gateway or card processing is performed.

---

## Commit 14: Implement Reports

**Commit message:** `Add vehicle rental reports`

### Objective
Provide basic reports using SQL queries.

### Steps
1. Add a query for available vehicles.
2. Add a query showing rentals with customer and vehicle details.
3. Add a query showing rental history for a customer.
4. Add a query counting rentals per vehicle.
5. Add a query listing payments for a rental.
6. Map query results into suitable model objects or display them through the service layer.

### Files
- `Repositories/VehicleRepository.cs`
- `Repositories/RentalRepository.cs`
- `Repositories/PaymentRepository.cs`
- Relevant service files, if required

### Expected result
- Available vehicles are displayed correctly.
- Rental reports include customer and vehicle information.
- Customer rental history is filtered correctly.
- Vehicles with no rentals are still included in the vehicle rental-count report.
- Payment reports show the correct rental and payment details.

---

## Commit 15: Connect the Console Menus

**Commit message:** `Connect console menus to vehicle rental services`

### Objective
Connect all implemented service methods to the console interface.

### Steps
1. Create one repository instance for each module.
2. Create one service instance for each module using the corresponding repository.
3. Add the main menu.
4. Add customer management options.
5. Add vehicle management options.
6. Add rental management options.
7. Add payment management options.
8. Add reports.
9. Handle invalid menu choices.
10. Catch expected exceptions and display user-friendly messages.

### Files
- `Program.cs`

### Expected result
- The application displays the main menu.
- Each menu calls the corresponding service.
- Users can navigate back to the main menu.
- Invalid menu choices do not terminate the application.
- Choosing `0` exits cleanly.

---

## Commit 16: Test and Finalize Version 1

**Commit message:** `Test and document Vehicle Rental System version 1`

### Objective
Verify the main workflows and document the actual implementation status.

### Steps
1. Build the application.
2. Run the console application.
3. Test customer creation, listing, updating, and deactivation.
4. Test vehicle creation, listing, updating, and deactivation.
5. Test booking and vehicle availability changes.
6. Test rental return and vehicle availability restoration.
7. Test payment recording and payment history.
8. Test reports.
9. Test invalid inputs and failed database operations.
10. Fix any verified bugs.
11. Update this document with the actual commit hashes and test results.

### Commands
```powershell
dotnet build
dotnet run
git status
```

### Files
- `docs.md`
- Source files changed to fix verified bugs

### Expected result
- The application builds without errors.
- All four modules work with SQL Server.
- Failed booking and return operations do not leave inconsistent records.
- The documentation matches the implementation that was actually tested.

---

# Manual Testing Checklist

## Customer Management
- [ ] Add a customer.
- [ ] List customers.
- [ ] Search for a customer by ID.
- [ ] Update customer details.
- [ ] Reject blank required fields.
- [ ] Reject duplicate email addresses.
- [ ] Deactivate a customer.

## Vehicle Management
- [ ] Add a vehicle.
- [ ] List vehicles.
- [ ] List available vehicles.
- [ ] Search for a vehicle by ID.
- [ ] Update vehicle details.
- [ ] Reject duplicate registration numbers.
- [ ] Reject a negative daily rate.
- [ ] Deactivate a vehicle.

## Rental Management
- [ ] Book an available vehicle.
- [ ] Reject a nonexistent customer ID.
- [ ] Reject a nonexistent vehicle ID.
- [ ] Reject booking an unavailable vehicle.
- [ ] Verify the rental amount.
- [ ] Verify that a booked vehicle becomes unavailable.
- [ ] Return an active rental.
- [ ] Verify that the vehicle becomes available again.
- [ ] Reject a second return for the same rental.
- [ ] Verify transaction rollback after a failed booking or return operation.

## Payment Management
- [ ] Record a payment for an existing rental.
- [ ] Reject a payment for a nonexistent rental.
- [ ] Reject zero or negative payment amounts.
- [ ] Reject a blank payment method.
- [ ] List all payments.
- [ ] List payments by rental ID.

## Reports and Integration
- [ ] View available vehicles.
- [ ] View rentals with customer and vehicle details.
- [ ] View rental history for a customer.
- [ ] View rental counts per vehicle.
- [ ] View payments for a rental.
- [ ] Verify that invalid menu choices are handled correctly.
- [ ] Verify that the application exits cleanly.

---

# Commit Record Template

After completing each commit, replace the placeholders with the actual details.

## Commit <number>: <feature name>

- **Commit message:** `<exact Git commit message>`
- **Commit hash:** `<actual short hash>`
- **Files added or modified:**
  - `<file path>`
- **Implementation details:**
  - `<describe the completed changes>`
- **Commands executed:**
  - `<commands used>`
- **Tests performed:**
  - `<test cases>`
- **Expected result:** `<expected behavior>`
- **Actual result:** `<observed behavior and pass/fail status>`
- **Issues found:** `<issues or None>`
- **Next step:** `<next implementation task>`

> **Documentation rule:** Record actual commit hashes and test results only after the work is completed. A planned commit must not be marked as finished until the code has been built and the relevant behavior has been tested.
