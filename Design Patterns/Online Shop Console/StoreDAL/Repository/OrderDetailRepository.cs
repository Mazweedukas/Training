using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StoreDAL.Data;
using StoreDAL.Entities;
using StoreDAL.Interfaces;

namespace StoreDAL.Repository
{
    public class OrderDetailRepository : AbstractRepository, IOrderDetailRepository
    {
        public OrderDetailRepository(StoreDbContext context)
            : base(context)
        {
        }

        public void Add(OrderDetail entity)
        {
            this.context.Add(entity);
            this.context.SaveChanges();
        }

        public void Delete(OrderDetail entity)
        {
            this.context.Remove(entity);
            this.context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            var orderDetail = this.context.OrderDetails.Find(id);
            if (orderDetail != null)
            {
                this.context.OrderDetails.Remove(orderDetail);
                this.context.SaveChanges();
            }
        }

        public IEnumerable<OrderDetail> GetAll()
        {
            return this.context.OrderDetails.ToList();
        }

        public IEnumerable<OrderDetail> GetAll(int pageNumber, int rowCount)
        {
            return this.context.OrderDetails
                .OrderBy(od => od.Id)
                .Skip(rowCount * (pageNumber - 1))
                .Take(rowCount)
                .ToList();
        }

        public OrderDetail GetById(int id)
        {
            return this.context.OrderDetails.Find(id);
        }

        public IEnumerable<OrderDetail> GetByOrderId(int id)
        {
            return this.context.OrderDetails
                .Include(od => od.Order)
                .Include(od => od.Product)
                .Where(od => od.OrderId == id)
                .ToList();
        }

        public void Update(OrderDetail entity)
        {
            this.context.OrderDetails.Update(entity);
            this.context.SaveChanges();
        }
    }
}
