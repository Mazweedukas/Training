using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Controllers;
using ConsoleApp.Services;
using ConsoleApp1;
using StoreBLL.Interfaces;
using StoreBLL.Models;

namespace ConsoleApp.Handlers.ContextMenuHandlers;

public class ShoppingContextMenuHandler : ContextMenuHandler
{
    public ShoppingContextMenuHandler(ICrud service, Func<AbstractModel> readModel)
        : base(service, readModel)
    {
    }

    public static void CreateOrder()
    {
        if (UserMenuController.CurrentOrderId == 0)
        {
            UserMenuController.CurrentOrderId = ShopController.AddOrder();
        }

        // Choose items to add
        ProductController.ShowAllProducts();
        Console.WriteLine("Write the product Id of product you would like to add");
        if (!int.TryParse(Console.ReadLine(), out int productChoice))
        {
            Console.WriteLine("Incorrect input");
            return;
        }

        Console.WriteLine("Write the amount of products you want to add");
        if (!int.TryParse(Console.ReadLine(), out int productAmount))
        {
            Console.WriteLine("Incorrect input");
            return;
        }

        var product = ProductController.GetProductById(productChoice);
        if (product == null)
        {
            Console.WriteLine("Product not found");
            return;
        }

        ShopController.AddOrderDetails(product, productAmount);
        ShopController.ShowOrder();
    }

    public override (ConsoleKey id, string caption, Action action)[] GenerateMenuItems()
    {
        (ConsoleKey id, string caption, Action action)[] array =
            {
                 (ConsoleKey.V, "View Details", this.GetItemDetails),
                 (ConsoleKey.A, "Add item to chart and create order", CreateOrder),
            };
        return array;
    }
}