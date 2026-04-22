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
    /// Provides business logic for managing product titles.
    /// Handles CRUD operations related to product titles.
    /// </summary>
    public class ProductTitleService : ICrud
    {
        private readonly ProductTitleRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductTitleService"/> class.
        /// </summary>
        /// <param name="context">Database context used for data access.</param>
        public ProductTitleService(StoreDbContext context)
        {
            this.repository = new ProductTitleRepository(context);
        }

        /// <summary>
        /// Adds a new product title.
        /// </summary>
        /// <param name="model">The product title model containing title data.</param>
        public void Add(AbstractModel model)
        {
            var titleModel = (ProductTitleModel)model;

            this.repository.Add(new ProductTitle(
                titleModel.Id,
                titleModel.Title,
                titleModel.CategoryId));
        }

        /// <summary>
        /// Deletes a product title by its identifier.
        /// </summary>
        /// <param name="modelId">The identifier of the product title to delete.</param>
        public void Delete(int modelId)
        {
            this.repository.DeleteById(modelId);
        }

        /// <summary>
        /// Retrieves all product titles.
        /// </summary>
        /// <returns>A collection of product title models.</returns>
        public IEnumerable<AbstractModel> GetAll()
        {
            return this.repository.GetAll()
                .Select(pt => new ProductTitleModel(
                    pt.Id,
                    pt.Title,
                    pt.CategoryId))
                .ToList();
        }

        /// <summary>
        /// Retrieves a product title by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the product title.</param>
        /// <returns>The corresponding product title model.</returns>
        public AbstractModel GetById(int id)
        {
            var productTitle = this.repository.GetById(id);

            return new ProductTitleModel(
                productTitle.Id,
                productTitle.Title,
                productTitle.CategoryId);
        }

        /// <summary>
        /// Updates an existing product title.
        /// </summary>
        /// <param name="model">The updated product title model.</param>
        public void Update(AbstractModel model)
        {
            var titleModel = (ProductTitleModel)model;

            this.repository.Update(new ProductTitle(
                titleModel.Id,
                titleModel.Title,
                titleModel.CategoryId));
        }
    }
}