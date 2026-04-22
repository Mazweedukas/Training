namespace StoreBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Represents the base model with a unique identifier.
/// </summary>
public abstract class AbstractModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AbstractModel"/> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    protected AbstractModel(int id)
    {
        this.Id = id;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AbstractModel"/> class.
    /// </summary>
    protected AbstractModel()
    {
    }

    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    public int Id { get; set; }
}
