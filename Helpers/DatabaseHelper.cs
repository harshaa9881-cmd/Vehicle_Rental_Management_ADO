using System;
using System.Collections.Generic;
using System.Text;
/*CREATE DATABASE VehicleRentalDB;
GO

USE VehicleRentalDB;
GO

CREATE TABLE Customers
(
    CustomerId INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    PhoneNumber NVARCHAR(20) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME()
);
GO

CREATE TABLE Vehicles
(
    VehicleId INT IDENTITY(1,1) PRIMARY KEY,
    RegistrationNumber NVARCHAR(20) NOT NULL UNIQUE,
    Make NVARCHAR(50) NOT NULL,
    Model NVARCHAR(50) NOT NULL,
    VehicleType NVARCHAR(30) NOT NULL,
    DailyRate DECIMAL(10,2) NOT NULL CHECK (DailyRate >= 0),
    IsAvailable BIT NOT NULL DEFAULT 1,
    IsActive BIT NOT NULL DEFAULT 1
);
GO

CREATE TABLE Rentals
(
    RentalId INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT NOT NULL,
    VehicleId INT NOT NULL,
    RentalDate DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    ExpectedReturnDate DATE NOT NULL,
    ActualReturnDate DATE NULL,
    TotalAmount DECIMAL(10,2) NOT NULL CHECK (TotalAmount >= 0),
    Status NVARCHAR(20) NOT NULL DEFAULT N'Active',

    CONSTRAINT FK_Rentals_Customers
        FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId),

    CONSTRAINT FK_Rentals_Vehicles
        FOREIGN KEY (VehicleId) REFERENCES Vehicles(VehicleId),

    CONSTRAINT CK_Rentals_Status
        CHECK (Status IN (N'Active', N'Returned', N'Cancelled')),

    CONSTRAINT CK_Rentals_ReturnDate
        CHECK (ExpectedReturnDate >= CAST(RentalDate AS DATE))
);
GO

CREATE TABLE Payments
(
    PaymentId INT IDENTITY(1,1) PRIMARY KEY,
    RentalId INT NOT NULL,
    Amount DECIMAL(10,2) NOT NULL CHECK (Amount > 0),
    PaymentDate DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    PaymentMethod NVARCHAR(30) NOT NULL,
    Status NVARCHAR(20) NOT NULL DEFAULT N'Successful',

    CONSTRAINT FK_Payments_Rentals
        FOREIGN KEY (RentalId) REFERENCES Rentals(RentalId),

    CONSTRAINT CK_Payments_Status
        CHECK (Status IN (N'Pending', N'Successful', N'Failed'))
);
GO*/
namespace Vehicle_rent_Management_ADO.Helpers
{
    internal class DatabaseHelper
    {
    }
}
