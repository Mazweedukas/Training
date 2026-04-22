namespace StoreBLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StoreBLL.Interfaces;
    using StoreBLL.Models;
    using StoreDAL.Data;
    using StoreDAL.Entities;
    using StoreDAL.Repository;

    /// <summary>
    /// Provides business logic for managing order details.
    /// Handles CRUD operations and retrieval of order items for specific orders.
    /// </summary>
    public class OrderDetailService : ICrud
    {
        private readonly OrderDetailRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderDetailService"/> class.
        /// </summary>
        /// <param name="context">Database context used for data access.</param>
        public OrderDetailService(StoreDbContext context)
        {
            this.repository = new OrderDetailRepository(context);
        }

        /// <summary>
        /// Adds a new order detail (item in an order).
        /// </summary>
        /// <param name="model">The order detail model containing item data.</param>
        public void Add(AbstractModel model)
        {
            var orderDetail = (OrderDetailModel)model;

            this.repository.Add(new OrderDetail(
                orderDetail.CustomerOrderId,
                orderDetail.ProductId,
                orderDetail.Price,
                orderDetail.ProductAmount));
        }

        /// <summary>
        /// Deletes an order detail by its identifier.
        /// </summary>
        /// <param name="modelId">The identifier of the order detail to delete.</param>
        public void Delete(int modelId)
        {
            this.repository.DeleteById(modelId);
        }

        /// <summary>
        /// Retrieves all order details.
        /// </summary>
        /// <returns>A collection of order detail models.</returns>
        public IEnumerable<AbstractModel> GetAll()
        {
            return this.repository.GetAll()
                .Select(od => new OrderDetailModel(
                    od.Id,
                    od.OrderId,
                    od.ProductId,
                    od.Price,
                    od.ProductAmount));
        }

        /// <summary>
        /// Retrieves an order detail by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the order detail.</param>
        /// <returns>The corresponding order detail model.</returns>
        public AbstractModel GetById(int id)
        {
            var orderDetail = this.repository.GetById(id);

            return new OrderDetailModel(
                orderDetail.Id,
                orderDetail.OrderId,
                orderDetail.ProductId,
                orderDetail.Price,
                orderDetail.ProductAmount);
        }

        /// <summary>
        /// Retrieves all order details for a specific order.
        /// </summary>
        /// <param name="id">The identifier of the customer order.</param>
        /// <returns>A list of order detail models.</returns>
        public List<AbstractModel> GetByOrderId(int id)
        {
            return this.repository.GetByOrderId(id)
                .Select(od => new OrderDetailModel(
                    od.Id,
                    od.OrderId,
                    od.ProductId,
                    od.Price,
                    od.ProductAmount))
                .Cast<AbstractModel>()
                .ToList();
        }

        /// <summary>
        /// Updates an existing order detail.
        /// </summary>
        /// <param name="model">The updated order detail model.</param>
        public void Update(AbstractModel model)
        {
            var orderDetail = (OrderDetailModel)model;

            this.repository.Update(new OrderDetail(
                orderDetail.Id,
                orderDetail.CustomerOrderId,
                orderDetail.ProductId,
                orderDetail.Price,
                orderDetail.ProductAmount));
        }
    }
}