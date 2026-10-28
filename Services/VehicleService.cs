
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

}
