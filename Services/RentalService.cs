
using VehicleRentalSystem.Models;
using VehicleRentalSystem.Repositories;

namespace VehicleRentalSystem.Services;

public class RentalService
{
    private readonly RentalRepository _repository;

    public RentalService(RentalRepository repository)
    {
        _repository = repository;
    }

    public Task<int> AddAsync(Rental rental)
    {
        if (rental.CustomerId <= 0)
            throw new ArgumentException("Customer ID must be greater than zero.");

        if (rental.VehicleId <= 0)
            throw new ArgumentException("Vehicle ID must be greater than zero.");

        if (rental.RentalDate.Date < DateTime.Today)
            throw new ArgumentException("Rental date cannot be in the past.");

        if (rental.ExpectedReturnDate.Date <= rental.RentalDate.Date)
            throw new ArgumentException("Expected return date must be after rental date.");

        if (rental.TotalAmount < 0)
            throw new ArgumentException("Total amount cannot be negative.");

        rental.Status = "Active";
        rental.ActualReturnDate = null;

        return _repository.AddAsync(rental);
    }

    public Task<List<Rental>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    public Task<Rental?> GetByIdAsync(int rentalId)
    {
        if (rentalId <= 0)
            throw new ArgumentException("Rental ID must be greater than zero.");

        return _repository.GetByIdAsync(rentalId);
    }

    public Task<bool> UpdateAsync(Rental rental)
    {
        if (rental.RentalId <= 0)
            throw new ArgumentException("Rental ID must be greater than zero.");

        if (rental.CustomerId <= 0)
            throw new ArgumentException("Customer ID must be greater than zero.");

        if (rental.VehicleId <= 0)
            throw new ArgumentException("Vehicle ID must be greater than zero.");

        if (rental.ExpectedReturnDate.Date <= rental.RentalDate.Date)
            throw new ArgumentException("Expected return date must be after rental date.");

        if (rental.TotalAmount < 0)
            throw new ArgumentException("Total amount cannot be negative.");

        return _repository.UpdateAsync(rental);
    }

    public Task<bool> DeleteAsync(int rentalId)
    {
        if (rentalId <= 0)
            throw new ArgumentException("Rental ID must be greater than zero.");

        return _repository.DeleteAsync(rentalId);
    }

    public async Task<bool> ReturnVehicleAsync(int rentalId)
    {
        if (rentalId <= 0)
            throw new ArgumentException("Rental ID must be greater than zero.");

        Rental? rental = await _repository.GetByIdAsync(rentalId);

        if (rental == null)
            return false;

        if (rental.Status != "Active")
            throw new ArgumentException("Only active rentals can be returned.");

        rental.ActualReturnDate = DateTime.Now;
        rental.Status = "Completed";

        return await _repository.UpdateAsync(rental);
    }
}
