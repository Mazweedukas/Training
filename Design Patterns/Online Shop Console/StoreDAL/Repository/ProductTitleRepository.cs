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
    public class ProductTitleRepository : AbstractRepository, IProductTitleRepository
    {
        public ProductTitleRepository(StoreDbContext context)
            : base(context)
        {
        }

        public void Add(ProductTitle entity)
        {
            this.context.ProductTitles.Add(entity);
            this.context.SaveChanges();
        }

        public void Delete(ProductTitle entity)
        {
            this.context.ProductTitles.Remove(entity);
            this.context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            var entity = this.context.ProductTitles.Find(id);
            if (entity != null)
            {
                this.context.ProductTitles.Remove(entity);
                this.context.SaveChanges();
            }
        }

        public IEnumerable<ProductTitle> GetAll()
        {
            return this.context.ProductTitles.ToList();
        }

        public IEnumerable<ProductTitle> GetAll(int pageNumber, int rowCount)
        {
            return this.context.ProductTitles
                .OrderBy(pt => pt.Id)
                .Skip((pageNumber - 1) * rowCount)
                .Take(rowCount)
                .ToList();
        }

        public ProductTitle GetById(int id)
        {
            return this.context.ProductTitles.Find(id);
        }

        public void Update(ProductTitle entity)
        {
            this.context.ProductTitles.Update(entity);
            this.context.SaveChanges();
        }
    }
}
