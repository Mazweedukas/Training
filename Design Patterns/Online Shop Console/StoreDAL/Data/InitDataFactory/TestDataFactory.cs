namespace StoreDAL.Data.InitDataFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreDAL.Entities;

public class TestDataFactory : AbstractDataFactory
{
    public override Category[] GetCategoryData()
    {
        return new[]
        {
            new Category(1, "fruits"),
            new Category(2, "water"),
            new Category(3, "vegetables"),
            new Category(4, "seafood"),
            new Category(5, "meet"),
            new Category(6, "grocery"),
            new Category(7, "milk food"),
            new Category(8, "smartphones"),
            new Category(9, "laptop"),
            new Category(10, "photocameras"),
            new Category(11, "kitchen accesories"),
            new Category(12, "spices"),
            new Category(13, "Juice"),
            new Category(14, "alcohol drinks"),
        };
    }

    public override CustomerOrder[] GetCustomerOrderData()
    {
        return new[]
        {
            new CustomerOrder(1, 1, "2026-04-12 09:30:00", 4),
            new CustomerOrder(2, 2, "2026-04-12 10:00:00", 1),
            new CustomerOrder(3, 3, "2026-04-12 11:15:00", 6),
            new CustomerOrder(4, 2, "2026-04-12 12:00:00", 7),
        };
    }

    public override Manufacturer[] GetManufacturerData()
    {
        return new[]
        {
            new Manufacturer(1, "Jumbo"),
            new Manufacturer(2, "TheDrinks"),
            new Manufacturer(3, "TechCorp"),
            new Manufacturer(4, "FreshFarm"),
        };
    }

    public override OrderDetail[] GetOrderDetailData()
    {
        return new[]
        {
            new OrderDetail(1, 1, 1, 30.90m, 2),   // vodka x2
            new OrderDetail(2, 1, 4, 4.40m, 2),    // apples x2

            new OrderDetail(3, 2, 3, 7.00m, 2),    // juice x2
            new OrderDetail(4, 2, 6, 3.00m, 2),    // milk x2

            new OrderDetail(5, 3, 7, 999.99m, 1),  // iPhone x1

            new OrderDetail(6, 4, 8, 1499.99m, 1), // laptop x1
            new OrderDetail(7, 4, 10, 5.98m, 2),   // pepper x2
        };
    }

    public override OrderState[] GetOrderStateData()
    {
        return new[]
        {
            new OrderState(1, "New Order"),
            new OrderState(2, "Cancelled by user"),
            new OrderState(3, "Cancelled by administrator"),
            new OrderState(4, "Confirmed"),
            new OrderState(5, "Moved to delivery company"),
            new OrderState(6, "In delivery"),
            new OrderState(7, "Delivered to client"),
            new OrderState(8, "Delivery confirmed by client"),
        };
    }

    public override Product[] GetProductData()
    {
        return new[]
        {
            new Product(1, 1, 2, "Premium Vodka", 15.45m),
            new Product(2, 2, 2, "Classic Rum", 20.00m),
            new Product(3, 3, 2, "Fresh Orange Juice", 3.50m),
            new Product(4, 4, 4, "Green Apples", 2.20m),
            new Product(5, 5, 4, "Atlantic Salmon", 12.75m),
            new Product(6, 6, 4, "Whole Milk", 1.50m),
            new Product(7, 7, 3, "Latest iPhone model", 999.99m),
            new Product(8, 8, 3, "High-end gaming laptop", 1499.99m),
            new Product(9, 9, 3, "Professional DSLR", 850.00m),
            new Product(10, 10, 1, "Ground black pepper", 2.99m),
        };
    }

    public override ProductTitle[] GetProductTitleData()
    {
        return new[]
        {
            new ProductTitle(1, "Vodka", 14),
            new ProductTitle(2, "Rum", 14),
            new ProductTitle(3, "Orange Juice", 13),
            new ProductTitle(4, "Apple", 1),
            new ProductTitle(5, "Salmon", 4),
            new ProductTitle(6, "Milk", 7),
            new ProductTitle(7, "iPhone 15", 8),
            new ProductTitle(8, "Gaming Laptop", 9),
            new ProductTitle(9, "DSLR Camera", 10),
            new ProductTitle(10, "Black Pepper", 12),
        };
    }

    public override User[] GetUserData()
    {
        return new[]
        {
            new User(1, "Tom", "Brady", "admin", "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", 1),
            new User(2, "Alice", "Smith", "alice123", "G6PRbpiBlZ+MmpdihU9yxuYyHN1ENYoQpOk5AzEX6rk=", 2),
            new User(3, "Bob", "Johnson", "bob123", "pass3", 2),
            new User(4, "Emma", "Brown", "emma123", "pass4", 3),
        };
    }

    public override UserRole[] GetUserRoleData()
    {
        return new[]
        {
            new UserRole(1, "Admin"),
            new UserRole(2, "Registered"),
            new UserRole(3, "Guest"),
        };
    }
}
