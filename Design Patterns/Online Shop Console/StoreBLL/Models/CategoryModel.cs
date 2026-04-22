namespace StoreBLL.Models;
using StoreDAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Represents a category in the system.
/// </summary>
public class CategoryModel : AbstractModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryModel"/> class.
    /// </summary>
    /// <param name="id">The category identifier.</param>
    /// <param name="name">The category name.</param>
    public CategoryModel(int id, string name)
        : base(id)
    {
        this.Name = name;
    }

    /// <summary>
    /// Gets or sets the category name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Returns a string representation of the category.
    /// </summary>
    /// <returns>Category (id name).</returns>
    public override string ToString()
    {
        return $"{this.Id} {this.Name}";
    }
}
