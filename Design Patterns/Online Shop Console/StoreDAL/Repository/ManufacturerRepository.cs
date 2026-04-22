using System;
using System.Collections.Generic;
using System.Linq;
using StoreDAL.Data;
using StoreDAL.Entities;
using StoreDAL.Interfaces;

namespace StoreDAL.Repository
{
    /// <summary>
    /// Provides data access operations for manufacturers.
    /// </summary>
    public class ManufacturerRepository : AbstractRepository, IManufacturerRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManufacturerRepository"/> class.
        /// </summary>
        /// <param name="context">Database context.</param>
        public ManufacturerRepository(StoreDbContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Adds a new manufacturer.
        /// </summary>
        /// <param name="entity">The manufacturer entity.</param>
        public void Add(Manufacturer entity)
        {
            if (entity == null)
            {
                return;
            }

            this.context.Manufacturers.Add(entity);
            this.context.SaveChanges();
        }

        /// <summary>
        /// Deletes a manufacturer.
        /// </summary>
        /// <param name="entity">The manufacturer entity.</param>
        public void Delete(Manufacturer entity)
        {
            if (entity == null)
            {
                return;
            }

            this.context.Manufacturers.Remove(entity);
            this.context.SaveChanges();
        }

        /// <summary>
        /// Deletes a manufacturer by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the manufacturer.</param>
        public void DeleteById(int id)
        {
            var entity = this.context.Manufacturers.Find(id);

            if (entity != null)
            {
                this.context.Manufacturers.Remove(entity);
                this.context.SaveChanges();
            }
        }

        /// <summary>
        /// Retrieves all manufacturers.
        /// </summary>
        /// <returns>A collection of manufacturers.</returns>
        public IEnumerable<Manufacturer> GetAll()
        {
            return this.context.Manufacturers.ToList();
        }

        /// <summary>
        /// Retrieves manufacturers with pagination.
        /// </summary>
        /// <param name="pageNumber">Page number.</param>
        /// <param name="rowCount">Number of records per page.</param>
        /// <returns>A paginated collection of manufacturers.</returns>
        public IEnumerable<Manufacturer> GetAll(int pageNumber, int rowCount)
        {
            return this.context.Manufacturers
                .OrderBy(m => m.Id)
                .Skip((pageNumber - 1) * rowCount)
                .Take(rowCount)
                .ToList();
        }

        /// <summary>
        /// Retrieves a manufacturer by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the manufacturer.</param>
        /// <returns>The manufacturer entity.</returns>
        public Manufacturer GetById(int id)
        {
            return this.context.Manufacturers.Find(id);
        }

        /// <summary>
        /// Updates an existing manufacturer.
        /// </summary>
        /// <param name="entity">The manufacturer entity.</param>
        public void Update(Manufacturer entity)
        {
            if (entity == null)
            {
                return;
            }

            this.context.Manufacturers.Update(entity);
            this.context.SaveChanges();
        }
    }
}