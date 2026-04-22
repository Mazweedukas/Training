namespace StoreDAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using StoreDAL.Entities;

public interface IOrderDetailRepository : IRepository<OrderDetail>
{
    public IEnumerable<OrderDetail> GetByOrderId(int id);
}
