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
    /// Provides business logic for managing user roles.
    /// Handles CRUD operations related to user roles.
    /// </summary>
    public class UserRoleService : ICrud
    {
        private readonly UserRoleRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRoleService"/> class.
        /// </summary>
        /// <param name="context">Database context used for data access.</param>
        public UserRoleService(StoreDbContext context)
        {
            this.repository = new UserRoleRepository(context);
        }

        /// <summary>
        /// Adds a new user role.
        /// </summary>
        /// <param name="model">The user role model containing role data.</param>
        public void Add(AbstractModel model)
        {
            var x = (UserRoleModel)model;

            this.repository.Add(new UserRole(x.Id, x.RoleName));
        }

        /// <summary>
        /// Deletes a user role by its identifier.
        /// </summary>
        /// <param name="modelId">The identifier of the role to delete.</param>
        public void Delete(int modelId)
        {
            this.repository.DeleteById(modelId);
        }

        /// <summary>
        /// Retrieves all user roles.
        /// </summary>
        /// <returns>A collection of user role models.</returns>
        public IEnumerable<AbstractModel> GetAll()
        {
            return this.repository.GetAll()
                .Select(x => new UserRoleModel(x.Id, x.RoleName));
        }

        /// <summary>
        /// Retrieves a user role by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the role.</param>
        /// <returns>The corresponding user role model.</returns>
        public AbstractModel GetById(int id)
        {
            var res = this.repository.GetById(id);

            return new UserRoleModel(res.Id, res.RoleName);
        }

        /// <summary>
        /// Updates an existing user role.
        /// </summary>
        /// <param name="model">The updated user role model.</param>
        /// <exception cref="NotImplementedException">
        /// Thrown when the method is not yet implemented.
        /// </exception>
        public void Update(AbstractModel model)
        {
            throw new NotImplementedException();
        }
    }
}