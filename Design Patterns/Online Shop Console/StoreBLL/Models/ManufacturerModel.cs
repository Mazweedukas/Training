namespace StoreBLL.Models;
using StoreDAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

/// <summary>
/// Represents a manufacturer in the system.
/// </summary>
public class ManufacturerModel : AbstractModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ManufacturerModel"/> class.
    /// </summary>
    /// <param name="id">The manufacturer identifier.</param>
    /// <param name="name">The manufacturer name.</param>
    public ManufacturerModel(int id, string name)
        : base(id)
    {
        this.Id = id;
        this.Name = name;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ManufacturerModel"/> class.
    /// </summary>
    /// <param name="name">The manufacturer name.</param>
    public ManufacturerModel(string name)
        : base()
    {
        this.Name = name;
    }

    /// <summary>
    /// Gets or sets the manufacturer name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Returns a string representation of the manufacturer.
    /// </summary>
    /// <returns>Manufacturer (id name).</returns>
    public override string ToString()
    {
        return $"{this.Id} {this.Name}";
    }
}
