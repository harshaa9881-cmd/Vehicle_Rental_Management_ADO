# Vehicle Rental Management System

## Project Overview

The **Vehicle Rental Management System** is a C# console application developed using **ADO.NET and SQL Server**. It manages customers, vehicles, rentals, and payments through a menu-driven interface.

The application performs CRUD operations (Create, Read, Update, and Delete) for the main entities and supports completing a rental when a vehicle is returned.

## Technologies Used

- **Programming Language:** C#
- **Framework:** .NET 10
- **Database:** Microsoft SQL Server
- **Data Access:** ADO.NET
- **SQL Client:** Microsoft.Data.SqlClient
- **Development Environment:** Visual Studio
- **Version Control:** Git and GitHub

## Features

### 1. Customer Management

- Add a new customer.
- Display all customers.
- Retrieve a customer by ID.
- Update customer details.
- Delete a customer.

### 2. Vehicle Management

- Add a new vehicle.
- Display all vehicles.
- Retrieve a vehicle by ID.
- Update vehicle details.
- Delete a vehicle.
- Manage vehicle information such as registration number, make, model, type, daily rate, availability, and active status.

### 3. Rental Management

- Create a rental.
- Display all rentals.
- Retrieve a rental by ID.
- Update rental details.
- Delete a rental.
- Record the rental date and expected return date.
- Track the actual return date and rental status.
- Mark a rental as completed when the vehicle is returned.

### 4. Payment Management

- Add a payment.
- Display all payments.
- Retrieve a payment by ID.
- Retrieve payments associated with a rental.
- Update payment details.
- Delete a payment.
- Store payment amount, payment date, payment method, and payment status.

## Project Structure

```text
Vehicle_rent_Management_ADO/
│
├── Models/
│   ├── Customer.cs
│   ├── Vehicle.cs
│   ├── Rental.cs
│   └── Payment.cs
│
├── Repositories/
│   ├── CustomerRepository.cs
│   ├── VehicleRepository.cs
│   ├── RentalRepository.cs
│   └── PaymentRepository.cs
│
├── Services/
│   ├── CustomerService.cs
│   ├── VehicleService.cs
│   ├── RentalService.cs
│   └── PaymentService.cs
│
├── Helpers/
│   └── DbConnectionHelper.cs
│
├── Program.cs
├── appsettings.json
├── README.md
└── Vehicle_rent_Management_ADO.csproj
```

*Note: The folder and file names above are illustrative. Keep the names consistent with your actual project structure.*

## Database Setup

### Step 1: Open SQL Server Management Studio (SSMS)

Connect to your SQL Server instance. This project uses the SQL Server Express instance:

```text
.\SQLEXPRESS
```

### Step 2: Create the Database

Create the project database using the SQL schema script included in the repository.

The database should contain the tables required for:

- Customers
- Vehicles
- Rentals
- Payments

Ensure the table names, column names, primary keys, and foreign keys match the SQL queries used in the repository classes.

### Step 3: Configure the Connection String

Update the connection string in your database connection helper or configuration file to match your SQL Server instance and database name.

Example for SQL Server Express using Windows Authentication:

```text
Server=.\SQLEXPRESS;Database=VehicleRentalDB;Trusted_Connection=True;TrustServerCertificate=True;
```

Replace `VehicleRentalDB` with the actual database name created from your SQL script.

## Installation and Execution

### Prerequisites

Install the following:

- .NET 10 SDK
- Visual Studio or Visual Studio Code
- SQL Server Express
- SQL Server Management Studio (SSMS)

### Step 1: Clone the Repository

```bash
git clone <your-github-repository-url>
```

Replace the placeholder with your actual GitHub repository URL.

### Step 2: Navigate to the Project Folder

```bash
cd Vehicle_rent_Management_ADO
```

If the `.csproj` file is inside a nested project directory, navigate to that directory before running the following commands.

### Step 3: Restore Dependencies

```bash
dotnet restore
```

### Step 4: Build the Project

```bash
dotnet build
```

### Step 5: Run the Application

```bash
dotnet run
```

The console application displays a menu that allows you to select customer, vehicle, rental, and payment operations.

## Application Workflow

1. Add customer details to the database.
2. Add vehicle details.
3. Create a rental using the customer ID and vehicle ID.
4. Record payments for the rental.
5. View or update customer, vehicle, rental, and payment records.
6. Mark the rental as completed when the vehicle is returned.

## Git and Version Control

Git is used to track the development of this project. The commit history records the implementation of database setup, customer operations, vehicle operations, rental features, payment features, and the console menu.

Useful commands:

```bash
git status
git log --oneline
git add .
git commit -m "Describe your changes"
git push origin master
```

## Learning Objectives

This project demonstrates practical use of:

- C# classes and objects
- Model, repository, and service separation
- ADO.NET database connectivity
- SQL commands and parameterized queries
- CRUD operations
- SQL Server integration
- Exception handling
- Console-based menu navigation
- Git and GitHub version control

## Future Improvements

- Add stronger validation for console inputs.
- Check customer and vehicle existence before creating a rental.
- Prevent unavailable vehicles from being rented.
- Update vehicle availability when a rental starts or finishes.
- Validate payment references against existing rentals.
- Use database transactions for related rental and vehicle updates.
- Add reports for rental history, vehicle availability, and payment summaries.
- Develop a web-based user interface.

## Author

**Harshini Priya R**

## License

This project is intended for learning and educational purposes. Add a license file if you plan to distribute or reuse the project under specific licensing terms.
