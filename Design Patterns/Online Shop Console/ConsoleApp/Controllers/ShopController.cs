using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Controllers;
using ConsoleApp.Handlers.ContextMenuHandlers;
using ConsoleApp.Helpers;
using ConsoleApp1;
using ConsoleMenu;
using StoreBLL.Models;
using StoreBLL.Services;
using StoreDAL.Data;

namespace ConsoleApp.Services
{
    public static class ShopController
    {
        private const int NewOrderId = 1;

        private static readonly Dictionary<int, string> OrderFeedback = new ();

        private static int userId = UserMenuController.CurrentUserId;

        private static StoreDbContext context = UserMenuController.Context;

        public static int AddOrder()
        {
            var service = new CustomerOrderService(context);
            var customerOrderId = service.CreateNewOrder(new CustomerOrderModel(userId, DateTime.Now.ToString(CultureInfo.InvariantCulture), NewOrderId));
            return customerOrderId;
        }

        public static void UpdateOrder(int orderId, string operationTime, int orderStateId)
        {
            var cOS = new CustomerOrderService(context);
            cOS.Update(new CustomerOrderModel(orderId, operationTime, orderStateId));
        }

        public static void ChangeOrderState(int id, OrderState orderState)
        {
            var service = new CustomerOrderService(context);

            try
            {
                service.ChangeOrderState(id, (int)orderState);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void DeleteOrder()
        {
            throw new NotImplementedException();
        }

        public static void ShowOrder()
        {
            var oDService = new OrderDetailService(context);
            var pService = new ProductService(context);
            var pTService = new ProductTitleService(context);
            var ordersList = oDService.GetByOrderId(UserMenuController.CurrentOrderId);

            foreach (var order in ordersList)
            {
                var oDM = (OrderDetailModel)order;
                var pM = (ProductModel)pService.GetById(oDM.ProductId);
                var tM = (ProductTitleModel)pTService.GetById(pM.ProductTitleId);
                Console.WriteLine($"Id: {oDM.Id}, Customer Order Id: {oDM.CustomerOrderId}, Product: {tM.Title}, Price: {oDM.Price}, Product Amount: {oDM.ProductAmount}");
            }
        }

        public static void ShowAllOrders()
        {
            var cOS = new CustomerOrderService(context);
            var customerOrdersList = cOS.GetByUserId(UserMenuController.CurrentUserId);

            foreach (var customerOrder in customerOrdersList)
            {
                Console.WriteLine(customerOrder);
            }
        }

        public static void ShowAllOrdersAdmin()
        {
            var oDService = new OrderDetailService(context);
            var pService = new ProductService(context);
            var pTService = new ProductTitleService(context);
            var ordersList = oDService.GetAll();

            foreach (var order in ordersList)
            {
                var oDM = (OrderDetailModel)order;
                var pM = (ProductModel)pService.GetById(oDM.ProductId);
                var tM = (ProductTitleModel)pTService.GetById(pM.ProductTitleId);
                Console.WriteLine($"Customer Order Id: {oDM.CustomerOrderId}, Product: {tM.Title}, Price: {oDM.Price}, Product Amount: {oDM.ProductAmount}");
            }
        }

        public static void AddOrderDetails(ProductModel product, int amount)
        {
            var service = new OrderDetailService(context);
            service.Add(new OrderDetailModel(UserMenuController.CurrentOrderId, product.Id, product.Price, amount));
        }

        public static void UpdateOrderDetails()
        {
            throw new NotImplementedException();
        }

        public static void DeleteOrderDetails()
        {
            throw new NotImplementedException();
        }

        public static void ShowAllOrderDetails()
        {
            throw new NotImplementedException();
        }

        public static void ProcessOrder()
        {
            throw new NotImplementedException();
        }

        public static void ShowAllOrderStates()
        {
            var service = new OrderStateService(context);
            var menu = new ContextMenu(new AdminContextMenuHandler(service, InputHelper.ReadOrderStateModel), service.GetAll);
            menu.Run();
        }

        public static void StartShopping()
        {
            var productService = new ProductService(context);

            var handler = new ShoppingContextMenuHandler(productService, () => new ProductModel(0, 0, string.Empty, 0));

            var menu = new ContextMenu(
                handler.GenerateMenuItems,
                productService.GetAll);

            menu.Run();
        }

        public static void AddFeedback()
        {
            Console.WriteLine("Enter order ID:");
            if (!int.TryParse(Console.ReadLine(), out int orderId))
            {
                return;
            }

            Console.WriteLine("Enter feedback:");
            var feedback = Console.ReadLine();

            if (feedback == null)
            {
                return;
            }

            OrderFeedback[orderId] = feedback;

            Console.WriteLine("Feedback saved (temporary)");
        }
    }
}
