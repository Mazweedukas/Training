using ConsoleApp.Controllers;
using ConsoleApp.Services;
using ConsoleApp1;
using StoreDAL.Data;

namespace ConsoleMenu.Builder;

public class AdminMainMenu : AbstractMenuCreator
{
    public override (ConsoleKey id, string caption, Action action)[] GetMenuItems(StoreDbContext context)
    {
        (ConsoleKey id, string caption, Action action)[] array =
            {
                (ConsoleKey.F1, "Logout", UserMenuController.Logout),
                (ConsoleKey.F2, "Show product list", () => { ProductController.ShowAllProducts(); }),
                (ConsoleKey.F3, "Add product", () => { Console.WriteLine("Add product"); }),
                (ConsoleKey.F4, "Show order list", () => { ShopController.ShowAllOrdersAdmin(); }),
                (ConsoleKey.F5, "Cancel order", () =>
                {
                    ShopController.ShowAllOrders();
                    Console.WriteLine($"Choose the Id of order you want to cancel:");
                    if (!int.TryParse(Console.ReadLine(), out int orderId))
                    {
                        Console.WriteLine("Incorrect input");
                        return;
                    }

                    ShopController.ChangeOrderState(orderId, OrderState.CancelledByUser);
                }),
                (ConsoleKey.F6, "Change order status", () =>
                {
                    ShopController.ShowAllOrdersAdmin();
                    Console.WriteLine($"Choose the Id of order you want to change:");
                    if (!int.TryParse(Console.ReadLine(), out int orderId))
                    {
                        Console.WriteLine("Incorrect input");
                        return;
                    }

                    Console.WriteLine($"Choose the order state id you want to change to:");
                    if (!int.TryParse(Console.ReadLine(), out int orderStateId))
                    {
                        Console.WriteLine("Incorrect input");
                        return;
                    }

                    ShopController.ChangeOrderState(orderId, (OrderState)orderStateId);
                }),
                (ConsoleKey.F7, "User roles", UserController.ShowAllUserRoles),
                (ConsoleKey.F8, "Order states", ShopController.ShowAllOrderStates),
            };
        return array;
    }
}