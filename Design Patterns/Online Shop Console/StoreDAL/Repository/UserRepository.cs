using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using StoreDAL.Data;
using StoreDAL.Entities;
using StoreDAL.Interfaces;

namespace StoreDAL.Repository
{
    public class UserRepository : AbstractRepository, IUserRepository
    {
        public UserRepository(StoreDbContext context)
            : base(context)
        {
        }

        public void Add(User entity)
        {
            if (entity == null)
            {
                return;
            }

            this.context.Users.Add(entity);
            this.context.SaveChanges();
        }

        public void Delete(User entity)
        {
            if (entity == null)
            {
                return;
            }

            this.context.Users.Remove(entity);
            this.context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            var entity = this.context.Users.Find(id);
            if (entity != null)
            {
                this.context.Users.Remove(entity);
                this.context.SaveChanges();
            }
        }

        public IEnumerable<User> GetAll()
        {
            return this.context.Users.ToList();
        }

        public IEnumerable<User> GetAll(int pageNumber, int rowCount)
        {
            return this.context.Users
                .OrderBy(u => u.Id)
                .Skip((pageNumber - 1) * rowCount)
                .Take(rowCount)
                .ToList();
        }

        public User GetById(int id)
        {
            return this.context.Users.Find(id);
        }

        public void Update(User entity)
        {
            if (entity == null)
            {
                return;
            }

            this.context.Users.Update(entity);
            this.context.SaveChanges();
        }

        public User GetByLogin(string login)
        {
            return this.context.Users.FirstOrDefault(user => user.Login == login);
        }
    }
}