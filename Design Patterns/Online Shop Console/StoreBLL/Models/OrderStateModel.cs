namespace StoreBLL.Models;
using System;
using System.Collections.Generic;

/// <summary>
/// Represents orderStateModel in system.
/// </summary>
public class OrderStateModel : AbstractModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OrderStateModel"/> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="stateName">The orderState name.</param>
    public OrderStateModel(int id, string stateName)
        : base(id)
    {
        this.Id = id;
        this.StateName = stateName;
    }

    /// <summary>
    /// Gets or sets the name of the orderState.
    /// </summary>
    public string StateName { get; set; }

    /// <summary>
    /// Returs a string representation of orderState.
    /// </summary>
    /// <returns>oderState (id name).</returns>
    public override string ToString()
    {
        return $"Id:{this.Id} {this.StateName}";
    }
}