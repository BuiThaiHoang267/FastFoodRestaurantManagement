using FastFoodManagement.Data.Infrastructure;
using FastFoodManagement.Model.Models;

namespace FastFoodManagement.Data.Repositories
{
    public interface IPurchaseInvoiceRepository : IRepository<PurchaseInvoice>
    {

    }
    public class PurchaseInvoiceRepository : RepositoryBase<PurchaseInvoice>, IPurchaseInvoiceRepository
    {
        public PurchaseInvoiceRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
