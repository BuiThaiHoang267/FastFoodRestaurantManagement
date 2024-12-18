﻿using FastFoodManagement.Data.Infrastructure;
using FastFoodManagement.Model.Models;

namespace FastFoodManagement.Data.Repositories
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
        // Add methods here
    }
    public class OrderItemRepository : RepositoryBase<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
