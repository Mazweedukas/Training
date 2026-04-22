using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreDAL.Data;
using StoreDAL.Entities;
using StoreDAL.Interfaces;

namespace StoreDAL.Repository
{
    public class CustomerOrderRepository : AbstractRepository, ICustomerOrderRepository
    {
        public CustomerOrderRepository(StoreDbContext context)
            : base(context)
        {
        }

        public void Add(CustomerOrder entity)
        {
            this.context.CustomerOrders.Add(entity);
            this.context.SaveChanges();
        }

        public void Delete(CustomerOrder entity)
        {
            this.context.CustomerOrders.Remove(entity);
            this.context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            var customerOrder = this.context.CustomerOrders.Find(id);
            if (customerOrder == null)
            {
                return;
            }

            this.Delete(customerOrder);
        }

        public IEnumerable<CustomerOrder> GetAll()
        {
            return this.context.CustomerOrders.ToList();
        }

        public IEnumerable<CustomerOrder> GetAll(int pageNumber, int rowCount)
        {
            return this.context.CustomerOrders
                .OrderBy(co => co.Id)
                .Skip((pageNumber - 1) * rowCount)
                .Take(rowCount)
                .ToList();
        }

        public CustomerOrder GetById(int id)
        {
            return this.context.CustomerOrders.Find(id);
        }

        public void Update(CustomerOrder entity)
        {
            this.context.CustomerOrders.Update(entity);
            this.context.SaveChanges();
        }
    }
}
