using Microsoft.EntityFrameworkCore;
using ShopReports.Models;
using ShopReports.Reports;

namespace ShopReports.Services;

public class ProductReportService : IDisposable
{
    private readonly ShopContext shopContext;

    public ProductReportService(ShopContext shopContext)
    {
        this.shopContext = shopContext;
    }

    public ProductCategoryReport GetProductCategoryReport()
    {
        var categories = this.shopContext.Categories
                             .OrderBy(c => c.Name);

        var categoryReportLines = new List<ProductCategoryReportLine>();

        foreach (var category in categories)
        {
            categoryReportLines.Add(new ProductCategoryReportLine { CategoryId = category.Id, CategoryName = category.Name });
        }

        return new ProductCategoryReport(categoryReportLines, DateTime.Now);
    }

    public ProductReport GetProductReport()
    {
        var products = this.shopContext.Products
                             .Include(p => p.Title)
                             .Include(p => p.Manufacturer)
                             .OrderByDescending(t => t.Title.Title);

        var productReportLines = new List<ProductReportLine>();

        foreach (var product in products)
        {
            productReportLines.Add(new ProductReportLine
            {
                ProductId = product.Id,
                ProductTitle = product.Title.Title,
                Manufacturer = product.Manufacturer.Name,
                Price = product.UnitPrice,
            });
        }

        return new ProductReport(productReportLines, DateTime.Now);
    }

    public FullProductReport GetFullProductReport()
    {
        var products = this.shopContext.Products
                             .Include(p => p.Title)
                             .Include(p => p.Manufacturer)
                             .Include(p => p.Title.Category)
                             .OrderBy(t => t.Title.Title);

        var productReportLines = new List<FullProductReportLine>();

        foreach (var product in products)
        {
            productReportLines.Add(new FullProductReportLine
            {
                ProductId = product.Id,
                Name = product.Title.Title,
                CategoryId = product.Title.Category.Id,
                Manufacturer = product.Manufacturer.Name,
                Price = product.UnitPrice,
                Category = product.Title.Category.Name,
            });
        }

        return new FullProductReport(productReportLines, DateTime.Now);
    }

    public ProductTitleSalesRevenueReport GetProductTitleSalesRevenueReport()
    {
        var reportLines = this.shopContext.OrderDetails
        .GroupBy(od => od.Product.Title.Title)
        .Select(g => new ProductTitleSalesRevenueReportLine
        {
            ProductTitleName = g.Key,
            SalesRevenue = g.Sum(x => x.PriceWithDiscount),
            SalesAmount = g.Sum(x => x.ProductAmount),
        })
        .OrderByDescending(x => x.SalesRevenue)
        .ToList();

        return new ProductTitleSalesRevenueReport(reportLines, DateTime.Now);
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
