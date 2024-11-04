using FastFoodManagement.Data.Infrastructure;
using FastFoodManagement.Model.Models;

namespace FastFoodManagement.Data.Repositories
{
    public interface IPurchaseInvoiceItemRepository : IRepository<PurchaseInvoiceItem>
    {

    }
    public class PurchaseInvoiceItemRepository : RepositoryBase<PurchaseInvoiceItem>, IPurchaseInvoiceItemRepository
    {
        public PurchaseInvoiceItemRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
