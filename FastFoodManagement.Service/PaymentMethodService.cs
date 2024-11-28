using FastFoodManagement.Data.Infrastructure;
using FastFoodManagement.Data.Repositories;
using FastFoodManagement.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFoodManagement.Service;

public interface IPaymentMethodService
{
    public Task<List<PaymentMethod>> GetAllPaymentMethods();
    public Task<PaymentMethod> GetPaymentMethodById(int id);
    public Task AddPaymentMethod(PaymentMethod paymentMethod);
    public Task DeletePaymentMethodById(int id);
    public void SaveChanges();
    public Task SuspendChanges();

}

public class PaymentMethodService : IPaymentMethodService
{
    private IPaymentMethodRepository _paymentMethodRepository;
    private IUnitOfWork _unitOfWork;
    public PaymentMethodService(IPaymentMethodRepository paymentMethodRepository, IUnitOfWork unitOfWork)
    {
        _paymentMethodRepository = paymentMethodRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task AddPaymentMethod(PaymentMethod paymentMethod)
    {
        await _paymentMethodRepository.Add(paymentMethod);
        await SuspendChanges();
    }

    public async Task DeletePaymentMethodById(int id)
    {
        await _paymentMethodRepository.DeleteById(id);
        await SuspendChanges();
    }

    public async Task<List<PaymentMethod>> GetAllPaymentMethods()
    {
        List<PaymentMethod> entity = await _paymentMethodRepository.GetAll().ToListAsync();
        return entity;
    }

    public async Task<PaymentMethod> GetPaymentMethodById(int id)
    {
        return await _paymentMethodRepository.GetSingleById(id);
    }

    public void SaveChanges()
    {
        _unitOfWork.Commit();
    }

    public async Task SuspendChanges()
    {
        await _unitOfWork.CommitAsync();
    }
}