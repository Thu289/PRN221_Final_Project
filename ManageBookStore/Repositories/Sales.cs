using ManageBookStore.IRepositories;
using ManageBookStore.Models;

namespace ManageBookStore.Repositories
{
    public class Sales : ISale
    {
        void ISale.AddSale(Sale sale)
        {
            throw new NotImplementedException();
        }

        void ISale.DeleteSale(int saleId)
        {
            throw new NotImplementedException();
        }

        List<Sale> ISale.GetAllSaleNow(DateTime? startDate, DateTime? endDate)
        {
            throw new NotImplementedException();
        }

        List<Sale> ISale.GetAllSales()
        {
            throw new NotImplementedException();
        }

        Sale ISale.GetSaleByBook(int bookId)
        {
            throw new NotImplementedException();
        }

        Sale ISale.GetSaleBySaleId(int saleId)
        {
            throw new NotImplementedException();
        }

        void ISale.UpdateSale(Sale sale)
        {
            throw new NotImplementedException();
        }
    }
}
