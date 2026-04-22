namespace StoreBLL.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents ProductModel in system.
    /// </summary>
    public class ProductModel : AbstractModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductModel"/> class.
        /// </summary>
        /// <param name="id">Product Identifier.</param>
        /// <param name="productTitleId">Product title Id.</param>
        /// <param name="manufacturerId">Manufacturer Id.</param>
        /// <param name="description">Product description.</param>
        /// <param name="price">Product price.</param>
        public ProductModel(int id, int productTitleId, int manufacturerId, string description, decimal price)
            : base(id)
        {
            this.Id = id;
            this.ProductTitleId = productTitleId;
            this.ManufacturerId = manufacturerId;
            this.ProductDescription = description;
            this.Price = price;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductModel"/> class.
        /// </summary>
        /// <param name="productTitleId">Product title Id.</param>
        /// <param name="manufacturerId">Manufacturer Id.</param>
        /// <param name="description">Product description.</param>
        /// <param name="price">Product price.</param>
        public ProductModel(int productTitleId, int manufacturerId, string description, decimal price)
        {
            this.ProductTitleId = productTitleId;
            this.ManufacturerId = manufacturerId;
            this.ProductDescription = description;
            this.Price = price;
        }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public int ProductTitleId { get; set; }

        /// <summary>
        /// Gets or sets the manufacturerId of the product.
        /// </summary>
        public int ManufacturerId { get; set; }

        /// <summary>
        /// Gets or sets the description of the product.
        /// </summary>
        public string ProductDescription { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Returns the string representation of productModel.
        /// </summary>
        /// <returns>oderState (id titleId manufacturerId description price).</returns>
        public override string ToString()
        {
            return $"{this.Id} {this.ProductTitleId} {this.ManufacturerId} {this.ProductDescription} {this.Price}";
        }
    }
}
