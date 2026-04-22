namespace StoreBLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using StoreBLL.Interfaces;
    using StoreBLL.Models;
    using StoreDAL.Data;
    using StoreDAL.Entities;
    using StoreDAL.Interfaces;
    using StoreDAL.Repository;

    /// <summary>
    /// Provides business logic for managing users, including registration,
    /// authentication, and CRUD operations.
    /// </summary>
    public class UserService : ICrud
    {
        /// <summary>
        /// Default role identifier assigned to newly registered users.
        /// </summary>
        private const int UserRoleId = 2;

        private readonly UserRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="context">Database context used for data access.</param>
        public UserService(StoreDbContext context)
        {
            this.repository = new UserRepository(context);
        }

        /// <summary>
        /// Adds a new user to the system.
        /// </summary>
        /// <param name="model">The user model containing user data.</param>
        public void Add(AbstractModel model)
        {
            var x = (UserModel)model;

            this.repository.Add(new User(
                x.FirstName,
                x.LastName,
                x.Login,
                HashPassword(x.Password),
                x.RoleId));
        }

        /// <summary>
        /// Deletes a user by identifier.
        /// </summary>
        /// <param name="modelId">The identifier of the user to delete.</param>
        public void Delete(int modelId)
        {
            this.repository.DeleteById(modelId);
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>A collection of user models.</returns>
        public IEnumerable<AbstractModel> GetAll()
        {
            return this.repository.GetAll()
                .Select(entity => new UserModel(
                    entity.Id,
                    entity.Name,
                    entity.LastName,
                    entity.Login,
                    entity.Password,
                    entity.RoleId));
        }

        /// <summary>
        /// Retrieves a user by identifier.
        /// </summary>
        /// <param name="id">The identifier of the user.</param>
        /// <returns>The corresponding user model.</returns>
        public AbstractModel GetById(int id)
        {
            User user = this.repository.GetById(id);

            return new UserModel(
                user.Id,
                user.Name,
                user.LastName,
                user.Login,
                user.Password,
                user.RoleId);
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="model">The updated user model.</param>
        public void Update(AbstractModel model)
        {
            var user = (UserModel)model;

            this.repository.Update(new User(
                user.Id,
                user.FirstName,
                user.LastName,
                user.Login,
                user.Password,
                user.RoleId));
        }

        /// <summary>
        /// Registers a new user if the login is not already taken.
        /// </summary>
        /// <param name="firstName">User's first name.</param>
        /// <param name="lastName">User's last name.</param>
        /// <param name="login">User's login.</param>
        /// <param name="password">User's password.</param>
        /// <returns>True if registration succeeded; otherwise false.</returns>
        public bool Register(string firstName, string lastName, string login, string password)
        {
            if (this.repository.GetByLogin(login) == null)
            {
                this.Add(new UserModel(firstName, lastName, login, password, UserRoleId));
                return true;
            }

            return false;
        }

        /// <summary>
        /// Authenticates a user by login and password.
        /// </summary>
        /// <param name="login">User login.</param>
        /// <param name="password">User password.</param>
        /// <returns>
        /// The authenticated user model if credentials are valid; otherwise null.
        /// </returns>
        public AbstractModel? Login(string login, string password)
        {
            var user = this.repository.GetByLogin(login);

            if (user == null)
            {
                return null;
            }

            if (user.Password != HashPassword(password))
            {
                return null;
            }

            return new UserModel(
                user.Id,
                user.Name,
                user.LastName,
                user.Login,
                user.Password,
                user.RoleId);
        }

        /// <summary>
        /// Hashes a password using SHA256 algorithm.
        /// </summary>
        /// <param name="password">The plain text password.</param>
        /// <returns>Hashed password as a Base64 string.</returns>
        private static string HashPassword(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = SHA256.HashData(bytes);

            return Convert.ToBase64String(hash);
        }
    }
}