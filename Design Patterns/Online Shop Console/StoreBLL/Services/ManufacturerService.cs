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
    /// Provides business logic for managing manufacturers.
    /// Handles CRUD operations related to manufacturers.
    /// </summary>
    public class ManufacturerService : ICrud
    {
        private readonly ManufacturerRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManufacturerService"/> class.
        /// </summary>
        /// <param name="context">Database context used for data access.</param>
        public ManufacturerService(StoreDbContext context)
        {
            this.repository = new ManufacturerRepository(context);
        }

        /// <summary>
        /// Adds a new manufacturer.
        /// </summary>
        /// <param name="model">The manufacturer model containing data.</param>
        public void Add(AbstractModel model)
        {
            var manufacturer = (ManufacturerModel)model;

            this.repository.Add(new Manufacturer(
                manufacturer.Name));
        }

        /// <summary>
        /// Deletes a manufacturer by its identifier.
        /// </summary>
        /// <param name="modelId">The identifier of the manufacturer to delete.</param>
        public void Delete(int modelId)
        {
            this.repository.DeleteById(modelId);
        }

        /// <summary>
        /// Retrieves all manufacturers.
        /// </summary>
        /// <returns>A collection of manufacturer models.</returns>
        public IEnumerable<AbstractModel> GetAll()
        {
            return this.repository.GetAll()
                .Select(m => new ManufacturerModel(
                    m.Id,
                    m.Name));
        }

        /// <summary>
        /// Retrieves a manufacturer by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the manufacturer.</param>
        /// <returns>The corresponding manufacturer model.</returns>
        public AbstractModel GetById(int id)
        {
            var manufacturer = this.repository.GetById(id);

            if (manufacturer == null)
            {
                throw new ArgumentException("Manufacturer not found");
            }

            return new ManufacturerModel(
                manufacturer.Id,
                manufacturer.Name);
        }

        /// <summary>
        /// Updates an existing manufacturer.
        /// </summary>
        /// <param name="model">The updated manufacturer model.</param>
        public void Update(AbstractModel model)
        {
            var manufacturer = (ManufacturerModel)model;

            this.repository.Update(new Manufacturer(
                manufacturer.Id,
                manufacturer.Name));
        }
    }
}