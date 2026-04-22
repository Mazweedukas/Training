namespace StoreBLL.Models
{
    using System;

    /// <summary>
    /// Represents a single item within a customer order in the business layer.
    /// Contains information about the product, its price, and quantity in the order.
    /// </summary>
    public class OrderDetailModel : AbstractModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderDetailModel"/> class with all properties specified.
        /// </summary>
        /// <param name="id">Unique identifier of the order detail.</param>
        /// <param name="customerOrderId">Identifier of the associated customer order.</param>
        /// <param name="productId">Identifier of the product included in the order.</param>
        /// <param name="price">Price of the product at the time of ordering.</param>
        /// <param name="productAmount">Quantity of the product in the order.</param>
        public OrderDetailModel(int id, int customerOrderId, int productId, decimal price, int productAmount)
            : base(id)
        {
            this.Id = id;
            this.CustomerOrderId = customerOrderId;
            this.ProductId = productId;
            this.Price = price;
            this.ProductAmount = productAmount;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderDetailModel"/> class without specifying the identifier.
        /// Used when creating a new order detail before it is persisted in the database.
        /// </summary>
        /// <param name="customerOrderId">Identifier of the associated customer order.</param>
        /// <param name="productId">Identifier of the product being added to the order.</param>
        /// <param name="price">Price of the product at the time of adding to the order.</param>
        /// <param name="productAmount">Quantity of the product.</param>
        public OrderDetailModel(int customerOrderId, int productId, decimal price, int productAmount)
            : base()
        {
            this.CustomerOrderId = customerOrderId;
            this.ProductId = productId;
            this.Price = price;
            this.ProductAmount = productAmount;
        }

        /// <summary>
        /// Gets or sets the identifier of the related customer order.
        /// </summary>
        public int CustomerOrderId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the product included in the order.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the price of the product at the time of ordering.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product in the order.
        /// </summary>
        public int ProductAmount { get; set; }

        /// <summary>
        /// Returns a string representation of the order detail.
        /// </summary>
        /// <returns>A formatted string containing order detail information.</returns>
        public override string ToString()
        {
            return $"{this.Id} {this.CustomerOrderId} {this.ProductId} {this.Price} {this.ProductAmount}";
        }
    }
}