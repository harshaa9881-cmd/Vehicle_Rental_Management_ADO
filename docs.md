# Vehicle Rental Management System — Development Documentation

## 1. Project Overview

This project is a **C# Console Application** developed using **ADO.NET and SQL Server** to manage vehicle rentals.

The application contains four main modules:

- **Customer Management** — Add, retrieve, update, and delete customers.
- **Vehicle Management** — Add, retrieve, update, and delete vehicles.
- **Rental Management** — Manage rental records and vehicle returns.
- **Payment Management** — Add, retrieve, update, and delete payment records.

The project follows a layered structure:

- **Models:** Represent the application's data.
- **Repositories:** Handle SQL Server database operations.
- **Services:** Contain validation and application logic.
- **DatabaseHelper:** Creates database connections.
- **Program.cs:** Provides the console menu and connects the application features.

## 2. Commit History Summary

The following table lists all 19 commits in chronological order.

| Commit ID | Commit Message | Purpose |
|---|---|---|
| `6e4229b` | Create Vehicle Rental System console project | Created the initial console application. |
| `e581ef4` | Add ADO.NET database connection helper | Added reusable SQL Server connection logic. |
| `4b57980` | Add Vehicle Rental System database schema | Added the database schema. |
| `4787705` | update:object files updated | Updated object/build-related files. |
| `87983b2` | update:microsoftdatasqlclient vulnerabilities resolved and updated | Updated the SQL client dependency to address reported vulnerabilities. |
| `144cfad` | feat:Implement customer insert and list operations | Added customer insertion and listing. |
| `eaf674f` | feat:Add customer list retrieval | Added or refined customer list retrieval. |
| `2309663` | feat:Add customer retrieval by ID | Added customer retrieval by ID. |
| `ff7ef32` | feat:Add customer update feature | Added customer update functionality. |
| `057752c` | feat:Add customer delete feature | Added customer deletion. |
| `e18d39e` | feat:Add Vehicle add feature | Added vehicle insertion. |
| `0cd0cb1` | feat:Add Vehicle display feature | Added vehicle list retrieval. |
| `afdebcb` | feat:Add Vehicle display by id feature | Added vehicle retrieval by ID. |
| `85a93de` | feat:Add Vehicle update feature | Added vehicle update functionality. |
| `9f40049` | feat:Add Vehicle delete feature | Added vehicle deletion. |
| `ab7b51f` | feat:Add rental features | Added rental-related functionality. |
| `01a16ad` | feat:Add payment add , getall,getbyid feature | Added payment insertion and retrieval. |
| `5decd25` | feat:Add payment UPDATE AND DELETE feature | Added payment update and deletion. |
| `4530f9d` | PROGRAM.cs added | Added the console menu to access the features. |

## 3. Detailed Commit Explanations

### Commit 1: `6e4229b` — Create Vehicle Rental System console project

This commit established the initial project structure.

**Purpose:**
- Created the C# console application.
- Established the foundation for the application.
- Provided the starting point for adding database connectivity, models, repositories, and services.

This step was necessary before implementing the application's features.

### Commit 2: `e581ef4` — Add ADO.NET database connection helper

This commit added a reusable helper for creating SQL Server connections.

**Purpose:**
- Centralized database connection creation.
- Avoided repeating connection setup code in every repository.
- Made database access easier to maintain.

Repositories can use the helper to create a connection before executing SQL commands.

The connection string must point to the correct SQL Server instance and database.

### Commit 3: `4b57980` — Add Vehicle Rental System database schema

This commit added the database schema for the Vehicle Rental System.

**Purpose:**
- Defined the database structures required by the application.
- Prepared the database for customer, vehicle, rental, and payment operations.
- Established the database foundation for repository implementation.

The repository SQL statements must match the actual table names, column names, data types, and constraints defined in the schema.

### Commit 4: `4787705` — update:object files updated

This commit records updates to object-related or build-related files.

The commit message does not identify the exact files or changes. The actual changes can be inspected using:

```powershell
git show --stat 4787705
git show --name-status 4787705
```

Generated build files are generally excluded from Git unless there is a specific reason to track them.

### Commit 5: `87983b2` — update:microsoftdatasqlclient vulnerabilities resolved and updated

This commit updated the `Microsoft.Data.SqlClient` dependency used for SQL Server connectivity.

**Purpose:**
- Updated the SQL Server client library.
- Addressed reported dependency vulnerability findings.
- Maintained the database connectivity dependency.

The package provides classes such as `SqlConnection`, `SqlCommand`, and `SqlDataReader`.

After dependency updates, the project can be checked using:

```powershell
dotnet restore
dotnet build
dotnet list package --vulnerable --include-transitive
```

A successful build confirms that the project compiles. The vulnerability command reports known advisories available to the current tooling; it does not guarantee that every possible security risk has been eliminated.

## 4. Customer Management Features

### Commit 6: `144cfad` — feat:Implement customer insert and list operations

This commit introduced the initial customer insertion and listing operations.

**Customer insertion:**
- Accepts customer details.
- Sends the data to the database layer.
- Inserts the customer record into SQL Server.

**Customer listing:**
- Retrieves customer records from SQL Server.
- Maps database records to customer model objects.
- Makes the records available for display.

These operations provide the foundation for managing customer information.

### Commit 7: `eaf674f` — feat:Add customer list retrieval

This commit is also related to customer list retrieval.

The previous commit already mentions listing, so the exact difference between these two commits cannot be determined from the commit messages alone. It may represent a follow-up implementation, correction, or refactoring.

To inspect the actual changes:

```powershell
git show --stat eaf674f
git show eaf674f
```

### Commit 8: `2309663` — feat:Add customer retrieval by ID

This commit added the ability to retrieve a specific customer using the customer ID.

**Purpose:**
- Accept a customer ID.
- Search for the matching record.
- Return the customer when found.
- Allow the application to handle a customer that does not exist.

This feature is useful when displaying customer details or loading a record before updating it.

### Commit 9: `ff7ef32` — feat:Add customer update feature

This commit added customer update functionality.

**Purpose:**
- Identify an existing customer using the customer ID.
- Accept updated customer details.
- Update the matching database record.
- Report whether the operation succeeded.

The update operation should use parameterized SQL and validate required fields.

### Commit 10: `057752c` — feat:Add customer delete feature

This commit added customer deletion.

**Purpose:**
- Accept a customer ID.
- Delete the matching customer record.
- Report whether the customer was deleted.

If a customer is referenced by a rental record, a foreign-key constraint may prevent deletion. Retaining customer history may require deactivation or archival rather than permanent deletion.

## 5. Vehicle Management Features

### Commit 11: `e18d39e` — feat:Add Vehicle add feature

This commit added vehicle insertion.

The `Vehicle` model contains the following properties:

- `VehicleId`
- `RegistrationNumber`
- `Make`
- `Model`
- `VehicleType`
- `DailyRate`
- `IsAvailable`
- `IsActive`

**Purpose:**
- Accept vehicle details.
- Store the registration number, make, model, and type.
- Store the daily rental rate.
- Track vehicle availability and active status.

Registration numbers should be unique if required by the application's business rules. The database can enforce uniqueness to prevent duplicate registrations.

### Commit 12: `0cd0cb1` — feat:Add Vehicle display feature

This commit added vehicle list retrieval.

**Purpose:**
- Retrieve vehicle records from SQL Server.
- Map database records to `Vehicle` objects.
- Display vehicle information.

The displayed information can include the registration number, make, model, vehicle type, daily rate, availability, and active status.

### Commit 13: `afdebcb` — feat:Add Vehicle display by id feature

This commit added vehicle retrieval by ID.

**Purpose:**
- Accept a vehicle ID.
- Retrieve the matching vehicle.
- Display the vehicle details.
- Report when the vehicle cannot be found.

This feature is useful for viewing an individual vehicle or loading its details before updating it.

### Commit 14: `85a93de` — feat:Add Vehicle update feature

This commit added vehicle update functionality.

**Purpose:**
- Identify a vehicle by ID.
- Update vehicle details.
- Save the changes to SQL Server.
- Report whether the update succeeded.

Updated fields may include the registration number, make, model, vehicle type, daily rate, and status.

The application should validate required fields and the daily rate. Vehicle availability should not be changed without considering active rentals.

### Commit 15: `9f40049` — feat:Add Vehicle delete feature

This commit added vehicle deletion.

**Purpose:**
- Accept a vehicle ID.
- Delete the matching vehicle record.
- Report whether the operation succeeded.

If rental records reference the vehicle, a foreign-key constraint may prevent deletion. Marking a vehicle inactive may be preferable when rental history must be retained.

## 6. Rental Management Features

### Commit 16: `ab7b51f` — feat:Add rental features

This commit introduced rental-related functionality.

The `Rental` model contains:

- `RentalId`
- `CustomerId`
- `VehicleId`
- `RentalDate`
- `ExpectedReturnDate`
- `ActualReturnDate`
- `TotalAmount`
- `Status`

**Purpose:**
- Associate a customer with a vehicle.
- Record the rental date.
- Record the expected return date.
- Store the actual return date when available.
- Store the total rental amount.
- Track the rental status.

These fields provide the foundation for recording rental transactions and managing vehicle returns.

**Important business rules to verify:**
- The customer and vehicle must exist.
- The expected return date must be after the rental date.
- The vehicle must be available before a rental is accepted.
- The same vehicle should not have overlapping active rentals.
- Returning a vehicle should update both the rental status and vehicle availability.
- Rental history should be protected from unsafe deletion.

The commit message alone does not establish whether every rule has been implemented.

## 7. Payment Management Features

### Commit 17: `01a16ad` — feat:Add payment add , getall,getbyid feature

This commit added payment insertion and retrieval operations.

The `Payment` model contains:

- `PaymentId`
- `RentalId`
- `Amount`
- `PaymentDate`
- `PaymentMethod`
- `Status`

**Add payment:**
- Accepts a rental ID.
- Stores the payment amount.
- Records the payment date and method.
- Stores the payment status.
- Inserts the payment into SQL Server.

**Get all payments:**
- Retrieves payment records from the database.
- Makes the records available for display.

**Get payment by ID:**
- Searches for a payment using its payment ID.
- Returns the matching payment when found.

These operations provide the foundation for recording and reviewing rental payments.

### Commit 18: `5decd25` — feat:Add payment UPDATE AND DELETE feature

This commit added payment update and deletion operations.

**Update payment:**
- Identifies a payment by ID.
- Accepts updated payment information.
- Saves the changes to SQL Server.

**Delete payment:**
- Accepts a payment ID.
- Deletes the matching record.
- Reports whether the deletion succeeded.

The application should validate payment amounts and required fields and ensure that the associated rental is valid.

Payment records may be needed for financial history or auditing. In a production system, a status correction or reversal record may be safer than permanently deleting a payment.

## 8. Console Application Integration

### Commit 19: `4530f9d` — PROGRAM.cs added

This is the latest commit in the supplied history. It added the console entry point and menu to access the application's features.

The menu provides the following sections:

1. **Manage Customers**
   - Add customer
   - View all customers
   - View customer by ID
   - Update customer
   - Delete customer

2. **Manage Vehicles**
   - Add vehicle
   - View all vehicles
   - View vehicle by ID
   - Update vehicle
   - Delete vehicle

3. **Manage Rentals**
   - Add rental
   - View all rentals
   - View rental by ID
   - Update rental
   - Delete rental
   - Return vehicle

4. **Manage Payments**
   - Add payment
   - View all payments
   - View payment by ID
   - View payments by rental ID
   - Update payment
   - Delete payment

5. **Exit**
   - Exit the console application.

### Application flow

```text
User
 |
 v
Program.cs
(Console Menu)
 |
 v
Service Classes
(Validation and Application Logic)
 |
 v
Repository Classes
(SQL Operations)
 |
 v
DatabaseHelper
(Connection Creation)
 |
 v
SQL Server Database
```

**How the layers work together:**

- The user selects an option from the console menu.
- `Program.cs` calls the appropriate service method.
- The service performs application-level validation.
- The repository executes SQL commands.
- `DatabaseHelper` creates the SQL Server connection.
- The database returns the operation result.
- The application displays the result to the user.

The menu is a basic console interface. Before production use, invalid-input handling and the consistency of rental and vehicle availability updates should be verified.

## 9. Inspecting Git Commits

The following commands can be used to inspect the actual changes made in each commit.

### Display the files changed by a commit

```powershell
git show --stat <commit-id>
```

### Display the full changes in a commit

```powershell
git show <commit-id>
```

### Display changed filenames and change types

```powershell
git show --name-status <commit-id>
```

### Compare a commit with its parent

```powershell
git diff <commit-id>^ <commit-id>
```

### Display commit history with file summaries

```powershell
git log --oneline --stat
```

### Inspect the latest Program.cs commit

```powershell
git show 4530f9d
```

These commands help verify which files changed and what each commit actually introduced.

## 10. Building and Running the Project

Run the following commands from the project directory:

```powershell
dotnet restore
dotnet build
dotnet run
```

If the application cannot connect to SQL Server, verify the following:

1. SQL Server Express is running.
2. The connection string points to the correct SQL Server instance and database.
3. The database schema has been created.
4. Table and column names match the SQL statements in the repositories.
5. The configured authentication method has permission to access the database.

## 11. Project Verification Checklist

Before considering the project complete, verify the following:

- [ ] The connection string is correct.
- [ ] Production secrets are not committed to Git.
- [ ] SQL statements use parameters.
- [ ] Required customer fields are validated.
- [ ] Required vehicle fields and daily rates are validated.
- [ ] Vehicle registration numbers are unique if required.
- [ ] Rental creation checks whether the customer and vehicle exist.
- [ ] Overlapping active rentals for the same vehicle are prevented.
- [ ] Vehicle availability is updated when a rental starts and when it is returned.
- [ ] Rental and payment statuses are validated consistently.
- [ ] Foreign-key constraints and deletion behavior preserve required history.
- [ ] Invalid console input is handled without unexpectedly terminating the application.
- [ ] The application builds successfully.
- [ ] Basic customer, vehicle, rental, and payment operations are tested against the intended database.

These are verification items, not claims that the current implementation already satisfies every item.

## 12. Conclusion

The commit history shows the incremental development of the Vehicle Rental Management System.

The project began with console application setup and database connectivity. Customer and vehicle CRUD operations were added next, followed by rental and payment functionality. The latest commit introduced `Program.cs`, which provides a menu for accessing these operations.

Using focused commits makes it easier to understand development progress, review changes, identify bugs, and maintain the application. For exact file-level details, especially where commit messages overlap, the actual changes should be inspected using `git show`.

---

**Project:** Vehicle Rental Management System  
**Technology:** C#, .NET, ADO.NET, SQL Server, Git  
**Latest commit shown:** `4530f9d` — `PROGRAM.cs added`
