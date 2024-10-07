using FastFoodManagement.Data.Infrastructure;
using FastFoodManagement.Model.Models;

namespace FastFoodManagement.Data.Repositories
{
    public interface IPurchaseInvoiceRepository
    {

    }
    public class PurchaseInvoiceRepository : RepositoryBase<PurchaseInvoice>, IPurchaseInvoiceRepository
    {
        public PurchaseInvoiceRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
