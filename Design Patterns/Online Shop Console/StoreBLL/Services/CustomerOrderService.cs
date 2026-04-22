namespace StoreBLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StoreBLL.Interfaces;
    using StoreBLL.Models;
    using StoreDAL.Data;
    using StoreDAL.Entities;
    using StoreDAL.Interfaces;
    using StoreDAL.Repository;

    /// <summary>
    /// Provides business logic operations for managing customer orders.
    /// Handles CRUD operations, order creation, and order state transitions.
    /// </summary>
    public class CustomerOrderService : ICrud
    {
        /// <summary>
        /// Defines allowed transitions between order states.
        /// Key = current state, Value = list of allowed next states.
        /// </summary>
        private static readonly Dictionary<int, List<int>> AllowedTransitions = new ()
        {
            { 1, new List<int> { 2, 3, 4 } },
            { 4, new List<int> { 5, 2, 3 } },
            { 5, new List<int> { 6, 3 } },
            { 6, new List<int> { 7, 3 } },
            { 7, new List<int> { 8, 3 } },
        };

        private readonly ICustomerOrderRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerOrderService"/> class.
        /// </summary>
        /// <param name="context">Database context used for data access.</param>
        public CustomerOrderService(StoreDbContext context)
        {
            this.repository = new CustomerOrderRepository(context);
        }

        /// <summary>
        /// Adds a new customer order.
        /// </summary>
        /// <param name="model">The order model containing order data.</param>
        public void Add(AbstractModel model)
        {
            var customerOrder = (CustomerOrderModel)model;
            this.repository.Add(new CustomerOrder(
                customerOrder.CustomerId,
                customerOrder.OperationTime,
                customerOrder.OrderStateId));
        }

        /// <summary>
        /// Deletes a customer order by its identifier.
        /// </summary>
        /// <param name="modelId">The identifier of the order to delete.</param>
        public void Delete(int modelId)
        {
            this.repository.DeleteById(modelId);
        }

        /// <summary>
        /// Retrieves all customer orders.
        /// </summary>
        /// <returns>A collection of customer order models.</returns>
        public IEnumerable<AbstractModel> GetAll()
        {
            return this.repository.GetAll()
                .Select(co => new CustomerOrderModel(
                    co.Id,
                    co.UserId,
                    co.OperationTime,
                    co.OrderStateId));
        }

        /// <summary>
        /// Retrieves a customer order by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the order.</param>
        /// <returns>The corresponding customer order model.</returns>
        public AbstractModel GetById(int id)
        {
            var customerOrder = this.repository.GetById(id);

            return new CustomerOrderModel(
                customerOrder.Id,
                customerOrder.UserId,
                customerOrder.OperationTime,
                customerOrder.OrderStateId);
        }

        /// <summary>
        /// Retrieves all orders associated with a specific user.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <returns>A collection of customer order models.</returns>
        public IEnumerable<AbstractModel> GetByUserId(int id)
        {
            return this.repository.GetAll()
                .Where(co => co.UserId == id)
                .Select(co => new CustomerOrderModel(
                    co.Id,
                    co.UserId,
                    co.OperationTime,
                    co.OrderStateId))
                .ToList();
        }

        /// <summary>
        /// Updates an existing customer order.
        /// </summary>
        /// <param name="model">The updated order model.</param>
        public void Update(AbstractModel model)
        {
            var customerOrder = (CustomerOrderModel)model;

            this.repository.Update(new CustomerOrder(
                customerOrder.Id,
                customerOrder.CustomerId,
                customerOrder.OperationTime,
                customerOrder.OrderStateId));
        }

        /// <summary>
        /// Creates a new order and returns its generated identifier.
        /// </summary>
        /// <param name="model">The order model.</param>
        /// <returns>The identifier of the newly created order.</returns>
        public int CreateNewOrder(AbstractModel model)
        {
            var customerOrder = (CustomerOrderModel)model;

            var newOrder = new CustomerOrder(
                customerOrder.CustomerId,
                customerOrder.OperationTime,
                customerOrder.OrderStateId);

            this.repository.Add(newOrder);

            return newOrder.Id;
        }

        /// <summary>
        /// Changes the state of an order if the transition is valid.
        /// </summary>
        /// <param name="id">The identifier of the order.</param>
        /// <param name="newStateId">The desired new state identifier.</param>
        public void ChangeOrderState(int id, int newStateId)
        {
            var order = this.repository.GetById(id);

            int currentState = order.OrderStateId;

            if (!AllowedTransitions.ContainsKey(currentState) ||
                !AllowedTransitions[currentState].Contains(newStateId))
            {
                Console.WriteLine("Invalid state transition");
                return;
            }

            order.OrderStateId = newStateId;

            this.repository.Update(order);
        }
    }
}