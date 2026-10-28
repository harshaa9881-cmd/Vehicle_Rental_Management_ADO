using VehicleRentalSystem.Models;
using VehicleRentalSystem.Repositories;

namespace VehicleRentalSystem.Services;

public class PaymentService
{
    private readonly PaymentRepository _repository;

    public PaymentService(PaymentRepository repository)
    {
        _repository = repository;
    }

    public Task<int> AddAsync(Payment payment)
    {
        if (payment.RentalId <= 0)
        {
            throw new ArgumentException("Rental ID must be greater than zero.");
        }

        if (payment.Amount <= 0)
        {
            throw new ArgumentException("Payment amount must be greater than zero.");
        }

        if (string.IsNullOrWhiteSpace(payment.PaymentMethod))
        {
            throw new ArgumentException("Payment method is required.");
        }

        if (payment.PaymentDate == default)
        {
            payment.PaymentDate = DateTime.Now;
        }

        if (string.IsNullOrWhiteSpace(payment.Status))
        {
            payment.Status = "Successful";
        }

        return _repository.AddAsync(payment);
    }

    public Task<List<Payment>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    public async Task<Payment?> GetByIdAsync(int paymentId)
    {
        if (paymentId <= 0)
        {
            throw new ArgumentException("Payment ID must be greater than zero.");
        }

        return await _repository.GetByIdAsync(paymentId);
    }

    public async Task<List<Payment>> GetByRentalIdAsync(int rentalId)
    {
        if (rentalId <= 0)
        {
            throw new ArgumentException("Rental ID must be greater than zero.");
        }

        return await _repository.GetByRentalIdAsync(rentalId);
    }
    public Task<bool> UpdateAsync(Payment payment)
    {
        if (payment.PaymentId <= 0)
        {
            throw new ArgumentException("Payment ID must be greater than zero.");
        }

        if (payment.RentalId <= 0)
        {
            throw new ArgumentException("Rental ID must be greater than zero.");
        }

        if (payment.Amount <= 0)
        {
            throw new ArgumentException("Payment amount must be greater than zero.");
        }

        if (string.IsNullOrWhiteSpace(payment.PaymentMethod))
        {
            throw new ArgumentException("Payment method is required.");
        }

        if (string.IsNullOrWhiteSpace(payment.Status))
        {
            throw new ArgumentException("Payment status is required.");
        }

        return _repository.UpdateAsync(payment);
    }

    public Task<bool> DeleteAsync(int paymentId)
    {
        if (paymentId <= 0)
        {
            throw new ArgumentException("Payment ID must be greater than zero.");
        }

        return _repository.DeleteAsync(paymentId);
    }
}