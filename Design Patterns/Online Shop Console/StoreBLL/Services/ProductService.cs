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
    /// Provides business logic for managing products.
    /// Handles CRUD operations related to products.
    /// </summary>
    public class ProductService : ICrud
    {
        private readonly ProductRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="context">Database context used for data access.</param>
        public ProductService(StoreDbContext context)
        {
            this.repository = new ProductRepository(context);
        }

        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="model">The product model containing product data.</param>
        public void Add(AbstractModel model)
        {
            var product = (ProductModel)model;

            this.repository.Add(new Product(
                product.Id,
                product.ProductTitleId,
                product.ManufacturerId,
                product.ProductDescription,
                product.Price));
        }

        /// <summary>
        /// Deletes a product by its identifier.
        /// </summary>
        /// <param name="modelId">The identifier of the product to delete.</param>
        public void Delete(int modelId)
        {
            this.repository.DeleteById(modelId);
        }

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A collection of product models.</returns>
        public IEnumerable<AbstractModel> GetAll()
        {
            return this.repository.GetAll()
                .Select(p => new ProductModel(
                    p.Id,
                    p.TitleId,
                    p.ManufacturerId,
                    p.Description,
                    p.UnitPrice));
        }

        /// <summary>
        /// Retrieves a product by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the product.</param>
        /// <returns>The corresponding product model.</returns>
        public AbstractModel GetById(int id)
        {
            var product = this.repository.GetById(id);

            return new ProductModel(
                product.Id,
                product.TitleId,
                product.ManufacturerId,
                product.Description,
                product.UnitPrice);
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="model">The updated product model.</param>
        public void Update(AbstractModel model)
        {
            var product = (ProductModel)model;

            this.repository.Update(new Product(
                product.Id,
                product.ProductTitleId,
                product.ManufacturerId,
                product.ProductDescription,
                product.Price));
        }
    }
}