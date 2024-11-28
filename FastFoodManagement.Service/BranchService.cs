using FastFoodManagement.Data.Infrastructure;
using FastFoodManagement.Data.Repositories;
using FastFoodManagement.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFoodManagement.Service;

public interface IBranchService
{
    public Task<List<Branch>> GetAllBranches();
    public Task<Branch> GetBranchById(int id);
    public Task AddBranch(Branch branch);
    public Task DeleteBranchById(int id);
    public Task UpdateBranch(Branch branch);
    public void SaveChanges();
    public Task SuspendChanges();
}

public class BranchService : IBranchService
{
    private IBranchRepository _branchRepository;
    private IUnitOfWork _unitOfWork;
    public BranchService(IBranchRepository branchRepository, IUnitOfWork unitOfWork)
    {
        _branchRepository = branchRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task AddBranch(Branch branch)
    {
        await _branchRepository.Add(branch);
        await SuspendChanges();
    }

    public async Task<List<Branch>> GetAllBranches()
    {
        List<Branch> entity = await _branchRepository.GetAll().ToListAsync();
        return entity;
    }

    public async Task<Branch> GetBranchById(int id)
    {
        return await _branchRepository.GetSingleById(id);
    }

    public async Task DeleteBranchById(int id)
    {
        await _branchRepository.DeleteById(id);
        await SuspendChanges();
    }
    
    public async Task UpdateBranch(Branch branch)
    {
        await _branchRepository.Update(branch);
        await SuspendChanges();
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