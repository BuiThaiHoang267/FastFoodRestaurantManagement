using FastFoodManagement.Data.Infrastructure;
using FastFoodManagement.Data.Repositories;
using FastFoodManagement.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodManagement.Service
{
    public interface IRoleService
	{
		public Task<List<Role>> GetAll();
	}
	public class RoleService : IRoleService
    {
		private readonly IRoleRepository _roleRepository;
		private readonly IUnitOfWork _unitOfWork;

		public RoleService(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
		{
			this._roleRepository = roleRepository;
			this._unitOfWork = unitOfWork;
		}

		public async Task<List<Role>> GetAll()
		{
			var roles = await _roleRepository.GetAll().ToListAsync();
			return roles;
		}
	}
}
