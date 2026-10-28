
using VehicleRentalSystem.Models;
using VehicleRentalSystem.Repositories;

namespace VehicleRentalSystem.Services;

public class VehicleService
{
    private readonly VehicleRepository _repository;

    public VehicleService(VehicleRepository repository)
    {
        _repository = repository;
    }

    public Task<int> AddAsync(Vehicle vehicle)
    {
        if (string.IsNullOrWhiteSpace(vehicle.RegistrationNumber))
            throw new ArgumentException("Vehicle registration number is required.");

        if (string.IsNullOrWhiteSpace(vehicle.Make))
            throw new ArgumentException("Vehicle make is required.");

        if (string.IsNullOrWhiteSpace(vehicle.Model))
            throw new ArgumentException("Vehicle model is required.");

        if (string.IsNullOrWhiteSpace(vehicle.VehicleType))
            throw new ArgumentException("Vehicle type is required.");

        if (vehicle.DailyRate < 0)
            throw new ArgumentException("Daily rate cannot be negative.");

        return _repository.AddAsync(vehicle);
    }

    public Task<List<Vehicle>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    public Task<Vehicle?> GetByIdAsync(int vehicleId)
    {
        if (vehicleId <= 0)
            throw new ArgumentException("Vehicle ID must be greater than zero.");

        return _repository.GetByIdAsync(vehicleId);
    }

    public Task<bool> UpdateAsync(Vehicle vehicle)
    {
        if (vehicle.VehicleId <= 0)
            throw new ArgumentException("Vehicle ID must be greater than zero.");

        if (string.IsNullOrWhiteSpace(vehicle.RegistrationNumber))
            throw new ArgumentException("Vehicle registration number is required.");

        if (string.IsNullOrWhiteSpace(vehicle.Make))
            throw new ArgumentException("Vehicle make is required.");

        if (string.IsNullOrWhiteSpace(vehicle.Model))
            throw new ArgumentException("Vehicle model is required.");

        if (string.IsNullOrWhiteSpace(vehicle.VehicleType))
            throw new ArgumentException("Vehicle type is required.");

        if (vehicle.DailyRate < 0)
            throw new ArgumentException("Daily rate cannot be negative.");

        return _repository.UpdateAsync(vehicle);
    }

    public Task<bool> DeleteAsync(int vehicleId)
    {
        if (vehicleId <= 0)
            throw new ArgumentException("Vehicle ID must be greater than zero.");

        return _repository.DeleteAsync(vehicleId);
    }

}
