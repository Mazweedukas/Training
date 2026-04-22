namespace StoreBLL.Models
{
    using System;

    /// <summary>
    /// Represents a product title in the business layer.
    /// A product title defines a general product name and is linked to a category.
    /// </summary>
    public class ProductTitleModel : AbstractModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductTitleModel"/> class with all properties specified.
        /// </summary>
        /// <param name="id">Unique identifier of the product title.</param>
        /// <param name="title">Name of the product title.</param>
        /// <param name="categoryId">Identifier of the category to which this product title belongs.</param>
        public ProductTitleModel(int id, string title, int categoryId)
            : base(id)
        {
            this.Id = id;
            this.Title = title;
            this.CategoryId = categoryId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductTitleModel"/> class without specifying the identifier.
        /// Used when creating a new product title before it is persisted in the database.
        /// </summary>
        /// <param name="title">Name of the product title.</param>
        /// <param name="categoryId">Identifier of the category to which this product title belongs.</param>
        public ProductTitleModel(string title, int categoryId)
            : base()
        {
            this.Title = title;
            this.CategoryId = categoryId;
        }

        /// <summary>
        /// Gets or sets the name of the product title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the category associated with this product title.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Returns a string representation of the product title.
        /// </summary>
        /// <returns>A formatted string containing product title information.</returns>
        public override string ToString()
        {
            return $"{this.Id} {this.Title}";
        }
    }
}