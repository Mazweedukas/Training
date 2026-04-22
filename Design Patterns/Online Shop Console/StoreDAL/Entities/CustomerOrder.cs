namespace StoreDAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Table("customer_orders")]
public class CustomerOrder : BaseEntity
{
    public CustomerOrder()
     : base()
    {
    }

    public CustomerOrder(int id, int userId, string operationTime, int orderStateId)
        : base(id)
    {
        this.OperationTime = operationTime;
        this.UserId = userId;
        this.OrderStateId = orderStateId;
    }

    public CustomerOrder(int userId, string operationTime, int orderStateId)
        : base()
    {
        this.OperationTime = operationTime;
        this.UserId = userId;
        this.OrderStateId = orderStateId;
    }

    [Column("customer_id"), ForeignKey(nameof(User))]
    public int UserId { get; set; }

    [Column("operation_time")]
    public string OperationTime { get; set; }

    [Column("order_state_id"), ForeignKey(nameof(State))]
    public int OrderStateId { get; set; }

    public User User { get; set; }

    public OrderState State { get; set; }

    public virtual IList<OrderDetail> Details { get; set; }
}