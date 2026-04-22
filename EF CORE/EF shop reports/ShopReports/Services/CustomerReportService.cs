using ShopReports.Models;
using ShopReports.Reports;

namespace ShopReports.Services;

public class CustomerReportService : IDisposable
{
    private readonly ShopContext shopContext;

    public CustomerReportService(ShopContext shopContext)
    {
        this.shopContext = shopContext;
    }

    public CustomerSalesRevenueReport GetCustomerSalesRevenueReport()
    {
        // customer identifier, person first name, person last name and sum of price with discount.
        var revenueReportLines = this.shopContext.OrderDetails
            .Where(od => od.Order.CustomerId != null)
            .GroupBy(od => new
            {
                od.Order.CustomerId,
                od.Order.Customer!.Person.FirstName,
                od.Order.Customer.Person.LastName,
            })
            .Select(g => new CustomerSalesRevenueReportLine
            {
                CustomerId = g.Key.CustomerId!.Value,
                SalesRevenue = g.Sum(g => g.PriceWithDiscount),
                PersonFirstName = g.Key.FirstName,
                PersonLastName = g.Key.LastName,
            })
            .OrderByDescending(x => x.SalesRevenue)
            .ToList();

        return new CustomerSalesRevenueReport(revenueReportLines, DateTime.Now);
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            this.shopContext?.Dispose();
        }
    }
}
