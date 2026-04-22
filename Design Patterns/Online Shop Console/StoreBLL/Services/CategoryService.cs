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
    /// Provides business logic for managing categories.
    /// Handles CRUD operations related to categories.
    /// </summary>
    public class CategoryService : ICrud
    {
        private readonly CategoryRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryService"/> class.
        /// </summary>
        /// <param name="context">Database context used for data access.</param>
        public CategoryService(StoreDbContext context)
        {
            this.repository = new CategoryRepository(context);
        }

        /// <summary>
        /// Adds a new category.
        /// </summary>
        /// <param name="model">The category model containing data.</param>
        public void Add(AbstractModel model)
        {
            var category = (CategoryModel)model;

            this.repository.Add(new Category(category.Name));
        }

        /// <summary>
        /// Deletes a category by its identifier.
        /// </summary>
        /// <param name="modelId">The identifier of the category to delete.</param>
        public void Delete(int modelId)
        {
            this.repository.DeleteById(modelId);
        }

        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        /// <returns>A collection of category models.</returns>
        public IEnumerable<AbstractModel> GetAll()
        {
            return this.repository.GetAll()
                .Select(c => new CategoryModel(
                    c.Id,
                    c.Name));
        }

        /// <summary>
        /// Retrieves a category by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the category.</param>
        /// <returns>The corresponding category model.</returns>
        public AbstractModel GetById(int id)
        {
            var category = this.repository.GetById(id);

            if (category == null)
            {
                throw new ArgumentException("Category not found");
            }

            return new CategoryModel(
                category.Id,
                category.Name);
        }

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="model">The updated category model.</param>
        public void Update(AbstractModel model)
        {
            var category = (CategoryModel)model;

            this.repository.Update(new Category(
                category.Id,
                category.Name));
        }
    }
}