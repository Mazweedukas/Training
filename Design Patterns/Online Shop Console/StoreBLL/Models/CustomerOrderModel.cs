namespace StoreBLL.Models
{
    using System;

    /// <summary>
    /// Represents a customer order in the business layer.
    /// Contains information about the customer, order time, and current state of the order.
    /// </summary>
    public class CustomerOrderModel : AbstractModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerOrderModel"/> class with all properties specified.
        /// </summary>
        /// <param name="id">Unique identifier of the order.</param>
        /// <param name="customerId">Identifier of the customer who created the order.</param>
        /// <param name="operationTime">Date and time when the order was created or last updated.</param>
        /// <param name="orderStateId">Identifier representing the current state of the order.</param>
        public CustomerOrderModel(int id, int customerId, string operationTime, int orderStateId)
            : base(id)
        {
            this.Id = id;
            this.CustomerId = customerId;
            this.OperationTime = operationTime;
            this.OrderStateId = orderStateId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerOrderModel"/> class without specifying the identifier.
        /// Used when creating a new order before it is persisted in the database.
        /// </summary>
        /// <param name="customerId">Identifier of the customer who creates the order.</param>
        /// <param name="operationTime">Date and time when the order is created.</param>
        /// <param name="orderStateId">Identifier representing the initial state of the order.</param>
        public CustomerOrderModel(int customerId, string operationTime, int orderStateId)
            : base()
        {
            this.CustomerId = customerId;
            this.OperationTime = operationTime;
            this.OrderStateId = orderStateId;
        }

        /// <summary>
        /// Gets or sets the identifier of the customer who placed the order.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the order was created or last modified.
        /// </summary>
        public string OperationTime { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the current order state.
        /// </summary>
        public int OrderStateId { get; set; }

        /// <summary>
        /// Returns a string representation of the customer order.
        /// </summary>
        /// <returns>A formatted string containing order details.</returns>
        public override string ToString()
        {
            return $"{this.Id} {this.CustomerId} {this.OperationTime} {this.OrderStateId}";
        }
    }
}