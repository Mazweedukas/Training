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
    /// Provides business logic for managing order states.
    /// Handles CRUD operations related to order states.
    /// </summary>
    public class OrderStateService : ICrud
    {
        private readonly OrderStateRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderStateService"/> class.
        /// </summary>
        /// <param name="context">Database context used for data access.</param>
        public OrderStateService(StoreDbContext context)
        {
            this.repository = new OrderStateRepository(context);
        }

        /// <summary>
        /// Adds a new order state.
        /// </summary>
        /// <param name="model">The order state model containing state data.</param>
        public void Add(AbstractModel model)
        {
            var x = (OrderStateModel)model;

            this.repository.Add(new OrderState(x.Id, x.StateName));
        }

        /// <summary>
        /// Deletes an order state by its identifier.
        /// </summary>
        /// <param name="modelId">The identifier of the order state to delete.</param>
        public void Delete(int modelId)
        {
            this.repository.DeleteById(modelId);
        }

        /// <summary>
        /// Retrieves all order states.
        /// </summary>
        /// <returns>A collection of order state models.</returns>
        public IEnumerable<AbstractModel> GetAll()
        {
            return this.repository.GetAll()
                .Select(x => new OrderStateModel(x.Id, x.StateName));
        }

        /// <summary>
        /// Retrieves an order state by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the order state.</param>
        /// <returns>The corresponding order state model.</returns>
        public AbstractModel GetById(int id)
        {
            var res = this.repository.GetById(id);

            return new OrderStateModel(res.Id, res.StateName);
        }

        /// <summary>
        /// Updates an existing order state.
        /// </summary>
        /// <param name="model">The updated order state model.</param>
        /// <exception cref="NotImplementedException">
        /// Thrown when the method is not yet implemented.
        /// </exception>
        public void Update(AbstractModel model)
        {
            throw new NotImplementedException();
        }
    }
}