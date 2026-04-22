namespace StoreBLL.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a user account with identifying information, login credentials, and an associated role within the
    /// system.
    /// </summary>
    public class UserModel : AbstractModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserModel"/> class with the specified user details.
        /// </summary>
        /// <param name="id">The unique identifier for the user.</param>
        /// <param name="name">The first name of the user.</param>
        /// <param name="lastName">The last name of the user.</param>
        /// <param name="login">The login username for the user.</param>
        /// <param name="password">The password associated with the user's account.</param>
        /// <param name="roleId">The identifier for the user's role, which determines their access level within the system.</param>
        public UserModel(int id, string name, string lastName, string login, string password, int roleId)
            : base(id)
        {
            this.Id = id;
            this.FirstName = name;
            this.LastName = lastName;
            this.Login = login;
            this.Password = password;
            this.RoleId = roleId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserModel"/> class with the specified user details.
        /// </summary>
        /// <param name="name">The first name of the user.</param>
        /// <param name="lastName">The last name of the user.</param>
        /// <param name="login">The login username for the user.</param>
        /// <param name="password">The password associated with the user's account.</param>
        /// <param name="roleId">The identifier for the user's role, which determines their access level within the system.</param>
        public UserModel(string name, string lastName, string login, string password, int roleId)
            : base()
        {
            this.FirstName = name;
            this.LastName = lastName;
            this.Login = login;
            this.Password = password;
            this.RoleId = roleId;
        }

        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the login identifier for the user.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Gets or sets the password used to authenticate the user account.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the RoleId used to authenticate the user account.
        /// </summary>
        public int RoleId { get; set; }
    }
}
