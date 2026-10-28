using System;
using System.Collections.Generic;
using System.Text;
using VehicleRentalSystem.Models;
using VehicleRentalSystem.Repositories;

namespace VehicleRentalSystem.Services;

public class CustomerService
{
    private readonly CustomerRepository _repository;

    public CustomerService(CustomerRepository repository)
    {
        _repository = repository;
    }

    public Task<int> AddAsync(Customer customer)
    {
        if (string.IsNullOrWhiteSpace(customer.FullName))
            throw new ArgumentException("Customer name is required.");

        if (string.IsNullOrWhiteSpace(customer.Email))
            throw new ArgumentException("Customer email is required.");

        if (string.IsNullOrWhiteSpace(customer.PhoneNumber))
            throw new ArgumentException("Customer phone number is required.");

        return _repository.AddAsync(customer);
    }

    public Task<List<Customer>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

}