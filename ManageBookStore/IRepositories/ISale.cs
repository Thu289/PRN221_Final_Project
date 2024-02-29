using ManageBookStore.Models;

namespace ManageBookStore.IRepositories
{
    public interface ISale
    {
        void AddSale(Sale sale);

        void UpdateSale(Sale sale);

        void DeleteSale(int saleId);

        List<Sale> GetAllSales();

        Sale GetSaleBySaleId(int saleId);

        List<Sale> GetAllSaleNow(DateTime? startDate, DateTime? endDate);

        Sale GetSaleByBook(int bookId);
    }
}
