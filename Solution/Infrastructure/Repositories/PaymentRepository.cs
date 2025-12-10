using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly Dictionary<Guid, Payment> _storage = new();

    public Task<Payment> SaveAsync(Payment payment)
    {
        _storage[payment.Id] = payment;
        return Task.FromResult(payment);
    }

    public Task<Payment?> GetByIdAsync(Guid id)
    {
        _storage.TryGetValue(id, out var payment);
        return Task.FromResult(payment);
    }

    public Task<Payment> UpdateAsync(Payment payment)
    {
        _storage[payment.Id] = payment;
        return Task.FromResult(payment);
    }
}
