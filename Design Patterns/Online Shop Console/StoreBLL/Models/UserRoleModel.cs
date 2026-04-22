namespace StoreBLL.Models;
using System;
using System.Collections.Generic;

/// <summary>
/// Represents a userRole in the system.
/// </summary>
public class UserRoleModel : AbstractModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserRoleModel"/> class.
    /// </summary>
    /// <param name="id">The userRole identifier.</param>
    /// <param name="roleName">The userRole name.</param>
    public UserRoleModel(int id, string roleName)
        : base(id)
    {
        this.Id = id;
        this.RoleName = roleName;
    }

    /// <summary>
    /// Gets or sets the userRole name.
    /// </summary>
    public string RoleName { get; set; }

    /// <summary>
    /// Returns a string representation of the userRole.
    /// </summary>
    /// <returns>UserRole (id name).</returns>
    public override string ToString()
    {
        return $"Id:{this.Id} {this.RoleName}";
    }
}
