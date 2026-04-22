using ConsoleApp.Controllers;
using ConsoleApp.Handlers.ContextMenuHandlers;
using ConsoleApp.Services;
using ConsoleApp1;
using StoreDAL.Data;

namespace ConsoleMenu.Builder;

public class UserMainMenu : AbstractMenuCreator
{
    public override (ConsoleKey id, string caption, Action action)[] GetMenuItems(StoreDbContext context)
    {
        (ConsoleKey id, string caption, Action action)[] array =
            {
                (ConsoleKey.F1, "Logout", UserMenuController.Logout),
                (ConsoleKey.F2, "Show product list", ProductController.ShowAllProducts),
                (ConsoleKey.F3, "Show order list", ShopController.ShowAllOrders),
                (ConsoleKey.F4, "Cancel order", () =>
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
                (ConsoleKey.F5, "Confirm order delivery", () =>
                {
                    ShopController.ShowAllOrders();
                    Console.WriteLine($"Choose the Id of order you want to confirm:");
                    if (!int.TryParse(Console.ReadLine(), out int orderId))
                    {
                        Console.WriteLine("Incorrect input");
                        return;
                    }

                    ShopController.ChangeOrderState(orderId, OrderState.Confirmed);
                }),
                (ConsoleKey.F6, "Add order feedback", () =>
                {
                    ShopController.AddFeedback();
                }),
                (ConsoleKey.F7, "Shop / Add items", () => { ShopController.StartShopping(); }),
            };
        return array;
    }
}